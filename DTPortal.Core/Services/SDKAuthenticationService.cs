using DTPortal.Common;
using DTPortal.Core.Constants;
using DTPortal.Core.Domain.Lookups;
using DTPortal.Core.Domain.Models;
using DTPortal.Core.Domain.Models.RegistrationAuthority;
using DTPortal.Core.Domain.Repositories;
using DTPortal.Core.Domain.Services;
using DTPortal.Core.Domain.Services.Communication;
using DTPortal.Core.DTOs;
using DTPortal.Core.Enums;
using DTPortal.Core.Exceptions;
using DTPortal.Core.Utilities;
using Fido2NetLib;
using Fido2NetLib.Development;
using Fido2NetLib.Objects;
using FirebaseAdmin.Auth;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Org.BouncyCastle.Asn1.Ocsp;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using static DTPortal.Common.CommonResponse;

namespace DTPortal.Core.Services
{
    public class SDKAuthenticationService:ISDKAuthenticationService
    {
        private readonly ILogger<SDKAuthenticationService> _logger;
        private readonly MessageConstants Constants;
        private readonly OIDCConstants OIDCConstants;
        private readonly WebConstants WebConstants;
        private readonly IGlobalConfiguration _globalConfiguration;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IHelper _helper;
        private readonly IConfiguration _configuration;
        private readonly SSOConfig ssoConfig;
        private readonly IRAServiceClient _raServiceClient;
        private readonly ICacheClient _cacheClient;
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IMessageLocalizer _messageLocalizer;
        public SDKAuthenticationService(ILogger<SDKAuthenticationService> logger,
            IGlobalConfiguration globalConfiguration,
            IUnitOfWork unitOfWork,
            IHelper helper,
            IConfiguration configuration,
            IRAServiceClient raServiceClient,
            ICacheClient cacheClient,
            IMessageLocalizer messageLocalizer,
            IHttpClientFactory httpClientFactory)
        {
            _logger = logger;
            _globalConfiguration = globalConfiguration;
            _messageLocalizer = messageLocalizer;
            ssoConfig = _globalConfiguration.GetSSOConfiguration();
            if (null == ssoConfig)
            {
                _logger.LogError("Get SSO Configuration failed in sdk auth service");
                throw new NullReferenceException();
            }
            var errorConfiguration = _globalConfiguration.
                GetErrorConfiguration();
            if (null == errorConfiguration)
            {
                _logger.LogError("Get Error Configuration failed");
                throw new NullReferenceException();
            }

            Constants = errorConfiguration.Constants;
            if (null == Constants)
            {
                _logger.LogError("Get Error Configuration failed");
                throw new NullReferenceException();
            }
            WebConstants = errorConfiguration.WebConstants;
            if (null == WebConstants)
            {
                _logger.LogError("Get Error Configuration failed");
                throw new NullReferenceException();
            }
            OIDCConstants = errorConfiguration.OIDCConstants;
            if (null == OIDCConstants)
            {
                _logger.LogError("Get Error Configuration failed");
                throw new NullReferenceException();
            }

            _unitOfWork = unitOfWork;
            _helper = helper;
            _configuration = configuration;
            _raServiceClient = raServiceClient;
            _cacheClient = cacheClient;
            _httpClientFactory = httpClientFactory;
        }
        public async Task<SubscriberView> GetSubscriberbyType(int requestType,
            string input)
        {
            var raSubscriber = new SubscriberView();

            switch (requestType)
            {
                case (int)InputType.emailId:
                    {
                        raSubscriber = await _unitOfWork.Subscriber.
                            GetSubscriberInfoByEmail(input);
                        if (null == raSubscriber)
                        {
                            _logger.LogError("Subscriber details not found : " + input?.SanitizeForLogging());
                            return null;
                        }
                        break;
                    }
                case (int)InputType.phoneno:
                    {
                        raSubscriber = await _unitOfWork.Subscriber.
                            GetSubscriberInfoByPhone(input);
                        if (null == raSubscriber)
                        {
                            _logger.LogError("Subscriber details not found : " + input?.SanitizeForLogging());
                            return null;
                        }
                        break;
                    }
                default:
                    {
                        _logger.LogError("Incorrect input received");
                        return null;
                    }
                    ;
            }
            return raSubscriber;
        }

        // ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
        public async Task<SubscriberView> GetSubscriberbyOrgnizationEmail(string input)
        {

            var raSubscriber = await _unitOfWork.Subscriber.
                 GetSubscriberInfoByOrgnizationEmail(input);
            if (raSubscriber.Count == 0 || raSubscriber.Count > 1)
            {
                _logger.LogError("Subscriber details not found : " + input?.SanitizeForLogging());
                return null;
            }

            return raSubscriber.First();
        }

        public async Task<Response> CreateTempSession(string clientId, string email)
        {
            var clientInDb = new Client();
            var errorMessage = string.Empty;
            UserLookupItem userInfo = new UserLookupItem();
            try
            {
                clientInDb = await _unitOfWork.Client.GetClientByClientIdAsync(
                    clientId);
                if (null == clientInDb)
                {
                    _logger.LogError(OIDCConstants.ClientNotFound.En);
                    return new Response()
                    {
                        Success = false,
                        Message = _messageLocalizer.GetMessage(OIDCConstants.ClientNotFound)
                    };
                }
            }
            catch (Exception)
            {
                errorMessage = _helper.GetErrorMsg(ErrorCodes.DB_ERROR);
                return new Response()
                {
                    Success = false,
                    Message = _messageLocalizer.
                    GetMessage(OIDCConstants.ClientNotFound)
                };
            }
            SubscriberView raSubscriber = new SubscriberView();

            try
            {
                raSubscriber = await _unitOfWork.Subscriber.
                            GetSubscriberInfoByEmail(email);
                if (null == raSubscriber)
                {
                    _logger.LogError(Constants.SubscriberNotFound.En);
                    return new Response()
                    {
                        Success = false,
                        Message = _messageLocalizer.GetMessage(Constants.SubscriberNotFound)
                    };
                }
                userInfo.Suid = raSubscriber.SubscriberUid;
                userInfo.DisplayName = raSubscriber.DisplayName;
                userInfo.Status = raSubscriber.SubscriberStatus;
                userInfo.DeviceToken = raSubscriber.FcmToken;
                userInfo.MobileNumber = raSubscriber.MobileNumber;
                userInfo.EmailId = raSubscriber.Email;
                userInfo.DocumentId = raSubscriber.IdDocNumber;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                errorMessage = _helper.GetErrorMsg(ErrorCodes.DB_ERROR);
                return new Response()
                {
                    Success= false,
                    Message = errorMessage

                };
            }
            var tempAuthNSessId = string.Empty;

            try
            {
                // Generate sessionid
                tempAuthNSessId = EncryptionLibrary.KeyGenerator.GetUniqueKey();
            }
            catch (Exception error)
            {
                _logger.LogError("GetUniqueKey failed: {0}", error.Message);
                errorMessage = _helper.GetErrorMsg(
                     ErrorCodes.GENERATE_UNIQUE_KEY_FAILED);
                return new Response()
                {
                    Success=false,
                    Message = errorMessage
                };
            }

            // Prepare clientdetails object
            ClientDetails clientdetails = new ClientDetails
            {
                ClientId = clientId,
                Scopes = string.Empty,
                RedirectUrl = string.Empty,
                ResponseType = string.Empty,
                AppName = clientInDb.ApplicationName
            };

            // Prepare temporary session object
            TemporarySession temporarySession = new TemporarySession
            {
                TemporarySessionId = tempAuthNSessId,
                UserId = userInfo.Suid,
                DisplayName = userInfo.DisplayName,
                PrimaryAuthNSchemeList = new List<string> { "WALLET" },
                AuthNSuccessList = new List<string>(),
                Clientdetails = clientdetails,
                IpAddress = "192.1.0.2",
                UserAgentDetails = "Mozilla/5.0 (Windows NT 10.0; Win64; x64) Chrome/129.0.0.0",
                TypeOfDevice = "Desktop",
                MacAddress = DTInternalConstants.NOT_AVAILABLE,
                withPkce = false,
                AdditionalValue = DTInternalConstants.pending,
                AuthNStartTime = DateTime.Now.ToString("s"),
                CoRelationId = Guid.NewGuid().ToString(),
                LoginType = "2",
                DocumentId = userInfo.DocumentId,
                DeviceToken = userInfo.DeviceToken,
                NotificationAuthNDone = false,
                NotificationAdditionalValue = DTInternalConstants.pending
            };

            if (userInfo.LoginProfile != null)
            {
                temporarySession.LoginProfile = userInfo.LoginProfile;
            }
            var task = await _cacheClient.Add(CacheNames.TemporarySession,
                tempAuthNSessId, temporarySession);
            if (DTInternalConstants.S_OK != task.retValue)
            {
                _logger.LogError("_cacheClient.Add failed");
                errorMessage = _helper.GetRedisErrorMsg(task.retValue,
                     ErrorCodes.REDIS_TEMP_SESS_ADD_FAILED);
                return new Response()
                {
                    Success = false,
                    Message = errorMessage
                };
            }
            return new Response()
            {
                Success = true,
                Message = "Temp session created successfully",
                Result = tempAuthNSessId
            };
        }

        // ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
        public async Task<List<LoginProfile>> GetSubscriberOrgnizationByEmail(string email)
        {
            var list = new List<LoginProfile>();
            var orgEmaillist = await _unitOfWork.OrgnizationEmail.
                 GetSubscriberOrgnizationByEmailAsync(email);
            if (orgEmaillist.Count == 0)
            {
                _logger.LogError("Subscriber details not found");
                return list;
            }

            foreach (var orgEmail in orgEmaillist)
            {
                list.Add(new LoginProfile()
                {
                    Email = email,
                    OrgnizationId = orgEmail.OrganizationUid
                });
            }

            return list;
        }

        public async Task<List<LoginProfile>> GetSubscriberOrgnizationBySuid(string suid)
        {
            var list = new List<LoginProfile>();
            var orgEmaillist = await _unitOfWork.OrgnizationEmail.
                 GetSubscriberOrgnizationBySuidAsync(suid);
            if (orgEmaillist.Count == 0)
            {
                _logger.LogError("Subscriber details not found");
                return list;
            }

            foreach (var orgEmail in orgEmaillist)
            {
                list.Add(new LoginProfile()
                {
                    Email = orgEmail.EmployeeEmail,
                    OrgnizationId = orgEmail.OrganizationUid
                });
            }

            return list;
        }

        public async Task<string> GetSubscriberPhoto(string url)
        {
            _logger.LogDebug("-->GetSubscriberPhoto");

            // Local Variable Declaration
            string response = null;
            var errorMessage = string.Empty;

            if (string.IsNullOrEmpty(url))
            {
                _logger.LogError("Invalid Input Parameter");
                return response;
            }

            _logger.LogDebug("Photo Url: {0}", url);
            try
            {
                HttpClient client = _httpClientFactory.CreateClient();

                // Call the webservice with Get method
                var result = await client.GetAsync(url);

                // Check the status code
                if (result.IsSuccessStatusCode)
                {
                    // Read the response
                    byte[] content = await result.Content.ReadAsByteArrayAsync();
                    response = Convert.ToBase64String(content);
                    return response;
                }
                else
                {
                    _logger.LogError("GetSubscriberPhoto failed returned" +
                        ",Status code : {0}", result.StatusCode);
                    return null;
                }
            }
            catch (TimeoutException error)
            {
                _logger.LogError("GetSubscriberPhoto failed due to timeout exception: {0}",
                    error.Message);
                return null;
            }
            catch (Exception error)
            {
                _logger.LogError("GetSubscriberPhoto failed: {0}", error.Message);
                return null;
            }

        }

        public async Task<Response> GetVerifierUrl()
        {
            var _client = new HttpClient();

            var url = _configuration["VerifierUrl"];
            var SelectedClaims = new SelectedClaims()
            {
                Document = new List<string> { "idDocNumber", "email" }
            };
            var verifierRequestDTO = new VerifierRequestDTO()
            {
                Type = "UAEIDCredential",
                SelectedClaims = SelectedClaims,
                scope = "UAEIDCredential",
                clientID = ""
            };

            var content = new StringContent(JsonConvert.SerializeObject(verifierRequestDTO), Encoding.UTF8, "application/json");

            var response = await _client.PostAsync(url, content);

            if (response.StatusCode == HttpStatusCode.OK)
            {
                APIResponse apiResponse = JsonConvert.DeserializeObject<APIResponse>(await response.Content.ReadAsStringAsync());
                return new Response()
                {
                    Success = apiResponse.Success,
                    Message = apiResponse.Message,
                    Result = apiResponse.Result != null ? apiResponse.Result.ToString() : null
                };
            }
            else
            {
                _logger.LogError($"Request to {url} failed with status code {response.StatusCode}");
                return new Response()
                {
                    Success = false,
                    Message = _messageLocalizer.GetMessage(Constants.InternalError),
                    Result = null
                };
            }
        }

        public async Task<APIResponse> VerifyQr(string code)
        {
            try
            {
                var client = new HttpClient();

                var url = _configuration["VerificationUrl"];

                url += code;

                HttpResponseMessage response = await client.GetAsync(url);

                if (response.StatusCode == HttpStatusCode.OK)
                {
                    APIResponse apiResponse = JsonConvert.DeserializeObject<APIResponse>(await response.Content.ReadAsStringAsync());

                    return apiResponse;
                }
                else
                {
                    _logger.LogError($"The request with URI={response.RequestMessage.RequestUri} failed " +
                               $"with status code={response.StatusCode}");
                    return null;
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return null;
            }
        }

        private async Task<Response> VerifyFace(string authData, string id)
        {
            Response response1 = new Response();
            try
            {
                var raUser = await _unitOfWork.Subscriber.GetSubscriberInfoBySUID(id);
                if (raUser == null)
                {
                    response1.Success = false;
                    response1.Message = _messageLocalizer.GetMessage(Constants.SubscriberNotFound);
                    return response1;
                }

                var SubcriberFace = await GetSubscriberPhoto(raUser.SelfieUri);

                if (string.IsNullOrEmpty(SubcriberFace))
                {
                    response1.Success = false;
                    response1.Message = _messageLocalizer.GetMessage(Constants.SubscriberFaceNotFound);
                    return response1;
                }

                HttpClient client = new HttpClient();

                var faceDTO = new FaceAuthenticationDTO()
                {
                    image1 = authData,
                    image2 = SubcriberFace
                };

                var url = _configuration["FaceVerifyUrl"];
                string json = JsonConvert.SerializeObject(faceDTO);

                StringContent content = new StringContent(json, Encoding.UTF8, "application/json");

                var response = client.PostAsync(url, content).Result;

                if (response.StatusCode == HttpStatusCode.OK)
                {
                    APIResponse apiResponse = JsonConvert.DeserializeObject<APIResponse>(await response.Content.ReadAsStringAsync());
                    if (apiResponse.Success)
                    {
                        response1.Success = true;
                        response1.Message = apiResponse.Message;
                    }
                    else
                    {
                        response1.Success = false;
                        response1.Message = _messageLocalizer.
                            GetMessage(Constants.FaceVerifyFailed);
                        _logger.LogError(apiResponse.Message);
                    }
                }
                else
                {

                    response1.Success = false;
                    response1.Message = _messageLocalizer.GetMessage(Constants.InternalError);
                    _logger.LogError($"Request to {url} failed with status code {response.StatusCode}");
                    _logger.LogError(response.StatusCode.ToString());
                }
                return response1;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.ToString());
                _logger.LogError(ex.Message);
                response1.Success = false;
                response1.Message = _messageLocalizer.GetMessage(Constants.InternalError);
                return response1;
            }
        }

        public async Task<List<LoginProfile>> GetSubscriberOrgnizationByPhoneNumber(string phoneno)
        {
            var list = new List<LoginProfile>();
            var orgEmaillist = await _unitOfWork.OrgnizationEmail.GetSubscriberOrgnizationByPhoneNumberAsync(phoneno);
            if (orgEmaillist.Count == 0)
            {
                _logger.LogError("Subscriber details not found");
                return list;
            }

            foreach (var orgEmail in orgEmaillist)
            {
                list.Add(new LoginProfile()
                {
                    Email = orgEmail.EmployeeEmail,
                    OrgnizationId = orgEmail.OrganizationUid
                });
            }

            return list;
        }

        public async Task<VerifyUserResponse> VerifyUser(SDKVerifyUserRequest request)
        {
            _logger.LogDebug("--->VerifyUser");

            if (null == request || string.IsNullOrEmpty(request.clientId) ||
                string.IsNullOrEmpty(request.userInput))
            {
                _logger.LogError(Constants.InvalidArguments.En);
                return new VerifyUserResponse(_messageLocalizer.GetMessage(Constants.InvalidArguments));
            }

            _logger.LogDebug("client id is {0}", request.clientId?.SanitizeForLogging());
            var errorMessage = string.Empty;
            var verifierUrl = string.Empty;
            var clientInDb = new Client();
            bool isMobileUser = false;
            try
            {
                clientInDb = await _unitOfWork.Client.GetClientByClientIdAsync(
                    request.clientId);
                if (null == clientInDb)
                {
                    _logger.LogError(OIDCConstants.ClientNotFound.En);
                    return new VerifyUserResponse(_messageLocalizer.GetMessage(OIDCConstants.ClientNotFound));
                }
            }
            catch (Exception)
            {
                errorMessage = _helper.GetErrorMsg(ErrorCodes.DB_ERROR);
                Monitor.SendMessage(errorMessage);
                return new VerifyUserResponse(errorMessage);
            }

            // Check client status
            if (clientInDb.Status != StatusConstants.ACTIVE)
            {
                _logger.LogError("{0}: {1}",
                    OIDCConstants.ClientNotActive, clientInDb.Status);
                return new VerifyUserResponse(_messageLocalizer.
                    GetMessage(OIDCConstants.ClientNotActive));
            }

            UserLookupItem userInfo = new UserLookupItem();
            Domain.Models.UserTable user = new Domain.Models.UserTable();
            var authScheme = new List<string>() { };
            authScheme.Add("FACE");
            SubscriberView raSubscriber = new SubscriberView();

            // Get Subscriber information from Registration Authority
            try
            {
                raSubscriber = await GetSubscriberbyType(request.type,
                    request.userInput);
                if (null == raSubscriber)
                {
                    if (request.type != (int)InputType.emailId)
                    {
                        _logger.LogError(Constants.SubscriberNotFound.En);
                        return new VerifyUserResponse(
                            _messageLocalizer.GetMessage(Constants.SubscriberNotFound));
                    }

                    raSubscriber = await GetSubscriberbyOrgnizationEmail(request.userInput);
                    if (null == raSubscriber)
                    {
                        _logger.LogError(Constants.SubscriberNotFound.En);
                        return new VerifyUserResponse(
                            _messageLocalizer.GetMessage(Constants.SubscriberNotFound));

                    }
                }
                _logger.LogInformation("IsMobileUser" + isMobileUser.ToString());
                userInfo.Suid = raSubscriber.SubscriberUid;
                userInfo.DisplayName = raSubscriber.DisplayName;
                userInfo.Status = raSubscriber.SubscriberStatus;
                userInfo.DeviceToken = raSubscriber.FcmToken;
                userInfo.MobileNumber = raSubscriber.MobileNumber;
                userInfo.EmailId = raSubscriber.Email;
                userInfo.DocumentId = raSubscriber.IdDocNumber;
                if (request.type == (int)InputType.emailId)
                {
                    var orgnizationlist = await GetSubscriberOrgnizationByEmail(request.userInput);
                    if (orgnizationlist.Count != 0)
                    {
                        userInfo.LoginProfile = orgnizationlist;
                    }
                    else
                    {
                        userInfo.LoginProfile = null;
                    }
                }
                else if (request.type == (int)InputType.phoneno)
                {
                    var orgnizationlist = await GetSubscriberOrgnizationBySuid(raSubscriber.SubscriberUid);
                    if (orgnizationlist.Count != 0)
                    {
                        userInfo.LoginProfile = orgnizationlist;
                    }
                    else
                    {
                        userInfo.LoginProfile = null;
                    }
                }
                else
                {
                    userInfo.LoginProfile = null;
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                errorMessage = _helper.GetErrorMsg(ErrorCodes.DB_ERROR);
                Monitor.SendMessage(errorMessage);
                return new VerifyUserResponse(errorMessage);
            }

            // Check Admin/Subscriber status
            if ((StatusConstants.ACTIVE != userInfo.Status) &&
                (StatusConstants.NEW != userInfo.Status) &&
                ("CHANGE_PASSWORD" != userInfo.Status) &&
                ("SET_FIDO2" != userInfo.Status))
            {
                if (userInfo.Status == StatusConstants.SUSPENDED)
                {
                    var id = string.Empty;
                    if (request.clientId.Equals(DTInternalConstants.DTPortalClientId))
                    {
                        id = userInfo.Id.ToString();
                    }
                    else
                    {
                        id = userInfo.Suid;
                    }

                    UserLoginDetail userLoginDetails = null;
                    try
                    {
                        // Get userlogin details
                        userLoginDetails = await _unitOfWork.UserLoginDetail.
                        GetUserLoginDetailAsync(id);
                    }
                    catch (Exception)
                    {
                        errorMessage = _helper.GetErrorMsg(ErrorCodes.DB_ERROR);
                        Monitor.SendMessage(errorMessage);
                        return new VerifyUserResponse(errorMessage);
                    }

                    if (null != userLoginDetails)
                    {
                        // Compare time difference
                        TimeSpan ts = (TimeSpan)(DateTime.Now - userLoginDetails.
                            BadLoginTime);
                        if (ts.TotalMinutes < (ssoConfig.sso_config.
                            account_lock_time * 60))
                        {
                            _logger.LogError(Constants.SubAccountSuspended.En);
                            return new VerifyUserResponse(
                                _messageLocalizer.GetMessage(Constants.SubAccountSuspended));
                        }

                        // UPDATE STATUS TO ACTIVE
                        if (request.clientId ==
                            DTInternalConstants.DTPortalClientId)
                        {
                            if (!string.IsNullOrEmpty(user.OldStatus))
                            {
                                user.Status = user.OldStatus;
                            }
                            else
                            {
                                user.Status = StatusConstants.ACTIVE;
                            }

                            try
                            {
                                _unitOfWork.Users.Update(user);
                                await _unitOfWork.SaveAsync();
                            }
                            catch
                            {
                                _logger.LogError("Subscriber status update failed");
                                errorMessage = _helper.GetErrorMsg(
                                    ErrorCodes.DB_USER_UPDATE_STATUS_FAILED);
                                Monitor.SendMessage(errorMessage);
                                return new VerifyUserResponse(errorMessage);
                            }
                        }
                        else
                        {
                            var statusUpdateRequest = new SubscriberStatusUpdateRequest();
                            statusUpdateRequest.description =
                                LogClientServices.SubscriberStatusUpdate;
                            statusUpdateRequest.subscriberStatus =
                                StatusConstants.ACTIVE;
                            statusUpdateRequest.subscriberUniqueId = userInfo.Suid;
                            var statusResponse = await _raServiceClient.
                                SubscriberStatusUpdate(statusUpdateRequest);
                            if (null == statusResponse)
                            {
                                _logger.LogError("SubscriberStatusUpdate failed");
                                errorMessage = _helper.GetErrorMsg(
                                    ErrorCodes.RA_USER_STATUS_UPDATE_FAILED);
                                return new VerifyUserResponse(errorMessage);
                            }
                            if (false == statusResponse.success)
                            {
                                _logger.LogError("SubscriberStatusUpdate failed, " +
                                    "{0}", statusResponse.message);
                                return new VerifyUserResponse(statusResponse.message);
                            }

                            var tempSession = new TemporarySession();
                            var client = new ClientDetails();
                            client.ClientId = clientInDb.ClientId;

                            tempSession.CoRelationId = Guid.NewGuid().ToString();
                            tempSession.AuthNStartTime = DateTime.Now.ToString();
                            tempSession.Clientdetails = client;

                        }
                        userLoginDetails.DeniedCount = 0;
                        userLoginDetails.WrongCodeCount = 0;
                        userLoginDetails.WrongPinCount = 0;

                        try
                        {
                            _unitOfWork.UserLoginDetail.Update(userLoginDetails);
                            await _unitOfWork.SaveAsync();
                        }
                        catch (Exception error)
                        {
                            _logger.LogError("GetUserPasswordDetailAsync update" +
                                "failed : {0}", error.Message);
                            errorMessage = _helper.GetErrorMsg(
                                ErrorCodes.DB_USER_UPDATE_LOGIN_DETAILS_FAILED);
                            Monitor.SendMessage(errorMessage);
                            return new VerifyUserResponse(errorMessage);
                        }
                    }
                    else
                    {
                        _logger.LogError("UserLoginDetail failed: not found");
                        errorMessage = _helper.GetErrorMsg(
                            ErrorCodes.DB_USER_LOGIN_DETAILS_NOT_FOUND);
                        return new VerifyUserResponse(errorMessage);
                    }
                }
                else
                {
                    _logger.LogError("_unitOfWork.UserTable.GetUserStatus: {0}",
                        userInfo.Status);
                    return new VerifyUserResponse(String.Format(
                        _messageLocalizer.GetMessage(Constants.SubNotActive)));
                }
            }

            var tempAuthNSessId = string.Empty;

            try
            {
                // Generate sessionid
                tempAuthNSessId = EncryptionLibrary.KeyGenerator.GetUniqueKey();
            }
            catch (Exception error)
            {
                _logger.LogError("GetUniqueKey failed: {0}", error.Message);
                errorMessage = _helper.GetErrorMsg(
                     ErrorCodes.GENERATE_UNIQUE_KEY_FAILED);
                Monitor.SendMessage(errorMessage);
                return new VerifyUserResponse(errorMessage);
            }

            // Prepare clientdetails object
            ClientDetails clientdetails = new ClientDetails
            {
                ClientId = request.clientId,
                Scopes = string.Empty,
                RedirectUrl = string.Empty,
                ResponseType = string.Empty,
                AppName = clientInDb.ApplicationName
            };

            // Prepare temporary session object
            TemporarySession temporarySession = new TemporarySession
            {
                TemporarySessionId = tempAuthNSessId,
                UserId = userInfo.Suid,
                DisplayName = userInfo.DisplayName,
                PrimaryAuthNSchemeList = new List<string> { "FACE"},
                AuthNSuccessList = new List<string>(),
                Clientdetails = clientdetails,
                IpAddress = request.ip,
                UserAgentDetails = request.userAgent,
                TypeOfDevice = request.typeOfDevice,
                MacAddress = DTInternalConstants.NOT_AVAILABLE,
                withPkce = false,
                AdditionalValue = DTInternalConstants.pending,
                AuthNStartTime = DateTime.Now.ToString("s"),
                CoRelationId = Guid.NewGuid().ToString(),
                LoginType = request.type.ToString(),
                DocumentId = userInfo.DocumentId,
                DeviceToken = userInfo.DeviceToken,
                NotificationAuthNDone = false,
                NotificationAdditionalValue = DTInternalConstants.pending
            };

            if (userInfo.LoginProfile != null)
            {
                temporarySession.LoginProfile = userInfo.LoginProfile;
            }

            var randomcodes = string.Empty;

            if (authScheme.Contains(AuthNSchemeConstants.WALLET))
            {
                var verifierRequest = await GetVerifierUrl();
                if (verifierRequest == null || !verifierRequest.Success)
                {
                    return new VerifyUserResponse(verifierRequest.Message);
                }
                verifierUrl = verifierRequest.Result.ToString();
            }


            // Create temporary session
            var task = await _cacheClient.Add(CacheNames.TemporarySession,
                tempAuthNSessId, temporarySession);
            if (DTInternalConstants.S_OK != task.retValue)
            {
                _logger.LogError("_cacheClient.Add failed");
                errorMessage = _helper.GetRedisErrorMsg(task.retValue,
                     ErrorCodes.REDIS_TEMP_SESS_ADD_FAILED);
                return new VerifyUserResponse(errorMessage);
            }


            // return success object to browser
            verifyUserResult response = new verifyUserResult
            {
                AuthnToken = tempAuthNSessId,
                AuthenticationSchemes = authScheme,
                userName = userInfo.DisplayName
            };
            _logger.LogInformation("VerifyUser response: {0}",
                JsonConvert.SerializeObject(response));
            _logger.LogDebug("<--VerifyUser");
            return new VerifyUserResponse(response);
        }

        public async Task<VerifyUserAuthDataResponse> VerifyUserAuthData(
            VerifyUserAuthDataRequest request)
        {
            _logger.LogDebug("-->VerifyUserAuthData");
            // Validate input
            if (
                (null == request) ||
                (string.IsNullOrEmpty(request.AuthnToken)) ||
                (string.IsNullOrEmpty(request.authenticationScheme)) ||
                (string.IsNullOrEmpty(request.authenticationData))
                )
            {
                _logger.LogError(Constants.InvalidArguments.En);
                VerifyUserAuthDataResponse response = new VerifyUserAuthDataResponse();
                response.Success = false;
                response.Message = _messageLocalizer.GetMessage(Constants.InvalidArguments);
                return response;
            }

            var errorMessage = string.Empty;
            var randomcodes = string.Empty;
            var randomcode = string.Empty;
            var verifierUrl = string.Empty;
            TemporarySession tempSession = null;

            _logger.LogInformation("AuthnToken is {0}",
              request.AuthnToken?.SanitizeForLogging());

            try
            {
                // Get the temporary session object
                tempSession = await _cacheClient.Get<TemporarySession>
                    (CacheNames.TemporarySession,
                    request.AuthnToken);
                if (null == tempSession)
                {
                    _logger.LogError(Constants.TempSessionExpired.En);
                    VerifyUserAuthDataResponse response = new VerifyUserAuthDataResponse();
                    response.Success = false;
                    response.Message = _messageLocalizer.
                        GetMessage(Constants.TempSessionExpired);
                    return response;
                }
            }
            catch (CacheException ex)
            {
                _logger.LogError("Failed to get Temporary Session Record");
                VerifyUserAuthDataResponse response = new VerifyUserAuthDataResponse();
                response.Success = false;
                errorMessage = _helper.GetRedisErrorMsg(
                    ex.ErrorCode, ErrorCodes.REDIS_TEMP_SESS_GET_FAILED);
                response.Message = errorMessage;
                return response;
            }
            _logger.LogInformation("Get Temporary Session Success");
            var isAuthSchm = false;
            isAuthSchm = false;

            // If already, there any authentication success count
            bool check = !tempSession.AuthNSuccessList.Any();
            if (check)
            {
                if (tempSession.AuthNSuccessList.Contains(
                    request.authenticationScheme))
                {
                    isAuthSchm = true;
                }
            }
            if (true == isAuthSchm)
            {
                _logger.LogError(Constants.AuthSchemeAlreadyAuthenticated.En);
                VerifyUserAuthDataResponse response = new VerifyUserAuthDataResponse();
                response.Success = false;
                response.Message = _messageLocalizer.
                    GetMessage(Constants.AuthSchemeAlreadyAuthenticated);
                return response;
            }

            _logger.LogDebug("TempSession ClientID is {0}",
                tempSession.Clientdetails.ClientId);

            var userId = new UserTable();
            UserLookupItem userInfo = new UserLookupItem();

            // Check client is Internal/External
            if (tempSession.Clientdetails.ClientId ==
                DTInternalConstants.DTPortalClientId)
            {
                _logger.LogDebug("Client is DT ADMIN PORTAL");

                // Get UserId
                userId = await _unitOfWork.Users.GetUserbyUuidAsync
                    (tempSession.UserId);
                if (null == userId)
                {
                    _logger.LogError("GetUserIdByName failed, not found");
                    VerifyUserAuthDataResponse response = new VerifyUserAuthDataResponse();
                    response.Success = false;
                    errorMessage = _helper.GetErrorMsg(
                        ErrorCodes.DB_USER_GET_DETAILS_FAILED);
                    response.Message = errorMessage;
                    return response;
                }

                userInfo.Id = userId.Id;
                userInfo.Suid = userId.Uuid;
                userInfo.DisplayName = userId.FullName;
                userInfo.MobileNumber = userId.MobileNo;
                userInfo.EmailId = userId.MailId;
                userInfo.Status = userId.Status;
                if (userInfo.Status.Equals(StatusConstants.SUSPENDED))
                {
                    _logger.LogError("User account is SUSPENDED");
                    VerifyUserAuthDataResponse response = new VerifyUserAuthDataResponse();
                    response.Success = false;
                    response.Message = _messageLocalizer.
                        GetMessage(Constants.SubAccountSuspended);
                    return response;
                }
            }
            else
            {
                _logger.LogDebug("Client is EXTERNAL CLIENT");
                userInfo.DisplayName = tempSession.DisplayName;
                userInfo.Suid = tempSession.UserId;
                userInfo.DocumentId = tempSession.DocumentId;
                userInfo.DeviceToken = tempSession.DeviceToken;
            }

            var wrongPin = false;
            var wrongCode = false;
            var denyCount = false;
            var wrongFace = false;
            var isAuthNPassed = false;

            if (request.authenticationScheme.Equals(AuthNSchemeConstants.FACE))
            {
                var faceRes = await VerifyFace(request.authenticationData, userInfo.Suid);
                if (faceRes.Success == false)
                {
                    if (faceRes.Message != Constants.FaceVerifyFailed.En)
                    {
                        var response = new VerifyUserAuthDataResponse();
                        response.Success = false;
                        response.Message = faceRes.Message;
                        return response;
                    }
                    else
                    {
                        isAuthNPassed = false;
                        wrongFace = true;
                    }
                }
                else
                {
                    isAuthNPassed = true;
                }

            }


            else if (request.authenticationScheme ==
                AuthNSchemeConstants.WALLET)
            {
                isAuthNPassed = true;
            }

            else if (request.authenticationScheme == AuthNSchemeConstants.TOTP)
            {
                // Get UserAuthData
                var userAuthData = await _unitOfWork.UserAuthData.GetUserAuthDataAsync
                    (userInfo.Suid, AuthNSchemeConstants.MOBILE_TOTP);
                if (null == userAuthData)
                {
                    VerifyUserAuthDataResponse VerifyUserAuthDataResponse = new VerifyUserAuthDataResponse();
                    VerifyUserAuthDataResponse.Success = false;
                    VerifyUserAuthDataResponse.Message = "User authentication data not found";
                    return VerifyUserAuthDataResponse;
                }

                // Verify TOTP
                var isSuccess = TOTPLibrary.VerifyTOTP(userAuthData.AuthData,
                    request.authenticationData);
                if (false == isSuccess)
                {
                    VerifyUserAuthDataResponse VerifyUserAuthDataResponse = new VerifyUserAuthDataResponse();
                    VerifyUserAuthDataResponse.Success = false;
                    VerifyUserAuthDataResponse.Message = "Wrong credentials";
                    isAuthNPassed = false;
                    wrongPin = true;
                }
                else
                {
                    isAuthNPassed = true;
                }
            }

            else
            {
                _logger.LogError(Constants.AuthSchemeMisMatch.En);
                var response = new VerifyUserAuthDataResponse();
                response.Success = false;
                response.Message = _messageLocalizer.
                    GetMessage(Constants.AuthSchemeMisMatch);
                return response;
            }


            // Failed case
            if ((isAuthNPassed == false) || (denyCount == true) ||
                (wrongCode == true) || (wrongPin == true) || (wrongFace == true))
            {
                // Get userpassword details
                if (userInfo.Id != 0)
                {
                    userInfo.Suid = userInfo.Id.ToString();
                }

                UserLoginDetail userLoginDetails = null;
                try
                {
                    userLoginDetails = await _unitOfWork.UserLoginDetail.
                        GetUserLoginDetailAsync(userInfo.Suid.ToString());
                }
                catch (Exception)
                {
                    var response = new VerifyUserAuthDataResponse();
                    response.Success = false;
                    errorMessage = _helper.GetErrorMsg(ErrorCodes.DB_ERROR);
                    response.Message = errorMessage;
                    return response;
                }

                if (null == userLoginDetails)
                {
                    _logger.LogDebug("GetUserPasswordDetailAsync failed, not found");
                    var response = new VerifyUserAuthDataResponse();
                    response.Success = false;
                    response.Message = _messageLocalizer.
                        GetMessage(Constants.InternalError);

                    userLoginDetails = new UserLoginDetail();

                    // Prepare user login details object
                    userLoginDetails.UserId = userInfo.Suid.ToString();
                    userLoginDetails.BadLoginTime = DateTime.Now;
                    userLoginDetails.IsReversibleEncryption = false;
                    userLoginDetails.IsScrambled = false;
                    userLoginDetails.PriAuthSchId = 0;
                    if (wrongCode == true)
                    {
                        userLoginDetails.WrongCodeCount = 1;
                    }
                    if ((wrongPin == true || wrongFace == true) || (request.authenticationScheme == "PASSWORD"))
                    {
                        userLoginDetails.WrongPinCount = 1;
                    }
                    if (denyCount == true)
                    {
                        userLoginDetails.DeniedCount = 1;
                    }

                    try
                    {
                        await _unitOfWork.UserLoginDetail.AddAsync(userLoginDetails);
                        await _unitOfWork.SaveAsync();
                    }
                    catch (Exception error)
                    {
                        _logger.LogError("UserLoginDetail.AddAsync failed: {0}",
                            error.Message);
                        response.Success = false;
                        errorMessage = _helper.GetErrorMsg(
                            ErrorCodes.DB_USER_ADD_LOGIN_DETAILS_FAILED);
                        response.Message = errorMessage;
                        return response;
                    }
                }
                else
                {
                    // Prepare user password object
                    userLoginDetails.UserId = userInfo.Suid.ToString();

                    userLoginDetails.BadLoginTime = DateTime.Now;
                    if (wrongCode == true)
                    {
                        userLoginDetails.WrongCodeCount = userLoginDetails.
                            WrongCodeCount + 1;
                    }
                    if ((wrongPin == true || wrongFace == true) || (request.authenticationScheme == "PASSWORD"))
                    {
                        userLoginDetails.WrongPinCount = userLoginDetails.
                            WrongPinCount + 1;
                    }
                    if (denyCount == true)
                    {
                        userLoginDetails.DeniedCount = userLoginDetails.
                            DeniedCount + 1;
                    }

                    // Update Login details
                    try
                    {
                        _unitOfWork.UserLoginDetail.Update(userLoginDetails);
                        await _unitOfWork.SaveAsync();
                    }
                    catch (Exception error)
                    {
                        _logger.LogError("UserLoginDetail Update failed: {0}",
                            error.Message);
                        var response = new VerifyUserAuthDataResponse();
                        response.Success = false;
                        errorMessage = _helper.GetErrorMsg(
                            ErrorCodes.DB_USER_UPDATE_LOGIN_DETAILS_FAILED);
                        response.Message = errorMessage;
                        return response;
                    }
                }

                if ((userLoginDetails.WrongPinCount >=
                    ssoConfig.sso_config.wrong_pin)
                    || (userLoginDetails.WrongCodeCount >=
                    ssoConfig.sso_config.wrong_code)
                    || (userLoginDetails.DeniedCount >=
                    ssoConfig.sso_config.deny_count))
                {
                    // If client is DTADMIN Portal, Change the user status in
                    // DataBase
                    if (tempSession.Clientdetails.ClientId ==
                        DTInternalConstants.DTPortalClientId)
                    {
                        var user = await _unitOfWork.Users.GetByIdAsync(userInfo.Id);
                        if (null == user)
                        {
                            _logger.LogError("Users GetByIdAsync failed, " +
                                "not found {0}",
                                userInfo.Id);
                            var response = new VerifyUserAuthDataResponse();
                            response.Success = false;
                            response.Message = _messageLocalizer.
                                GetMessage(Constants.InternalError);
                            return response;
                        }

                        user.OldStatus = userInfo.Status;
                        user.Status = StatusConstants.SUSPENDED;
                        _logger.LogInformation("User status is updated to" +
                            " SUSPENDED");

                        try
                        {
                            _unitOfWork.Users.Update(user);
                            await _unitOfWork.SaveAsync();
                        }
                        catch (Exception error)
                        {
                            _logger.LogError("user status update failed: {0}",
                                error.Message);
                            var response = new VerifyUserAuthDataResponse();
                            response.Success = false;
                            response.Message = _messageLocalizer.
                                GetMessage(Constants.InternalError);
                            return response;
                        }
                    }
                    else
                    {
                        var statusUpdateRequest =
                            new SubscriberStatusUpdateRequest();

                        statusUpdateRequest.description =
                            LogClientServices.SubscriberStatusUpdate;
                        statusUpdateRequest.subscriberStatus =
                            StatusConstants.SUSPENDED;
                        statusUpdateRequest.subscriberUniqueId = userInfo.Suid;

                        var statusResponse = await _raServiceClient.
                            SubscriberStatusUpdate(statusUpdateRequest);
                        if (null == statusResponse)
                        {
                            var response = new VerifyUserAuthDataResponse();
                            response.Success = false;
                            errorMessage = _helper.GetErrorMsg(
                                ErrorCodes.RA_USER_STATUS_UPDATE_FAILED);
                            response.Message = errorMessage;
                            return response;
                        }
                        if (false == statusResponse.success)
                        {
                            _logger.LogError("SubscriberStatusUpdate failed, " +
                                "{0}", statusResponse.message);
                            var response = new VerifyUserAuthDataResponse();
                            response.Success = false;
                            response.Message = statusResponse.message;
                            return response;
                        }
                    }

                }
            }
            if (false == isAuthNPassed)
            {
                var response = new VerifyUserAuthDataResponse();
                _logger.LogError(Constants.AuthNFailed.En);
                tempSession.AllAuthNDone = false;
                tempSession.AdditionalValue = DTInternalConstants.E_False;

                if (request.authenticationScheme.Equals(AuthNSchemeConstants.PASSWORD))
                {
                    _logger.LogInformation(Constants.WrongCredentials.En);
                    response.Message = _messageLocalizer.
                        GetMessage(Constants.WrongCredentials);
                }

                if (request.authenticationScheme.Equals(AuthNSchemeConstants.WALLET))
                {
                    _logger.LogInformation(Constants.WrongCredentials.En);
                    response.Message = _messageLocalizer.
                        GetMessage(Constants.WrongCredentials);
                }

                if (denyCount == true)
                {
                    _logger.LogInformation(Constants.UserDeniedAuthN.En);
                    tempSession.AllAuthNDone = false;
                    tempSession.AdditionalValue =
                        Constants.UserDeniedAuthN.En;
                    response.Message = _messageLocalizer.
                        GetMessage(Constants.UserDeniedAuthN);
                }
                else if (wrongCode == true)
                {
                    _logger.LogInformation(Constants.WrongCode.En);
                    tempSession.AllAuthNDone = false;
                    tempSession.AdditionalValue = Constants.WrongCode.En;
                    response.Message = _messageLocalizer.
                        GetMessage(Constants.WrongCode);
                }
                else if (wrongPin == true)
                {
                    _logger.LogInformation(Constants.WrongPin.En);
                    tempSession.AllAuthNDone = false;
                    tempSession.AdditionalValue = Constants.WrongPin.En;
                    response.Message = _messageLocalizer.
                        GetMessage(Constants.WrongPin);
                }
                else if (wrongFace == true)
                {
                    _logger.LogInformation(Constants.WrongFace.En);
                    tempSession.AllAuthNDone = false;
                    tempSession.AdditionalValue = Constants.WrongFace.En;
                    response.Message = _messageLocalizer.
                        GetMessage(Constants.WrongFace);
                }

                if (request.authenticationScheme.Equals(AuthNSchemeConstants.TOTP))
                {
                    _logger.LogInformation(Constants.WrongCredentials.En);
                    response.Message = _messageLocalizer.
                        GetMessage(Constants.WrongCredentials);
                }
                // Return failed response
                response.Success = false;

                // Modify temporary session
                var (retValue1, errorMsg1) = await _cacheClient.Add
                    (CacheNames.TemporarySession, tempSession.TemporarySessionId,
                    tempSession);
                if (0 != retValue1)
                {
                    _logger.LogError("TemporarySession add failed");
                    var cacheResponse = new VerifyUserAuthDataResponse();
                    cacheResponse.Success = false;
                    errorMessage = _helper.GetRedisErrorMsg(retValue1,
                        ErrorCodes.REDIS_TEMP_SESS_ADD_FAILED);
                    cacheResponse.Message = errorMessage;
                    return cacheResponse;
                }
                return response;
            }
            if (true == isAuthNPassed)
            {
                tempSession.AllAuthNDone = true;
                tempSession.AdditionalValue = DTInternalConstants.S_True;

                if (tempSession.Clientdetails.ClientId ==
                        DTInternalConstants.DTPortalClientId)
                {
                    userInfo.Suid = userInfo.Id.ToString();
                }

                UserLoginDetail userLoginDetail = null;
                try
                {
                    userLoginDetail = await _unitOfWork.UserLoginDetail.
                    GetUserLoginDetailAsync(userInfo.Suid.ToString());
                }
                catch (Exception)
                {
                    VerifyUserAuthDataResponse response = new VerifyUserAuthDataResponse();
                    response.Success = false;
                    errorMessage = _helper.GetErrorMsg(ErrorCodes.DB_ERROR);
                    response.Message = errorMessage;
                    return response;
                }

                if (null != userLoginDetail)
                {
                    if ((userLoginDetail.WrongCodeCount > 0)
                        || (userLoginDetail.WrongPinCount > 0)
                        || (userLoginDetail.DeniedCount > 0))
                    {
                        userLoginDetail.WrongPinCount = 0;
                        userLoginDetail.WrongCodeCount = 0;
                        userLoginDetail.DeniedCount = 0;

                        try
                        {
                            _unitOfWork.UserLoginDetail.Update(userLoginDetail);
                            await _unitOfWork.SaveAsync();
                        }
                        catch
                        {
                            _logger.LogError("UserLoginDetail update failed");
                            VerifyUserAuthDataResponse response = new VerifyUserAuthDataResponse();
                            response.Success = false;
                            errorMessage = _helper.GetErrorMsg(
                            ErrorCodes.DB_USER_UPDATE_LOGIN_DETAILS_FAILED);
                            response.Message = errorMessage;
                            return response;
                        }
                    }
                }
                if (tempSession.Clientdetails.ClientId ==
                    DTInternalConstants.DTPortalClientId)
                {
                    if (userId.LastLoginTime == null)
                    {
                        userId.LastLoginTime = DateTime.Now;
                    }
                    else
                    {
                        userId.LastLoginTime = userId.CurrentLoginTime;
                    }
                    userId.CurrentLoginTime = DateTime.Now;

                    try
                    {
                        _unitOfWork.Users.Update(userId);
                        await _unitOfWork.SaveAsync();
                    }
                    catch (Exception error)
                    {
                        _logger.LogError("UserLoginDetail update failed : {0}",
                             error.Message);
                        VerifyUserAuthDataResponse response = new VerifyUserAuthDataResponse();
                        response.Success = false;
                        errorMessage = _helper.GetErrorMsg(
                            ErrorCodes.DB_USER_UPDATE_LOGIN_DETAILS_FAILED);
                        response.Message = errorMessage;
                        return response;
                    }
                }
            }
            if (request.authenticationScheme == AuthNSchemeConstants.FACE
                || request.authenticationScheme == AuthNSchemeConstants.PIN)
            {
                tempSession.AuthNSuccessList.Add(AuthNSchemeConstants.PUSH_NOTIFICATION);
            }
            else
            {
                tempSession.AuthNSuccessList.Add(request.authenticationScheme);
            }

            if (tempSession.AuthNSuccessList.Count != tempSession.PrimaryAuthNSchemeList.Count)
            {
            }
            // Modify temporary session
            var (retValue, errorMsg) = await _cacheClient.Add(
                CacheNames.TemporarySession,
                tempSession.TemporarySessionId,
                tempSession);
            if (0 != retValue)
            {
                _logger.LogError("TemporarySession add failed");
                var response = new VerifyUserAuthDataResponse();
                response.Success = false;
                errorMessage = _helper.GetRedisErrorMsg(retValue,
                    ErrorCodes.REDIS_TEMP_SESS_ADD_FAILED);
                response.Message = errorMessage;
                return response;
            }
            string GlobalSessionId = string.Empty;
            if (tempSession.AuthNSuccessList.Count ==
            tempSession.PrimaryAuthNSchemeList.Count)
            {
                // globalsession 
                var isExists = await _cacheClient.Exists(CacheNames.UserSessions,
                    tempSession.UserId);
                if (CacheCodes.KeyExist == isExists.retValue)
                {
                    IList<string> userSessions = null;
                    try
                    {
                        userSessions = await _cacheClient.Get<IList<string>>
                            (CacheNames.UserSessions, tempSession.UserId);
                        if (userSessions == null)
                        {
                            _logger.LogError("Failed to get User Sessions");
                            var response = new VerifyUserAuthDataResponse();
                            response.Success = false;
                            response.Message = _helper.GetErrorMsg(
                                ErrorCodes.REDIS_USER_SESS_GET_FAILED);
                            return response;
                        }
                    }
                    catch (CacheException ex)
                    {
                        _logger.LogError("Failed to get User Sessions");
                        var response = new VerifyUserAuthDataResponse();
                        response.Success = false;
                        response.Message = _helper.GetRedisErrorMsg(ex.ErrorCode,
                            ErrorCodes.REDIS_USER_SESS_GET_FAILED);
                        return response;
                    }

                    if (userSessions.Count > 0)
                    {
                        var cacheres = await _cacheClient.Remove(
                            CacheNames.GlobalSession,
                            userSessions.First());
                        if (0 != cacheres.retValue)
                        {
                            _logger.LogError("GlobalSession Remove failed");
                            //var response = new Response();
                            //response.Success = false;
                            //response.Message = Constants.InternalError;
                            //return response;
                        }

                        var res = await _cacheClient.Remove(CacheNames.UserSessions,
                            tempSession.UserId);
                        if (0 != res.retValue)
                        {
                            _logger.LogError("UserSessions Remove failed");
                            //var response = new Response();
                            //response.Success = false;
                            //response.Message = Constants.InternalError;
                            //return response;
                        }
                    }
                }

                GlobalSessionId = EncryptionLibrary.KeyGenerator.GetUniqueKey();

                // Prepare global session object
                GlobalSession globalSession = new GlobalSession
                {
                    GlobalSessionId = GlobalSessionId,
                    UserId = tempSession.UserId,
                    FullName = userInfo.DisplayName,
                    IpAddress = tempSession.IpAddress,
                    MacAddress = tempSession.MacAddress,
                    UserAgentDetails = tempSession.UserAgentDetails,
                    AuthenticationScheme = tempSession.AuthNSuccessList.Last(),
                    LoggedInTime = DateTime.Now.ToString().Replace("/", "-"),
                    LastAccessTime = DateTime.Now.ToString().Replace("/", "-"),
                    TypeOfDevice = tempSession.TypeOfDevice,
                    CoRelationId = tempSession.CoRelationId,
                    ClientId = new List<string>() { },
                    OperationsDetails = new List<OperationsDetails>() { }
                };

                if (tempSession.LoginProfile != null)
                {
                    globalSession.LoginProfile = tempSession.LoginProfile;
                }
                // Add global session in cache
                var cacheAdd = await _cacheClient.Add(CacheNames.GlobalSession,
                    GlobalSessionId, globalSession);
                if (0 != cacheAdd.retValue)
                {
                    _logger.LogError("GlobalSession Add failed");
                    var response = new VerifyUserAuthDataResponse();
                    response.Success = false;
                    errorMessage = _helper.GetRedisErrorMsg(cacheAdd.retValue,
                        ErrorCodes.REDIS_GLOBAL_SESS_ADD_FAILED);
                    response.Message = errorMessage;
                    return response;
                }

                var globalSessionList = new List<string>();
                globalSessionList.Add(GlobalSessionId);

                cacheAdd = await _cacheClient.Add(CacheNames.UserSessions,
                    tempSession.UserId,
                    globalSessionList);
                if (0 != cacheAdd.retValue)
                {
                    _logger.LogError("UserSessions Add failed");
                    var response = new VerifyUserAuthDataResponse();
                    response.Success = false;
                    errorMessage = _helper.GetRedisErrorMsg(cacheAdd.retValue,
                        ErrorCodes.REDIS_USER_SESS_ADD_FAILED);
                    response.Message = errorMessage;
                    return response;
                }


            }

            // Return success response
            if (tempSession.AdditionalValue == DTInternalConstants.S_True)
            {
                var VerifyUserAuthDataResponse = new VerifyUserAuthDataResponse();
                VerifyUserAuthDataResponse.Success = true;
                VerifyUserAuthDataResponse.Message = DTInternalConstants.AuthNDone;
                VerifyUserAuthDataResponse.Result = new verifyUserAuthResult();
                GetAuthZCodeRequest getAuthZCodeRequest = new GetAuthZCodeRequest();
                clientDetails clientDetails = new clientDetails()
                {
                    clientId = tempSession.Clientdetails.ClientId,
                    redirect_uri = "https://localhost:4455",
                    
                    grant_type= tempSession.Clientdetails.GrantType,
                    response_type= tempSession.Clientdetails.ResponseType,
                    scopes= tempSession.Clientdetails.Scopes
                };
                getAuthZCodeRequest.ClientDetails = clientDetails;
                getAuthZCodeRequest.GlobalSessionId = GlobalSessionId;
                var authZCodeResponse = await GetAuthorizationCode(getAuthZCodeRequest);
                if (authZCodeResponse.Success == true)
                {
                    VerifyUserAuthDataResponse.Result.AuthorizationCode = authZCodeResponse.AuthorizationCode;
                }
                else
                {
                    VerifyUserAuthDataResponse.Success = false;
                    VerifyUserAuthDataResponse.Message = authZCodeResponse.Message;
                    return VerifyUserAuthDataResponse;
                }
                _logger.LogInformation("VerifyUserAuthData response: {0}",
                    JsonConvert.SerializeObject(VerifyUserAuthDataResponse));
                _logger.LogDebug("<--VerifyUserAuthData");
                return VerifyUserAuthDataResponse;
            }
            else
            {
                var response = new VerifyUserAuthDataResponse();
                response.Success = false;
                response.Message = _messageLocalizer.GetMessage(Constants.AuthNFailed);
                _logger.LogInformation(Constants.AuthNFailed.En);
                _logger.LogDebug("VerifyUserAuthData response: {0}",
                    JsonConvert.SerializeObject(response));
                _logger.LogDebug("<--VerifyUserAuthData");
                return response;
            }
        }

        public async Task<GetAuthZCodeResponse> GetAuthorizationCode
            (GetAuthZCodeRequest request)
        {

            _logger.LogDebug("-->GetAuthorizationCode");

            // Validate input
            if (null == request)
            {
                _logger.LogError(Constants.InvalidArguments.En);
                GetAuthZCodeResponse response = new GetAuthZCodeResponse();
                response.Success = false;
                response.Message = _messageLocalizer
                    .GetMessage(Constants.InvalidArguments);

                return response;
            }
            string errorMessage = string.Empty;

            Client clientDetails = null;
            try
            {
                // Get client details
                clientDetails = await _unitOfWork.Client.GetClientByClientIdAsync
                (request.ClientDetails.clientId);
                if (null == clientDetails)
                {
                    _logger.LogError("GetIdByClientId failed, ClientId not found");
                    GetAuthZCodeResponse response = new GetAuthZCodeResponse();
                    response.Success = false;
                    response.Message = _messageLocalizer.GetMessage(OIDCConstants.ClientNotFound);
                    return response;
                }
            }
            catch (Exception)
            {
                var response = new GetAuthZCodeResponse();
                response.Success = false;
                errorMessage = _helper.GetErrorMsg(ErrorCodes.DB_ERROR);
                response.Message = errorMessage;
                return response;
            }

            // Check client state - if not active 
            if (clientDetails.Status != StatusConstants.ACTIVE)
            {
                _logger.LogError(OIDCConstants.ClientNotActive.En);
                GetAuthZCodeResponse response = new GetAuthZCodeResponse();
                response.Success = false;
                response.Message = _messageLocalizer
                    .GetMessage(OIDCConstants.ClientNotActive);
                return response;
            }

            GlobalSession globalSession = null;
            try
            {
                // Get GlobalSession
                globalSession = await _cacheClient.Get<GlobalSession>
                    (CacheNames.GlobalSession,
                    request.GlobalSessionId);
                if (null == globalSession)
                {
                    _logger.LogError(Constants.SessionMismatch.En);
                    GetAuthZCodeResponse response = new GetAuthZCodeResponse();
                    response.Success = false;
                    response.Message = _messageLocalizer.GetMessage(Constants.SessionMismatch);
                    return response;
                }
            }
            catch (CacheException ex)
            {
                _logger.LogError("Failed to get Global Session Record");
                var response = new GetAuthZCodeResponse();
                response.Success = false;
                errorMessage = _helper.GetRedisErrorMsg(
                    ex.ErrorCode, ErrorCodes.REDIS_GLOBAL_SESS_GET_FAILED);
                response.Message = errorMessage;
                return response;
            }
            _logger.LogInformation("GlobalSession found");
            // Compare session timeout
            var sessionTime = DateTime.Now - Convert.ToDateTime(
                globalSession.LastAccessTime);
            if (sessionTime.TotalMinutes >= ssoConfig.sso_config.ideal_timeout)
            {
                // Remove global session
                var CacheRes = await _cacheClient.Remove("GlobalSession",
                     globalSession.GlobalSessionId);
                if (0 != CacheRes.retValue)
                {
                    _logger.LogError("_cacheClient.Remove failed, GlobalSession " +
                            "remove failed");

                    GetAuthZCodeResponse res = new GetAuthZCodeResponse();
                    res.Success = false;
                    res.Message = _messageLocalizer.GetMessage(WebConstants.InternalServerError);
                    return res;
                }

                // globalsession 
                var sesIsExists = await _cacheClient.Exists(CacheNames.UserSessions,
                    globalSession.UserId);
                if (CacheCodes.KeyExist == sesIsExists.retValue)
                {
                    IList<string> userSessions = null;

                    try
                    {
                        // Get usersessions
                        userSessions = await _cacheClient.Get<IList<string>>(
                            CacheNames.UserSessions,
                            globalSession.UserId);
                        if (null == userSessions)
                        {
                            _logger.LogError("Failed to get user sessions");
                            var cacheResponse = new GetAuthZCodeResponse();
                            cacheResponse.Success = false;
                            cacheResponse.Message = _messageLocalizer.
                                GetMessage(Constants.UserSessionNotFound);
                            return cacheResponse;
                        }
                    }
                    catch (CacheException ex)
                    {
                        _logger.LogError("Failed to get user sessions");
                        var cacheResponse = new GetAuthZCodeResponse();
                        cacheResponse.Success = false;
                        cacheResponse.Message = _helper.GetRedisErrorMsg(ex.ErrorCode,
                            ErrorCodes.REDIS_USER_SESS_GET_FAILED);
                        return cacheResponse;
                    }

                    if (userSessions.Count > 0)
                    {
                        var res = await _cacheClient.Remove(CacheNames.UserSessions,
                            globalSession.UserId);
                        if (0 != res.retValue)
                        {
                            _logger.LogError("UserSessions Remove failed");

                            GetAuthZCodeResponse Response = new GetAuthZCodeResponse();
                            Response.Success = false;
                            Response.Message = _messageLocalizer.GetMessage(Constants.InternalError);
                            return Response;
                        }
                    }
                }

                GetAuthZCodeResponse response = new GetAuthZCodeResponse();
                _logger.LogError("GlobalSession expired/not exists");
                response.Success = false;
                response.Message = _messageLocalizer.GetMessage(Constants.GlobalSessionNotFound);

                return response;
            }
            _logger.LogInformation("GlobalSession is valid");
            globalSession.LastAccessTime = DateTime.Now.ToString().Replace("/", "-");

            if (globalSession.ClientId == null)
            {
                globalSession.ClientId = new List<string>()
                {
                    clientDetails.ApplicationName.ToString()
                 };
            }
            else
            {
                if (!globalSession.ClientId.Contains(clientDetails.ApplicationName.
                    ToString()))
                {
                    globalSession.ClientId.Add(clientDetails.ApplicationName.
                        ToString());
                }
            }

            var cacheRes = await _cacheClient.Add(CacheNames.GlobalSession,
                request.GlobalSessionId,
                globalSession);
            if (0 != cacheRes.retValue)
            {
                _logger.LogError("GlobalSession add failed");
                GetAuthZCodeResponse response = new GetAuthZCodeResponse();
                response.Success = false;
                response.Message = _messageLocalizer.GetMessage(Constants.InternalError);
                return response;
            }

            // Generate authorization code
            var AuthZCodeId = EncryptionLibrary.KeyGenerator.GetUniqueKey(92);
            if (null == AuthZCodeId)
            {
                _logger.LogError("GetUniqueKey failed");
                GetAuthZCodeResponse response = new GetAuthZCodeResponse();
                response.Success = false;
                response.Message = _messageLocalizer.GetMessage(Constants.InternalError);
                return response;
            }
            _logger.LogInformation("Generated AuthZCodeId: {0}", AuthZCodeId);
            // Prepare authorization code object
            Authorizationcode AuthZCode = new Authorizationcode
            {
                AuthZCode = AuthZCodeId,
                GlobalSessionId = request.GlobalSessionId,
                ClientId = request.ClientDetails.clientId,
                RedirectUrl = clientDetails.RedirectUri,
                ResponseType = request.ClientDetails.response_type,
                Scopes = clientDetails.Scopes
            };

            // Add authorization code in cache
            var Res = await _cacheClient.Add(CacheNames.AuthorizationCode,
                AuthZCodeId,
                AuthZCode);
            if (0 != Res.retValue)
            {
                _logger.LogError("AuthorizationCode add failed");
                GetAuthZCodeResponse response = new GetAuthZCodeResponse();
                response.Success = false;
                response.Message = _messageLocalizer.
                    GetMessage(Constants.InternalError);
                return response;
            }

            GetAuthZCodeResponse successResponse = new GetAuthZCodeResponse();
            // return success response
            successResponse.Success = true;
            successResponse.Message = string.Empty;
            successResponse.AuthorizationCode = AuthZCodeId;

            _logger.LogDebug("GetAuthorizationCode response: {0}",
                JsonConvert.SerializeObject(successResponse));
            _logger.LogDebug("<-- GetAuthorizationCode");
            return successResponse;
        }
        public async Task<ServiceResult> GetVerificationUrl()
        {
            var verifierRequest = await GetVerifierUrl();
            if(verifierRequest.Success == false)
            {
                return new ServiceResult(false, verifierRequest.Message);
            }
            string verifierUrl = verifierRequest.Result.ToString();
            string VerifierCode = verifierUrl.Substring(verifierUrl.LastIndexOf('/') + 1);
            VerifierUrlResponse verifierUrlResponse = new VerifierUrlResponse
            {
                verifierUrl = verifierUrl,
                VerifierCode = VerifierCode
            };
            return new ServiceResult(true, "Get Verifier Url Success", verifierUrlResponse);
        }

        public async Task<VerifyUserAuthDataResponse> IsUserVerifiedQrCode
            (VerifyQrRequest verifyQrCodeRequest)
        {
            var time = DateTime.Now.AddMinutes(2);

            var isVerified = false;

            var response = new APIResponse();

            while (DateTime.Now < time)
            {
                response = await VerifyQr(verifyQrCodeRequest.qrCode);

                if (response == null)
                {
                    return new VerifyUserAuthDataResponse()
                    {
                        Success = false,
                        Message = _messageLocalizer.GetMessage(Constants.InternalError)
                    };
                }
                if (!response.Success)
                {
                    if (response.Message != "Data not yet posted")
                    {
                        return new VerifyUserAuthDataResponse()
                        {
                            Success = false,
                            Message = response.Message
                        };
                    }
                }
                else
                {
                    isVerified = true;
                    break;
                }
            }
            if (!isVerified)
            {
                return new VerifyUserAuthDataResponse()
                {
                    Success = false,
                    Message = "Request Timed out"
                };
            }
            JObject jsonObject = JObject.Parse(JsonConvert.SerializeObject(response));

            string documentNumber = jsonObject.SelectToken
                ("Result.attributesList.Document.idDocNumber")?.ToString();

            string email = jsonObject.SelectToken
                ("Result.attributesList.Document.email")?.ToString();

            bool verified1 = jsonObject.SelectToken
                ("Result.verifyResult.presentationResult.verified").ToObject<bool>();
            if (!verified1)
            {
                return new VerifyUserAuthDataResponse()
                {
                    Success = false,
                    Message = "Verifiable Presentation Verification Failed"
                };
            }

            bool verified2 = jsonObject.SelectToken
                ("Result.verifyResult.credentialResults[0].verified").ToObject<bool>();

            if (!verified2)
            {
                return new VerifyUserAuthDataResponse()
                {
                    Success = false,
                    Message = "Verifiable Credential Verification Failed"
                };
            }
            bool verified3 = jsonObject.SelectToken
                ("Result.verifyResult.credentialResults[0].statusResult.verified").ToObject<bool>();

            if (!verified3)
            {
                return new VerifyUserAuthDataResponse()
                {
                    Success = false,
                    Message = "Verifiable Credential Revoked"
                };

            }
            var tempSessionResponse = await CreateTempSession(verifyQrCodeRequest.clientId, email);
            if(!tempSessionResponse.Success)
            {
                return new VerifyUserAuthDataResponse()
                {
                    Success = false,
                    Message = tempSessionResponse.Message
                };
            }
            VerifyUserAuthDataRequest verifyUserAuthDataRequest = new VerifyUserAuthDataRequest()
            {
                AuthnToken = tempSessionResponse.Result,
                authenticationScheme = AuthNSchemeConstants.WALLET,
                authenticationData = documentNumber,
                documentNumber = documentNumber
            };

            var result = await VerifyUserAuthData(verifyUserAuthDataRequest);

            return result;
        }

    }
}
