//using Base32;
using DTPortal.Core.Constants;
using DTPortal.Core.Domain.Services;
using DTPortal.Core.Domain.Services.Communication;
using DTPortal.Core.Utilities;
using DTPortal.IDP.ViewModel;
using DTPortal.IDP.ViewModel.Login;
using DTPortal.IDP.ViewModel.Oauth2;
using Fido2NetLib;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Primitives;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using OtpSharp;
using PhoneNumbers;
using QRCoder;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.IO;
using System.Linq;
using System.Net;
using System.Security.Claims;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Web;
using static DTPortal.Common.CommonResponse;
using APIResponse = DTPortal.Core.Domain.Services.Communication.APIResponse;

namespace DTPortal.IDP.Controllers
{
    [ApiExplorerSettings(IgnoreApi = true)]
    [AllowAnonymous]
    public class LoginController : Controller
    {
        private readonly DTPortal.Core.Domain.Services.IAuthenticationService
                                            _authenticationService;
        private DTPortal.Core.Domain.Services.ITokenManagerService _tokenManager;
        private DTPortal.Core.Domain.Services.IClientService _clientService;
        private DTPortal.Core.Domain.Services.ICertificateService _certificateService;
        private readonly ILogger<LoginController> _logger;

        public IConfiguration Configuration { get; }
        private readonly IGlobalConfiguration _globalConfiguration;
        private readonly IConfigurationService _configurationService;
        private readonly WebConstants WEBConstants;
        private readonly OIDCConstants OidcConstants;
        private readonly MessageConstants ErrMsgconstants;
        private readonly SSOConfig ssoConfig;
        private readonly IHelper _helper;

        public LoginController(IConfigurationService configurationService,
            ILogger<LoginController> logger,
            ICertificateService certificateService,
            DTPortal.Core.Domain.Services.IAuthenticationService authenticationService,
            DTPortal.Core.Domain.Services.ITokenManagerService tokenManager,
            IGlobalConfiguration globalConfiguration,
            IConfiguration configuration,
            IHelper helper,
            DTPortal.Core.Domain.Services.IClientService clientService)
        {
            _authenticationService = authenticationService;
            _tokenManager = tokenManager;
            _clientService = clientService;
            _certificateService = certificateService;
            _logger = logger;
            _globalConfiguration = globalConfiguration;
            Configuration = configuration;
            _configurationService = configurationService;
            _helper = helper;

            var errorConfiguration = _globalConfiguration.
              GetErrorConfiguration();
            if (null == errorConfiguration)
            {
                _logger.LogError("Get Error Configuration failed");
                throw new NullReferenceException();
            }

            WEBConstants = errorConfiguration.WebConstants;
            if (null == WEBConstants)
            {
                _logger.LogError("Get  Error WebConstants Configuration failed");
                throw new NullReferenceException();
            }

            ErrMsgconstants = errorConfiguration.Constants;
            if (null == ErrMsgconstants)
            {
                _logger.LogError("Get Error Constants Configuration failed");
                throw new NullReferenceException();
            }

            OidcConstants = errorConfiguration.OIDCConstants;
            if (null == OidcConstants)
            {
                _logger.LogError("Get Error OIDCConstants Configuration failed");
                throw new NullReferenceException();
            }

            ssoConfig = _globalConfiguration.GetSSOConfiguration();
            if (null == ssoConfig)
            {
                _logger.LogError("Get SSO Configuration failed");
                throw new NullReferenceException();
            }

        }

        public IActionResult Index(string method = null, string target = null)
        {
            _logger.LogDebug("-->Login get");
            var da = Request.Cookies.ContainsKey("IDPCookieConsent");
            if (User.Identity.IsAuthenticated)
            {
                _logger.LogDebug("Login IsAuthenticated true");
                if (!string.IsNullOrWhiteSpace(target))
                {
                    var valueBytes = System.Convert.FromBase64String(target);
                    var url = Encoding.UTF8.GetString(valueBytes);

                    if (method == "get")
                    {
                        _logger.LogDebug("<--Login get : redirect to get url");
                        return LocalRedirect(url);
                    }
                    else
                    {
                        _logger.LogDebug("<--Login get : redirect to post url");
                        return View("Action", JsonConvert
                            .DeserializeObject<DTPortal.IDP.ViewModel.Saml2.Saml2Response>
                            (Encoding.UTF8.GetString(System.Convert.FromBase64String(target))));
                        // return RedirectToAction("Index", "Home");
                    }
                }
                else
                {
                    _logger.LogError("Login get: target value not found ");
                    ViewBag.error = WEBConstants.InternalError;
                    ViewBag.error_description = _helper.GetErrorMsg(
                        ErrorCodes.LOGIN_METHOD_TARGET_NOT_FOUND);
                    return View("Error");
                }
            }

            if (!string.IsNullOrWhiteSpace(method) && !string.IsNullOrWhiteSpace(target))
            {
                var schemasSection = Configuration.GetSection("AuthSchemas");
                var schemas = schemasSection.GetChildren()
                                            .ToDictionary(
                                                x => x.Key,
                                                x => x.Get<AuthSchema>());
                var rememberedUser = Request.Cookies["idp.remembered_user"];

                var model = new LoginViewModel()
                {
                    Target = target,
                    Method = method,
                    AuthSchemas = schemas,
                    RememberedUser = rememberedUser
                };
                _logger.LogDebug("<--Login get : load login page");
                return View("Index1", model);
            }
            else
            {
                _logger.LogError("Login get: method and target value not found ");
                ViewBag.error = WEBConstants.InternalError;
                ViewBag.error_description = _helper.GetErrorMsg(
                    ErrorCodes.LOGIN_METHOD_TARGET_NOT_FOUND);
                return View("Error");
            }
        }

        [HttpPost]
        [IgnoreAntiforgeryToken]
        public async Task<IActionResult> Index(LoginViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                _logger.LogDebug("--> Login post");

                if (!string.IsNullOrEmpty(model.Method) && string.IsNullOrEmpty(model.Target))
                {
                    _logger.LogError("Login post: method or target value not found ");
                    ViewBag.error = WEBConstants.InternalError;
                    ViewBag.error_description = _helper.GetErrorMsg(ErrorCodes.LOGIN_METHOD_TARGET_NOT_FOUND);
                    return View("Error");
                }


                var UserName = "";
                var UserMail = "";
                if (HttpContext.Request.Cookies.ContainsKey("UserName"))
                {
                    UserName = StringFromBase64(HttpContext.Request.Cookies["UserName"]);
                }
                else
                {
                    UserName = model.userName;
                }
                if (HttpContext.Request.Cookies.ContainsKey("UserMail"))
                {
                    UserMail = StringFromBase64(HttpContext.Request.Cookies["UserMail"]);
                }

                if (!HttpContext.Request.Cookies.ContainsKey("TempSession"))
                {
                    _logger.LogError("Login post: TempSession value not found ");
                    ViewBag.error = WEBConstants.InternalError;
                    ViewBag.error_description = _helper.GetErrorMsg(ErrorCodes.LOGIN_TEMP_SESS_NOT_FOUND_IN_COOCKIES);
                    return View("Error");
                }
                _logger.LogInformation("Login post: UserName :{UserName}", UserName?.SanitizeForLogging());

                var temprorySession = StringFromBase64(HttpContext.Request.Cookies["TempSession"]);

                _logger.LogInformation("Login post: TempSession :{temp}", temprorySession?.SanitizeForLogging());

                if (string.IsNullOrEmpty(temprorySession))
                {
                    _logger.LogError("Login post: TempSession value not found ");
                    ViewBag.error = WEBConstants.InternalError;
                    ViewBag.error_description = _helper.GetErrorMsg(ErrorCodes.LOGIN_TEMP_SESS_NOT_FOUND_IN_COOCKIES);
                    return View("Error");
                }

                HttpContext.Response.Cookies.Delete("TempSession");
                HttpContext.Response.Cookies.Delete("UserName");

                var response = await _authenticationService.GetLoginSession(temprorySession);
                if (response == null)
                {
                    _logger.LogError("Login post: Response value getting null ");
                    ViewBag.error = WEBConstants.InternalError;
                    ViewBag.error_description = _helper.GetErrorMsg(ErrorCodes.LOGIN_GET_GETLOGINSESSION_RES_NULL);
                    return View("Error");
                }
                if (!response.Success)
                {
                    _logger.LogError("Login post :{0}", response.Message);
                    model.userName = string.Empty;
                    model.error = response.Message;
                    return View(model);
                }

                _logger.LogInformation("Login post: UserName verified :{userName}", UserName?.SanitizeForLogging());
                _logger.LogInformation("Login post: UserName verified :{userName}", model.userName?.SanitizeForLogging());
                _logger.LogInformation("Login post: User Sesssion :{userSession}", response.Session?.SanitizeForLogging());



                //Check the user name and password
                //Here can be implemented checking logic from the database
                ClaimsIdentity identity = new ClaimsIdentity(new[] {
                    new Claim(ClaimTypes.Name, UserName),
                    new Claim(ClaimTypes.NameIdentifier,UserName),
                    new Claim(ClaimTypes.Email,UserName),
                    new Claim("UserId",response.Suid),
                    new Claim("Session",response.Session),
                    new Claim("grantPermission", "false")
                }, CookieAuthenticationDefaults.AuthenticationScheme);

                var principal = new ClaimsPrincipal(identity);

                var properties = new AuthenticationProperties();
                properties.IsPersistent = true;
                properties.AllowRefresh = false;

                var configInDB = _globalConfiguration.GetSSOConfiguration();
                if (configInDB == null)
                {
                    _logger.LogError("Login post : Fail to get SSO Configuration");
                    model.userName = string.Empty;
                    model.error = _helper.GetErrorMsg(ErrorCodes.LOGIN_GET_SSOCONFIG_RES_NULL);
                    return View(model);
                }

                int SessionTimeOut = configInDB.sso_config.session_timeout;

                if (0 == SessionTimeOut)
                    SessionTimeOut = 90;

                properties.ExpiresUtc = DateTime.UtcNow.AddMinutes(
                    Convert.ToDouble(SessionTimeOut));

                await HttpContext.SignInAsync(CookieAuthenticationDefaults
                    .AuthenticationScheme, principal, properties);

                string cookieName = "idp.remembered_user";

                if (HttpContext.Request.Cookies.ContainsKey("rememberUser"))
                {
                    HttpContext.Response.Cookies.Append(
                        cookieName,
                        UserMail,
                        new CookieOptions
                        {
                            Path = "/",
                            HttpOnly = true,
                            Secure = true,
                            Expires = DateTimeOffset.UtcNow.AddDays(30)
                        }
                    );
                }

                if (!string.IsNullOrEmpty(model.Method) &&
                    !string.IsNullOrWhiteSpace(model.Target))
                {
                    var valueBytes = System.Convert.FromBase64String(model.Target);
                    var url = Encoding.UTF8.GetString(valueBytes);

                    if (model.Method == "get")
                    {
                        _logger.LogDebug("<-- Login post");
                        return LocalRedirect(url);
                    }
                    else
                    {
                        _logger.LogDebug("<-- Login post");
                        return View("Action", JsonConvert
                            .DeserializeObject<DTPortal.IDP.ViewModel.Saml2.Saml2Response>
                            (Encoding.UTF8.GetString(System.Convert.
                            FromBase64String(model.Target))));
                        // return RedirectToAction("Index", "Home");
                    }
                }
                else
                {
                    _logger.LogError("Login post: method or target value not found ");
                    ViewBag.error = WEBConstants.InternalError;
                    ViewBag.error_description = _helper.GetErrorMsg(ErrorCodes.LOGIN_METHOD_TARGET_NOT_FOUND);
                    return View("Error");
                }
            }
            catch (Exception e)
            {
                _logger.LogError("Login post :{0}", e.Message);
                ViewBag.error = WEBConstants.InternalError;
                ViewBag.error_description = _helper.GetErrorMsg(ErrorCodes.LOGIN_INDEX_POST_METHOD_EXCP);
                return View("Error");
            }
        }

        [HttpPost]
        [IgnoreAntiforgeryToken]
        public async Task<IActionResult> VerifyUser(VerifyUserRequest Model, [FromHeader(Name = "User-Agent")] string userAgent)
        {
            try
            {
                _logger.LogDebug("--> VerifyUser");
                if (string.IsNullOrEmpty(Model.ip))
                {
                    Model.ip = GetIpAddress();
                    if (string.IsNullOrEmpty(Model.ip))
                    {
                        Model.ip = "Not Available";
                    }
                }
                if (string.IsNullOrEmpty(Model.userAgent))
                {
                    Model.userAgent = userAgent;
                }
                if (string.IsNullOrEmpty(Model.typeOfDevice))
                {
                    Model.typeOfDevice = GetDeviceType(userAgent);
                }


                if (!string.IsNullOrEmpty(Model.userInput))
                {
                    Model.userInput = Model.userInput.Trim(' ');
                }

                if (!ModelState.IsValid)
                {
                    var firstError = ModelState
                        .Values
                        .SelectMany(v => v.Errors)
                        .Select(e => e.ErrorMessage)
                        .FirstOrDefault();

                    return BadRequest(new
                    {
                        Success = false,
                        Message = firstError
                    });
                }

                //Only validate with libphonenumber if it's a phone
                if (Model.userInput.StartsWith("+"))
                {
                    var phoneUtil = PhoneNumberUtil.GetInstance();

                    try
                    {
                        var number = phoneUtil.Parse(Model.userInput, null);

                        if (!phoneUtil.IsValidNumber(number))
                        {
                            return BadRequest(new
                            {
                                Success = false,
                                Message = "Invalid phone number"
                            });
                        }

                        // Normalize to E.164
                        Model.userInput = phoneUtil.Format(number, PhoneNumberFormat.E164);
                    }
                    catch
                    {
                        return BadRequest(new
                        {
                            Success = false,
                            Message = "Invalid phone number"
                        });
                    }
                }
                else if (Model.userInput.Contains("@"))
                {
                    // Normalize email domain only
                    var parts = Model.userInput.Split('@');
                    Model.userInput = $"{parts[0]}@{parts[1].ToLowerInvariant()}";
                }

                SetRememberUser(Model.rememberUser);

                _logger.LogInformation("VerifyUser: UserId :{0},UserIP:{1}," +
                    "UserDevice:{2},UserAgent:{3}", Model.userInput?.SanitizeForLogging(),
                        Model.ip?.SanitizeForLogging(), Model.typeOfDevice?.SanitizeForLogging(), Model.userAgent?.SanitizeForLogging());

                var response = await _authenticationService.VerifyUser(Model);
                if (response == null)
                {
                    _logger.LogError("VerifyUser: Response value getting null ");
                    throw new Exception("VerifyUser Response value getting null");
                }
                if (response.Success)
                {
                    var cookieOptions = new CookieOptions
                    {
                        HttpOnly = true,
                        Secure = HttpContext.Request.IsHttps,
                        SameSite = SameSiteMode.Lax,
                        Expires = DateTimeOffset.UtcNow.AddMinutes(30)
                    };
                    HttpContext.Response.Cookies.Append("TempSession",
                       StringToBase64(response.Result.AuthnToken), cookieOptions);
                    HttpContext.Response.Cookies.Append("UserName",
                       StringToBase64(response.Result.userName), cookieOptions);
                    if (!string.IsNullOrEmpty(response.Result.userMail))
                    {
                        HttpContext.Response.Cookies.Append("UserMail",
                            StringToBase64(response.Result.userMail), cookieOptions);
                    }
                    response.Result.AuthnToken = string.Empty;
                    response.Result.userName = Model.userInput;
                    if (response.Result.AuthenticationSchemes.Contains("QRCODE"))
                    {
                        response.Result.QrCode = GetQrCodeData(StringToBase64(response.Result.AuthnToken));
                    }
                    else if (response.Result.AuthenticationSchemes.Contains("WALLET"))
                    {
                        response.Result.QrCode = GetQrCodeData(response.Result.VerifierUrl);
                        HttpContext.Response.Cookies.Append("VerifierCode", 
                            StringToBase64(response.Result.VerifierCode), cookieOptions);
                    }

                    if (response.Result.AuthenticationSchemes.Contains("FIDO2"))
                        HttpContext.Response.Cookies.Append("fido2.assertionOptions",
                           StringToBase64(response.Result.Fido2Options), cookieOptions);
                    _logger.LogDebug("<-- VerifyUser");
                    return Ok(response);
                }
                else
                {
                    _logger.LogDebug("VerifyUser : {0}", response.Message?.SanitizeForLogging());
                    _logger.LogDebug("<-- VerifyUser");

                    var Res = new
                    {
                        Success = response.Success,
                        Message = response.Message,
                        accountlocktime = ssoConfig.sso_config.
                            account_lock_time.ToString(),
                        ErrorCode = getErrorCode(response.Message)
                    };
                    return Ok(Res);
                }
            }
            catch (Exception e)
            {
                _logger.LogError("VerifyUser : {0}", e.Message);
                _logger.LogDebug("<-- VerifyUser");
                return StatusCode(500, _helper.GetErrorMsg(ErrorCodes.LOGIN_VERIFYUESR_METHOD_EXCP));
            }
            // return response;
        }

        [HttpPost]
        public async Task<IActionResult> AuthenticatUser(
            [FromBody] VerifyUserAuthDataRequest Model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                _logger.LogDebug("--> AuthenticatUser");

                var temprorySession = "";

                if (string.IsNullOrEmpty(temprorySession))
                {
                    if (!HttpContext.Request.Cookies.ContainsKey("TempSession"))
                    {
                        _logger.LogError("Login post: TempSession value not found ");
                        throw new Exception("TempSession value not found");
                    }
                    temprorySession = StringFromBase64(HttpContext.Request.Cookies["TempSession"]);
                    if (string.IsNullOrEmpty(temprorySession))
                    {
                        _logger.LogError("Login post: TempSession value not found ");
                        throw new Exception("TempSession value not found");
                    }
                }
                if (Model.authenticationScheme == "FIDO2")
                {
                    if (!HttpContext.Request.Cookies.ContainsKey("fido2.assertionOptions"))
                    {
                        _logger.LogError("AuthenticatUser: fido2 assertionOptions not found ");
                        throw new Exception("fido2 assertionOptions not found");
                    }

                    var jsonOptions = StringFromBase64(HttpContext.Request.Cookies["fido2.assertionOptions"]);
                    if (string.IsNullOrEmpty(jsonOptions))
                    {
                        _logger.LogError("AuthenticatUser: fido2 assertionOptions not found ");
                        throw new Exception(" fido2 assertionOptions not found");
                    }
                    Model.authenticationData = Model.authenticationData + "#" + jsonOptions;
                }
                Model.AuthnToken = temprorySession;
                Model.approved = true;
                Model.randomCode = "";

                var response = await _authenticationService.VerifyUserAuthData(Model);
                if (response == null)
                {
                    _logger.LogError("AuthenticatUser: Response value getting null ");
                    throw new Exception("Response value getting null");
                }
                if (response.Success)
                {
                    if (HttpContext.Request.Cookies.ContainsKey("fido2.assertionOptions"))
                    {
                        HttpContext.Response.Cookies.Delete("fido2.assertionOptions");
                    }
                    if (response.Result != null)
                    {
                        if (!string.IsNullOrEmpty(response.Result.VerifierUrl))
                        {
                            var cookieOptions = new CookieOptions
                            {
                                HttpOnly = true,
                                Secure = HttpContext.Request.IsHttps,
                                SameSite = SameSiteMode.Lax,
                                Expires = DateTimeOffset.UtcNow.AddMinutes(60)
                            };
                            response.Result.QrCode = GetQrCodeData(response.Result.VerifierUrl);
                            HttpContext.Response.Cookies.Append("VerifierCode",
                                StringToBase64(response.Result.VerifierUrl.Substring
                                (response.Result.VerifierUrl.LastIndexOf('/') + 1)),cookieOptions);
                        }
                    }
                    _logger.LogDebug("<--AuthenticatUser");
                    return Ok(response);
                }
                else
                {
                    _logger.LogInformation("AuthenticatUser: {0}", response.Message?.SanitizeForLogging());
                    _logger.LogDebug("<--AuthenticatUser");
                    var Res = new
                    {
                        Success = response.Success,
                        Message = response.Message,
                        ErrorCode = getErrorCode(response.Message)
                    };
                    return Ok(Res);
                }

            }
            catch (Exception e)
            {
                _logger.LogError("AuthenticatUser: {0}", e.Message);
                _logger.LogDebug("<--AuthenticatUser");
                return StatusCode(500, _helper.GetErrorMsg(ErrorCodes.LOGIN_AUTHENTICATUSER_METHOD_EXCP));
            }
        }

        [HttpPost]
        [IgnoreAntiforgeryToken]
        public async Task<IActionResult> IsUserVerifiedCode()
        {
            try
            {
                _logger.LogDebug("--> IsUserVerifiedCode");

                if (!HttpContext.Request.Cookies.ContainsKey("TempSession"))
                {
                    _logger.LogError("Login post: TempSession value not found ");
                    throw new Exception("TempSession value not found");
                }
                var temprorySession = StringFromBase64(HttpContext.Request.Cookies["TempSession"]);
                if (string.IsNullOrEmpty(temprorySession))
                {
                    _logger.LogError("Login post: TempSession value not found ");
                    throw new Exception("TempSession value not found");
                }

                //var temprorySession = HttpContext.Session.GetString("TempSession");
                //if (string.IsNullOrEmpty(temprorySession))
                //{
                //    _logger.LogError("IsUserVerifiedCode: TempSession value not found ");
                //    return StatusCode(400, new Response
                //    {
                //        Success = false,
                //        Message = WEBConstants.SessionNotFound
                //    });
                //}

                var response = await _authenticationService.IsUserVerified(temprorySession);
                if (response == null)
                {
                    _logger.LogError("IsUserVerifiedCode: Response value getting null ");
                    throw new Exception("Response value getting null");
                }
                if (response.Success)
                {
                    if (response.Status != "success")
                    {

                        if (!HttpContext.Request.Cookies.ContainsKey("NotificationVerifierValidTime"))
                        {
                            response.Status = "stop";
                        }
                    }
                }
                _logger.LogDebug("<-- IsUserVerifiedCode");
                var Res = new
                {
                    Success = response.Success,
                    Message = response.Message,
                    Status = response.Status,
                    ErrorCode = (response.Status == "failed" ? getErrorCode(response.Message) : 0)
                };
                return Ok(Res);
            }
            catch (Exception e)
            {
                _logger.LogError("IsUserVerifiedCode: {0}", e.Message);
                _logger.LogDebug("<--IsUserVerifiedCode");
                return StatusCode(500, _helper.GetErrorMsg(ErrorCodes.LOGIN_ISUSERVERIFYCODE_METHOD_EXCP));
            }
        }

        [HttpPost]
        [IgnoreAntiforgeryToken]
        public async Task<IActionResult> SendPushNotification()
        {
            try
            {
                _logger.LogDebug("--> SendPushNotification");

                if (!HttpContext.Request.Cookies.ContainsKey("TempSession"))
                {
                    _logger.LogError("Login post: TempSession value not found ");
                    throw new Exception("TempSession value not found");
                }
                var temprorySession = StringFromBase64(HttpContext.Request.Cookies["TempSession"]);
                if (string.IsNullOrEmpty(temprorySession))
                {
                    _logger.LogError("Login post: TempSession value not found ");
                    throw new Exception("TempSession value not found");
                }

                //var temprorySession = HttpContext.Session.GetString("TempSession");
                //if (string.IsNullOrEmpty(temprorySession))
                //{
                //    _logger.LogError("SendPushNotification: TempSession value not found ");
                //    return StatusCode(400, new Response
                //    {
                //        Success = false,
                //        Message = WEBConstants.SessionNotFound
                //    });
                //}

                var response = await _authenticationService.SendMobileNotification(
                    temprorySession);
                if (response == null)
                {
                    _logger.LogError("SendPushNotification: Response value getting null ");
                    throw new Exception("Response value getting null");
                }

                if (response.Success)
                {
                    var Res = new
                    {
                        Success = response.Success,
                        Message = response.Message,
                        RandomCode = response.RandomCode,
                        ErrorCode = (!string.IsNullOrEmpty(response.RandomCode) ? 0 : getErrorCode(response.Message)),
                    };
                    return Ok(Res);
                }
                else
                {
                    var Res = new
                    {
                        Success = response.Success,
                        Message = response.Message,
                        RandomCode = response.RandomCode,
                        accountlocktime = ssoConfig.sso_config.
                            account_lock_time.ToString(),
                        ErrorCode = getErrorCode(response.Message)
                    };
                    return Ok(Res);
                }

            }
            catch (Exception e)
            {
                _logger.LogError("SendPushNotification: {0}", e.Message);
                _logger.LogDebug("<--SendPushNotification");
                return StatusCode(500, _helper.GetErrorMsg(ErrorCodes.LOGIN_SENDPUSHNOTIFICATION_METHOD_EXCP));
            }
        }

        [HttpPost]
        [IgnoreAntiforgeryToken]
        public async Task<IActionResult> IsUserVerified()
        {
            _logger.LogDebug("--> IsUserVerifiedWalletQrCode");

            if (!HttpContext.Request.Cookies.ContainsKey("TempSession"))
            {
                _logger.LogError("Login post: TempSession value not found ");
                throw new Exception("TempSession value not found");
            }
            var temprorySession = StringFromBase64(HttpContext.Request.Cookies["TempSession"]);
            if (string.IsNullOrEmpty(temprorySession))
            {
                _logger.LogError("Login post: TempSession value not found ");
                throw new Exception("TempSession value not found");
            }
            if (!HttpContext.Request.Cookies.ContainsKey("VerifierCode"))
            {
                _logger.LogError("Login post: TempSession value not found ");
                throw new Exception("TempSession value not found");
            }
            var VerifierCode = StringFromBase64(HttpContext.Request.Cookies["VerifierCode"]);
            if (string.IsNullOrEmpty(temprorySession))
            {
                _logger.LogError("Login post: VerifierCode value not found ");
                throw new Exception("VerifierCode value not found");
            }
            VerifyQrCodeRequest verifyQrCodeRequest = new VerifyQrCodeRequest();

            verifyQrCodeRequest.tempSession = temprorySession;

            verifyQrCodeRequest.qrCode = VerifierCode;

            var response = await _authenticationService.IsUserVerifiedQrCode(verifyQrCodeRequest);

            return Ok(response);
        }


        [HttpPost]
        [IgnoreAntiforgeryToken]
        public async Task<IActionResult> IsUserVerifiedQRCode()
        {
            try
            {
                _logger.LogDebug("--> IsUserVerifiedQRCode");

                if (!HttpContext.Request.Cookies.ContainsKey("TempSession"))
                {
                    _logger.LogError("Login post: TempSession value not found ");
                    throw new Exception("TempSession value not found");
                }
                var temprorySession = StringFromBase64(HttpContext.Request.Cookies["TempSession"]);
                if (string.IsNullOrEmpty(temprorySession))
                {
                    _logger.LogError("Login post: TempSession value not found ");
                    throw new Exception("TempSession value not found");
                }

                //var temprorySession = HttpContext.Session.GetString("TempSession");
                //if (string.IsNullOrEmpty(temprorySession))
                //{
                //    _logger.LogError("IsUserVerifiedCode: TempSession value not found ");
                //    return StatusCode(400, new Response
                //    {
                //        Success = false,
                //        Message = WEBConstants.SessionNotFound
                //    });
                //}

                var response = await _authenticationService.IsUserVerified(temprorySession);
                if (response == null)
                {
                    _logger.LogError("IsUserVerifiedQRCode: Response value getting null ");
                    throw new Exception("Response value getting null");
                }
                _logger.LogDebug("<-- IsUserVerifiedQRCode");
                var Res = new
                {
                    Success = response.Success,
                    Message = response.Message,
                    Status = response.Status,
                    ErrorCode = (response.Status == "failed" ? getErrorCode(response.Message) : 0)
                };
                return Ok(Res);
            }
            catch (Exception e)
            {
                _logger.LogError("IsUserVerifiedQRCode: {0}", e.Message);
                _logger.LogDebug("<--IsUserVerifiedQRCode");
                return StatusCode(500, _helper.GetErrorMsg(ErrorCodes.LOGIN_ISUSERVERIFYCODE_METHOD_EXCP));
            }
        }

        [HttpGet]
        public async Task<IActionResult> GenerateQrCode()
        {
            var verifierUrlResponse = await _authenticationService.GetVerifierUrl();
            if (verifierUrlResponse == null || !verifierUrlResponse.Success)
            {
                return Ok(verifierUrlResponse);
            }
            var cookieOptions = new CookieOptions
            {
                HttpOnly = true,
                Secure = HttpContext.Request.IsHttps,
                SameSite = SameSiteMode.Lax,
                Expires = DateTimeOffset.UtcNow.AddMinutes(60)
            };
            var qrCode = GetQrCodeData(verifierUrlResponse.Result);
            HttpContext.Response.Cookies.Append("VerifierCode", StringToBase64
                (verifierUrlResponse.Result.Substring(verifierUrlResponse.Result.LastIndexOf('/') + 1)),cookieOptions);
            var Response = new Response()
            {
                Success = true,
                Message = verifierUrlResponse.Message,
                Result = qrCode
            };
            return Ok(Response);
        }

        [HttpPost]
        [IgnoreAntiforgeryToken]
        public async Task<IActionResult> SendQR()
        {
            try
            {
                _logger.LogDebug("--> SendQR");

                if (!HttpContext.Request.Cookies.ContainsKey("TempSession"))
                {
                    _logger.LogError("Login post: TempSession value not found ");
                    throw new Exception("TempSession value not found");
                }
                var temprorySession = StringFromBase64(HttpContext.Request.Cookies["TempSession"]);
                if (string.IsNullOrEmpty(temprorySession))
                {
                    _logger.LogError("Login post: TempSession value not found ");
                    throw new Exception("TempSession value not found");
                }

                //var temprorySession = HttpContext.Session.GetString("TempSession");
                //if (string.IsNullOrEmpty(temprorySession))
                //{
                //    _logger.LogError("SendPushNotification: TempSession value not found ");
                //    return StatusCode(400, new Response
                //    {
                //        Success = false,
                //        Message = WEBConstants.SessionNotFound
                //    });
                //}

                var response = await _authenticationService.SendMobileNotification(
                    temprorySession);
                if (response == null)
                {
                    _logger.LogError("SendQR: Response value getting null ");
                    throw new Exception("Response value getting null");
                }

                if (response.Success)
                {
                    var Res = new
                    {
                        Success = response.Success,
                        Message = response.Message,
                        RandomCode = response.RandomCode,
                        ErrorCode = (!string.IsNullOrEmpty(response.RandomCode) ? 0 : getErrorCode(response.Message)),
                    };
                    return Ok(Res);
                }
                else
                {
                    var Res = new
                    {
                        Success = response.Success,
                        Message = response.Message,
                        RandomCode = response.RandomCode,
                        accountlocktime = ssoConfig.sso_config.
                            account_lock_time.ToString(),
                        ErrorCode = getErrorCode(response.Message)
                    };
                    return Ok(Res);
                }

            }
            catch (Exception e)
            {
                _logger.LogError("SendQR: {0}", e.Message);
                _logger.LogDebug("<--SendQR");
                return StatusCode(500, _helper.GetErrorMsg(ErrorCodes.LOGIN_SENDPUSHNOTIFICATION_METHOD_EXCP));
            }
        }


        [EnableCors("AllowedOrigins")]
        [Route("Logout")]
        [HttpGet]
        public async Task<IActionResult> Logout(string redirect_uri)
        {
            try
            {
                _logger.LogDebug("--> Logout");
                if (string.IsNullOrEmpty(redirect_uri))
                {
                    ViewBag.error = "invalid_request";
                    ViewBag.error_description = WEBConstants.RedirectUriMissing;
                    _logger.LogError("Logout: The request has missing parameter: " +
                        "`redirect_uri`");
                    _logger.LogDebug("<--Logout");
                    return View("Error");
                }

                if (User.Identity.IsAuthenticated && HttpContext.User.Claims.Count() != 0)
                {
                    var User = HttpContext.User.Claims
                        .FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier).Value;
                    var GlobalSession = HttpContext.User.Claims
                        .FirstOrDefault(c => c.Type == "Session").Value;
                    if (!string.IsNullOrEmpty(GlobalSession))
                    {
                        var data = new LogoutUserRequest
                        {
                            GlobalSession = GlobalSession
                        };
                        var response = await _authenticationService.LogoutUser(data);
                    }
                    await HttpContext.SignOutAsync(CookieAuthenticationDefaults
                        .AuthenticationScheme);
                    _logger.LogInformation("{0} Logout successfull", User);
                }

                if (!string.IsNullOrEmpty(redirect_uri))
                {
                    bool isValidRedirect = await _clientService.IsLogoutUriExistsAsync(redirect_uri);

                    if (isValidRedirect)
                    {
                        return Redirect(redirect_uri);
                    }
                    else
                    {
                        ViewBag.error = "unauthorized_client";
                        ViewBag.error_description = "The redirect URI is not registered.";
                        return View("Error");
                    }
                }
                else
                {
                    ViewBag.error = "Invalid Redirect Url";
                    ViewBag.error_description = "The redirect URI is not registered.";
                    return View("Error");
                }


            }
            catch (Exception e)
            {
                _logger.LogError("Logout: {0}", e.Message);
                _logger.LogDebug("<--Logout");
                ViewBag.error = WEBConstants.InternalError;
                ViewBag.error_description = _helper.GetErrorMsg(ErrorCodes.LOGIN_LOGOUT_METHOD_EXCP);
                return View("Error");
            }

        }

        [Route("OIDCLogout")]
        [HttpGet]
        public async Task<IActionResult> OIDCLogout(LogoutRequest model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                _logger.LogDebug("--> OIDCLogout");
                string redirectUrl = null;
                if (!User.Identity.IsAuthenticated && HttpContext.User.Claims.Count() == 0)
                {
                    if (string.IsNullOrEmpty(model.post_logout_redirect_uri))
                    {
                        ViewBag.error = "invalid_request";
                        ViewBag.error_description = WEBConstants.LogoutUriMissing;
                        _logger.LogError("OIDCLogout: The request has missing parameter: " +
                            "`post_logout_redirect_uri`");
                        _logger.LogDebug("<-- OIDCLogout");
                        return View("Error");
                    }
                    else
                    {
                        if (model.state != null)
                        {
                            redirectUrl = model.post_logout_redirect_uri + "?state="
                                + model.state;
                        }
                        else
                        {
                            redirectUrl = model.post_logout_redirect_uri;
                        }
                        _logger.LogDebug("<--OIDCLogout");
                        // Redirect to SP Logout Url
                        return Redirect(redirectUrl);
                    }
                }

                if (null == model || string.IsNullOrEmpty(model.id_token_hint) ||
                string.IsNullOrEmpty(model.post_logout_redirect_uri))
                {
                    // Internal error page
                    _logger.LogError("OIDCLogout: The request has Invalid parameters");
                    ViewBag.error = "invalid_request";
                    ViewBag.error_description = "The request has Invalid parameters";
                    return View("Error");
                }

                var idpCert = await _certificateService.GetIdpActiveCertificateAsync();
                if (null == idpCert)
                {
                    _logger.LogError("OIDCLogout: fail to get idp certificate");
                    ViewBag.error = "Internal error";
                    ViewBag.error_description = _helper.GetErrorMsg(ErrorCodes.LOGIN_OIDCLOGOUT_FAIL_TO_GET_IDP_CERT);
                    return View("Error");
                }

                // Verify ID Token
                var isTokenValid = _tokenManager.ValidateLogoutJWToken(
                    model.id_token_hint, idpCert.Data);

                // isTokenValid = true;
                if (!isTokenValid)
                {
                    // Internal error page
                    _logger.LogError("OIDCLogout: Invalid id_token_hint");
                    ViewBag.error = "invalid_request";
                    ViewBag.error_description = WEBConstants.InvalidIdToken;
                    return View("Error");
                }

                _logger.LogInformation("Log1");

                // Check user claims
                if (!HttpContext.User.Identity.IsAuthenticated ||
                     !HttpContext.User.Claims.Any())
                {
                    _logger.LogError("OIDCLogout: Aunauthorized");
                    ViewBag.error = "invalid_request";
                    ViewBag.error_description = WEBConstants.NotAuthorized;
                    return View("Error");
                }
                _logger.LogInformation("Log2");

                var handler = new JwtSecurityTokenHandler();
                var token = handler.ReadJwtToken(model.id_token_hint);

                _logger.LogInformation("Log3");

               
                _logger.LogInformation("Log4");
                // Get LoggedIn Session user
                var sessionUser = HttpContext.User.Claims.FirstOrDefault(
                    c => c.Type == ClaimTypes.Name);
                if (null == sessionUser || string.IsNullOrEmpty(sessionUser.Value))
                {
                    _logger.LogError("OIDCLogout: Session not found");
                    ViewBag.error = WEBConstants.InternalError;
                    ViewBag.error_description = _helper.GetErrorMsg(ErrorCodes.SESSION_NOT_FOUND);
                    return View("Error");
                }
                _logger.LogInformation("Log5");
                // Compare Session user and ID Token user
                _logger.LogInformation("Log6");
                // Get Session Claim
                var GlobalSession = HttpContext.User.Claims.FirstOrDefault(
                    c => c.Type == "Session").Value;
                if (string.IsNullOrEmpty(GlobalSession))
                {
                    _logger.LogError("OIDCLogout: global Session not found ");
                    ViewBag.error = WEBConstants.InternalError;
                    ViewBag.error_description = _helper.GetErrorMsg(ErrorCodes.SESSION_NOT_FOUND);
                    return View("Error");
                }
                _logger.LogInformation("Log7");
                
                var data = new LogoutUserRequest
                {
                    GlobalSession = GlobalSession
                };

                // Logout user session
                await _authenticationService.LogoutUser(data);
                await HttpContext.SignOutAsync(CookieAuthenticationDefaults.
                    AuthenticationScheme);
                _logger.LogInformation("Log10");
                _logger.LogInformation("{0} Logout Successfull", sessionUser.Value);

                if (model.state != null)
                {
                    redirectUrl = model.post_logout_redirect_uri + "?state=" + model.state;
                }
                else
                {
                    redirectUrl = model.post_logout_redirect_uri;
                }

                if (!string.IsNullOrEmpty(redirectUrl))
                {
                    bool isValidRedirect = await _clientService.IsLogoutUriExistsAsync(redirectUrl);

                    if (isValidRedirect)
                    {
                        return Redirect(redirectUrl);
                    }
                    else
                    {
                        ViewBag.error = "unauthorized_client";
                        ViewBag.error_description = "The redirect URI is not registered.";
                        return View("Error");
                    }
                }
                else
                {
                    ViewBag.error = "Invalid Redirect Url";
                    ViewBag.error_description = "The redirect URI is not registered.";
                    return View("Error");
                }
            }
            catch (Exception e)
            {
                _logger.LogInformation("Log11");
                _logger.LogError("OIDCLogout: {0}", e.Message);
                _logger.LogDebug("<--OIDCLogout");
                ViewBag.error = WEBConstants.InternalError;
                ViewBag.error_description = _helper.GetErrorMsg(ErrorCodes.LOGIN_OIDCLOGOUT_METHOD_EXCP);
                return View("Error");
            }
        }

        [HttpGet]
        public IActionResult GetErrorConstant()
        {
            try
            {
                _logger.LogDebug("--> GetErrorConstant");

                return Json(WEBConstants);
            }
            catch (Exception e)
            {
                _logger.LogError("SendPushNotification: {0}", e.Message);
                _logger.LogDebug("<--SendPushNotification");
                return StatusCode(500, _helper.GetErrorMsg(ErrorCodes.LOGIN_GETERRORCONSTANT_METHOD_EXCP));
            }
        }

        [HttpGet]
        public IActionResult Error(string error = null, string error_description = null)
        {
            ViewBag.error = (string.IsNullOrEmpty(error)) ?
                WEBConstants.InternalError.En : error;
            ViewBag.error_description = (string.IsNullOrEmpty(error_description))
                ? WEBConstants.InternalServerError.En : error_description;
            return View("Error");
        }

        //[HttpGet]
        //public string GetQrCode()
        //{
        //    QRCodeGenerator qrGenerator = new QRCodeGenerator();

        //    string IosLink = Configuration["IOSLink"];
        //    QRCodeData IsoqrCodeData = qrGenerator.CreateQrCode(IosLink, QRCodeGenerator.ECCLevel.H);
        //    QRCode IosqrCode = new QRCode(IsoqrCodeData);
        //    Bitmap IosqrCodeImage = IosqrCode.GetGraphic(20);

        //    string AndroidLink = Configuration["AndroidLink"];
        //    QRCodeData AndroidqrCodeData = qrGenerator.CreateQrCode(AndroidLink, QRCodeGenerator.ECCLevel.H);
        //    QRCode AndroidqrCode = new QRCode(AndroidqrCodeData);
        //    Bitmap AndroidqrCodeImage = AndroidqrCode.GetGraphic(20);

        //    string content = "<div class='col-6'><img src=" + BitmapToBytes(IosqrCodeImage) + " class='qr-code img-thumbnail img-responsive'><h4>IOS</h4></div><div class='col-6'><img src=" + BitmapToBytes(AndroidqrCodeImage) + " class='qr-code img-thumbnail img-responsive'><h4>Android</h4></div>";

        //    return content;

        //}

        /*[HttpGet]
        public string GetQrCodeData(string randomCode)
        {
            QRCodeGenerator qrGenerator = new QRCodeGenerator();

            string IosLink = randomCode;
            QRCodeData IsoqrCodeData = qrGenerator.CreateQrCode(IosLink, QRCodeGenerator.ECCLevel.H);
            QRCode IosqrCode = new QRCode(IsoqrCodeData);
            Bitmap IosqrCodeImage = IosqrCode.GetGraphic(20);

            string content = BitmapToBytes(IosqrCodeImage);

            return content;

        }*/
        [HttpGet]
        public string GetQrCodeData(string randomCode)
        {
            string qrCodeBase64 = GenerateQrCodeBase64(randomCode);
            return qrCodeBase64;
        }
        //private string BitmapToBytes(Bitmap img)
        //{
        //    using (MemoryStream stream = new MemoryStream())
        //    {
        //        img.Save(stream, System.Drawing.Imaging.ImageFormat.Png);
        //        var data = String.Format("data:image/png;base64,{0}", Convert.ToBase64String(stream.ToArray()));
        //        return data;
        //    }
        //}
        private string GenerateQrCodeBase64(string text)
        {
            using (QRCodeGenerator qrGenerator = new QRCodeGenerator())
            {
                QRCodeData qrCodeData = qrGenerator.CreateQrCode(text, QRCodeGenerator.ECCLevel.H);

                // Use PngByteQRCode instead of Bitmap (No System.Drawing dependency)
                PngByteQRCode qrCode = new PngByteQRCode(qrCodeData);
                byte[] qrCodeBytes = qrCode.GetGraphic(20); // 20 = Pixel size

                return $"data:image/png;base64,{Convert.ToBase64String(qrCodeBytes)}";
            }
        }

        /*private string BitmapToBytes(Bitmap img)
        {
            using (MemoryStream stream = new MemoryStream())
            {
                img.Save(stream, System.Drawing.Imaging.ImageFormat.Png);
                var data = String.Format("data:image/png;base64,{0}", Convert.ToBase64String(stream.ToArray()));
                return data;
            }
        }*/
        public string StringToBase64(string data)
        {
            return Convert.ToBase64String(Encoding.UTF8.GetBytes(data));
        }

        public string StringFromBase64(string data)
        {
            return Encoding.UTF8.GetString(Convert.FromBase64String(data));
        }

        public string GetIpAddress()
        {
            _logger.LogDebug("--> GetIpAddress");
            string result = "";
            try
            {
                var ip = GetHeaderValueAs<string>("REMOTE_ADDR");
                IPAddress remoteIpAddress = HttpContext.Request
                    .HttpContext.Connection.RemoteIpAddress;

                if (remoteIpAddress != null)
                {
                    // If we got an IPV6 address, then we need to ask the network for the IPV4 address 
                    // This usually only happens when the browser is on the same machine as the server.
                    if (remoteIpAddress.AddressFamily == System.Net.Sockets.
                        AddressFamily.InterNetworkV6)
                    {
                        remoteIpAddress = System.Net.Dns.GetHostEntry(remoteIpAddress)
                            .AddressList.First(x => x.AddressFamily == System.Net.Sockets.
                            AddressFamily.InterNetwork);
                    }
                    result = remoteIpAddress.ToString();
                }

                if (IsNullOrWhitespace(result))
                    result = SplitCsv(GetHeaderValueAs<string>("X-Forwarded-For"))
                        .FirstOrDefault();

                // RemoteIpAddress is always null in DNX RC1 Update1 (bug).
                if (IsNullOrWhitespace(result) &&
                    HttpContext?.Connection?.RemoteIpAddress != null)
                    result = HttpContext.Connection.RemoteIpAddress.ToString();

                if (IsNullOrWhitespace(result))
                    result = GetHeaderValueAs<string>("REMOTE_ADDR");

                // _httpContextAccessor.HttpContext?.Request?.Host this is the local host.

                _logger.LogDebug("<--GetIpAddress");
                return result;
            }
            catch (Exception e)
            {
                _logger.LogError("GetIpAddress: {0}", e.Message);
                _logger.LogDebug("<--GetIpAddress");
                return result;
            }
        }
        public string GetDeviceType(string userAgent)
        {
            try
            {
                _logger.LogDebug("--> GetDeviceType");
                Regex OS = new Regex(@"(android|bb\d+|meego).+mobile|avantgo|bada\/|
                                    blackberry|blazer|compal|elaine|fennec|hiptop|iemobile|
                                    ip(hone|od)|iris|kindle|lge |maemo|midp|mmp|mobile.+firefox|
                                    netfront|opera m(ob|in)i|palm( os)?|phone|p(ixi|re)\/|
                                    plucker|pocket|psp|series(4|6)0|symbian|treo|up\.(browser|
                                    link)|vodafone|wap|windows ce|xda|xiino",
                                    RegexOptions.IgnoreCase | RegexOptions.Multiline);
                Regex device = new Regex(@"1207|6310|6590|3gso|4thp|50[1-6]i|770s|802s|
                                    a wa|abac|ac(er|oo|s\-)|ai(ko|rn)|al(av|ca|co)|amoi|
                                    an(ex|ny|yw)|aptu|ar(ch|go)|as(te|us)|attw|au(di|\-m|r |s )
                                    |avan|be(ck|ll|nq)|bi(lb|rd)|bl(ac|az)|br(e|v)w|bumb|bw\-(n|u)
                                    |c55\/|capi|ccwa|cdm\-|cell|chtm|cldc|cmd\-|co(mp|nd)|
                                    craw|da(it|ll|ng)|dbte|dc\-s|devi|dica|dmob|do(c|p)o|
                                    ds(12|\-d)|el(49|ai)|em(l2|ul)|er(ic|k0)|esl8|
                                    ez([4-7]0|os|wa|ze)|fetc|fly(\-|_)|g1 u|g560|gene|gf\-5|g\-mo|
                                    go(\.w|od)|gr(ad|un)|haie|hcit|hd\-(m|p|t)|hei\-|hi(pt|ta)|
                                    hp( i|ip)|hs\-c|ht(c(\-| |_|a|g|p|s|t)|tp)|hu(aw|tc)|
                                    i\-(20|go|ma)|i230|iac( |\-|\/)|ibro|idea|ig01|ikom|im1k|
                                    inno|ipaq|iris|ja(t|v)a|jbro|jemu|jigs|kddi|keji|kgt( |\/)|
                                    klon|kpt |kwc\-|kyo(c|k)|le(no|xi)|lg( g|\/(k|l|u)|50|54|
                                    \-[a-w])|libw|lynx|m1\-w|m3ga|m50\/|ma(te|ui|xo)|
                                    mc(01|21|ca)|m\-cr|me(rc|ri)|mi(o8|oa|ts)|mmef|
                                    mo(01|02|bi|de|do|t(\-| |o|v)|zz)|mt(50|p1|v )|
                                    mwbp|mywa|n10[0-2]|n20[2-3]|n30(0|2)|n50(0|2|5)|n7(0(0|1)
                                    |10)|ne((c|m)\-|on|tf|wf|wg|wt)|nok(6|i)|nzph|o2im|op(ti|wv)|
                                    oran|owg1|p800|pan(a|d|t)|pdxg|pg(13|\-([1-8]|c))|phil|pire|
                                    pl(ay|uc)|pn\-2|po(ck|rt|se)|prox|psio|pt\-g|qa\-a|
                                    qc(07|12|21|32|60|\-[2-7]|i\-)|qtek|r380|r600|raks|rim9|
                                    ro(ve|zo)|s55\/|sa(ge|ma|mm|ms|ny|va)|sc(01|h\-|oo|p\-)|
                                    sdk\/|se(c(\-|0|1)|47|mc|nd|ri)|sgh\-|shar|sie(\-|m)|sk\-0|
                                    sl(45|id)|sm(al|ar|b3|it|t5)|so(ft|ny)|sp(01|h\-|v\-|v )|
                                    sy(01|mb)|t2(18|50)|t6(00|10|18)|ta(gt|lk)|tcl\-|tdg\-|
                                    tel(i|m)|tim\-|t\-mo|to(pl|sh)|ts(70|m\-|m3|m5)|tx\-9|
                                    up(\.b|g1|si)|utst|v400|v750|veri|vi(rg|te)|vk(40|5[0-3]|\-v)
                                    |vm40|voda|vulc|vx(52|53|60|61|70|80|81|83|85|98)|w3c(\-| )|
                                    webc|whit|wi(g |nc|nw)|wmlb|wonu|x700|yas\-|your|zeto|
                                    zte\-", RegexOptions.IgnoreCase | RegexOptions.Multiline);
                string device_info = string.Empty;
                if (OS.IsMatch(userAgent))
                {
                    device_info = OS.Match(userAgent).Groups[0].Value;
                }
                if (device.IsMatch(userAgent.Substring(0, 4)))
                {
                    device_info += device.Match(userAgent).Groups[0].Value;
                }
                if (!string.IsNullOrEmpty(device_info))
                {
                    _logger.LogDebug("<--GetDeviceType");
                    return device_info;
                }
                else
                {
                    _logger.LogDebug("<--GetDeviceType");
                    return "Unknown";
                }
            }
            catch (Exception e)
            {
                _logger.LogError("GetDeviceType: {0}", e.Message);
                _logger.LogDebug("<--GetDeviceType");
                return "Unknown";
            }
        }

        public int getErrorCode(string error)
        {
            var code = 0;
            if (string.IsNullOrEmpty(error))
            {
                throw new Exception("Message value getting null");
            }
            if ("Cookies Not Found" == error)
            {
                code = 99;
            }
            if (OidcConstants.ClientNotFound.Equals(error))
            {
                code = 100;
            }
            else if (OidcConstants.ClientNotActive.Equals(error))
            {
                code = 101;
            }
            else if (ErrMsgconstants.SubscriberNotFound.Equals(error))
            {
                code = 102;
            }
            else if (ErrMsgconstants.SubAccountSuspended.Equals(error))
            {
                code = 103;
            }
            else if (ErrMsgconstants.SubNotActive.Equals(error))
            {
                code = 104;
            }
            else if (ErrMsgconstants.NotificationSendFailed.Equals(error))
            {
                code = 105;
            }

            else if (ErrMsgconstants.TempSessionExpired.Equals(error))
            {
                code = 106;
            }

            else if (ErrMsgconstants.AuthnTokenExpired.Equals(error))
            {
                code = 107;
            }

            else if (ErrMsgconstants.SubAlreadyAuthenticated.Equals(error))
            {
                code = 108;
            }


            return code;
        }

        public static bool IsNullOrWhitespace(string s)
        {
            return String.IsNullOrWhiteSpace(s);
        }

        public T GetHeaderValueAs<T>(string headerName)
        {
            StringValues values = "";

            if (HttpContext?.Request?.Headers?.TryGetValue(headerName, out values) ?? false)
            {
                string rawValues = values.ToString();// writes out as Csv when there are multiple.

                if (!IsNullOrWhitespace(rawValues))
                    return (T)Convert.ChangeType(values.ToString(), typeof(T));
            }
            return default(T);
        }

        public static List<string> SplitCsv(string csvList,
            bool nullOrWhitespaceInputReturnsNull = false)
        {
            if (string.IsNullOrWhiteSpace(csvList))
                return nullOrWhitespaceInputReturnsNull ? null : new List<string>();

            return csvList
                .TrimEnd(',')
                .Split(',')
                .AsEnumerable<string>()
                .Select(s => s.Trim())
                .ToList();
        }

        [HttpGet]
        public IActionResult GetAuthSchemas()
        {
            var schemas = Configuration.GetSection("AuthSchemas").Get<Dictionary<string, string>>();
            return Json(schemas);
        }

        [HttpGet]
        public async Task<IActionResult> ChangeAuthScheme(string authScheme)
        {
            if (!HttpContext.Request.Cookies.ContainsKey("TempSession"))
            {
                _logger.LogError("Login post: TempSession value not found ");
                throw new Exception("TempSession value not found");
            }

            var temporarySession = StringFromBase64(HttpContext.Request.Cookies["TempSession"]);

            var response = await _authenticationService.ChangeAuthScheme(authScheme, temporarySession);
            if (response == null)
            {
                _logger.LogError("VerifyUser: Response value getting null ");
                throw new Exception("VerifyUser Response value getting null");
            }
            if (response.Success)
            {
                response.Result.AuthnToken = string.Empty;
                if (response.Result.AuthenticationSchemes.Contains("QRCODE"))
                {
                    response.Result.QrCode = GetQrCodeData(StringToBase64(response.Result.AuthnToken));
                }
                else if (response.Result.AuthenticationSchemes.Contains("WALLET"))
                {
                    response.Result.QrCode = GetQrCodeData(response.Result.VerifierUrl);
                }

                _logger.LogDebug("<-- VerifyUser");
                return Ok(response);
            }
            else
            {
                _logger.LogDebug("VerifyUser : {0}", response.Message);
                _logger.LogDebug("<-- VerifyUser");

                var Res = new
                {
                    Success = response.Success,
                    Message = response.Message,
                    accountlocktime = ssoConfig.sso_config.
                        account_lock_time.ToString(),
                    ErrorCode = getErrorCode(response.Message)
                };
                return Ok(Res);
            }
        }

        public IActionResult SetRememberUser(bool value)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if (value)
            {
                Response.Cookies.Append(
                    "rememberUser",
                    "true",
                    new CookieOptions
                    {
                        Path = "/",
                        MaxAge = TimeSpan.FromDays(30),
                        HttpOnly = true,
                        Secure = true,
                        SameSite = SameSiteMode.Lax
                    });
            }
            else
            {
                ChangeUser();
            }
            return Ok();
        }

        [HttpPost]
        [IgnoreAntiforgeryToken]
        public IActionResult ChangeUser()
        {
            var cookieOptions = new CookieOptions
            {
                Path = "/",
                Secure = true,
                SameSite = SameSiteMode.Lax
            };

            Response.Cookies.Delete("rememberUser", cookieOptions);

            Response.Cookies.Delete("idp.remembered_user", cookieOptions);

            return Ok();
        }

    }
}
