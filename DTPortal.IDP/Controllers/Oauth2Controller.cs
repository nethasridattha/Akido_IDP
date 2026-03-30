using DTPortal.Core.Constants;
using DTPortal.Core.Domain.Models;
using DTPortal.Core.Domain.Services;
using DTPortal.Core.Domain.Services.Communication;
using DTPortal.Core.Utilities;
using DTPortal.IDP.Attribute;
using DTPortal.IDP.ViewModel.Oauth2;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
namespace DTPortal.IDP.Controllers
{

    // [CustomAuthorization]
    [ApiExplorerSettings(IgnoreApi = true)]
    [Route("authorization")]
    [AllowAnonymous]
    [ServiceFilter(typeof(CustomAuthorizationAttribute))]
    public class Oauth2Controller : Controller
    {
        private DTPortal.Core.Domain.Services.IAuthenticationService
                            _authenticationService;
        private IUserConsentService _userConsentService;
        private ITokenManagerService _tokenManager;
        private readonly IConfigurationService _configurationService;
        private readonly IGlobalConfiguration _globalConfiguration;
        private readonly ILogger<Oauth2Controller> _logger;
        private readonly WebConstants WEBConstants;
        private readonly MessageConstants ErrorMsgConstants;
        private readonly IScopeService _scopeService;
        private DTPortal.Core.Domain.Services.IClientService _clientService;
        private readonly IUserClaimService _userClaimService;
        private readonly IHelper _helper;
        private readonly IMessageLocalizer _messageLocalizer;
        private readonly IConfiguration _configuration;
        public Oauth2Controller(ILogger<Oauth2Controller> logger,
                      IConfigurationService configurationService,
                      ITokenManagerService tokenManager,
                      IGlobalConfiguration globalConfiguration,
                      DTPortal.Core.Domain.Services.IAuthenticationService authenticationService,
                      IUserConsentService userConsentService,
                      IHelper helper,
                      IScopeService scopeService,
                      IUserClaimService userClaimService,
                      DTPortal.Core.Domain.Services.IClientService clientService,
                      IConfiguration configuration,
                      IMessageLocalizer messageLocalizer)
        {
            _authenticationService = authenticationService;
            _configurationService = configurationService;
            _userConsentService = userConsentService;
            _tokenManager = tokenManager;
            _clientService = clientService;
            _logger = logger;
            _globalConfiguration = globalConfiguration;
            _helper = helper;
            _scopeService = scopeService;
            _userClaimService = userClaimService;
            _configuration = configuration;

            var errorConfiguration = _globalConfiguration.
               GetErrorConfiguration();
            if (null == errorConfiguration)
            {
                _logger.LogError("Get Error Configuration failed");
                throw new NullReferenceException();
            }

            ErrorMsgConstants = errorConfiguration.Constants;
            if (null == ErrorMsgConstants)
            {
                _logger.LogError("Get Error Constants failed");
                throw new NullReferenceException();
            }

            WEBConstants = errorConfiguration.WebConstants;
            if (null == WEBConstants)
            {
                _logger.LogError("Get Error WebConstants failed");
                throw new NullReferenceException();
            }

            _messageLocalizer = messageLocalizer;
        }

        public APIResponse ValidateData(AuthorizationViewModel model,
            clientDetails jwtParams = null)
        {
            if (!ModelState.IsValid)
            {
                //skipped model state validation as we are validating parameters separately and returning specific error messages for each parameter.
            }
            APIResponse response = new APIResponse();
            response.Success = true;
            response.Message = "";

            if (jwtParams != null)
            {
                if (!string.IsNullOrEmpty(jwtParams.clientId))
                {
                    model.client_id = jwtParams.clientId;
                }
                if (!string.IsNullOrEmpty(jwtParams.redirect_uri))
                {
                    model.redirect_uri = jwtParams.redirect_uri;
                }
                if (!string.IsNullOrEmpty(jwtParams.response_type))
                {
                    model.response_type = jwtParams.response_type;
                }
                if (!string.IsNullOrEmpty(jwtParams.scopes))
                {
                    model.scope = jwtParams.scopes;
                }
                if (!string.IsNullOrEmpty(jwtParams.state))
                {
                    model.state = jwtParams.state;
                }
                if (!string.IsNullOrEmpty(jwtParams.nonce))
                {
                    model.nonce = jwtParams.nonce;
                }
            }

            if (string.IsNullOrEmpty(model.client_id))
            {
                response.Success = false;
                response.Message = _messageLocalizer.GetMessage(WEBConstants.ClientIdNotFound);
            }
            if (string.IsNullOrEmpty(model.redirect_uri) && !Uri.IsWellFormedUriString(
                model.redirect_uri, UriKind.Absolute))
            {
                response.Success = false;
                response.Message = _messageLocalizer.GetMessage(WEBConstants.RedirectUriMissing);
            }
            if (string.IsNullOrEmpty(model.response_type))
            {
                response.Success = false;
                response.Message = _messageLocalizer.GetMessage(WEBConstants.ResponseTypeNotFound)  ;
            }
            if (string.IsNullOrEmpty(model.scope))
            {
                response.Success = false;
                response.Message = _messageLocalizer.GetMessage(WEBConstants.ScopeNotFound);
            }
            if (string.IsNullOrEmpty(model.state))
            {
                response.Success = false;
                response.Message = _messageLocalizer.GetMessage(WEBConstants.StateNotFound);
            }
            if (model.scope.Contains("openid") && string.IsNullOrEmpty(model.nonce))
            {
                response.Success = false;
                response.Message = _messageLocalizer.GetMessage(WEBConstants.NonceNotFound);
            }

            return response;
        }

        [HttpGet]
        public async Task<IActionResult> Index(AuthorizationViewModel model)
        {
            if(!ModelState.IsValid)
                return View(model);
            try
            {
                _logger.LogDebug("---> Oauth2Controller Get");
                clientDetails clientData = null;

                var IDPconfigInDB = _globalConfiguration.GetIDPConfiguration();
                if (null == IDPconfigInDB)
                {
                    _logger.LogError("GetCode:fail to get idp_configuration");
                    ViewBag.error = WEBConstants.InternalError;
                    ViewBag.error_description = _helper.GetErrorMsg(ErrorCodes.OAUTH2_GET_IDPCONFIG_RES_NULL);
                    return View("Error");
                }

          

                if (string.IsNullOrEmpty(model.client_id))
                {
                    if (!string.IsNullOrEmpty(model.redirect_uri))
                    {
                        // 1. Validate the URI first
                        bool isValidRedirect = await _clientService.IsRedirectUriExistsAsync(model.redirect_uri);

                        if (isValidRedirect)
                        {
                            // 2. Build the query parameters safely
                            var queryParams = new Dictionary<string, string?>
                            {
                                { "error", "invalid_request_object" },
                                { "error_description", WEBConstants.ClientIdNotFound.En },
                                { "state", model.state }
                            };

                            // 3. Use QueryHelpers to combine the URI and parameters (prevents CRLF/injection)
                            string safeUrl = QueryHelpers.AddQueryString(model.redirect_uri, queryParams);

                            // CodeQL recognizes this as safe because 'model.redirect_uri' was validated 
                            // by 'IsRedirectUriExistsAsync' immediately before use.
                            return Redirect(safeUrl);
                        }
                        else
                        {
                            // 4. Handle invalid/unregistered URIs by showing a local error page
                            _logger.LogWarning("Blocked redirect attempt for missing ClientID: {0}",
                                model.redirect_uri?.SanitizeForLogging());

                            ViewBag.error = "unauthorized_client";
                            ViewBag.error_description = "The redirect URI is not registered.";
                            return View("Error");
                        }
                    }
                    else
                    {
                        ViewBag.error = "invalid_request_object";
                        ViewBag.error_description = WEBConstants.ClientIdNotFound;
                        return View("Error");
                    }
                }

                // Get client details
                var client = await _clientService.GetClientByClientIdAsync(model.client_id);
                if (null == client)
                {
                    if (!string.IsNullOrEmpty(model.redirect_uri))
                    {
                        // 1. Validate the redirect URI against the database
                        bool isValidRedirect = await _clientService.IsRedirectUriExistsAsync(model.redirect_uri);

                        if (isValidRedirect)
                        {
                            // 2. Build the query parameters safely
                            // Extract the string from WEBConstants.ClientIdNotFound (using .Message or .ToString())
                            var queryParams = new Dictionary<string, string?>
                        {
                            { "error", "invalid_request_object" },
                            { "error_description", WEBConstants.ClientIdNotFound.En ?? "Client ID not found" },
                            { "state", model.state }
                        };

                            // 3. Construct the final URL using QueryHelpers to prevent injection
                            string safeUrl = QueryHelpers.AddQueryString(model.redirect_uri, queryParams);

                            // CodeQL marks this as safe because 'model.redirect_uri' passed the 'isValidRedirect' check
                            return Redirect(safeUrl);
                        }
                        else
                        {
                            // 4. If the URI is not in the DB, do not redirect. Show a local error instead.
                            _logger.LogWarning("Blocked redirect attempt for unknown Client: {0}",
                                model.redirect_uri?.SanitizeForLogging());

                            ViewBag.error = "unauthorized_client";
                            ViewBag.error_description = "The redirect URI is not registered.";
                            return View("Error");
                        }
                    }
                    else
                    {
                        // Handle case where redirect_uri is also null/empty
                        ViewBag.error = "invalid_request_object";
                        ViewBag.error_description = WEBConstants.ClientIdNotFound.En ?? "Client ID not found";
                        return View("Error");
                    }
                }

                Core.Domain.Services.Communication.Common openIdCommonConfig = JsonConvert.DeserializeObject<Core.Domain.Services.Communication.Common>
                        (IDPconfigInDB.common.ToString());

                bool isRequestSigningMandatory = _configuration.GetValue<bool>("RequestSigningMandatory");
                if (isRequestSigningMandatory)
                {
                    if (string.IsNullOrEmpty(model.request))
                    {
                        if (!string.IsNullOrEmpty(model.redirect_uri))
                        {
                            // 1. Perform the database validation
                            bool isValidRedirect = await _clientService.IsRedirectUriExistsAsync(model.redirect_uri);

                            if (isValidRedirect)
                            {
                                // 2. Build the query parameters in a Dictionary
                                var queryParams = new Dictionary<string, string?>
            {
                { "error", "invalid_request_object" },
                { "error_description", "Request has missing request parameter" },
                { "state", model.state }
            };

                                // 3. Construct the final URL safely using QueryHelpers
                                // This prevents CRLF injection and ensures proper encoding of the 'state'
                                string safeUrl = QueryHelpers.AddQueryString(model.redirect_uri, queryParams);

                                // CodeQL recognizes this as safe because 'model.redirect_uri' passed the database check
                                return Redirect(safeUrl);
                            }
                            else
                            {
                                // 4. Handle the case where the redirect_uri is not authorized
                                _logger.LogWarning("Blocked unauthorized redirect for missing request: {0}",
                                    model.redirect_uri?.SanitizeForLogging());

                                ViewBag.error = "unauthorized_client";
                                ViewBag.error_description = "The redirect URI is not registered in our system.";
                                return View("Error");
                            }
                        }
                        else
                        {
                            // No redirect_uri provided, show the error locally
                            ViewBag.error = "invalid_request_object";
                            ViewBag.error_description = "Request has missing request parameter";
                            return View("Error");
                        }
                    }
                }

                if (!string.IsNullOrEmpty(model.request))
                {
                    var isTokenValid = _tokenManager.ValidateRequestJWToken(
                        model.request, model.client_id, client.PublicKeyCert);
                    if (!isTokenValid)
                    {
                        if (!string.IsNullOrEmpty(model.redirect_uri))
                        {
                            // 1. Validate the redirect URI against the database
                            bool isValidRedirect = await _clientService.IsRedirectUriExistsAsync(model.redirect_uri);

                            if (isValidRedirect)
                            {
                                // 2. Build the query parameters safely
                                // Extracting the string from the LocalizedMessage object
                                var queryParams = new Dictionary<string, string?>
                                {
                                    { "error", "invalid_request_object" },
                                    { "error_description", WEBConstants.InvalidJWT.En ?? "Invalid JWT" },
                                    { "state", model.state }
                                };

                                // 3. Construct the final URL using QueryHelpers (prevents injection/encoding issues)
                                string safeUrl = QueryHelpers.AddQueryString(model.redirect_uri, queryParams);

                                // CodeQL marks this as safe because the base URI was validated by your service
                                return Redirect(safeUrl);
                            }
                            else
                            {
                                // 4. Handle unregistered URIs by showing a local error page
                                _logger.LogWarning("Blocked invalid redirect attempt for invalid token: {0}",
                                    model.redirect_uri?.SanitizeForLogging());

                                ViewBag.error = "unauthorized_client";
                                ViewBag.error_description = "The redirect URI is not registered.";
                                return View("Error");
                            }
                        }
                        else
                        {
                            // No redirect_uri provided, show error locally
                            ViewBag.error = "invalid_request_object";
                            ViewBag.error_description = WEBConstants.InvalidJWT.En ?? "Invalid JWT";
                            return View("Error");
                        }
                    }
                    clientData = _tokenManager.GetClientDetailsfromJwt(model.request);
                }

                var isValidParam = ValidateData(model, clientData);
                if (!isValidParam.Success)
                {
                    if (!string.IsNullOrEmpty(model.redirect_uri))
                    {
                        // 1. Validate the redirect URI against your database (The Sanitizer)
                        bool isValidRedirect = await _clientService.IsRedirectUriExistsAsync(model.redirect_uri);

                        if (isValidRedirect)
                        {
                            // 2. Build the query parameters safely in a dictionary
                            // Note: If isValidParam.Message is a LocalizedMessage, use .Message or .ToString()
                            var queryParams = new Dictionary<string, string?>
                            {
                                { "error", "invalid_request" },
                                { "error_description", isValidParam.Message?.ToString() ?? "Invalid parameters" },
                                { "state", model.state }
                            };

                            // 3. Construct the final URL using QueryHelpers to prevent CRLF/Injection
                            string safeUrl = QueryHelpers.AddQueryString(model.redirect_uri, queryParams);

                            // CodeQL recognizes this is safe because the base URI passed the database check
                            return Redirect(safeUrl);
                        }
                        else
                        {
                            // 4. Handle unregistered URIs by showing a local error page
                            _logger.LogWarning("Blocked redirect attempt for invalid parameters: {0}",
                                model.redirect_uri?.SanitizeForLogging());

                            ViewBag.error = "unauthorized_client";
                            ViewBag.error_description = "The redirect URI is not registered.";
                            return View("Error");
                        }
                    }
                    else
                    {
                        // No redirect_uri provided, show error locally
                        ViewBag.error = "invalid_request";
                        ViewBag.error_description = isValidParam.Message?.ToString() ?? "Invalid parameters";
                        return View("Error");
                    }
                }

                bool openIdConnectMandatory = _configuration.GetValue<bool>("OpenIdConnectMandatory");

                if (openIdConnectMandatory)
                {
                    if (!model.scope.Contains("openid"))
                    {
                        if (!string.IsNullOrEmpty(model.redirect_uri))
                        {
                            // 1. Validate the redirect URI against the database to prevent Open Redirect
                            bool isValidRedirect = await _clientService.IsRedirectUriExistsAsync(model.redirect_uri);

                            if (isValidRedirect)
                            {
                                // 2. Build query parameters in a Dictionary
                                var queryParams = new Dictionary<string, string?>
                                {
                                    { "error", "invalid_request_object" },
                                    { "error_description", "Request has missing openid scope" },
                                    { "state", model.state }
                                };

                                // 3. Construct the final URL using QueryHelpers (safe from CRLF injection)
                                string safeUrl = QueryHelpers.AddQueryString(model.redirect_uri, queryParams);

                                // CodeQL marks this as safe because the base URI passed the validation check
                                return Redirect(safeUrl);
                            }
                            else
                            {
                                // 4. Handle unregistered URIs by showing a local error page
                                _logger.LogWarning("Blocked unauthorized redirect for missing openid scope: {0}",
                                    model.redirect_uri?.SanitizeForLogging());

                                ViewBag.error = "unauthorized_client";
                                ViewBag.error_description = "The redirect URI is not registered.";
                                return View("Error");
                            }
                        }
                        else
                        {
                            // No redirect_uri provided, show the error locally
                            ViewBag.error = "invalid_request_object";
                            ViewBag.error_description = "Request has missing openid scope";
                            return View("Error");
                        }
                    }

                }

                var scopes = model.scope?.Split(' ', StringSplitOptions.RemoveEmptyEntries);

                if ((scopes == null) || (scopes.Length == 1 && scopes.Contains("openid")))
                {
                    if (!string.IsNullOrEmpty(model.redirect_uri))
                    {
                        // 1. Validate the redirect URI against the database (The Sanitizer)
                        bool isValidRedirect = await _clientService.IsRedirectUriExistsAsync(model.redirect_uri);

                        if (isValidRedirect)
                        {
                            // 2. Build the query parameters in a Dictionary
                            var queryParams = new Dictionary<string, string?>
                            {
                                { "error", "invalid_request_object" },
                                { "error_description", "Invalid scopes" },
                                { "state", model.state }
                            };

                            // 3. Construct the final URL safely using QueryHelpers
                            // This prevents CRLF injection and ensures proper encoding of the 'state'
                            string safeUrl = QueryHelpers.AddQueryString(model.redirect_uri, queryParams);

                            // CodeQL recognizes this as safe because the base URI passed the database check
                            return Redirect(safeUrl);
                        }
                        else
                        {
                            // 4. Handle the case where the redirect_uri is not authorized
                            _logger.LogWarning("Blocked unauthorized redirect for invalid scopes: {0}",
                                model.redirect_uri?.SanitizeForLogging());

                            ViewBag.error = "unauthorized_client";
                            ViewBag.error_description = "The redirect URI is not registered in our system.";
                            return View("Error");
                        }
                    }
                    else
                    {
                        // No redirect_uri provided, show the error locally
                        ViewBag.error = "invalid_request_object";
                        ViewBag.error_description = "Request has Invalid scopes";
                        return View("Error");
                    }
                }

                var clientDetails = new GetAuthSessClientDetails
                {
                    clientId = model.client_id,
                    redirect_uri = model.redirect_uri,
                    response_type = model.response_type,
                    scopes = model.scope,
                    withPkce = (string.IsNullOrEmpty(model.code_challenge) &&
                    string.IsNullOrEmpty(model.code_challenge_method)) ? false : true
                };

                var pkceDetails = new Pkcedetails
                {
                    codeChallenge = (!string.IsNullOrEmpty(model.code_challenge))
                                                        ? model.code_challenge : "",
                    codeChallengeMethod = (!string.IsNullOrEmpty(model.code_challenge_method))
                                                     ? model.code_challenge_method : ""
                };

                var data = new ValidateClientRequest
                {
                    clientDetails = clientDetails,
                    PkceDetails = pkceDetails,
                    clientDetailsInDb = client
                };

                var response = _authenticationService.ValidateClient(data);

                if (response == null)
                {
                    _logger.LogError("Oauth2Controller: Response value getting null ");
                    ViewBag.error = "Internal_Error";
                    ViewBag.error_description = _helper.GetErrorMsg
                        (ErrorCodes.OAUTH2_GET_VALIDATCLIENT_RES_NULL);
                    return View("Error");
                }

                if (!response.Success)
                {
                    if (!string.IsNullOrEmpty(model.redirect_uri))
                    {
                        // 1. Validate the redirect URI against the database (The Sanitizer)
                        bool isValidRedirect = await _clientService.IsRedirectUriExistsAsync(model.redirect_uri);

                        if (isValidRedirect)
                        {
                            // 2. Build the query parameters in a Dictionary
                            // Note: If response.Message is a LocalizedMessage object, use .ToString() or .Message
                            var queryParams = new Dictionary<string, string?>
                            {
                                { "error", "invalid_request" },
                                { "error_description", response.Message?.ToString() ?? "Invalid request" },
                                { "state", model.state }
                            };

                            // 3. Construct the final URL safely using QueryHelpers
                            // This prevents CRLF injection and ensures proper encoding
                            string safeUrl = QueryHelpers.AddQueryString(model.redirect_uri, queryParams);

                            // CodeQL recognizes this as safe because the base URI passed the database check
                            return Redirect(safeUrl);
                        }
                        else
                        {
                            // 4. Handle the case where the redirect_uri is not authorized
                            _logger.LogWarning("Blocked unauthorized redirect: {0}",
                                model.redirect_uri?.SanitizeForLogging());

                            ViewBag.error = "unauthorized_client";
                            ViewBag.error_description = "The redirect URI is not registered in our system.";
                            return View("Error");
                        }
                    }
                    else
                    {
                        // No redirect_uri provided, show the error locally
                        ViewBag.error = "invalid_request";
                        ViewBag.error_description = response.Message?.ToString() ?? "Invalid request";
                        return View("Error");
                    }

                }

                model.Application_Name = response.Result;

                _logger.LogDebug("<--- Oauth2Controller Get");

                var GlobalSession = HttpContext.User.Claims
                    .FirstOrDefault(c => c.Type == "Session").Value;

                var LogResponse = await _authenticationService.SendAuthenticationLogMessage
                    (GlobalSession, model.client_id);

                if (LogResponse == null || !LogResponse.Success)
                {
                    ViewBag.error = WEBConstants.InternalError;
                    ViewBag.error_description = _helper.GetErrorMsg
                        (ErrorCodes.OAUTH2_GET_METHOD_EXCP);
                    return View("Error");
                }

                var result = await _authenticationService.
                    IsUserGivenConsent(GlobalSession, model.client_id);

                if (result == null || !result.Success)
                {
                    var url = model.redirect_uri + "?error=invalid_request&" +
                        "error_description=" + result.Message +
                        "&state=" + model.state;
                }

                if (result.ConsentGiven)
                {
                    return RedirectToAction("Getcode", model);
                }

                return RedirectToAction("ConsentPage", model);
            }
            catch (Exception e)
            {
                _logger.LogError("Oauth2Controller Get:{0}", e.Message);
                ViewBag.error = WEBConstants.InternalError;
                ViewBag.error_description = _helper.GetErrorMsg
                    (ErrorCodes.OAUTH2_GET_METHOD_EXCP);
                return View("Error");
            }
        }

        [Route("Getcode")]
        [HttpGet]
        public async Task<IActionResult> Getcode(AuthorizationViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                _logger.LogDebug("---> GetCode");
                var clientDetails = new clientDetails
                {
                    clientId = model.client_id,
                    redirect_uri = model.redirect_uri,
                    response_type = model.response_type,
                    scopes = model.scope,
                    nonce = (string.IsNullOrEmpty(model.nonce)) ? "" : model.nonce,
                    grant_type = (!string.IsNullOrEmpty(model.response_type) &&
                                model.response_type == "code") ?
                                "authorization_code" : "implicit",
                    withPkce = (string.IsNullOrEmpty(model.code_challenge) &&
                    string.IsNullOrEmpty(model.code_challenge_method)) ? true : false
                };

                var pkceDetails = new Pkcedetails
                {
                    codeChallenge = (!string.IsNullOrEmpty(model.code_challenge))
                                            ? model.code_challenge : "",
                    codeChallengeMethod = (!string.IsNullOrEmpty(model.code_challenge_method))
                                                              ? model.code_challenge_method : ""
                };

                var GlobalSession = HttpContext.User.Claims
                    .FirstOrDefault(c => c.Type == "Session").Value;
                if (string.IsNullOrEmpty(GlobalSession))
                {
                    _logger.LogError("GetCode : GlobalSession not found");
                    _logger.LogDebug("<--- GetCode");
                    if (!string.IsNullOrEmpty(model.redirect_uri))
                    {
                        // 1. Validate the redirect URI against the database (The Sanitizer)
                        bool isValidRedirect = await _clientService.IsRedirectUriExistsAsync(model.redirect_uri);

                        if (isValidRedirect)
                        {
                            // 2. Build the query parameters in a Dictionary
                            // Note: Using .ToString() ensures the error message is a string type for the dictionary
                            var queryParams = new Dictionary<string, string?>
                            {
                                { "error", "Internal_Error" },
                                { "error_description", _helper.GetErrorMsg(ErrorCodes.SESSION_NOT_FOUND)?.ToString() },
                                { "state", model.state }
                            };

                            // 3. Construct the final URL safely using QueryHelpers
                            // This prevents CRLF injection and ensures proper encoding of the 'state'
                            string safeUrl = QueryHelpers.AddQueryString(model.redirect_uri, queryParams);

                            // CodeQL recognizes this as safe because the base URI passed the database check
                            return Redirect(safeUrl);
                        }
                        else
                        {
                            // 4. Handle the case where the redirect_uri is not authorized
                            _logger.LogWarning("Blocked unauthorized redirect for session error: {0}",
                                model.redirect_uri?.SanitizeForLogging());

                            ViewBag.error = WEBConstants.InternalError;
                            ViewBag.error_description = _helper.GetErrorMsg(ErrorCodes.SESSION_NOT_FOUND);
                            return View("GetCodeError");
                        }
                    }
                    else
                    {
                        // No redirect_uri provided, show the error locally
                        ViewBag.error = WEBConstants.InternalError;
                        ViewBag.error_description = _helper.GetErrorMsg(ErrorCodes.SESSION_NOT_FOUND);
                        return View("GetCodeError");
                    }
                }
                var data = new GetAuthZCodeRequest
                {
                    ClientDetails = clientDetails,
                    GlobalSessionId = GlobalSession,
                    pkcedetails = pkceDetails
                };

                if (null != model.code_challenge)
                {
                    clientDetails.withPkce = true;
                    Pkcedetails pkcedetails = new Pkcedetails()
                    {
                        codeChallenge = model.code_challenge,
                        codeChallengeMethod = model.code_challenge_method
                    };
                    data.pkcedetails = pkcedetails;
                }

                var response = await _authenticationService.GetAuthorizationCode(data);
                if (response == null)
                {
                    _logger.LogError("GetCode: Response value getting null ");
                    ViewBag.error = WEBConstants.InternalError;
                    ViewBag.error_description = _helper.GetErrorMsg(ErrorCodes.OAUTH2_GETCODE_GETAUTHORIZATIONCODE_RES_NULL);
                    return View("GetCodeError");
                }
                if (response.Success)
                {
                    _logger.LogDebug("<--- GetCode");
                    if (!string.IsNullOrEmpty(model.redirect_uri))
                    {
                        // 1. Validate the redirect URI against the database (The Sanitizer)
                        // This ensures you only send the Authorization Code to a trusted, registered application.
                        bool isValidRedirect = await _clientService.IsRedirectUriExistsAsync(model.redirect_uri);

                        if (isValidRedirect)
                        {
                            // 2. Build the query parameters in a Dictionary
                            var queryParams = new Dictionary<string, string?>
                            {
                                { "code", response.AuthorizationCode },
                                { "state", model.state }
                            };

                            // 3. Construct the final URL safely using QueryHelpers
                            // This handles URL encoding for the 'state' and prevents CRLF/Header injection.
                            string safeUrl = QueryHelpers.AddQueryString(model.redirect_uri, queryParams);

                            // CodeQL recognizes this as safe because the base URI passed the database check.
                            _logger.LogInformation("Redirecting to authorized client with code.");
                            return Redirect(safeUrl);
                        }
                        else
                        {
                            // 4. Handle the case where the redirect_uri is NOT registered in your system.
                            // SECURITY: Never send an Authorization Code to an unverified URL.
                            _logger.LogWarning("Blocked attempt to send AuthCode to unregistered URI: {0}",
                                model.redirect_uri?.SanitizeForLogging());

                            ViewBag.error = "unauthorized_client";
                            ViewBag.error_description = "The redirect URI is not registered in our system.";
                            return View("GetCodeError");
                        }
                    }
                    else
                    {
                        // No redirect_uri provided, show an error locally
                        ViewBag.error = "invalid_request";
                        ViewBag.error_description = "Redirect URI is missing.";
                        return View("GetCodeError");
                    }
                }
                else
                {
                    _logger.LogDebug("GetCode :{0}", response.Message);
                    if (response.Message.StartsWith("User denied consent") ||
                        response.Message.StartsWith("Subscriber denied consent"))
                    {
                        _logger.LogDebug("<--- GetCode");
                        if (!string.IsNullOrEmpty(model.redirect_uri))
                        {
                            // 1. Validate the redirect URI against the database (The Sanitizer)
                            bool isValidRedirect = await _clientService.IsRedirectUriExistsAsync(model.redirect_uri);

                            if (isValidRedirect)
                            {
                                // 2. Build the query parameters in a Dictionary
                                // Extract the string from WEBConstants.DeniedConsent (LocalizedMessage)
                                var queryParams = new Dictionary<string, string?>
                                {
                                    { "error", "Access_Denied" },
                                    { "error_description", WEBConstants.DeniedConsent.En ?? "Access Denied" }
                                };

                                // 3. Construct the final URL safely using QueryHelpers
                                // This prevents CRLF injection and ensures proper encoding
                                string safeUrl = QueryHelpers.AddQueryString(model.redirect_uri, queryParams);

                                // CodeQL marks this as safe because the base URI passed the database check
                                return Redirect(safeUrl);
                            }
                            else
                            {
                                // 4. Handle the case where the redirect_uri is not authorized
                                _logger.LogWarning("Blocked unauthorized redirect for Access Denied: {0}",
                                    model.redirect_uri?.SanitizeForLogging());

                                ViewBag.error = "Access_Denied";
                                ViewBag.error_description = WEBConstants.DeniedConsent.En ?? "Access Denied";
                                return View("GetCodeError");
                            }
                        }
                        else
                        {
                            // No redirect_uri provided, show the error locally
                            ViewBag.error = "Access_Denied";
                            ViewBag.error_description = WEBConstants.DeniedConsent.En ?? "Access Denied";
                            return View("GetCodeError");
                        }
                    }
                    else if (response.Message.StartsWith("User Consent Required for") ||
                         response.Message.StartsWith("Subscriber Consent Required for"))
                    {
                        var Email = HttpContext.User.Claims
                           .FirstOrDefault(c => c.Type == ClaimTypes.Email).Value;

                        var UserName = HttpContext.User.Claims
                            .FirstOrDefault(c => c.Type == ClaimTypes.Name).Value;

                        var start = response.Message.IndexOf("(") + 1;
                        var end = response.Message.IndexOf(")");
                        var scope = response.Message.Substring(start, end - start);
                        _logger.LogDebug("GetCode -> required user consent " +
                            "for scopes:{0}", scope?.SanitizeForLogging());
                        if (scope.Length == 0)
                        {
                            throw new Exception("Invalid Message :" + response.Message);
                        }
                        start = response.Message.IndexOf("[") + 1;
                        end = response.Message.IndexOf("]");
                        var suid = response.Message.Substring(start, end - start);
                        _logger.LogDebug("GetCode suid:{0}", suid);

                        var IDPconfigInDB = _globalConfiguration.GetIDPConfiguration();
                        if (null == IDPconfigInDB)
                        {
                            _logger.LogError("GetCode:fail to get idp_configuration");
                            ViewBag.error = WEBConstants.InternalError;
                            ViewBag.error_description = WEBConstants.SomethingWrong;
                            return View("Error");
                        }

                        var scopesListInDb = await _scopeService.ListScopeAsync();

                        if (null == scopesListInDb)
                        {
                            _logger.LogError("GetCode:fail to get scopes List");
                            ViewBag.error = WEBConstants.InternalError;
                            ViewBag.error_description = WEBConstants.SomethingWrong;
                            return View("Error");
                        }

                        var scopes = scope.Split(" ").ToList<string>();
                        var scopeList = new List<ScopeDetails>() { };

                        var attributesListInDb = await _userClaimService.ListUserClaimAsync();

                        foreach (var key in scopes)
                        {
                            foreach (var obj in scopesListInDb)
                            {
                                if (obj.Name == key)
                                {
                                    var attributesList = obj.ClaimsList?.Split(' ', StringSplitOptions.RemoveEmptyEntries).
                                        ToList() ?? new List<string>();

                                    List<AttributeDetails> attributesDetailsList = new List<AttributeDetails>();

                                    foreach (var attribute in attributesList)
                                    { 
                                        
                                        var attributeObj = attributesListInDb.FirstOrDefault(a => a.Name == attribute);
                                        if (attributeObj != null)
                                        {
                                            AttributeDetails attributeDetails = new AttributeDetails()
                                            {
                                                displayName = attributeObj.DisplayName,
                                                name = attributeObj.Name,
                                                mandatory = attributeObj.DefaultClaim
                                            };
                                            attributesDetailsList.Add(attributeDetails);
                                        } 
                                    }

                                    scopeList.Add(new ScopeDetails
                                    {
                                        name = key,
                                        displayName = obj.DisplayName,
                                        description = obj.Description,
                                        version = obj.Version,
                                        attributes = attributesDetailsList
                                    });
                                }
                            }
                        }
                        ;

                        var consentModel = new ConsentViewModel
                        {
                            clientDetails = model,
                            username = UserName,
                            usermail = Email,
                            suid = suid,
                            scopes = scope.Split(" ").ToList<string>(),
                            scopesList = scopeList
                        };

                        _logger.LogDebug("<--- GetCode");
                        return View("ConsentPage", consentModel);

                    }
                    else if (response.Message == "Client is not Active")
                    {
                        _logger.LogDebug("<--- GetCode");
                        if (!string.IsNullOrEmpty(model.redirect_uri))
                        {
                            // 1. Validate the redirect URI against the database (The Sanitizer)
                            bool isValidRedirect = await _clientService.IsRedirectUriExistsAsync(model.redirect_uri);

                            if (isValidRedirect)
                            {
                                // 2. Build the query parameters in a Dictionary
                                // Note: Extract the string message from the LocalizedMessage object
                                var queryParams = new Dictionary<string, string?>
                                {
                                    { "error", "Access_Denied" },
                                    { "error_description", WEBConstants.ClientNotActive.En ?? "Client is not active" }
                                };

                                // 3. Construct the final URL safely using QueryHelpers
                                // This prevents CRLF/Header injection and handles proper encoding
                                string safeUrl = QueryHelpers.AddQueryString(model.redirect_uri, queryParams);

                                // CodeQL recognizes this as safe because the base URI passed the database check
                                return Redirect(safeUrl);
                            }
                            else
                            {
                                // 4. Handle the case where the redirect_uri is NOT registered
                                _logger.LogWarning("Blocked unauthorized redirect for inactive client error: {0}",
                                    model.redirect_uri?.SanitizeForLogging());

                                ViewBag.error = "Access_Denied";
                                ViewBag.error_description = WEBConstants.ClientNotActive.En ?? "Client is not active";
                                return View("GetCodeError");
                            }
                        }
                        else
                        {
                            // No redirect_uri provided, show the error locally
                            ViewBag.error = "Access_Denied";
                            ViewBag.error_description = WEBConstants.ClientNotActive.En ?? "Client is not active";
                            return View("GetCodeError");
                        }

                    }
                    else
                    {
                        ViewBag.error = WEBConstants.InternalError;
                        ViewBag.error_description = response.Message;
                        return View("GetCodeError");
                    }

                }

            }
            catch (Exception e)
            {
                _logger.LogError("GetCode:{0}", e.Message);
                ViewBag.error = WEBConstants.InternalError;
                ViewBag.error_description = _helper.GetErrorMsg(ErrorCodes.OAUTH2_GETCODE_METHOD_EXCP);
                return View("GetCodeError");
            }
        }

        [Route("ConsentPage")]
        [HttpGet]
        public async Task<IActionResult> ConsentPage(AuthorizationViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var Email = HttpContext.User.Claims
                           .FirstOrDefault(c => c.Type == ClaimTypes.Email).Value;

            var UserName = HttpContext.User.Claims
                .FirstOrDefault(c => c.Type == ClaimTypes.Name).Value;

            var Suid = HttpContext.User.Claims
                .FirstOrDefault(c => c.Type == "UserId").Value;

            var scope = model.scope;

            _logger.LogDebug("GetCode -> required user consent " +
                "for scopes:{0}", scope?.SanitizeForLogging());
            if (scope.Length == 0)
            {
                throw new Exception("Invalid Scopes :");
            }

            var IDPconfigInDB = _globalConfiguration.GetIDPConfiguration();
            if (null == IDPconfigInDB)
            {
                _logger.LogError("GetCode:fail to get idp_configuration");
                ViewBag.error = WEBConstants.InternalError;
                ViewBag.error_description = WEBConstants.SomethingWrong;
                return View("Error");
            }

            var scopesListInDb = await _scopeService.ListScopeAsync();

            if (null == scopesListInDb)
            {
                _logger.LogError("GetCode:fail to get scopes List");
                ViewBag.error = WEBConstants.InternalError;
                ViewBag.error_description = WEBConstants.SomethingWrong;
                return View("Error");
            }

            var scopes = scope.Split(" ").ToList<string>();
            var scopeList = new List<ScopeDetails>() { };

            var attributesListInDb = await _userClaimService.ListUserClaimAsync();

            foreach (var key in scopes)
            {
                foreach (var obj in scopesListInDb)
                {
                    if (obj.Name == key)
                    {
                        var attributesList = obj.ClaimsList?.Split(' ', StringSplitOptions.RemoveEmptyEntries).
                            ToList() ?? new List<string>();

                        List<AttributeDetails> attributesDetailsList = new List<AttributeDetails>();

                        foreach (var attribute in attributesList)
                        {

                            var attributeObj = attributesListInDb.FirstOrDefault(a => a.Name == attribute);
                            if (attributeObj != null)
                            {
                                AttributeDetails attributeDetails = new AttributeDetails()
                                {
                                    displayName = attributeObj.DisplayName,
                                    name = attributeObj.Name,
                                    mandatory = attributeObj.DefaultClaim
                                };
                                attributesDetailsList.Add(attributeDetails);
                            }
                        }

                        scopeList.Add(new ScopeDetails
                        {
                            name = key,
                            displayName = obj.DisplayName,
                            description = obj.Description,
                            version = obj.Version,
                            attributes = attributesDetailsList
                        });
                    }
                }
            }
                        ;

            var consentModel = new ConsentViewModel
            {
                clientDetails = model,
                username = UserName,
                usermail = Email,
                suid = Suid,
                scopes = scope.Split(" ").ToList<string>(),
                scopesList = scopeList
            };

            _logger.LogDebug("<--- GetCode");
            return View("ConsentPage", consentModel);
        }

        [Route("Allow1")]
        [IgnoreAntiforgeryToken]
        [HttpPost]
        public async Task<IActionResult> Allow1(ConsentViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                _logger.LogDebug("--->Allow");
                List<approved_scopes> array = new List<approved_scopes>();

                var scopesList = await _scopeService.ListScopeAsync();

                if (scopesList == null)
                {
                    ViewBag.error = "Internal_Error";
                    ViewBag.error_description = "Failed to Get Scopes List";
                    return View("GetCodeError");
                }
                var GlobalSession = HttpContext.User.Claims
                    .FirstOrDefault(c => c.Type == "Session").Value;

                
                foreach (var element in model.scopes)
                {
                    var scope = scopesList
                        .FirstOrDefault(s => s.Name.Equals(element, StringComparison.OrdinalIgnoreCase));
                    if (scope == null)
                    {
                        ViewBag.error = "Internal_Error";
                        ViewBag.error_description = "Failed to Get Scopes";
                        return View("GetCodeError");
                    }
                    var Arrayobj = new approved_scopes()
                    {
                        scope = element,
                        permission = true,
                        version = scope.Version,
                        attributes = scope.ClaimsList?
                            .Split(' ', StringSplitOptions.RemoveEmptyEntries)
                            .ToList()
                            ?? new List<string>(),
                        created_date = DateTime.Now.ToString()
                    };
                    array.Add(Arrayobj);
                }
                var AllowedScopeDetailsList = new Scopes()
                {
                    approved_scopes = array
                };

                var data = new UserConsent
                {
                    ClientId = model.clientDetails.client_id,
                    Suid = model.suid,
                    Scopes = JsonConvert.SerializeObject(AllowedScopeDetailsList)
                };

                var response = await _userConsentService.ModifyUserConsent(data);
                if (response == null)
                {
                    _logger.LogError("Allow: Response value getting null ");
                    ViewBag.error = WEBConstants.InternalError;
                    ViewBag.error_description = _helper.GetErrorMsg(ErrorCodes.OAUTH2_ALLOW_MODIFYUSERCONCENT_RES_NULL);
                    return View("GetCodeError");
                }
                if (response.Success)
                {
                    _logger.LogInformation("User Allowed scopes {0}",
                     string.Join(",", model.scopes ?? Enumerable.Empty<string>()).SanitizeForLogging());
                    return RedirectToAction("Getcode", model.clientDetails);
                }
                else
                {

                    if (!string.IsNullOrEmpty(model.clientDetails.redirect_uri))
                    {
                        // 1. Validate the redirect URI against the database (The Sanitizer)
                        bool isValidRedirect = await _clientService.IsRedirectUriExistsAsync(model.clientDetails.redirect_uri);

                        if (isValidRedirect)
                        {
                            // 2. Sanitize the message for logging to prevent Log Forging
                            _logger.LogError("Allow: {0}", response.Message?.ToString().SanitizeForLogging());
                            _logger.LogDebug("<---Allow");

                            // 3. Build query parameters in a Dictionary
                            var queryParams = new Dictionary<string, string?>
                            {
                                { "error", "Internal_Error" },
                                { "error_description", response.Message?.ToString() ?? "Internal Server Error" },
                                { "state", model.clientDetails.state }
                            };

                            // 4. Construct the final URL safely using QueryHelpers
                            string safeUrl = QueryHelpers.AddQueryString(model.clientDetails.redirect_uri, queryParams);

                            // CodeQL recognizes this as safe because the base URI passed the database check
                            return Redirect(safeUrl);
                        }
                        else
                        {
                            // 5. Handle the case where the redirect_uri is NOT registered
                            _logger.LogWarning("Blocked unauthorized redirect in Allow: {0}",
                                model.clientDetails.redirect_uri?.SanitizeForLogging());

                            ViewBag.error = "unauthorized_client";
                            ViewBag.error_description = "The redirect URI is not registered in our system.";
                            return View("GetCodeError");
                        }
                    }
                    else
                    {
                        // No redirect_uri provided, show the error locally
                        _logger.LogError("Allow: {0}", response.Message?.ToString().SanitizeForLogging());
                        _logger.LogDebug("<---Allow");

                        ViewBag.error = "Internal_Error";
                        ViewBag.error_description = response.Message?.ToString() ?? "Internal Server Error";
                        return View("GetCodeError");
                    }
                }
            }
            catch (Exception e)
            {
                _logger.LogError("Allow:{0}", e.Message);
                ViewBag.error = WEBConstants.InternalError;
                ViewBag.error_description = _helper.GetErrorMsg(ErrorCodes.OAUTH2_ALLOW_METHOD_EXCP);
                return View("GetCodeError");
            }
        }

        [Route("Deny")]
        [IgnoreAntiforgeryToken]
        [HttpPost]
        public async Task<IActionResult> Deny(ConsentViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            _logger.LogInformation("User denied scopes {0}", model.scopes.ToString());

            var GlobalSession = HttpContext.User.Claims
                .FirstOrDefault(c => c.Type == "Session").Value;

            var response = await _authenticationService.SendConsentDeniedLogMessage
                (GlobalSession, model.clientDetails.client_id);

            if (!string.IsNullOrEmpty(model.clientDetails.redirect_uri))
            {

                bool isValidRedirect = await _clientService.IsRedirectUriExistsAsync(model.clientDetails.redirect_uri);

                if (isValidRedirect)
                {
                    _logger.LogDebug("<---Deny");

                    var queryParams = new Dictionary<string, string?>
                        {
                            { "error", "Access denied" },
                            { "error_description", WEBConstants.DeniedConsent.En ?? "Access Denied" },
                            { "state", model.clientDetails.state }
                        };


                    string safeUrl = QueryHelpers.AddQueryString(model.clientDetails.redirect_uri, queryParams);


                    return Redirect(safeUrl);
                }
                else
                {
                    // 4. Handle the case where the redirect_uri is NOT registered in your system.
                    _logger.LogWarning("Blocked unauthorized redirect in Deny: {0}",
                        model.clientDetails.redirect_uri?.SanitizeForLogging());

                    ViewBag.error = "Access denied";
                    ViewBag.error_description = WEBConstants.DeniedConsent.En ?? "Access Denied";
                    return View("GetCodeError");
                }
            }
            else
            {
                // No redirect_uri provided, show the error locally
                ViewBag.error = "Access denied";
                ViewBag.error_description = WEBConstants.DeniedConsent.En ?? "Access Denied";
                return View("GetCodeError");
            }
        }

        [Route("ApproveScopes")]
        [IgnoreAntiforgeryToken]
        [HttpPost]
        public async Task<IActionResult> ApproveScopes(ConsentViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var GlobalSession = HttpContext.User.Claims
                    .FirstOrDefault(c => c.Type == "Session").Value;

                List<ScopeDetail> scopeDetailList = new List<ScopeDetail>();

                var LogAttributesRequest = new LogAttributesRequest
                {
                    clientId = model.clientDetails.client_id,
                };
                foreach (var scope in model.scopesList)
                {
                    ScopeDetail scopeDetail = new ScopeDetail
                    {
                        Name = scope.name,
                        DisplayName = scope.displayName,
                    };

                    List<AttributeInfo> attributeDetailList = new List<AttributeInfo>();

                    foreach (var attribute in scope.attributes)
                    {
                        AttributeInfo attributeInfo = new AttributeInfo
                        {
                            Name = attribute.name,
                            DisplayName = attribute.displayName
                        };
                        attributeDetailList.Add(attributeInfo);
                    }
                }
                var response=await _authenticationService.LogAttributes
                    (GlobalSession,LogAttributesRequest);

                if (response == null)
                {
                    _logger.LogError("Allow: Response value getting null ");
                    ViewBag.error = WEBConstants.InternalError;
                    ViewBag.error_description = _helper.GetErrorMsg
                        (ErrorCodes.OAUTH2_ALLOW_MODIFYUSERCONCENT_RES_NULL);
                    return View("GetCodeError");
                }
                if (response.Success)
                {
                    _logger.LogInformation("User Allowed scopes {0}",
                    string.Join(",", model.scopes ?? Enumerable.Empty<string>()).SanitizeForLogging());
                    return RedirectToAction("Getcode", model.clientDetails);
                }
                else
                {

                    if (!string.IsNullOrEmpty(model.clientDetails.redirect_uri))
                    {
                        // 1. Validate the redirect URI against the database (The Sanitizer)
                        // This is the primary fix for the CodeQL "URL redirection" error.
                        bool isValidRedirect = await _clientService.IsRedirectUriExistsAsync(model.clientDetails.redirect_uri);

                        if (isValidRedirect)
                        {
                            // 2. Sanitize the message for logging to prevent Log Forging errors
                            _logger.LogError("Allow: {0}", response.Message?.ToString().SanitizeForLogging());
                            _logger.LogDebug("<---Allow");

                            // 3. Build query parameters safely in a Dictionary
                            // Note: Extract the string message from the LocalizedMessage object (response.Message)
                            var queryParams = new Dictionary<string, string?>
                            {
                                { "error", "Internal_Error" },
                                { "error_description", response.Message?.ToString() ?? "Internal Server Error" },
                                { "state", model.clientDetails.state }
                            };

                            // 4. Construct the final URL using QueryHelpers (prevents CRLF/Header injection)
                            string safeUrl = QueryHelpers.AddQueryString(model.clientDetails.redirect_uri, queryParams);

                            // CodeQL recognizes this as safe because the base URI passed the database check.
                            return Redirect(safeUrl);
                        }
                        else
                        {
                            // 5. Handle the case where the redirect_uri is NOT registered in your system.
                            // SECURITY: Block potential Open Redirect attacks.
                            _logger.LogWarning("Blocked unauthorized redirect attempt in Allow: {0}",
                                model.clientDetails.redirect_uri?.SanitizeForLogging());

                            ViewBag.error = "unauthorized_client";
                            ViewBag.error_description = "The redirect URI is not registered in our system.";
                            return View("GetCodeError");
                        }
                    }
                    else
                    {
                        // Case where redirect_uri is null or empty
                        _logger.LogError("Allow: {0}", response.Message?.ToString().SanitizeForLogging());
                        _logger.LogDebug("<---Allow");

                        ViewBag.error = "Internal_Error";
                        ViewBag.error_description = response.Message?.ToString() ?? "Internal Server Error";
                        return View("GetCodeError");
                    }
                }

            }
            catch (Exception e)
            {
                _logger.LogError("Allow:{0}", e.Message);
                ViewBag.error = WEBConstants.InternalError;
                ViewBag.error_description = _helper.GetErrorMsg(ErrorCodes.OAUTH2_ALLOW_METHOD_EXCP);
                return View("GetCodeError");
            }
        }

        [Route("Allow")]
        [IgnoreAntiforgeryToken]
        [HttpPost]
        public async Task<IActionResult> Allow(ConsentViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if (string.IsNullOrEmpty(model.SelectedAttributesJson))
            {
                _logger.LogError("Allow: SelectedAttributesJson is null or empty");
                return View("GetCodeError");
            }

            var selectedAttributesMap = JsonConvert.DeserializeObject
                    <Dictionary<string, List<string>>>(model.SelectedAttributesJson);

            List<LogProfileInfo> ScopeDetails = new List<LogProfileInfo>();

            List<string> approvedAttributes = new List<string>();

            var GlobalSession = HttpContext.User.Claims
                .FirstOrDefault(c => c.Type == "Session").Value;

            foreach (var (scopeName, attributes) in selectedAttributesMap)
            {
                LogProfileInfo logProfileInfo = new LogProfileInfo
                {
                    Name = scopeName,
                    Attributes = attributes
                };
                ScopeDetails.Add(logProfileInfo);
            }

            LogAttributesRequest LogAttributesRequest = new LogAttributesRequest
            {
                clientId = model.clientDetails.client_id,
                ScopeDetail = ScopeDetails
            };

            var response = await _authenticationService.LogAttributes
                (GlobalSession, LogAttributesRequest);

            if (response == null || !response.Success)
            {
                return View("GetCodeError");
            }

            return RedirectToAction("Getcode", model.clientDetails);
        }
    }
}