using DTPortal.Core.Constants;
using DTPortal.Core.Domain.Models;
using DTPortal.Core.Domain.Models.RegistrationAuthority;
using DTPortal.Core.Domain.Repositories;
using DTPortal.Core.Domain.Services;
using DTPortal.Core.Domain.Services.Communication;
using DTPortal.Core.DTOs;
using DTPortal.Core.Exceptions;
using DTPortal.Core.Utilities;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace DTPortal.Core.Services
{
    public class UserInfoService : IUserInfoService
    {

        // Initialize logger
        private readonly ILogger<UserInfoService> _logger;

        // Initialize Db
        private readonly IUnitOfWork _unitOfWork;
        private readonly ICacheClient _cacheClient;
        private readonly ITokenManager _tokenManager;
        private readonly idp_configuration idpConfiguration;
        private readonly IGlobalConfiguration _globalConfiguration;
        private readonly OIDCConstants OIDCConstants;
        private readonly IConfiguration configuration;
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IHelper _helper;
        private readonly HttpClient _client;
        private readonly IScopeService _scopeService;
        private readonly IMessageLocalizer _messageLocalizer;

        public UserInfoService(ILogger<UserInfoService> logger,
            IUnitOfWork unitofWork, ICacheClient cacheClient, ITokenManager tokenManager,
            IGlobalConfiguration globalConfiguration, IConfiguration Configuration,
            IHttpClientFactory httpClientFactory,
            HttpClient httpClient,
            IMessageLocalizer messageLocalizer,
            IHelper helper, IScopeService scopeService)
        {
            _logger = logger;
            _unitOfWork = unitofWork;
            _cacheClient = cacheClient;
            _tokenManager = tokenManager;
            _globalConfiguration = globalConfiguration;
            configuration = Configuration;
            _helper = helper;
            _httpClientFactory = httpClientFactory;
            httpClient.BaseAddress = new Uri(configuration["APIServiceLocations:UserProfileServiceBaseAddress"]);
            httpClient.DefaultRequestHeaders.Add("Accept", "application/json");
            _client = httpClient;
            _scopeService = scopeService;
            _messageLocalizer = messageLocalizer;   

            idpConfiguration = _globalConfiguration.GetIDPConfiguration();
            if (null == idpConfiguration)
            {
                _logger.LogError("Get IDP Configuration failed");
                throw new NullReferenceException();
            }

            var errorConfiguration = _globalConfiguration.GetErrorConfiguration();
            if (null == errorConfiguration)
            {
                _logger.LogError("Get Error Configuration failed");
                throw new NullReferenceException();
            }

            OIDCConstants = errorConfiguration.OIDCConstants;
            if (null == errorConfiguration)
            {
                _logger.LogError("Get Error Configuration failed");
                throw new NullReferenceException();
            }
        }

        public async Task<APIResponse> GetUserDetailsAsync(string id)
        {
            try
            {
                HttpResponseMessage response = await _client.GetAsync($"api/get/user/profile/by/suid/{id}");
                if (response.StatusCode == HttpStatusCode.OK)
                {
                    APIResponse apiResponse = JsonConvert.DeserializeObject<APIResponse>(await response.Content.ReadAsStringAsync());

                    return apiResponse;
                }
                else
                {
                    _logger.LogError($"The request with URI={response.RequestMessage.RequestUri} failed " +
                               $"with status code={response.StatusCode}");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
            }

            return null;
        }
        public async Task<string> GetFaceByUrl(string SelfieUri)
        {
            _logger.LogDebug("-->GetSubscriberPhoto");

            // Local Variable Declaration
            string response = null;
            var errorMessage = string.Empty;

            if (string.IsNullOrEmpty(SelfieUri))
            {
                _logger.LogError("Get face : Invalid Input Parameter");
                return response;
            }

            try
            {
                HttpClient client = _httpClientFactory.CreateClient();

                var result = await client.GetAsync(SelfieUri);

                if (result.IsSuccessStatusCode)
                {
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
        // ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
        public async Task<Object> GetUserInfo(string AccessToken, bool signed)
        {
            _logger.LogDebug("---->GetUserInfo");

            if (null == AccessToken)
            {
                _logger.LogError("Access token not recieved");
                ErrorResponseDTO error = new ErrorResponseDTO();
                error.error = _messageLocalizer.GetMessage(OIDCConstants.InternalError);
                error.error_description = _messageLocalizer.GetMessage(OIDCConstants.InvalidInput);
                return error;
            }

            var errorMessage = string.Empty;
            Accesstoken accessToken = null;
            try
            {
                // Get the access token record
                accessToken = await _cacheClient.Get<Accesstoken>("AccessToken",
                    AccessToken);
                if (null == accessToken)
                {
                    _logger.LogError("Access token not recieved from cache." +
                        "Expired or Invalid access token");
                    ErrorResponseDTO error = new ErrorResponseDTO();
                    error.error = _messageLocalizer.GetMessage(OIDCConstants.InvalidToken);
                    error.error_description = _messageLocalizer.GetMessage(OIDCConstants.InvalidTokenDesc);
                    return error;
                }
            }
            catch (CacheException ex)
            {
                _logger.LogError("Failed to get Access Token Record");
                ErrorResponseDTO error = new ErrorResponseDTO();
                error.error = _messageLocalizer.GetMessage(OIDCConstants.InternalError);
                error.error_description = _helper.GetRedisErrorMsg(
                    ex.ErrorCode, ErrorCodes.REDIS_ACCESS_TOKEN_GET_FAILED);
                return error;
            }

            if (configuration.GetValue<string>("IDP_TYPE").Equals("INTERNAL"))
            {
                GlobalSession globalSession = null;
                try
                {
                    // Get the Global Session record
                    globalSession = await _cacheClient.Get<GlobalSession>("GlobalSession",
                        accessToken.GlobalSessionId);
                    if (null == globalSession)
                    {
                        _logger.LogError("Global session not recieved from cache." +
                            "Expired or Invalid access token");
                        ErrorResponseDTO error = new ErrorResponseDTO();
                        error.error = _messageLocalizer.GetMessage(OIDCConstants.InvalidToken);
                        error.error_description = _messageLocalizer.GetMessage(OIDCConstants.InvalidTokenDesc);
                        return error;
                    }
                }
                catch (CacheException ex)
                {
                    _logger.LogError("Failed to get Global Session Record");
                    ErrorResponseDTO error = new ErrorResponseDTO();
                    error.error = _messageLocalizer.GetMessage(OIDCConstants.InternalError);
                    error.error_description = _helper.GetRedisErrorMsg(
                        ex.ErrorCode, ErrorCodes.REDIS_GLOBAL_SESS_GET_FAILED);
                    return error;
                }
            }

            Client clientInDb = null;
            try
            {
                clientInDb = await _unitOfWork.Client.GetClientByClientIdAsync(
                    accessToken.ClientId);
                if (null == clientInDb)
                {
                    _logger.LogError("Client not found");
                    ErrorResponseDTO error = new ErrorResponseDTO();
                    error.error = _messageLocalizer.GetMessage(OIDCConstants.InvalidClient);
                    error.error_description = _messageLocalizer.GetMessage(OIDCConstants.ClientNotFound);
                    return error;
                }
            }
            catch (Exception)
            {
                errorMessage = _helper.GetErrorMsg(ErrorCodes.DB_ERROR);
                ErrorResponseDTO error = new ErrorResponseDTO();
                error.error = _messageLocalizer.GetMessage(OIDCConstants.InternalError);
                error.error_description = errorMessage;
                return error;
            }

            if (StatusConstants.ACTIVE != clientInDb.Status)
            {
                _logger.LogError("Client status is not Active: {0}", clientInDb.Status);
                ErrorResponseDTO error = new ErrorResponseDTO();
                error.error = _messageLocalizer.GetMessage(OIDCConstants.InvalidClient);
                error.error_description = _messageLocalizer.GetMessage(OIDCConstants.ClientNotActive);
                return error;
            }

            // Parse the space seperated scopes
            var scopes = accessToken.Scopes.Split(' ');
            _logger.LogDebug("Allowed access scopes for access token {0}",
                    accessToken.Scopes);

            // Check for any valid scope
            if (!scopes.Contains(OAuth2Constants.Profile) &&
                !scopes.Contains(OAuth2Constants.openid))
            {
                _logger.LogError(OIDCConstants.InsufficientScopeDesc.En);
                ErrorResponseDTO error = new ErrorResponseDTO();
                error.error = _messageLocalizer.GetMessage(OIDCConstants.InsufficientScopeDesc);
                error.error = _messageLocalizer.GetMessage(OIDCConstants.InsufficientScopeDesc);
                return error;
            }

            var openidconnect = JsonConvert.DeserializeObject<OpenIdConnect>(
                idpConfiguration.openidconnect.ToString());

            var user = new Domain.Models.UserTable();
            if (accessToken.ClientId.Equals(DTInternalConstants.DTPortalClientId))
            {
                // Get user profile
                user = await _unitOfWork.Users.GetUserbyUuidAsync(
                    accessToken.UserId);
                if (null == user)
                {
                    _logger.LogError("_unitOfWork.UserTable.GetUserIdByName failed" +
                        "UserId : {0}", accessToken.UserId);
                    ErrorResponseDTO error = new ErrorResponseDTO();
                    errorMessage = _helper.GetErrorMsg(
                        ErrorCodes.DB_USER_GET_DETAILS_FAILED);
                    error.error = errorMessage;
                    error.error_description = _messageLocalizer.GetMessage(OIDCConstants.InternalError);
                    return error;
                }

                // Add claims/attributes related to particular scope
                if ((scopes.Contains(OAuth2Constants.Profile) &&
                     scopes.Contains(OAuth2Constants.openid)) ||
                     (scopes.Contains(OAuth2Constants.Profile)))
                {
                    adminProfileFields adminProfileFields = new adminProfileFields();
                    daesAdminProfile daesResponse = new daesAdminProfile();

                    adminProfileFields.sub = user.Id.ToString();
                    adminProfileFields.iss = openidconnect.issuer;
                    adminProfileFields.aud = accessToken.ClientId;
                    daesResponse.sub = user.Id.ToString();
                    daesResponse.uuid = user.Uuid;
                    daesResponse.name = user.FullName;
                    daesResponse.birthdate = user.Dob.ToString();
                    daesResponse.gender = (int)user.Gender;
                    daesResponse.email = user.MailId;
                    daesResponse.phone_number = user.MobileNo;
                    adminProfileFields.daes_response = daesResponse;

                    if (signed == true)
                    {
                        var signedResponse = await _tokenManager.CreateUserInfoToken(
                            adminProfileFields,
                            "profile",
                            accessToken.ClientId,
                            openidconnect.issuer
                            );
                        if (null == signedResponse)
                        {
                            _logger.LogError("CreateUserInfoToken failed");
                            ErrorResponseDTO error = new ErrorResponseDTO();
                            errorMessage = _helper.GetErrorMsg(
                                ErrorCodes.GENERATE_USER_INFO_TOKEN_FAILED);
                            error.error = errorMessage;
                            error.error_description = _messageLocalizer.GetMessage(OIDCConstants.InternalError);
                            return error;
                        }

                        return signedResponse;
                    }

                    if (!scopes.Contains("openid"))
                    {
                        return daesResponse;
                    }

                    return adminProfileFields;
                }

                // OpenId scopes: only basic fields
                if (scopes.Contains("openid"))
                {
                    adminBasicFields adminBasicFields = new adminBasicFields();
                    daesAdminBasic daesResponse = new daesAdminBasic();

                    adminBasicFields.sub = user.Id.ToString();
                    adminBasicFields.iss = openidconnect.issuer;
                    adminBasicFields.aud = accessToken.ClientId;
                    daesResponse.sub = user.Id.ToString();
                    daesResponse.uuid = user.Uuid;
                    daesResponse.name = user.FullName;
                    daesResponse.birthdate = user.Dob.ToString();
                    daesResponse.gender = (int)user.Gender;
                    adminBasicFields.daes_response = daesResponse;

                    if (signed == true)
                    {

                        var signedResponse = await _tokenManager.CreateUserInfoToken(
                            adminBasicFields,
                            "openid",
                            accessToken.ClientId,
                            openidconnect.issuer
                            );
                        if (null == signedResponse)
                        {
                            _logger.LogError("CreateUserInfoToken failed");
                            ErrorResponseDTO error = new ErrorResponseDTO();
                            errorMessage = _helper.GetErrorMsg(
                                ErrorCodes.GENERATE_USER_INFO_TOKEN_FAILED);
                            error.error = errorMessage;
                            error.error_description = _messageLocalizer.GetMessage(OIDCConstants.InternalError);
                            return error;
                        }

                        return signedResponse;
                    }

                    return adminBasicFields;
                }

            }
            else
            {
                SubscriberView raUserInfo = null;
                try
                {
                    raUserInfo = await _unitOfWork.Subscriber.GetSubscriberInfoBySUID(
                        accessToken.UserId);
                    if (null == raUserInfo)
                    {
                        _logger.LogError("_unitOfWork.UserTable.GetUserIdByName failed" +
                            "UserId : {0}", accessToken.UserId);
                        ErrorResponseDTO error = new ErrorResponseDTO();
                        errorMessage = _helper.GetErrorMsg(
                            ErrorCodes.DB_USER_GET_DETAILS_FAILED);
                        error.error = _messageLocalizer.GetMessage(OIDCConstants.InternalError);
                        error.error_description = errorMessage;
                        return error;
                    }
                }
                catch (Exception)
                {
                    var error = new ErrorResponseDTO();
                    errorMessage = _helper.GetErrorMsg(ErrorCodes.DB_ERROR);
                    error.error = _messageLocalizer.GetMessage(OIDCConstants.InternalError);
                    error.error_description = errorMessage;
                    return error;
                }

                //get login profile from global session
                GlobalSession globalSession = null;
                try
                {
                    // Get the Global Session record
                    globalSession = await _cacheClient.Get<GlobalSession>("GlobalSession",
                        accessToken.GlobalSessionId);
                    if (null == globalSession)
                    {
                        _logger.LogError("Global session not recieved from cache." +
                            "Expired or Invalid access token");
                        ErrorResponseDTO error = new ErrorResponseDTO();
                        error.error = _messageLocalizer.GetMessage(OIDCConstants.InvalidToken);
                        error.error_description = _messageLocalizer.GetMessage(OIDCConstants.InvalidTokenDesc);
                        return error;
                    }
                }
                catch (CacheException ex)
                {
                    _logger.LogError("Failed to get Global Session Record");
                    ErrorResponseDTO error = new ErrorResponseDTO();
                    error.error = _messageLocalizer.GetMessage(OIDCConstants.InternalError);
                    error.error_description = _helper.GetRedisErrorMsg(
                        ex.ErrorCode, ErrorCodes.REDIS_GLOBAL_SESS_GET_FAILED);
                    return error;
                }

                // Add claims/attributes related to particular scope
                if ((scopes.Contains(OAuth2Constants.Profile) &&
                     scopes.Contains(OAuth2Constants.openid)) ||
                     (scopes.Contains(OAuth2Constants.Profile)))
                {
                    subscriberProfileFields subscriberProfileFields =
                        new subscriberProfileFields();
                    daesSubProfile daesResponse = new daesSubProfile();
                    subscriberProfileFields.sub = raUserInfo.SubscriberUid;
                    subscriberProfileFields.iss = openidconnect.issuer;
                    subscriberProfileFields.aud = accessToken.ClientId;

                    daesResponse.name = raUserInfo.DisplayName;
                    daesResponse.birthdate = raUserInfo.DateOfBirth.ToString();
                    daesResponse.suid = raUserInfo.SubscriberUid;
                    daesResponse.id_document_type = raUserInfo.IdDocType;
                    daesResponse.id_document_number = raUserInfo.IdDocNumber;
                    daesResponse.loa = raUserInfo.Loa;
                    daesResponse.email = raUserInfo.Email;
                    daesResponse.email = raUserInfo.Email;
                    daesResponse.gender = raUserInfo.Gender;
                    daesResponse.phone = raUserInfo.MobileNumber;
                    daesResponse.country = raUserInfo.Country;
                    daesResponse.login_type = globalSession.LoginType;
                    subscriberProfileFields.daes_claims = daesResponse;
                    if (globalSession.LoginProfile != null)
                    {
                        daesResponse.login_profile = globalSession.LoginProfile;
                    }
                    if (signed == true)
                    {
                        var signedResponse = await _tokenManager.CreateUserInfoToken(
                            subscriberProfileFields,
                            "sub_profile",
                            accessToken.ClientId,
                            openidconnect.issuer
                            );
                        if (null == signedResponse)
                        {
                            _logger.LogError("CreateUserInfoToken failed");
                            ErrorResponseDTO error = new ErrorResponseDTO();
                            errorMessage = _helper.GetErrorMsg(
                                ErrorCodes.GENERATE_USER_INFO_TOKEN_FAILED);
                            error.error = errorMessage;
                            error.error_description = _messageLocalizer.GetMessage(OIDCConstants.InternalError);
                            return error;
                        }

                        return signedResponse;
                    }

                    if (!scopes.Contains("openid"))
                    {
                        return daesResponse;
                    }

                    return subscriberProfileFields;
                }

                if (scopes.Contains("openid"))
                {
                    subscriberBasicFields subscriberBasicFields = new subscriberBasicFields();
                    daesSubBasic daesResponse = new daesSubBasic();

                    subscriberBasicFields.sub = raUserInfo.SubscriberUid;
                    subscriberBasicFields.iss = openidconnect.issuer;
                    subscriberBasicFields.aud = accessToken.ClientId;

                    daesResponse.name = raUserInfo.DisplayName;
                    daesResponse.birthdate = raUserInfo.DateOfBirth.ToString();
                    daesResponse.gender = raUserInfo.Gender;
                    subscriberBasicFields.daes_claims = daesResponse;

                    if (signed == true)
                    {
                        var signedResponse = await _tokenManager.CreateUserInfoToken(
                            subscriberBasicFields,
                            "sub_openid",
                            accessToken.ClientId,
                            openidconnect.issuer
                            );
                        if (null == signedResponse)
                        {
                            _logger.LogError("CreateUserInfoToken failed");
                            ErrorResponseDTO error = new ErrorResponseDTO();
                            errorMessage = _helper.GetErrorMsg(
                                ErrorCodes.GENERATE_USER_INFO_TOKEN_FAILED);
                            error.error = errorMessage;
                            error.error_description = _messageLocalizer.GetMessage(OIDCConstants.InternalError);
                            return error;
                        }

                        return signedResponse;
                    }
                    return subscriberBasicFields;
                }

            }

            return null;
        }
        // ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
        public async Task<GetUserInfoResponse> GetUserProfile(string GlobalSession)
        {
            GetUserInfoResponse response = new GetUserInfoResponse();
            response.Success = false;

            GlobalSession globalSession = null;
            try
            {
                // Get the Global Session record
                globalSession = await _cacheClient.Get<GlobalSession>("GlobalSession",
                    GlobalSession);
                if (null == globalSession)
                {
                    _logger.LogError("Global session not recieved from cache." +
                        "Expired or Invalid access token");
                    response.Error = "invalid_token";
                    response.Message = "The access token is invalid";
                    return response;
                }
            }
            catch (CacheException ex)
            {
                _logger.LogError("Failed to get Global Session Record");
                response.Error = _messageLocalizer.GetMessage(OIDCConstants.InternalError);
                response.Message = _helper.GetRedisErrorMsg(
                    ex.ErrorCode, ErrorCodes.REDIS_GLOBAL_SESS_GET_FAILED);
                return response;
            }

            // Get user profile
            var user = await _unitOfWork.Users.GetUserbyNameAsync(
                globalSession.UserId);
            if (null == user)
            {
                _logger.LogError("_unitOfWork.UserTable.GetUserIdByName failed" +
                    "UserId : {0}", globalSession.UserId);
                response.Error = "invalid_token";
                response.Message = "Internal error occured";
                return response;
            }

            // Create respone object
            GetUserInfoResponse getUserInoRes = new GetUserInfoResponse();

            getUserInoRes.Sub = user.Id.ToString();
            getUserInoRes.UserId = user.Uuid;
            getUserInoRes.Name = user.FullName;
            getUserInoRes.Dob = user.Dob;
            getUserInoRes.Gender = (int)user.Gender;
            getUserInoRes.MailId = user.MailId;
            getUserInoRes.MobileNo = user.MobileNo;

            _logger.LogInformation("<--GetUserInfo");
            getUserInoRes.Success = true;
            return getUserInoRes;
        }
        // ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
        public async Task<APIResponse> GetUserImage(string AccessToken)
        {
            _logger.LogDebug("---->GetUserInfo");

            if (null == AccessToken)
            {
                _logger.LogError("Access token not recieved");

                return new APIResponse("Invalid Parameters");
            }
            var errorMessage = string.Empty;

            Accesstoken accessToken = null;

            try
            {
                // Get the access token record
                accessToken = await _cacheClient.Get<Accesstoken>("AccessToken",
                    AccessToken);
                if (null == accessToken)
                {
                    _logger.LogError("Access token not recieved from cache." +
                        "Expired or Invalid access token");

                    return new APIResponse("Invalid Token");
                }
            }
            catch (CacheException)
            {
                _logger.LogError("Failed to get Access Token Record");

                return new APIResponse("Internal Error");
            }

            Client clientInDb = null;
            try
            {
                clientInDb = await _unitOfWork.Client.GetClientByClientIdAsync(
                    accessToken.ClientId);

                if (null == clientInDb)
                {
                    _logger.LogError("Client not found");
                    return new APIResponse("Client Not Found");
                }
            }
            catch (Exception)
            {
                errorMessage = _helper.GetErrorMsg(ErrorCodes.DB_ERROR);
                return new APIResponse(errorMessage);
            }

            if (StatusConstants.ACTIVE != clientInDb.Status)
            {
                _logger.LogError("Client status is not Active: {0}", clientInDb.Status);

                return new APIResponse("Client Not Active");
            }

            // Parse the space seperated scopes
            var scopes = accessToken.Scopes.Split(' ');

            _logger.LogDebug("Allowed access scopes for access token {0}",
                    accessToken.Scopes);

            SubscriberView raUserInfo = null;
            try
            {
                raUserInfo = await _unitOfWork.Subscriber.GetSubscriberInfoBySUID(
                    accessToken.UserId);
                if (null == raUserInfo)
                {
                    _logger.LogError("_unitOfWork.UserTable.GetUserIdByName failed" +
                        "UserId : {0}", accessToken.UserId);
                    errorMessage = _helper.GetErrorMsg(
                        ErrorCodes.DB_USER_GET_DETAILS_FAILED);
                    return new APIResponse(errorMessage);
                }
            }
            catch (Exception)
            {
                errorMessage = _helper.GetErrorMsg(ErrorCodes.DB_ERROR);

                return new APIResponse(errorMessage);
            }

            var picture = raUserInfo.Selfie;

            if (picture == null)
            {
                _logger.LogError("Image is Null");

                return new APIResponse("Failed to get user picture");
            }
            return new APIResponse()
            {
                Success = true,
                Message = "Get Image Success",
                Result = picture
            };
        }
        // ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
        public async Task<Object> UserProfile(string AccessToken)
        {
            if (null == AccessToken)
            {
                _logger.LogError("Access token not recieved");
                ErrorResponseDTO error = new ErrorResponseDTO();
                error.error = _messageLocalizer.GetMessage(OIDCConstants.InternalError);
                error.error_description = _messageLocalizer.GetMessage(OIDCConstants.InvalidInput);
                return error;
            }

            var errorMessage = string.Empty;
            Accesstoken accessToken = null;
            try
            {
                // Get the access token record
                accessToken = await _cacheClient.Get<Accesstoken>("AccessToken",
                    AccessToken);
                if (null == accessToken)
                {
                    _logger.LogError("Access token not recieved from cache." +
                        "Expired or Invalid access token");
                    ErrorResponseDTO error = new ErrorResponseDTO();
                    error.error = _messageLocalizer.GetMessage(OIDCConstants.InvalidToken);
                    error.error_description = _messageLocalizer.GetMessage(OIDCConstants.InvalidTokenDesc);
                    return error;
                }
            }
            catch (CacheException ex)
            {
                _logger.LogError("Failed to get Access Token Record");
                ErrorResponseDTO error = new ErrorResponseDTO();
                error.error = _messageLocalizer.GetMessage(OIDCConstants.InternalError);
                error.error_description = _helper.GetRedisErrorMsg(
                    ex.ErrorCode, ErrorCodes.REDIS_ACCESS_TOKEN_GET_FAILED);
                return error;
            }

            Client clientInDb = null;
            try
            {
                clientInDb = await _unitOfWork.Client.GetClientByClientIdAsync(
                    accessToken.ClientId);
                if (null == clientInDb)
                {
                    _logger.LogError("Client not found");
                    ErrorResponseDTO error = new ErrorResponseDTO();
                    error.error = _messageLocalizer.GetMessage(OIDCConstants.InvalidClient);
                    error.error_description = _messageLocalizer.GetMessage(OIDCConstants.ClientNotFound);
                    return error;
                }
            }
            catch (Exception)
            {
                errorMessage = _helper.GetErrorMsg(ErrorCodes.DB_ERROR);
                ErrorResponseDTO error = new ErrorResponseDTO();
                error.error = _messageLocalizer.GetMessage(OIDCConstants.InternalError);
                error.error_description = errorMessage;
                return error;
            }

            if (StatusConstants.ACTIVE != clientInDb.Status)
            {
                _logger.LogError("Client status is not Active: {0}", clientInDb.Status);
                ErrorResponseDTO error = new ErrorResponseDTO();
                error.error = _messageLocalizer.GetMessage(OIDCConstants.InvalidClient);
                error.error_description = _messageLocalizer.GetMessage(OIDCConstants.ClientNotActive);
                return error;
            }

            APIResponse userInfo= null;

            if(clientInDb.ClientId.Equals(DTInternalConstants.DTPortalClientId))
            {
                userInfo = await GetAdminProfile(accessToken.UserId);
            }
            else
            {
                userInfo = await GetUserDetailsAsync(accessToken.UserId);
            }

            if (userInfo == null)
            {
                ErrorResponseDTO error = new ErrorResponseDTO();
                error.error = _messageLocalizer.GetMessage(OIDCConstants.InternalError);
                error.error_description = "Failed to get user profile";
            }

            if (!userInfo.Success)
            {
                ErrorResponseDTO error = new ErrorResponseDTO();
                error.error = _messageLocalizer.GetMessage(OIDCConstants.InternalError);
                error.error_description = userInfo.Message;
            }

            var attributes = new List<string>();

            attributes = accessToken.AcceptedAttributes;

            if (attributes==null || attributes.Count == 0)
            {
                attributes=new List<string>();
                _logger.LogInformation("No attributes found for the scopes assigned to access token. Adding default attributes");
            }

            var subscriberProfile = JsonConvert.DeserializeObject
                        <SubscriberProfileDTO>(userInfo.Result.ToString());

            Domain.Services.Communication.SubscriberInfo subscriberInfo =
                new Domain.Services.Communication.SubscriberInfo();

            if (attributes.Contains("idn"))
                subscriberInfo.Idn = subscriberProfile.Idn;

            if (attributes.Contains("fullnameEN"))
                subscriberInfo.FullnameEN = subscriberProfile.FullnameEN;

            if (attributes.Contains("fullnameAR"))
                subscriberInfo.FullnameAR = subscriberProfile.FullnameAR;

            if (attributes.Contains("firstnameEN"))
                subscriberInfo.FirstnameEN = subscriberProfile.FirstnameEN;

            if (attributes.Contains("firstnameAR"))
                subscriberInfo.FirstnameAR = subscriberProfile.FirstnameAR;

            if (attributes.Contains("lastnameEN"))
                subscriberInfo.LastnameEN = subscriberProfile.LastnameEN;

            if (attributes.Contains("lastnameAR"))
                subscriberInfo.LastnameAR = subscriberProfile.LastnameAR;

            if (attributes.Contains("nationalityEN"))
                subscriberInfo.NationalityEN = subscriberProfile.NationalityEN;

            if (attributes.Contains("nationalityAR"))
                subscriberInfo.NationalityAR = subscriberProfile.NationalityAR;

            if (attributes.Contains("gender"))
                subscriberInfo.Gender = subscriberProfile.Gender;

            if (attributes.Contains("idType"))
                subscriberInfo.IdType = subscriberProfile.IdType;

            if (attributes.Contains("titleEN"))
                subscriberInfo.TitleEN = subscriberProfile.TitleEN;

            if (attributes.Contains("titleAR"))
                subscriberInfo.TitleAR = subscriberProfile.TitleAR;

            if (attributes.Contains("dateOfBirth"))
                subscriberInfo.DateOfBirth = subscriberProfile.DateOfBirth;

            if (attributes.Contains("profileType"))
                subscriberInfo.ProfileType = subscriberProfile.ProfileType;

            if (attributes.Contains("suid"))
                subscriberInfo.Suid = subscriberProfile.Suid;

            if (attributes.Contains("loa"))
                subscriberInfo.Loa = subscriberProfile.Loa;

            if (attributes.Contains("unifiedId"))
                subscriberInfo.UnifiedId = subscriberProfile.UnifiedId;

            if (attributes.Contains("passportNumber"))
                subscriberInfo.PassportNumber = subscriberProfile.PassportNumber;

            return JsonConvert.DeserializeObject<Domain.Services.Communication.SubscriberInfo>(
                JsonConvert.SerializeObject(subscriberInfo,
                new JsonSerializerSettings
                    {
                        NullValueHandling = NullValueHandling.Ignore
                    }
                ));
            }
        // ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
        public async Task<APIResponse> GetAdminProfile(string Id)
        {
            var errorMessage = string.Empty;
            var user = new Domain.Models.UserTable();
            user = await _unitOfWork.Users.GetUserbyUuidAsync(Id);
            if (null == user)
            {
                ErrorResponseDTO error = new ErrorResponseDTO();
                errorMessage = _helper.GetErrorMsg(
                    ErrorCodes.DB_USER_GET_DETAILS_FAILED);
                return new APIResponse()
                {
                    Success = true,
                    Message = errorMessage
                };
            }
            adminProfileFields adminProfileFields = new adminProfileFields();
            daesAdminProfile daesResponse = new daesAdminProfile();

            daesResponse.sub = user.Id.ToString();
            daesResponse.uuid = user.Uuid;
            daesResponse.name = user.FullName;
            daesResponse.birthdate = user.Dob.ToString();
            daesResponse.gender = (int)user.Gender;
            daesResponse.email = user.MailId;
            daesResponse.phone_number = user.MobileNo;

            return new APIResponse()
            {
                Success = true,
                Message = "Get Profile Success",
                Result = daesResponse
            };

        }
    }
}
