using Confluent.Kafka;
using DTPortal.Common;
using DTPortal.Core.Constants;
using DTPortal.Core.Domain.Lookups;
using DTPortal.Core.Domain.Models;
using DTPortal.Core.Domain.Repositories;
using DTPortal.Core.Domain.Services;
using DTPortal.Core.Domain.Services.Communication;
using DTPortal.Core.DTOs;
using DTPortal.Core.Exceptions;
using DTPortal.Core.Utilities;
using Fido2NetLib;
using Google.Apis.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Serialization;
using Org.BouncyCastle.Asn1.Ocsp;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using static DTPortal.Common.CommonResponse;
using static System.Formats.Asn1.AsnWriter;

namespace DTPortal.Core.Services
{
    public class MobileAuthenticationService : IMobileAuthenticationService
    {
        private readonly ILogger<MobileAuthenticationService> _logger;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ICacheClient _cacheClient;
        private readonly IPushNotificationClient _pushNotificationClient;
        private readonly ITokenManager _tokenManager;
        private readonly ILogClient _LogClient;
        private readonly IPKIServiceClient _pkiServiceClient;
        private readonly IRAServiceClient _raServiceClient;
        private readonly SSOConfig ssoConfig;
        private readonly MessageConstants Constants;
        private readonly OIDCConstants OIDCConstants;
        private readonly IGlobalConfiguration _globalConfiguration;
        private readonly IConfiguration configuration;
        private readonly IHelper _helper;
        private readonly ThresholdConfiguration thresholdConfiguration;
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IScopeService _scopeService;
        private readonly IUserClaimService _userClaimService;
        private readonly IUserConsentService _userConsentService;
        private readonly ILogReportService _reportsService;
        private readonly IMessageLocalizer _messageLocalizer;

        public MobileAuthenticationService(
            ILogger<MobileAuthenticationService> logger,
            IUnitOfWork unitOfWork,
            ICacheClient cacheClient,
            IPushNotificationClient pushNotificationClient,
            ITokenManager tokenManager,
            ILogClient LogClient,
            IFido2 fido2,
            IPKIServiceClient pkiServiceClient,
            IRAServiceClient raServiceClient,
            IGlobalConfiguration globalConfiguration,
            IConfiguration Configuration,
            IHelper helper,
            IHttpClientFactory httpClient,
            IScopeService scopeService,
            IUserClaimService userClaimService,
            IUserConsentService userConsentService,
            ILogReportService logReportService,
            IMessageLocalizer messageLocalizer)
        {
            _logger = logger;
            _logger = logger;
            _unitOfWork = unitOfWork;
            _cacheClient = cacheClient;
            _pushNotificationClient = pushNotificationClient;
            _tokenManager = tokenManager;
            _LogClient = LogClient;
            _pkiServiceClient = pkiServiceClient;
            _raServiceClient = raServiceClient;
            configuration = Configuration;
            _httpClientFactory = httpClient;
            _scopeService = scopeService;
            _globalConfiguration = globalConfiguration;
            _userClaimService = userClaimService;
            _helper = helper;
            _userConsentService = userConsentService;
            _reportsService = logReportService;
            _messageLocalizer = messageLocalizer;
            ssoConfig = _globalConfiguration.GetSSOConfiguration();
            if (null == ssoConfig)
            {
                _logger.LogError("Get SSO Configuration failed");
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

            OIDCConstants = errorConfiguration.OIDCConstants;
            if (null == OIDCConstants)
            {
                _logger.LogError("Get Error Configuration failed");
                throw new NullReferenceException();
            }

            thresholdConfiguration = _globalConfiguration.GetThresholdConfiguration();
            if (null == thresholdConfiguration)
            {
                _logger.LogError("Get Threshold Configuration failed");
                throw new NullReferenceException();
            }
            
        }

        [NonAction]
        public double CalculateTime(string startTime)
        {
            try
            {
                // Convert string to DateTime Object
                CultureInfo provider = CultureInfo.InvariantCulture;

                // Parse Start Date Time
                DateTime startDateTime = DateTime.ParseExact(startTime,
                new string[] { "yyyy'-'MM'-'dd'T'HH':'mm':'ss" }, provider,
                DateTimeStyles.None);

                // Current Date Time
                DateTime endDateTime = DateTime.Now;

                // Calculate Total Time
                TimeSpan totalTime = endDateTime - startDateTime;

                _logger.LogInformation("Start Date Time:{0}", startDateTime);
                _logger.LogInformation("End Date Time:{0}", endDateTime);
                _logger.LogInformation("Total Time in Seconds:{0}",
                    totalTime.TotalSeconds);

                return Math.Round(totalTime.TotalSeconds, 2);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Calculate Time Failed:{0}", ex.Message);
                return -1;
            }
        }

        private bool IsIcpFaceMatchSuccessful(IcpJourneyDetailsResponse journey)
        {
            return journey?.result?.data?.selfieAnalysis?.faceMatch != null
                && journey.result.data.selfieAnalysis.faceMatch.success
                && journey.result.data.selfieAnalysis.faceMatch.matched;
        }

        public async Task<Response> VerifyUAEKycFace(string journeyToken)
        {
            var journeyDetails = await GetJourneyDetailsAsync(journeyToken);
            if (journeyDetails == null)
            {
                _logger.LogError("Failed to retrieve ICP journey details for token: {0}", journeyToken?.SanitizeForLogging());
                return new Response
                {
                    Success = false,
                    Message = _messageLocalizer.GetMessage(Constants.JourneyDetailsFailed)
                };
            }

            var isFaceMatchSuccessful = IsIcpFaceMatchSuccessful(journeyDetails);

            if (isFaceMatchSuccessful)
            {
                return new Response
                {
                    Success = true,
                    Message = _messageLocalizer.GetMessage(Constants.AuthNDone)
                };
            }
            else
            {
                return new Response
                {
                    Success = false,
                    Message = _messageLocalizer.GetMessage(Constants.FaceVerifyFailed)
                };
            }

        }

        public async Task<IcpJourneyDetailsResponse> GetJourneyDetailsAsync
            (string journeyToken)
        {
            var client = _httpClientFactory.CreateClient();

            var request = new HttpRequestMessage(
                HttpMethod.Get,
                $"{configuration["ICP:BaseUrl"]}/otk-service/v2/journey-details/{journeyToken}"
            );

            request.Headers.Add(
                "x-transaction-key",
                configuration["ICP:TransactionKey"]
            );

            using var response = await client.SendAsync(request);
            var json = await response.Content.ReadAsStringAsync();

            if (!response.IsSuccessStatusCode)
            {
                return null;
            }

            return JsonConvert.DeserializeObject<IcpJourneyDetailsResponse>(json);
        }

        public async Task<ServiceResult> InitiateMobileAuthenticationAsync
            (MobileAuthRequest request)
        {
            if (null == request || string.IsNullOrEmpty(request.ClientId))
            {
                _logger.LogError("Invalid Arguments");
                return new ServiceResult(false, _messageLocalizer.GetMessage(Constants.InvalidArguments));
            }

            _logger.LogDebug("client id is {0}", request.ClientId);
            var errorMessage = string.Empty;
            var clientInDb = new Client();
            try
            {
                clientInDb = await _unitOfWork.Client.GetClientByClientIdAsync(
                    request.ClientId);
                if (null == clientInDb)
                {
                    _logger.LogError(OIDCConstants.ClientNotFound.En);
                    return new ServiceResult(false,_messageLocalizer.
                        GetMessage(OIDCConstants.ClientNotFound));
                }
            }
            catch (Exception)
            {
                errorMessage = _helper.GetErrorMsg(ErrorCodes.DB_ERROR);
                Monitor.SendMessage(errorMessage);
                return new ServiceResult(false, errorMessage);
            }

            if (clientInDb.Status != StatusConstants.ACTIVE)
            {
                _logger.LogError("{0}: {1}",
                    OIDCConstants.ClientNotActive.En, clientInDb.Status);
                return new ServiceResult(false, _messageLocalizer.GetMessage(OIDCConstants.ClientNotActive));
            }

            ClientDetails clientdetails = new ClientDetails
            {
                ClientId = request.ClientId,
                Scopes = request.Scope,
                RedirectUrl = request.RedirectUri,
                ResponseType = request.ResponseType,
                AppName = clientInDb.ApplicationName
            };
            var tempAuthNSessId = string.Empty;

            try
            {
                tempAuthNSessId = EncryptionLibrary.KeyGenerator.GetUniqueKey();
            }
            catch (Exception error)
            {
                _logger.LogError("GetUniqueKey failed: {0}", error.Message);
                errorMessage = _helper.GetErrorMsg(
                     ErrorCodes.GENERATE_UNIQUE_KEY_FAILED);
                return new ServiceResult(false, errorMessage);
            }

            MobileAuthTemporarySession mobileAuthTemporarySession =
                new MobileAuthTemporarySession
                {
                    TemporarySessionId = tempAuthNSessId,
                    ClientDetails = clientdetails,
                    State = request.State,
                    AcrValues = request.AcrValues
                };

            var task = await _cacheClient.Add(CacheNames.MobileAuthTemporarySession,
                tempAuthNSessId, mobileAuthTemporarySession);
            if (DTInternalConstants.S_OK != task.retValue)
            {
                _logger.LogError("_cacheClient.Add failed");
                errorMessage = _helper.GetRedisErrorMsg(task.retValue,
                     ErrorCodes.REDIS_TEMP_SESS_ADD_FAILED);
                return new ServiceResult(false, errorMessage);
            }

            return new ServiceResult(true, "Temp Session Created Successfully", tempAuthNSessId);
        }

        public async Task<ServiceResult> GetConsentDetailsAsync
            (string sessionId)
        {
            MobileAuthTemporarySession tempSession = null;
            var errorMessage = string.Empty;

            List<approved_scopes> approved_Scopes = new List<approved_scopes>();

            try
            {
                tempSession = await _cacheClient.Get<MobileAuthTemporarySession>
                    (CacheNames.MobileAuthTemporarySession,
                    sessionId);
                if (null == tempSession)
                {
                    _logger.LogError(Constants.TempSessionExpired.En);
                    errorMessage = _messageLocalizer.GetMessage(Constants.TempSessionExpired);
                    return new ServiceResult(false, errorMessage);
                }
            }
            catch (CacheException ex)
            {
                _logger.LogError("Failed to get Temporary Session Record");
                errorMessage = _helper.GetRedisErrorMsg(
                    ex.ErrorCode, ErrorCodes.REDIS_TEMP_SESS_GET_FAILED);
                return new ServiceResult(false, errorMessage);
            }

            var scopesList = tempSession.ClientDetails.Scopes.Split(' ').ToList();

            List<ScopeDetail> scopeDetail = new List<ScopeDetail>();

            var attributeDictionary = await _userClaimService
                .GetAttributeNameDisplayNameAsync();

            if (null == attributeDictionary)
            {
                _logger.LogError("Failed to get Attribute Name Display Name Mapping");
                return new ServiceResult(false, _messageLocalizer.GetMessage(Constants.InternalError));
            }

            var attributeMandatoryDictionary = await _userClaimService
                .GetAttributeNameMandatoryAsync();

            foreach (var scope in scopesList)
            {
                var scopeInDb = await _unitOfWork.Scopes.GetScopeByNameAsync(scope);
                if (null == scopeInDb)
                {
                    _logger.LogError("Scope not found: {0}", scope);
                    return new ServiceResult(false, _messageLocalizer.GetMessage(Constants.InternalError));
                }

                var attributesList = string.IsNullOrWhiteSpace(scopeInDb?.ClaimsList)
                    ? new List<string>()
                    : scopeInDb.ClaimsList.Split(' ', StringSplitOptions.RemoveEmptyEntries).ToList();

                List<AttributeInfo> attributeDetails = new List<AttributeInfo>();

                foreach (var attribute in attributesList)
                {
                    if (attributeDictionary.ContainsKey(attribute))
                    {
                        AttributeInfo attributeInfo = new AttributeInfo()
                        {
                            Name = attribute,
                            DisplayName = _messageLocalizer.
                                GetLocalizedDisplayName(attributeDictionary[attribute]),
                            Mandatory = attributeMandatoryDictionary.ContainsKey(attribute) ?
                            attributeMandatoryDictionary[attribute] : false
                        };
                        attributeDetails.Add(attributeInfo);
                    }
                    else
                    {
                        AttributeInfo attributeInfo = new AttributeInfo()
                        {
                            Name = attribute,
                            DisplayName = attribute
                        };
                        attributeDetails.Add(attributeInfo);
                    }
                }

                ScopeDetail scopeDetails = new ScopeDetail()
                {
                    Name = scopeInDb.Name,
                    DisplayName = _messageLocalizer.GetLocalizedDisplayName
                        (scopeInDb.DisplayName,scopeInDb.DisplayNameArabic),
                    Attributes = attributeDetails
                };
                scopeDetail.Add(scopeDetails);

            }

            ConsentResponse consentResponse = new ConsentResponse()
            {
                clientId = tempSession.ClientDetails.ClientId,
                clientName = _messageLocalizer.GetLocalizedDisplayName(tempSession.ClientDetails.AppName,
                    tempSession.ClientDetails.AppNameAr),
                consentRequired= scopeDetail.Count > 0 ? true : false,
                scopes = scopeDetail
            };

            return new ServiceResult(true, _messageLocalizer.
                GetMessage(Constants.ConsentDetailsFetchedSuccessfully), consentResponse);
        }

        public async Task<ServiceResult> AuthenticateUserAsync
            (AuthenticateUserRequest request)
        {
            _logger.LogDebug("-->VerifyUserAuthData");
            // Validate input
            if (
                (null == request) ||
                (string.IsNullOrEmpty(request.SessionId)) ||
                (string.IsNullOrEmpty(request.AuthenticationScheme))
                )
            {
                _logger.LogError(Constants.InvalidArguments.En);

                return new ServiceResult(false, _messageLocalizer.
                    GetMessage(Constants.InvalidArguments));
            }

            _logger.LogInformation("Request - {0}",
     JsonConvert.SerializeObject(request)?.SanitizeForLogging());

            var errorMessage = string.Empty;
            var randomcodes = string.Empty;
            var randomcode = string.Empty;
            var verifierUrl = string.Empty;
            var userActivityType = string.Empty;

            MobileAuthTemporarySession tempSession = null;

            try
            {
                tempSession = await _cacheClient.Get<MobileAuthTemporarySession>
                    (CacheNames.MobileAuthTemporarySession,
                    request.SessionId);
                if (null == tempSession)
                {
                    _logger.LogError(Constants.TempSessionExpired.En);
                    return new ServiceResult(false, _messageLocalizer.
                        GetMessage(Constants.TempSessionExpired));
                }
            }
            catch (CacheException ex)
            {
                _logger.LogError("Failed to get Temporary Session Record");
                errorMessage = _helper.GetRedisErrorMsg(
                    ex.ErrorCode, ErrorCodes.REDIS_TEMP_SESS_GET_FAILED);
                return new ServiceResult(false, errorMessage);
            }

            if (tempSession.ClientDetails.Scopes.Contains("uaeid:idp:basic:auth"))
            {
                userActivityType = LogClientServices.IdpAuthentication;
            }
            else 
            {                 
                userActivityType = LogClientServices.IdpRegistration;
            }
            var userId = new UserTable();
            UserLookupItem userInfo = new UserLookupItem();

            userInfo.Suid = request.UserId;


            var wrongPin = false;
            var wrongCode = false;
            var denyCount = false;
            var wrongFace = false;
            var cancelledAuthN = false;
            var expiredAuthN = false;
            var rejectedAuthN = false;
            var errorAuthN = false;
            var isAuthNPassed = false;
            double pinVerifyTime = default;

            var responseMessage = string.Empty;

            if (request.AuthenticationScheme ==
                    AuthNSchemeConstants.DEVICE_AUTHENTICATION)
            {
                isAuthNPassed = true;
                _logger.LogInformation("Device Authentication Verified");
            }

            else if (request.AuthenticationScheme == AuthNSchemeConstants.UAEKYCFACE)
            {
                // Check if subscriber denied authentication
                if (!request.Approved)
                {
                    _logger.LogError(Constants.SubscriberNotApproved.En);
                    Response response = new Response();
                    response.Success = false;
                    response.Message = _messageLocalizer.
                        GetMessage(Constants.SubscriberNotApproved);
                    denyCount = true;
                    isAuthNPassed = false;
                }

                if (request.AuthenticationScheme == AuthNSchemeConstants.UAEKYCFACE)
                {
                    if (request.statusCode == FaceVerificationConstants.SUCCESS)
                    {
                        var verifyFaceResponse = await VerifyUaeKycAuthentication
                            (request.AuthenticationData);
                        if (false == verifyFaceResponse.Success)
                        {
                            if (!string.IsNullOrEmpty(verifyFaceResponse.Message))
                            {
                                _logger.LogError("Something went wrong: {0}",
                                    verifyFaceResponse.Message);
                                return new ServiceResult(false, verifyFaceResponse.Message);
                            }
                            else
                            {
                                _logger.LogError("Something went wrong: {0}",
                                        Constants.InternalError);
                                return new ServiceResult(false, _messageLocalizer.
                                    GetMessage(Constants.InternalError));
                            }
                        }
                        else
                        {
                            isAuthNPassed = true;
                            if (wrongCode == true || denyCount == true)
                            {
                                isAuthNPassed = false;
                            }
                            _logger.LogInformation("Face Verified: {0}",
                                verifyFaceResponse.Success);
                        }
                    }

                    else if (request.statusCode == FaceVerificationConstants.DOCUMENT_VERIFICATION_FAILED ||
                                request.statusCode == FaceVerificationConstants.FACE_VERIFICATION_FAILED)
                    {
                        _logger.LogInformation("Face verification failed with status code: {0}",
                            request.statusCode);
                        wrongFace = true;
                        isAuthNPassed = false;
                    }
                    else
                    {
                        if (request.statusCode == FaceVerificationConstants.CANCELLED)
                        {
                            _logger.LogInformation("Face verification cancelled by user.");
                            cancelledAuthN = true;
                            responseMessage = FaceVerificationConstants.
                                FACE_VERIFICATION_CANCELLED_MESSAGE;
                        }
                        if (request.statusCode == FaceVerificationConstants.EXPIRED)
                        {
                            _logger.LogInformation("Face verification session expired.");
                            expiredAuthN = true;
                            responseMessage = FaceVerificationConstants.
                                FACE_VERIFICATION_ERROR_MESSAGE;
                        }
                        if (request.statusCode == FaceVerificationConstants.REJECT)
                        {
                            _logger.LogInformation("Face verification rejected.");
                            rejectedAuthN = true;
                            denyCount = true;
                            responseMessage = _messageLocalizer.
                                GetMessage(Constants.SubscriberNotApproved);
                        }
                        if (request.statusCode == FaceVerificationConstants.ERROR)
                        {
                            _logger.LogInformation("Error occurred during face verification.");
                            errorAuthN = true;
                            responseMessage = FaceVerificationConstants.
                                FACE_VERIFICATION_ERROR_MESSAGE;
                        }
                        isAuthNPassed = false;
                    }
                }
            }

            else
            {
                _logger.LogError(Constants.AuthSchemeMisMatch.En);
                return new ServiceResult(false, _messageLocalizer.
                    GetMessage(Constants.AuthSchemeMisMatch));
            }

            TemporarySession temporarySession = new TemporarySession
            {
                AuthNStartTime = DateTime.Now.ToString("s"),
                CoRelationId = tempSession.CoRelationId,
                Clientdetails = new ClientDetails
                {
                    ClientId = tempSession.ClientDetails.ClientId
                }
            };

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
                    errorMessage = _helper.GetErrorMsg(ErrorCodes.DB_ERROR);
                    return new ServiceResult(false, errorMessage);
                }

                if (null == userLoginDetails)
                {
                    _logger.LogDebug("GetUserPasswordDetailAsync failed, not found");
                    var response = new VerifyUserAuthDataResponse();
                    response.Success = false;
                    response.Message = _messageLocalizer.GetMessage(Constants.InternalError);

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
                    if ((wrongPin == true || wrongFace == true))
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
                        errorMessage = _helper.GetErrorMsg(
                            ErrorCodes.DB_USER_ADD_LOGIN_DETAILS_FAILED);
                        return new ServiceResult(false, errorMessage);
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
                    if ((wrongPin == true || wrongFace == true))
                    {
                        userLoginDetails.WrongPinCount = userLoginDetails.
                            WrongPinCount + 1;
                    }
                    if (denyCount == true)
                    {
                        userLoginDetails.DeniedCount = userLoginDetails.
                            DeniedCount + 1;
                    }

                    try
                    {
                        _unitOfWork.UserLoginDetail.Update(userLoginDetails);
                        await _unitOfWork.SaveAsync();
                    }
                    catch (Exception error)
                    {
                        _logger.LogError("UserLoginDetail Update failed: {0}",
                            error.Message);
                        errorMessage = _helper.GetErrorMsg(
                            ErrorCodes.DB_USER_UPDATE_LOGIN_DETAILS_FAILED);
                        return new ServiceResult(false, errorMessage);
                    }
                }

                if ((userLoginDetails.WrongPinCount >=
                    ssoConfig.sso_config.wrong_pin)
                    || (userLoginDetails.WrongCodeCount >=
                    ssoConfig.sso_config.wrong_code)
                    || (userLoginDetails.DeniedCount >=
                    ssoConfig.sso_config.deny_count))
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
                        errorMessage = _helper.GetErrorMsg(
                            ErrorCodes.RA_USER_STATUS_UPDATE_FAILED);
                        return new ServiceResult(false, errorMessage);
                    }
                    if (false == statusResponse.success)
                    {
                        _logger.LogError("SubscriberStatusUpdate failed, " +
                            "{0}", statusResponse.message);
                        var response = new VerifyUserAuthDataResponse();
                        response.Success = false;
                        response.Message = statusResponse.message;
                        return new ServiceResult(false, statusResponse.message);
                    }
                }
            }
            if (false == isAuthNPassed)
            {
                _logger.LogError(Constants.AuthNFailed.En);
                var message = string.Empty;

                if (denyCount == true)
                {
                    _logger.LogInformation(Constants.UserDeniedAuthN.En);
                    message = _messageLocalizer.
                        GetMessage(Constants.UserDeniedAuthN);
                }
                else if (wrongCode == true)
                {
                    _logger.LogInformation(Constants.WrongCode.En);
                    message = _messageLocalizer.GetMessage(Constants.WrongCode);
                }
                else if (wrongPin == true)
                {
                    _logger.LogInformation(Constants.WrongPin.En);
                    message = _messageLocalizer.GetMessage(Constants.WrongPin);
                }
                else if (wrongFace == true)
                {
                    _logger.LogInformation(Constants.WrongFace.En);
                    message = _messageLocalizer.GetMessage(Constants.WrongFace);
                }

                else if(cancelledAuthN==true || expiredAuthN==true 
                    || rejectedAuthN==true || errorAuthN == true)
                {
                    message= responseMessage;
                }

                var (retValue1, errorMsg1) = await _cacheClient.Add
                    (CacheNames.MobileAuthTemporarySession, tempSession.TemporarySessionId,
                    tempSession);
                if (0 != retValue1)
                {
                    _logger.LogError("TemporarySession add failed");
                    errorMessage = _helper.GetRedisErrorMsg(retValue1,
                        ErrorCodes.REDIS_TEMP_SESS_ADD_FAILED);
                    return new ServiceResult(false, errorMessage);
                }

                string logMessage = string.Empty;
                string logServiceName = LogClientServices.AuthenticationFailed;
                string logServiceStatus = LogClientServices.Failure;
                bool centralLog = true;

                if (true == wrongPin)
                    logMessage = "Wrong Pin Submitted,Verification Time in seconds: "
                        + pinVerifyTime;
                else if (true == wrongCode)
                    logMessage = "Wrong Code Submitted";
                else if (true == denyCount)
                {
                    logMessage = "Subscriber Denied";
                    logServiceStatus = LogClientServices.Declined;
                }
                else if (true == wrongFace)
                {
                    logMessage = Constants.WrongFace.En;
                    logServiceStatus = LogClientServices.Failure;
                }
                else if (cancelledAuthN == true || expiredAuthN == true
                    || rejectedAuthN == true || errorAuthN == true)
                {
                    logMessage = responseMessage;
                    logServiceStatus=LogClientServices.Failure;
                }
                else
                {
                    logMessage = "Face Verified";
                    logServiceName = LogClientServices.PinVerification;
                    logServiceStatus = LogClientServices.Success;
                }

                var logResponse = _LogClient.SendAuthenticationLogMessage(
                temporarySession,
                userInfo.Suid,
                logServiceName,
                logMessage,
                logServiceStatus,
                LogClientServices.Business,
                centralLog,
                null,
                userActivityType
                );
                if (false == logResponse.Success)
                {
                    _logger.LogError("SendAuthenticationLogMessage failed: " +
                        "{0}", logResponse.Message);
                    //return logResponse;
                }

                return new ServiceResult(false, message);
            }
            if (true == isAuthNPassed)
            {
                UserLoginDetail userLoginDetail = null;
                try
                {
                    userLoginDetail = await _unitOfWork.UserLoginDetail.
                    GetUserLoginDetailAsync(userInfo.Suid.ToString());
                }
                catch (Exception)
                {
                    errorMessage = _helper.GetErrorMsg(ErrorCodes.DB_ERROR);
                    return new ServiceResult(false, errorMessage);
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
                            errorMessage = _helper.GetErrorMsg(
                            ErrorCodes.DB_USER_UPDATE_LOGIN_DETAILS_FAILED);
                            return new ServiceResult(false, errorMessage);
                        }
                    }
                }
            }

            var isExists = await _cacheClient.Exists(CacheNames.UserSessions,
                    request.UserId);
            if (CacheCodes.KeyExist == isExists.retValue)
            {
                IList<string> userSessions = null;
                try
                {
                    userSessions = await _cacheClient.Get<IList<string>>
                        (CacheNames.UserSessions, request.UserId);
                    if (userSessions == null)
                    {
                        _logger.LogError("Failed to get User Sessions");
                        errorMessage = _helper.GetErrorMsg(
                            ErrorCodes.REDIS_USER_SESS_GET_FAILED);
                        return new ServiceResult(false, errorMessage);
                    }
                }
                catch (CacheException ex)
                {
                    _logger.LogError("Failed to get User Sessions");
                    errorMessage = _helper.GetRedisErrorMsg(ex.ErrorCode,
                        ErrorCodes.REDIS_USER_SESS_GET_FAILED);
                    return new ServiceResult(false, errorMessage);
                }

                if (userSessions.Count > 0)
                {
                    var cacheres = await _cacheClient.Remove(
                        CacheNames.GlobalSession,
                        userSessions.First());
                    if (0 != cacheres.retValue)
                    {
                        _logger.LogError("GlobalSession Remove failed");
                    }

                    var res = await _cacheClient.Remove(CacheNames.UserSessions,
                        request.UserId);
                    if (0 != res.retValue)
                    {
                        _logger.LogError("UserSessions Remove failed");
                    }
                }
            }

            List<string> AcceptedAttributes= new List<string>();

            List<LogProfileInfo> logProfileInfos = new List<LogProfileInfo>();

            foreach (var scope in request.scopes)
            {
                List<string> attributesList = new List<string>();
                foreach(var attributes in scope.Attributes)
                {
                    AcceptedAttributes.Add(attributes.Name);
                    attributesList.Add(attributes.Name);
                }
                LogProfileInfo logProfileInfo = new LogProfileInfo
                {
                    Name = scope.Name,
                    Attributes = attributesList
                };
                logProfileInfos.Add(logProfileInfo);
            }

            SaveConsentRequest saveConsentRequest = new SaveConsentRequest
            {
                suid = request.UserId,
                sessionId = request.SessionId,
                scopes = logProfileInfos
            };  

            var response1 = await SaveUserScopesAsync(saveConsentRequest,request.SessionId);

            if(!response1.Success)
            {
                _logger.LogError("Failed to save consented scopes");
                return new ServiceResult(false, _messageLocalizer.GetMessage(Constants.InternalError));
            }

            var GlobalSessionId = EncryptionLibrary.KeyGenerator.GetUniqueKey();

            ClientAttributes clientAttributes = new ClientAttributes()
            {
                ClientId=tempSession.ClientDetails.ClientId,
                Attributes= AcceptedAttributes
            };

            List<ClientAttributes> clientAttributesList=new List<ClientAttributes>()
            { clientAttributes };

            GlobalSession globalSession = new GlobalSession
            {
                GlobalSessionId = GlobalSessionId,
                UserId = request.UserId,
                FullName = userInfo.Suid,
                AcceptedAttributes = clientAttributesList,
                ClientId = new List<string>() { },
                OperationsDetails = new List<OperationsDetails>() { }
            };
            
            globalSession.LoggedInTime = DateTime.UtcNow.Ticks.ToString();
            globalSession.LastAccessTime = DateTime.UtcNow.Ticks.ToString();

            var cacheAdd = await _cacheClient.Add(CacheNames.GlobalSession,
                GlobalSessionId, globalSession);
            if (0 != cacheAdd.retValue)
            {
                _logger.LogError("GlobalSession Add failed");
                errorMessage = _helper.GetRedisErrorMsg(cacheAdd.retValue,
                    ErrorCodes.REDIS_GLOBAL_SESS_ADD_FAILED);
                return new ServiceResult(false, errorMessage);
            }

            tempSession.globalSessionId = GlobalSessionId;
            tempSession.AllAuthNDone = true;

            var (retValue, errorMsg) = await _cacheClient.Add(
                CacheNames.MobileAuthTemporarySession,
                tempSession.TemporarySessionId,
                tempSession);
            if (0 != retValue)
            {
                _logger.LogError("TemporarySession add failed");
                errorMessage = _helper.GetRedisErrorMsg(retValue,
                    ErrorCodes.REDIS_TEMP_SESS_ADD_FAILED);
                return new ServiceResult(false, errorMessage);
            }

            var globalSessionList = new List<string>();
            globalSessionList.Add(GlobalSessionId);

            cacheAdd = await _cacheClient.Add(CacheNames.UserSessions,
                request.UserId,
                globalSessionList);
            if (0 != cacheAdd.retValue)
            {
                _logger.LogError("UserSessions Add failed");
                errorMessage = _helper.GetRedisErrorMsg(cacheAdd.retValue,
                    ErrorCodes.REDIS_USER_SESS_ADD_FAILED);
                return new ServiceResult(false, errorMessage);
            }

            if (isAuthNPassed)
            {
                string logMessage = "Authentication Successfully Completed";
                var scopes = tempSession.ClientDetails.Scopes.Split(new char[] { ' ', '\t' });

                var scopesList = await _scopeService.ListScopeAsync();

                List<ScopeInfoLog> scopeInfoLogs = new List<ScopeInfoLog>();

                foreach (var scope in scopes)
                {
                    var scopeDetail = scopesList.FirstOrDefault
                        (s => s.Name.Equals(scope, StringComparison.OrdinalIgnoreCase));
                    if (scopeDetail != null)
                    {
                        ScopeInfoLog scopeInfoLog = new ScopeInfoLog
                        {
                            Name = scopeDetail.Name,
                            DisplayName = scopeDetail.Description,
                            Version = scopeDetail.Version
                        };
                        scopeInfoLogs.Add(scopeInfoLog);
                    }
                }

                var logResponse = _LogClient.SendAuthenticationLogMessage(
                        temporarySession,
                        request.UserId,
                        LogClientServices.AuthenticationSuccess,
                        logMessage,
                        LogClientServices.Success,
                        LogClientServices.Business,
                        true,
                        JsonConvert.SerializeObject(scopeInfoLogs),
                        userActivityType
                        );
                if (false == logResponse.Success)
                {
                    _logger.LogError("SendAuthenticationLogMessage failed: " +
                        "{0}", logResponse.Message);
                }
                _logger.LogInformation(DTInternalConstants.AuthNDone);
                return new ServiceResult(true, _messageLocalizer.GetMessage(Constants.AuthNDone));
            }
            else
            {
                _logger.LogInformation(Constants.AuthNFailed.En);
                return new ServiceResult(false, _messageLocalizer.
                    GetMessage(Constants.AuthNFailed));
            }
        }

        public async Task<GetAuthZCodeResponse> GetAuthorizationCode
            (string sessionId)
        {
            _logger.LogDebug("-->GetAuthorizationCode");

            // Validate input
            if (string.IsNullOrEmpty(sessionId))
            {
                _logger.LogError(Constants.InvalidArguments.En);
                GetAuthZCodeResponse response = new GetAuthZCodeResponse();
                response.Success = false;
                response.Message = _messageLocalizer.
                    GetMessage(Constants.InvalidArguments);

                return response;
            }
            string errorMessage = string.Empty;
            MobileAuthTemporarySession mobileAuthTemporarySession;

            try
            {
                mobileAuthTemporarySession = await _cacheClient.Get<MobileAuthTemporarySession>
                    (CacheNames.MobileAuthTemporarySession, sessionId);
                if (null == mobileAuthTemporarySession)
                {
                    _logger.LogError(Constants.SessionMismatch.En);
                    GetAuthZCodeResponse response = new GetAuthZCodeResponse();
                    response.Success = false;
                    response.Message = _messageLocalizer.
                        GetMessage(Constants.SessionMismatch);
                    return response;
                }
            }
            catch (CacheException ex)
            {
                _logger.LogError("Failed to get Temporary Session");
                var response = new GetAuthZCodeResponse();
                response.Success = false;
                errorMessage = _helper.GetRedisErrorMsg(
                    ex.ErrorCode, ErrorCodes.REDIS_GLOBAL_SESS_GET_FAILED);
                response.Message = errorMessage;
                return response;
            }

            if (string.IsNullOrEmpty(mobileAuthTemporarySession.globalSessionId))
            {
                _logger.LogError("Global Session Id is null");
                GetAuthZCodeResponse response = new GetAuthZCodeResponse();
                response.Success = false;
                response.Message = "Global Session Id is null";
                return response;
            }

            if (!mobileAuthTemporarySession.AllAuthNDone)
            {
                _logger.LogError("User is not authenticated");
                GetAuthZCodeResponse response = new GetAuthZCodeResponse();
                response.Success = false;
                response.Message = "User is not authenticated";
                return response;
            }

            Client clientDetails = null;

            try
            {
                // Get client details
                clientDetails = await _unitOfWork.Client.GetClientByClientIdAsync
                (mobileAuthTemporarySession.ClientDetails.ClientId);
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
                response.Message = _messageLocalizer.
                    GetMessage(OIDCConstants.ClientNotActive);
                return response;
            }

            GlobalSession globalSession = null;
            try
            {
                // Get GlobalSession
                globalSession = await _cacheClient.Get<GlobalSession>
                    (CacheNames.GlobalSession,
                    mobileAuthTemporarySession.globalSessionId);
                if (null == globalSession)
                {
                    _logger.LogError(Constants.SessionMismatch.En);
                    GetAuthZCodeResponse response = new GetAuthZCodeResponse();
                    response.Success = false;
                    response.Message = _messageLocalizer.
                        GetMessage(Constants.SessionMismatch);
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

            long lastAccessTicks = long.Parse(globalSession.LastAccessTime);
            TimeSpan sessionTime =
                TimeSpan.FromTicks(DateTime.UtcNow.Ticks - lastAccessTicks);

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
                    res.Message = _messageLocalizer.GetMessage(Constants.InternalError);
                    return res;
                }

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
                            Response.Message = _messageLocalizer.
                                GetMessage(Constants.InternalError);
                            return Response;
                        }
                    }
                }

                GetAuthZCodeResponse response = new GetAuthZCodeResponse();
                _logger.LogError("GlobalSession expired/not exists");
                response.Success = false;
                response.Message = "GlobalSession expired/not exists";

                return response;
            }

            globalSession.LastAccessTime = DateTime.UtcNow.Ticks.ToString();

            var Scopes = mobileAuthTemporarySession.ClientDetails.Scopes.Split(new char[]
            { ' ', '\t' });

            var Responsetypes = mobileAuthTemporarySession.ClientDetails.ResponseType.Split(new char[]
            { ' ', '\t' });
            var clientResponseTypes = clientDetails.ResponseTypes.Split(new char[]
            { ' ', '\t' });

            var count = 0;

            foreach (var item in Responsetypes)
            {
                if (clientResponseTypes.Contains(item))
                {
                    count++;
                }
            }
            if (count != Responsetypes.Length)
            {
                _logger.LogError(OIDCConstants.ResponseTypeMisMatch.En);
                GetAuthZCodeResponse response = new GetAuthZCodeResponse();
                response.Success = false;
                response.Message = _messageLocalizer.
                    GetMessage(OIDCConstants.ResponseTypeMisMatch);
            }

            if (clientDetails.RedirectUri != mobileAuthTemporarySession.ClientDetails.RedirectUrl)
            {
                _logger.LogError(OIDCConstants.RedirectUriMisMatch.En);
                GetAuthZCodeResponse response = new GetAuthZCodeResponse();
                response.Success = false;
                response.Message = _messageLocalizer.
                    GetMessage(OIDCConstants.RedirectUriMisMatch);
                return response;
            }

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

            // Add globalsession to cache
            var cacheRes = await _cacheClient.Add(CacheNames.GlobalSession,
                mobileAuthTemporarySession.globalSessionId,
                globalSession);
            if (0 != cacheRes.retValue)
            {
                _logger.LogError("GlobalSession add failed");
                GetAuthZCodeResponse response = new GetAuthZCodeResponse();
                response.Success = false;
                response.Message = _messageLocalizer.
                    GetMessage(Constants.InternalError);
                return response;
            }

            // Generate authorization code
            var AuthZCodeId = EncryptionLibrary.KeyGenerator.GetUniqueKey(92);
            if (null == AuthZCodeId)
            {
                _logger.LogError("GetUniqueKey failed");
                GetAuthZCodeResponse response = new GetAuthZCodeResponse();
                response.Success = false;
                response.Message = _messageLocalizer.
                    GetMessage(Constants.InternalError);
                return response;
            }

            // Prepare authorization code object
            Authorizationcode AuthZCode = new Authorizationcode
            {
                AuthZCode = AuthZCodeId,
                GlobalSessionId = mobileAuthTemporarySession.globalSessionId,
                ClientId = mobileAuthTemporarySession.ClientDetails.ClientId,
                RedirectUrl = mobileAuthTemporarySession.ClientDetails.RedirectUrl,
                ResponseType = mobileAuthTemporarySession.ClientDetails.ResponseType,
                Scopes = mobileAuthTemporarySession.ClientDetails.Scopes
            };

            if (true == mobileAuthTemporarySession.withPkce)
            {
                if ((null == mobileAuthTemporarySession.PkceDetails.codeChallenge) ||
                            (null == mobileAuthTemporarySession.PkceDetails.codeChallengeMethod))
                {
                    _logger.LogError("No PKCE details found");
                    GetAuthZCodeResponse response = new GetAuthZCodeResponse();
                    response.Success = false;
                    response.Message = _messageLocalizer.
                        GetMessage(OIDCConstants.GrantTypeMismatch);
                    return response;
                }
                AuthZCode.withPkce = true;
                AuthZCode.PkceDetails.codeChallenge =
                    mobileAuthTemporarySession.PkceDetails.codeChallenge;
                AuthZCode.PkceDetails.codeChallengeMethod =
                    mobileAuthTemporarySession.PkceDetails.codeChallengeMethod;
            }

            // If scope has openid, validate nonce, add nonce
            if (AuthZCode.Scopes.Contains(OAuth2Constants.openid))
            {
                if (string.IsNullOrEmpty(mobileAuthTemporarySession.nonce))
                {
                    _logger.LogError("Nonce not received");
                    GetAuthZCodeResponse response = new GetAuthZCodeResponse();
                    response.Success = false;
                    response.Message = _messageLocalizer.
                        GetMessage(OIDCConstants.NonceNotReceived);
                    return response;
                }
                AuthZCode.Nonce = mobileAuthTemporarySession.nonce;
            }

            // Add authorization code in cache
            var Res = await _cacheClient.Add(CacheNames.AuthorizationCode,
                AuthZCodeId,
                AuthZCode);
            if (0 != Res.retValue)
            {
                _logger.LogError("AuthorizationCode add failed");
                GetAuthZCodeResponse response = new GetAuthZCodeResponse();
                response.Success = false;
                response.Message = _messageLocalizer.GetMessage(Constants.InternalError);
                return response;
            }

            var result = await _cacheClient.Remove(
                CacheNames.MobileAuthTemporarySession,
                sessionId);

            if (0 != result.retValue)
            {
                _logger.LogError("Temporary Session Remove failed");
                GetAuthZCodeResponse response = new GetAuthZCodeResponse();
                response.Success = false;
                response.Message = _messageLocalizer.GetMessage(Constants.InternalError);
                return response;
            }

            GetAuthZCodeResponse successResponse = new GetAuthZCodeResponse();
            // return success response
            successResponse.Success = true;
            successResponse.Message = string.Empty;
            successResponse.AuthorizationCode = AuthZCodeId;
            successResponse.RedirectUri = mobileAuthTemporarySession.
                ClientDetails.RedirectUrl;

            successResponse.State = mobileAuthTemporarySession.State;

            _logger.LogDebug("GetAuthorizationCode response: {0}",
                JsonConvert.SerializeObject(successResponse));
            _logger.LogDebug("<-- GetAuthorizationCode");
            return successResponse;
        }

        public async Task<ServiceResult> VerifyClientDetails
            (AuthorizationRequest request)
        {
            if (null == request || string.IsNullOrEmpty(request.client_id))
            {
                _logger.LogError(Constants.InvalidArguments.En);
                return new ServiceResult(false, _messageLocalizer.
                    GetMessage(Constants.InvalidArguments));
            }

            _logger.LogDebug("client id is {0}", request.client_id?.SanitizeForLogging());
            var errorMessage = string.Empty;
            var clientInDb = new Client();
            try
            {
                clientInDb = await _unitOfWork.Client.GetClientByClientIdAsync(
                    request.client_id);
                if (null == clientInDb)
                {
                    _logger.LogError(OIDCConstants.ClientNotFound.En);
                    return new ServiceResult(false, _messageLocalizer.
                        GetMessage(OIDCConstants.ClientNotFound));
                }
            }
            catch (Exception)
            {
                errorMessage = _helper.GetErrorMsg(ErrorCodes.DB_ERROR);
                Monitor.SendMessage(errorMessage);
                return new ServiceResult(false, errorMessage);
            }

            if (clientInDb.Status != StatusConstants.ACTIVE)
            {
                _logger.LogError("{0}: {1}",
                    OIDCConstants.ClientNotActive, clientInDb.Status);
                return new ServiceResult(false, _messageLocalizer.
                    GetMessage(OIDCConstants.ClientNotActive));
            }

            if (clientInDb.RedirectUri != request.redirect_uri)
            {
                _logger.LogError("{0}: {1}",
                    OIDCConstants.RedirectUriMisMatch, clientInDb.RedirectUri);
                return new ServiceResult(false, _messageLocalizer.
                    GetMessage(OIDCConstants.RedirectUriMisMatch));
            }

            var scopes = request.scope?.Split(' ', StringSplitOptions.RemoveEmptyEntries);

            if ((scopes == null) || (scopes.Contains("openid") && scopes.Length == 1))
            {
                _logger.LogError("Invalid scope request");
                return new ServiceResult(false,
                    _messageLocalizer.GetMessage(OIDCConstants.InsufficientScope));
            }

            bool openIdConnectMandatory = configuration.GetValue<bool>("OpenIdConnectMandatory");

            if(openIdConnectMandatory && (scopes == null || !scopes.Contains("openid")))
            {
                _logger.LogError("OpenID scope is mandatory but not present in the request");
                return new ServiceResult(false,
                    _messageLocalizer.GetMessage(OIDCConstants.InsufficientScope));
            }

            var requestedScopes = (request.scope ?? "")
                .Split(' ', StringSplitOptions.RemoveEmptyEntries);

            var allowedScopes = (clientInDb.Scopes ?? "")
                .Split(' ', StringSplitOptions.RemoveEmptyEntries);

            bool isValidScope = requestedScopes.All(rs => allowedScopes.Contains(rs));

            if (!isValidScope)
            {
                return new ServiceResult(false, "Invalid scope requested");
            }

            ClientDetails clientdetails = new ClientDetails
            {
                ClientId = request.client_id,
                Scopes = request.scope,
                RedirectUrl = request.redirect_uri,
                ResponseType = request.response_type,
                AppName = clientInDb.ApplicationName,
                AppNameAr=clientInDb.ApplicationNameArabic
            };

            var tempAuthNSessId = string.Empty;

            try
            {
                tempAuthNSessId = EncryptionLibrary.KeyGenerator.GetUniqueKey();
            }
            catch (Exception error)
            {
                _logger.LogError("GetUniqueKey failed: {0}", error.Message);
                errorMessage = _helper.GetErrorMsg(
                     ErrorCodes.GENERATE_UNIQUE_KEY_FAILED);
                return new ServiceResult(false, errorMessage);
            }

            MobileAuthTemporarySession mobileAuthTemporarySession =
                new MobileAuthTemporarySession
                {
                    TemporarySessionId = tempAuthNSessId,
                    ClientDetails = clientdetails,
                    State = request.state,
                    CoRelationId=Guid.NewGuid().ToString(),
                    nonce = request.nonce
                };

            var task = await _cacheClient.Add(CacheNames.MobileAuthTemporarySession,
                tempAuthNSessId, mobileAuthTemporarySession);
            if (DTInternalConstants.S_OK != task.retValue)
            {
                _logger.LogError("_cacheClient.Add failed");
                errorMessage = _helper.GetRedisErrorMsg(task.retValue,
                     ErrorCodes.REDIS_TEMP_SESS_ADD_FAILED);
                return new ServiceResult(false, errorMessage);
            }

            return new ServiceResult(true, "Temp Session Created Successfully", tempAuthNSessId);
        }

        public async Task<ServiceResult> AddTransactionLog
            (WalletTransactionRequestDTO request)
        {
            string status = string.Empty;

            CallStackObject callStackObject=new CallStackObject
            {
                presentationToken = request.presentationToken,
                profiles = request.profiles
            };

            var serializedCallStack = JsonConvert.SerializeObject(callStackObject);

            var logResponse = await _LogClient.SendWalletAuthenticationLog(
                request.suid,
                request.clientID,
                LogClientServices.walletAuthenticationLog,
                request.statusMessage,
                request.status,
                LogClientServices.Business,
                serializedCallStack,
                true,
                request.userActivityType
                );
            if (false == logResponse.Success)
            {
                _logger.LogError("Failed to send log message to central " +
                    "log server");
                return new ServiceResult(false, "Failed to send log message");
            }
            return new ServiceResult(true, "Successfully send log message");
        }

        public async Task<PaginatedList<LogReportDTO>> GetAuthenticationTransactionLog
            (string suid, int pageNumber, int perPage = 10)
        {
            HttpClient client = _httpClientFactory.CreateClient("ignoreSSL");
            client.BaseAddress = new Uri(configuration["APIServiceLocations:AuthenticationTransactionsBaseAddress"]);

            try
            {
                string json = JsonConvert.SerializeObject(
                    new
                    {
                        identifier = suid,
                        transactionStatus = "BOTH",
                        perPage = 10
                    }, new JsonSerializerSettings { ContractResolver = new CamelCasePropertyNamesContractResolver() });

                StringContent content = new StringContent(json, Encoding.UTF8, "application/json");

                using (HttpResponseMessage response = await client.PostAsync($"api/audit-logs/wallet/authentication/{pageNumber}", content))
                {
                    if (response.StatusCode == HttpStatusCode.OK)
                    {
                        using (Stream stream = await response.Content.ReadAsStreamAsync())
                        {
                            using (StreamReader streamReader = new StreamReader(stream))
                            {
                                using (JsonReader reader = new JsonTextReader(streamReader))
                                {
                                    JsonSerializer serializer = new JsonSerializer();
                                    APIResponse apiResponse = serializer.Deserialize<APIResponse>(reader);
                                    if (apiResponse.Success)
                                    {
                                        JObject result = (JObject)JToken.FromObject(apiResponse.Result);
                                        var logs = JsonConvert.DeserializeObject<IEnumerable<LogReportDTO>>(result["data"].ToString());
                                        return new PaginatedList<LogReportDTO>(logs, Convert.ToInt32(result["currentPage"]),
                                            perPage, Convert.ToInt32(result["totalPages"]), Convert.ToInt32(result["totalCount"]));
                                    }
                                    else
                                    {
                                        _logger.LogError(apiResponse.Message);
                                    }
                                }
                            }
                        }
                    }
                    else
                    {
                        _logger.LogError($"The request with URI={response.RequestMessage.RequestUri} failed " +
                                   $"with status code={response.StatusCode}");
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
            }

            return null;
        }

        public async Task<ServiceResult> GetServiceProviderAppDetails
            (string token)
        {
            try
            {
                Accesstoken accessToken = null;
                try
                {
                    accessToken = await _cacheClient.Get<Accesstoken>("AccessToken",
                        token);
                    if (null == accessToken)
                    {
                        _logger.LogError("Access token not recieved from cache." +
                            "Expired or Invalid access token");
                        return new ServiceResult(false, "UnAuthorized");
                    }
                }
                catch (CacheException ex)
                {
                    _logger.LogError("Failed to get Access Token Record");
                    ErrorResponseDTO error = new ErrorResponseDTO();
                    error.error = "Internal Error";
                    error.error_description = _helper.GetRedisErrorMsg(
                        ex.ErrorCode, ErrorCodes.REDIS_ACCESS_TOKEN_GET_FAILED);
                    return new ServiceResult(false, "Internal Error" + error.error_description);
                }

                var clientId = accessToken.ClientId;

                var clientInDb = await _unitOfWork.Client.GetClientByClientIdAsync(
                    clientId);
                if (null == clientInDb)
                {
                    _logger.LogError(OIDCConstants.ClientNotFound.En);
                    return new ServiceResult(false, _messageLocalizer.
                        GetMessage(OIDCConstants.ClientNotFound));
                }
                var data = new
                {
                    ServiceProviderAppName = clientInDb.ApplicationName,
                    ServiceProviderRedirectUrl = clientInDb.RedirectUri
                };
                return new ServiceResult(true, "Success", data);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return new ServiceResult(false, "Internal Error");
            }
        }

        public async Task<ServiceResult> SaveConsentAsync
            (ConsentApprovalRequest request)
        {
            var userConsent = new UserConsent();

            List<approved_scopes> approvedScopes = new List<approved_scopes>();

            var scopesListInDb = await _unitOfWork.Scopes.ListAllScopeAsync();

            foreach (var item in request.scopes)
            {
                var scopeInDb = scopesListInDb.Where(x => x.Name == item.Name).FirstOrDefault();

                if (null == scopeInDb)
                {
                    return new ServiceResult(false, "Failed to Get Scope");
                }

                approved_scopes scope = new approved_scopes
                {
                    scope = item.Name,
                    permission = true,
                    version = scopeInDb != null ? scopeInDb.Version : string.Empty,
                    created_date = DateTime.UtcNow.ToString(),
                    attributes = item.Attributes
                };
                approvedScopes.Add(scope);
            }

            try
            {
                userConsent = await _unitOfWork.UserConsent.GetUserConsent(
                    request.suid,
                    request.clientId);

                if (null == userConsent)
                {
                    UserConsent consent = new UserConsent
                    {
                        Suid = request.suid,
                        ClientId = request.clientId,
                        Scopes = JsonConvert.SerializeObject(approvedScopes),
                        CreatedDate = DateTime.Now,
                        ModifiedDate = DateTime.Now
                    };

                    var addConsent = await _userConsentService.AddUserConsentAsync(consent);

                    return new ServiceResult(addConsent.Success, addConsent.Message);

                }
            }
            catch (Exception error)
            {
                _logger.LogError("GetUserConsent:{0}", error.Message);
                ServiceResult response = new ServiceResult(false, "Internal Error");
                return response;
            }

            var userScopes = JsonConvert.DeserializeObject<List<approved_scopes>>
            (userConsent.Scopes);

            if (null == userScopes)
            {
                _logger.LogError("DeserializeObject failed");
                return new ServiceResult(false, "Failed to Get Scope");
            }
            foreach (var item in approvedScopes)
            {
                userScopes.Add(item);
            }

            userConsent.Scopes = JsonConvert.SerializeObject(userScopes);

            userConsent.ModifiedDate = DateTime.Now;

            var updateConsent = await _userConsentService.UpdateUserConsentAsync(userConsent);

            return new ServiceResult(updateConsent.Success, updateConsent.Message);

        }

        public async Task<ServiceResult> GetLogDetailsAsync
        (string identifier)

        {
            HttpClient client = _httpClientFactory.CreateClient("ignoreSSL");
            client.BaseAddress = new Uri(configuration["APIServiceLocations:AuthenticationTransactionsBaseAddress"]);
            try
            {
                HttpResponseMessage response = await client.GetAsync($"api/getLog?logId={identifier}");
                if (response.StatusCode == HttpStatusCode.OK)
                {
                    APIResponse apiResponse = JsonConvert.DeserializeObject<APIResponse>(await response.Content.ReadAsStringAsync());
                    if (apiResponse.Success && apiResponse.Result != null )
                    {
                        var logMessage = JsonConvert.DeserializeObject<Utilities.LogMessage>(apiResponse.Result.ToString());
                        return new ServiceResult(true, apiResponse.Message, logMessage);
                    }
                    else
                    {
                        _logger.LogError(apiResponse.Message);
                        return new ServiceResult(false, apiResponse.Message);
                    }
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

        public async Task<ServiceResult> GetScopeDetailsAsync
            (string callStack,string ServiceName,string ActivityType)
        {
            try
            {
                if (callStack == null)
                {
                    return new ServiceResult(false,_messageLocalizer.
                        GetMessage(Constants.InvalidArguments));
                }

                if (ServiceName == LogClientServices.walletAuthenticationLog)
                {
                    var logMessage = JsonConvert.DeserializeObject<CallStackObject>(callStack);

                    logMessage.ActivityType=ActivityType;
                    
                    return new ServiceResult(true, _messageLocalizer.GetMessage(Constants.GetLogSuccess),
                        logMessage);
                }

                LogDetailsObject logDetailsObject=new LogDetailsObject();

                List<LogProfileInfo> scopeDetailsList = JsonConvert.DeserializeObject
                    <List<LogProfileInfo>>(callStack);

                List<ProfileInfo> profileInfo = new List<ProfileInfo>();

                var attributeDictionary = await _userClaimService
                    .GetAttributeNameDisplayNameAsync();

                var scopesDictionary = await _scopeService.
                    GetScopeNameDisplayNameAsync();

                foreach (var profile in scopeDetailsList)
                {
                    var scope = scopesDictionary[profile.Name];
                    ProfileInfo profileInfo1 = new ProfileInfo()
                    {
                        Name = profile.Name,
                        DisplayName = _messageLocalizer.GetLocalizedDisplayName(scope)
                    };

                    List<ClaimsDetail> claimDetailList = new List<ClaimsDetail>();

                    foreach(var attribute in profile.Attributes)
                    {
                        var attributeInfo = attributeDictionary[attribute];

                        ClaimsDetail claimDetail = new ClaimsDetail()
                        {
                            Name = attribute,
                            DisplayName = _messageLocalizer.GetLocalizedDisplayName(attributeInfo)
                        };

                        claimDetailList.Add(claimDetail);
                    }
                    profileInfo1.Attributes=claimDetailList;

                    profileInfo.Add(profileInfo1);
                }

                logDetailsObject.ActivityType = ActivityType;
                logDetailsObject.Profiles = profileInfo;

                return new ServiceResult(true, _messageLocalizer.GetMessage(Constants.GetLogSuccess),
                    logDetailsObject);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return new ServiceResult(false, _messageLocalizer.GetMessage(Constants.InternalError));
            }
        }

        public async Task<Response> VerifyUAEKycFace
            (string journeyToken, string status, string message)
        {
            var journeyDetails = await GetJourneyDetailsAsync(journeyToken);
            if (journeyDetails == null)
            {
                _logger.LogError("Failed to retrieve ICP journey details for token: {0}", journeyToken?.SanitizeForLogging());
                return new Response
                {
                    Success = false,
                    Message = "Failed to retrieve journey details"
                };
            }

            var isFaceMatchSuccessful = IsIcpFaceMatchSuccessful(journeyDetails);

            if (isFaceMatchSuccessful)
            {
                return new Response
                {
                    Success = true,
                    Message = DTInternalConstants.AuthNDone
                };
            }
            else
            {
                return new Response
                {
                    Success = false,
                    Message = _messageLocalizer.
                    GetMessage(Constants.FaceVerifyFailed)
                };
            }

        }

        public async Task<Response> VerifyUaeKycAuthentication(
            string authenticationData)
        {
            try
            {
                string journeyToken = authenticationData;

                if (configuration.GetValue<bool>("VerifyJourneyToken") == true)
                {
                    var response = await VerifyUAEKycFace(journeyToken);

                    return response;
                }

                return new Response()
                {
                    Success = true,
                    Message = _messageLocalizer.GetMessage(Constants.AuthNDone)
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return new Response()
                {
                    Success = false,
                    Message = ex.Message
                };
            }
        }

        public ServiceResult AddAuthenticationTransactionLog
            (Utilities.LogMessage request)
        {
            string status = string.Empty;

            TemporarySession temporarySession = new TemporarySession()
            {
                CoRelationId=new Guid().ToString(),
                AuthNStartTime=DateTime.Now.ToString("s"),
                Clientdetails=new ClientDetails()
                {
                    ClientId=request.serviceProviderAppName,
                }
            };
            var logResponse = _LogClient.SendAuthenticationLogMessage(
                temporarySession,
                request.identifier,
                request.serviceName,
                request.logMessage,
                request.logMessageType,
                LogClientServices.Business,
                true,
                request.callStack
                );
            if (false == logResponse.Success)
            {
                _logger.LogError("Failed to send log message to central " +
                    "log server");
                return new ServiceResult(false, "Failed to send log message");
            }
            return new ServiceResult(true, "Successfully send log message");
        }

        public async Task<ServiceResult> GetTransactionLogCount(string Id)
        {
            HttpClient client = _httpClientFactory.CreateClient("ignoreSSL");
            client.BaseAddress = new Uri(configuration["APIServiceLocations:AuthenticationLogsBaseAddress"]);
            try
            {
                HttpResponseMessage response = await client.GetAsync($"api/get/authentication/count?identifier={Id}");
                if (response.StatusCode == HttpStatusCode.OK)
                {
                    APIResponse apiResponse = JsonConvert.DeserializeObject<APIResponse>(await response.Content.ReadAsStringAsync());
                    if (apiResponse.Success)
                    {
                        return new ServiceResult(true, apiResponse.Message, apiResponse.Result);
                    }
                    else
                    {
                        _logger.LogError(apiResponse.Message);
                        return new ServiceResult(false, apiResponse.Message);
                    }
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
            return new ServiceResult(false, "Failed to retrieve transaction log count");
        }

        public async Task<ServiceResult> SaveUserScopesAsync
            (SaveConsentRequest request, string SessionId)
        {
            try
            {
                MobileAuthTemporarySession globalSession = null;
                string errorMessage = string.Empty;

                try
                {
                    // Get GlobalSession
                    globalSession = await _cacheClient.Get<MobileAuthTemporarySession>
                        (CacheNames.MobileAuthTemporarySession,
                        SessionId);
                    if (null == globalSession)
                    {
                        _logger.LogError(Constants.SessionMismatch.En);
                        return new ServiceResult(false,_messageLocalizer.
                            GetMessage(Constants.SessionMismatch));
                    }
                }
                catch (CacheException ex)
                {
                    _logger.LogError("Failed to get Global Session Record");
                    errorMessage = _helper.GetRedisErrorMsg(
                        ex.ErrorCode, ErrorCodes.REDIS_GLOBAL_SESS_GET_FAILED);
                    return new ServiceResult(false, errorMessage);
                }

                List<string> AcceptedAttributes = new List<string>();

                foreach (var scope in request.scopes)
                {
                    foreach (var attribute in scope.Attributes)
                    {
                        if (!AcceptedAttributes.Contains(attribute))
                        {
                            AcceptedAttributes.Add(attribute);
                        }
                    }
                }

                if (globalSession.AcceptedAttributes == null)
                {
                    globalSession.AcceptedAttributes = AcceptedAttributes;
                }
                else
                {
                    globalSession.AcceptedAttributes.AddRange(AcceptedAttributes);
                }

                var cacheAdd = await _cacheClient.Add(CacheNames.MobileAuthTemporarySession,
                        SessionId, globalSession);
                if (0 != cacheAdd.retValue)
                {
                    _logger.LogError("GlobalSession Add failed");
                    errorMessage = _helper.GetRedisErrorMsg(cacheAdd.retValue,
                        ErrorCodes.REDIS_GLOBAL_SESS_ADD_FAILED);
                    return new ServiceResult(false, errorMessage);
                }

                string logMessage = "Consent Approved";
                string logServiceName = LogClientServices.Other;
                string logServiceStatus = LogClientServices.Success;
                bool centralLog = false;

                TemporarySession tempSession = new TemporarySession()
                {
                    Clientdetails = new ClientDetails()
                    {
                        ClientId = globalSession.ClientDetails.ClientId,
                    },
                    CoRelationId = globalSession.CoRelationId,
                    AuthNStartTime = DateTime.Now.ToString(),
                };

                var logResponse = _LogClient.SendAuthenticationLogMessage(
                    tempSession,
                    request.suid,
                    logServiceName,
                    logMessage,
                    logServiceStatus,
                    LogClientServices.Business,
                    centralLog,
                    JsonConvert.SerializeObject(request.scopes)
                );

                if (false == logResponse.Success)
                {
                    _logger.LogError("SendAuthenticationLogMessage failed: " +
                        "{0}", logResponse.Message);
                }

                return new ServiceResult(true, "Consent Saved Successfully");
            }
            catch (Exception error)
            {
                _logger.LogError("GetUserConsentAsync:{0}", error.Message);
                return new ServiceResult(false, "Internal Error");
            }
        }

        public async Task<ServiceResult> GetAuthenticationDetailsAsync
            (string CorrelationId,string ClientId)
        {
            var reports = await _reportsService.
                GetDigitalAuthenticationLogReportByCorrelationIDAsync(CorrelationId);

            if (reports == null)
            {
                return new ServiceResult(false, "Log Not Found");
            }

            LogReportDTO logReport = null;

            foreach(var report in reports)
            {
                if (report.ServiceName == LogClientServices.Other &&
                    report.ServiceProviderAppName==ClientId)
                {
                    logReport = report;
                }
            }
            if (logReport == null)
            {
                return new ServiceResult(false, "Log Not Found");
            }
            return new ServiceResult(true, "Success", logReport);
        }
    }
}
