using DTPortal.Common;
using DTPortal.Core.Constants;
using DTPortal.Core.Domain.Models;
using DTPortal.Core.Domain.Models.RegistrationAuthority;
using DTPortal.Core.Domain.Repositories;
using DTPortal.Core.Domain.Services;
using DTPortal.Core.Domain.Services.Communication;
using DTPortal.Core.Utilities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using DTPortal.Core.DTOs;
using DTPortal.Core.Exceptions;
using System.IO;
using Microsoft.AspNetCore.Hosting;
using System.Globalization;
using static System.Net.WebRequestMethods;
using static DTPortal.Common.CommonResponse;
using System.Net;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Serialization;
//using static System.Formats.Asn1.AsnWriter;

namespace DTPortal.Core.Services
{
    public class UserProfileService : IUserProfileService
    {
        // Initialize logger
        private readonly ILogger<UserInfoService> _logger;
        // Initialize Db
        private readonly IUnitOfWork _unitOfWork;
        private readonly ICacheClient _cacheClient;
        //private readonly ITokenManager _tokenManager;
        private readonly idp_configuration idpConfiguration;
        private readonly IGlobalConfiguration _globalConfiguration;
        private readonly OpenIdConnect openidconnect;
        private readonly OIDCConstants OIDCConstants;
        private readonly IConfiguration configuration;
        private readonly IClientService _clientService;
        private readonly IPushNotificationClient _pushNotificationClient;
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IPurposeService _purposeService;
        //private readonly ITokenManagerService _tokenManagerService;
        private readonly ITransactionProfileRequestService _transactionProfileRequestService;
        private readonly ITransactionProfileConsentService _transactionProfileConsentService;
        private readonly ITransactionProfileStatusService _transactionProfileStatusService;
        private readonly IAttributeServiceTransactionsService _attributeServiceTransactionsService;
        private readonly IScopeService _scopeService;
        private readonly IHelper _helper;
        private readonly IWebHostEnvironment _environment;
        private readonly IUserClaimService _userClaimService;
        private readonly IMessageLocalizer _messageLocalizer;   
        public UserProfileService(ILogger<UserInfoService> logger,
            IUnitOfWork unitofWork, ICacheClient cacheClient,
            //ITokenManager tokenManager,
            IGlobalConfiguration globalConfiguration, IConfiguration Configuration,
            IClientService clientService,
            IPushNotificationClient pushNotificationClient,
            IHttpClientFactory httpClientFactory,
            IPurposeService purposeService,
            //ITokenManagerService tokenManagerService,
            ITransactionProfileRequestService transactionProfileRequestService,
            ITransactionProfileConsentService transactionProfileConsentService,
            ITransactionProfileStatusService transactionProfileStatusService,
            IAttributeServiceTransactionsService attributeServiceTransactionsService,
            IScopeService scopeService,
            IHelper helper,
            IWebHostEnvironment environment,
            IEConsentService econsentService,
            IUserClaimService userClaimService,
            IMessageLocalizer messageLocalizer
            )
        {
            _logger = logger;
            _unitOfWork = unitofWork;
            _cacheClient = cacheClient;
            //_tokenManager = tokenManager;
            _globalConfiguration = globalConfiguration;
            configuration = Configuration;
            _clientService = clientService;
            _pushNotificationClient = pushNotificationClient;
            _httpClientFactory = httpClientFactory;
            _purposeService = purposeService;
            //_tokenManagerService = tokenManagerService;
            _transactionProfileConsentService = transactionProfileConsentService;
            _transactionProfileRequestService = transactionProfileRequestService;
            _transactionProfileStatusService = transactionProfileStatusService;
            _attributeServiceTransactionsService = attributeServiceTransactionsService;
            _scopeService = scopeService;
            _helper = helper;
            _environment = environment;
            _userClaimService = userClaimService;
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

            openidconnect = JsonConvert.DeserializeObject<OpenIdConnect>
                (idpConfiguration.openidconnect.ToString());
            //_econsentService = econsentService;
        }
        public async Task<TransactionProfileRequestResponse> AddTransactionProfileRequest(string transactionId, int clientId, string Suid, string requestDetails)
        {
            try
            {
                TransactionProfileRequest transactionProfileRequest = new TransactionProfileRequest()
                {
                    TransactionId = transactionId,
                    ClientId = clientId,
                    Suid = Suid,
                    RequestDetails = requestDetails,
                    CreatedDate = DateTime.Now
                };
                var response = await _transactionProfileRequestService.AddProfileRequest(transactionProfileRequest);
                return response;
            }
            catch (Exception ex)
            {
                _logger.LogError("AddTransactionProfileRequest Failed   " + ex.Message);
                return null;
            }
        }
        public async Task<TransactionProfileConsentResponse> AddTransactionProfileConsent(int transactionId, string ConsentStatus, string ConsentDataSignature, string RequestedProfileAttributes, string approvedAttributes)
        {
            try
            {
                TransactionProfileConsent transactionProfileConsent = new TransactionProfileConsent()
                {
                    TransactionId = transactionId,
                    ConsentStatus = ConsentStatus,
                    ConsentDataSignature = ConsentDataSignature,
                    RequestedProfileAttributes = RequestedProfileAttributes,
                    CreatedDate = DateTime.Now,
                    ApprovedProfileAttributes = approvedAttributes
                };
                var response = await _transactionProfileConsentService.AddProfileConsent(transactionProfileConsent);
                return response;
            }
            catch (Exception ex)
            {
                _logger.LogError("Add transactionProfileConsent Failed   " + ex.Message);
                return null;
            }
        }
        public async Task<TransactionProfileStatusResponse> AddTransactionProfileStatus(int transactionId, string TransactionStatus, string FailedReason, string PivotSignedConsent, int DatapivotId)
        {
            try
            {
                TransactionProfileStatus transactionProfileStatus = new TransactionProfileStatus()
                {
                    TransactionId = transactionId,
                    TransactionStatus = TransactionStatus,
                    FailedReason = FailedReason,
                    PivotSignedConsent = PivotSignedConsent,
                    CreatedDate = DateTime.Now,
                    DatapivotId = DatapivotId
                };
                var response = await _transactionProfileStatusService.AddProfileStatus(transactionProfileStatus);
                return response;
            }
            catch (Exception ex)
            {
                _logger.LogError("Add TransactionProfileStatus Failed   " + ex.Message);
                return null;
            }
        }
        public async Task<TransactionProfileConsentResponse> UpdateTransactionProfileConsent(int transactionId, string approvedAttributes, string ConsentStatus)
        {
            try
            {
                var transactionprofileConsent = new TransactionProfileConsent()
                {
                    TransactionId = transactionId,
                    UpdatedDate = DateTime.Now,
                    ConsentStatus = ConsentStatus,
                    ApprovedProfileAttributes = approvedAttributes
                };
                var response = await _transactionProfileConsentService.UpdateTransactionProfileConsent(transactionprofileConsent);
                return response;
            }
            catch (Exception ex)
            {
                _logger.LogError("Update TransactionProfileConsent Failed   " + ex.Message);
                return null;
            }
        }
        [NonAction]
        public bool ValidateScopes(string requestScopes, string clientScopes)
        {
            var Scopes = requestScopes.Split(new char[] { ' ', '\t' });
            var clientcopes = clientScopes.Split(new char[] { ' ', '\t' });
            var count = 0;
            foreach (var item in Scopes)
            {
                if (clientcopes.Contains(item))
                {
                    count++;
                }
            }
            if (count != Scopes.Length)
            {

                return false;
            }

            return true;
        }

        [NonAction]
        public bool ValidateProfiles(string requestScopes, string clientScopes)
        {
            var Scopes = requestScopes.Split(new char[] { ',', '\t' });
            var clientcopes = clientScopes.Split(new char[] { ',', '\t' });
            var count = 0;
            foreach (var item in Scopes)
            {
                if (clientcopes.Contains(item))
                {
                    count++;
                }
            }
            if (count != Scopes.Length)
            {

                return false;
            }

            return true;
        }

        public async Task<ServiceResult> GetNiraToken()
        {
            var client = new HttpClient();

            var url = "https://api.ugpass.go.ug/nira-api/login";

            client.DefaultRequestHeaders.Add("daes_authorization", "VUpneWQ3OEp9eVMvKV1WOkxKTEtoakBxZjllSlFrSA==");

            client.DefaultRequestHeaders.Add("identifier", "abcd");

            var response = await client.GetAsync(url);

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
                _logger.LogError($"The request with uri={response.RequestMessage.RequestUri} failed " +
                   $"with status code={response.StatusCode}");
                return new ServiceResult(false, "Internal Error");
            }

        }
        public async Task<ServiceResult> GetUserDetailsNira(string userId)
        {
            var tokenResponse = await GetNiraToken();
            if (!tokenResponse.Success)
            {
                return tokenResponse;
            }
            var tokenResult = JsonConvert.DeserializeObject<TokenResponseDTO>(tokenResponse.Resource.ToString());

            var client = new HttpClient();

            var url = $"https://api.ugpass.go.ug/nira-api/profile/{userId}";

            client.DefaultRequestHeaders.Add("daes_authorization", "VUpneWQ3OEp9eVMvKV1WOkxKTEtoakBxZjllSlFrSA==");

            client.DefaultRequestHeaders.Add("identifier", "abcd");

            client.DefaultRequestHeaders.Add("access_token", tokenResult.access_token);

            var response = await client.GetAsync(url);

            if (response.StatusCode == HttpStatusCode.OK)
            {
                APIResponse apiResponse = JsonConvert.DeserializeObject<APIResponse>(await response.Content.ReadAsStringAsync());
                if (apiResponse.Success)
                {
                    var niraResponse = JsonConvert.DeserializeObject<NiraResponseDTO>(apiResponse.Result.ToString());
                    return new ServiceResult(true, apiResponse.Message, niraResponse);
                }
                else
                {
                    _logger.LogError(apiResponse.Message);
                    return new ServiceResult(false, apiResponse.Message);
                }
            }
            else
            {
                _logger.LogError($"The request with uri={response.RequestMessage.RequestUri} failed " +
                  $"with status code={response.StatusCode}");
                return new ServiceResult(false, "Internal Error");
            }
        }
        public bool CheckPurposes(string requestPurposes, IEnumerable<Purpose> PurposeListinDb)
        {
            var requestPurposeList = requestPurposes.Split(new char[] { ',' });
            foreach (var purpose in requestPurposeList)
            {
                foreach (var purposes in PurposeListinDb)
                {
                    var purposeId = purposes.Id.ToString();
                    if (purposeId.Equals(purpose))
                    {
                        if (purposes.UserConsentRequired)
                        {
                            return true;
                        }
                    }
                }
            }
            return false;
        }
        public bool ValidatePurposes(string requestPurposes, IEnumerable<string> clientPurposesinDb)
        {
            var purposes = requestPurposes.Split(new char[] { ',' });

            var count = 0;

            foreach (var purpose in purposes)
            {
                if (clientPurposesinDb.Contains(purpose))
                {
                    count++;
                }
            }
            if (purposes.Length == count)
            {
                return true;
            }
            return false;
        }

        public List<string> GetClaimsList(string[] scopesList, IEnumerable<Scope> scopesinDb)
        {
            List<string> claimsList = new List<string>();
            string[] claims;
            foreach (var scope in scopesList)
            {
                foreach (var scopes in scopesinDb)
                {
                    if (scopes.Name.Equals(scope))
                    {
                        if (scopes.IsClaimsPresent)
                        {
                            claims = scopes.ClaimsList.Split(new char[] { ' ', '\t' });
                            claimsList.AddRange(claims);
                        }
                    }
                }
            }
            return claimsList;
        }
        public async Task<string> GetSubscriberPhoto(string url)
        {
            _logger.LogDebug("-->GetSubscriberPhoto");
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
        public async Task<UserData> GetUserBasicDataAsync(
            string UserId, string UserIdType)
        {
            var raSubscriber = new SubscriberView();

            switch (UserIdType)
            {
                case "3":
                    {
                        raSubscriber = await _unitOfWork.Subscriber.
                            GetSubscriberInfoByEmail(UserId);
                        if (null == raSubscriber)
                        {
                            _logger.LogError("Subscriber details not found");
                            return null;
                        }
                        break;
                    }
                case "4":
                    {
                        raSubscriber = await _unitOfWork.Subscriber.
                            GetSubscriberInfoByPhone(UserId);
                        if (null == raSubscriber)
                        {
                            _logger.LogError("Subscriber details not found");
                            return null;
                        }
                        break;
                    }
                case "2":
                    {
                        raSubscriber = await _unitOfWork.Subscriber.
                            GetSubscriberInfobyDocType(UserId);
                        if (null == raSubscriber)
                        {
                            _logger.LogError("Subscriber details not found");
                            return null;
                        }
                        break;
                    }
                case "1":
                    {
                        raSubscriber = await _unitOfWork.Subscriber.
                            GetSubscriberInfobyDocType(UserId);
                        if (null == raSubscriber)
                        {
                            _logger.LogError("Subscriber details not found");
                            return null;
                        }
                        break;
                    }
                case "5":
                    {
                        raSubscriber = await _unitOfWork.Subscriber.
                            GetSubscriberInfoBySUID(UserId);
                        if (null == raSubscriber)
                        {
                            _logger.LogError("Subscriber details not found");
                            return null;
                        }
                        break;
                    }
                default:
                    {
                        _logger.LogError("Incorrect input received");
                        return null;
                    };
            }

            DateTime? dob = null;
            if (DateTime.TryParse(raSubscriber.DateOfBirth, out DateTime parsed))
            {
                dob = parsed;
            }

            var userBasicProfile = new UserData()
            {
                CertificateStatus = raSubscriber.CertificateStatus,
                Country = raSubscriber.Country,
                DateOfBirth = dob,
                DisplayName = raSubscriber.DisplayName,
                Email = raSubscriber.Email,
                FcmToken = raSubscriber.FcmToken,
                Gender = raSubscriber.Gender,
                IdDocNumber = raSubscriber.IdDocNumber,
                Loa = raSubscriber.Loa,
                MobileNumber = raSubscriber.MobileNumber,
                SubscriberStatus = raSubscriber.SubscriberStatus,
                SubscriberUid = raSubscriber.SubscriberUid
            };

            if (raSubscriber.IdDocType == "1")
            {
                userBasicProfile.IdDocType = "National ID";
            }
            if (raSubscriber.IdDocType == "3")
            {
                userBasicProfile.IdDocType = "Passport";
            }
            try
            {
                var response = await GetSubscriberPhoto(raSubscriber.SelfieUri);
                if (null == response)
                {
                    _logger.LogError("Failed to get Subscriber Photo");
                    //return null;
                }
                userBasicProfile.Photo = response;
            }
            catch (Exception)
            {

            }

            return userBasicProfile;
        }
        public async Task<UserBasicProfile> GetUserBasicProfileAsync(
            string UserId, string UserIdType, ProfileConfig config)
        {
            var raSubscriber = new SubscriberView();

            switch (UserIdType)
            {
                case "EMAIL":
                    {
                        raSubscriber = await _unitOfWork.Subscriber.
                            GetSubscriberInfoByEmail(UserId);
                        if (null == raSubscriber)
                        {
                            _logger.LogError("Subscriber details not found");
                            return null;
                        }
                        break;
                    }
                case "PHONE_NUMBER":
                    {
                        raSubscriber = await _unitOfWork.Subscriber.
                            GetSubscriberInfoByPhone(UserId);
                        if (null == raSubscriber)
                        {
                            _logger.LogError("Subscriber details not found");
                            return null;
                        }
                        break;
                    }
                case "PASSPORT":
                    {
                        raSubscriber = await _unitOfWork.Subscriber.
                            GetSubscriberInfobyDocType(UserId);
                        if (null == raSubscriber)
                        {
                            _logger.LogError("Subscriber details not found");
                            return null;
                        }
                        break;
                    }
                case "CARD_NUMBER":
                    {
                        raSubscriber = await _unitOfWork.Subscriber.
                            GetSubscriberInfobyDocType(UserId);
                        if (null == raSubscriber)
                        {
                            _logger.LogError("Subscriber details not found");
                            return null;
                        }
                        break;
                    }
                case "SUID":
                    {
                        raSubscriber = await _unitOfWork.Subscriber.
                            GetSubscriberInfoBySUID(UserId);
                        if (null == raSubscriber)
                        {
                            _logger.LogError("Subscriber details not found");
                            return null;
                        }
                        break;
                    }
                default:
                    {
                        _logger.LogError("Incorrect input received");
                        return null;
                    };
            }

            DateTime? dob = null;

            if (DateTime.TryParse(raSubscriber.DateOfBirth, out DateTime parsedDob))
            {
                dob = parsedDob;
            }

            var userBasicProfile = new UserBasicProfile()
            {
                CertificateStatus = raSubscriber.CertificateStatus,
                Country = raSubscriber.Country,
                DateOfBirth = dob, // ✅ safely assigned after parsing
                DisplayName = raSubscriber.DisplayName,
                Email = raSubscriber.Email,
                FcmToken = raSubscriber.FcmToken,
                Gender = raSubscriber.Gender,
                IdDocNumber = raSubscriber.IdDocNumber,
                //IdDocType = raSubscriber.IdDocType,
                Loa = raSubscriber.Loa,
                MobileNumber = raSubscriber.MobileNumber,
                SubscriberStatus = raSubscriber.SubscriberStatus,
                SubscriberUid = raSubscriber.SubscriberUid
            };

            if (raSubscriber.IdDocType == "1")
            {
                userBasicProfile.IdDocType = "National ID";
            }
            if (raSubscriber.IdDocType == "3")
            {
                userBasicProfile.IdDocType = "Passport";
            }
            try
            {
                var response = await GetSubscriberPhoto(raSubscriber.SelfieUri);
                //if (null == response)
                //{
                //    _logger.LogError("Failed to get Subscriber Photo");
                //    return null;
                //}
                userBasicProfile.Photo = response;
            }
            catch (Exception)
            {

            }

            var subscriberCard = await _unitOfWork.SubscriberCardDetail.GetSubscriberCard(raSubscriber.SubscriberUid);

            if (subscriberCard != null)
            {
                userBasicProfile.SubscriberCard = subscriberCard.CardDocumnet;
            }
            else
            {
                var _client = new HttpClient();

                var url = configuration.GetValue<string>("APIServiceLocations:Simulation") + $"api/update/Card/{userBasicProfile.SubscriberUid}";

                var content = new StringContent("{}", Encoding.UTF8, "application/json");

                var response = await _client.PostAsync(url,content);

                if (response.StatusCode == HttpStatusCode.OK)
                {
                    APIResponse apiResponse = JsonConvert.DeserializeObject<APIResponse>(await response.Content.ReadAsStringAsync());
                    if (apiResponse.Success)
                    {
                        userBasicProfile.SubscriberCard = apiResponse.Result.ToString();
                    }
                    else
                    {
                        userBasicProfile.SubscriberCard = null;
                    }
                }
                else
                {
                    userBasicProfile.SubscriberCard = null;
                }                
            }
            return userBasicProfile;
        }
        public async Task<APIResponse> GetUserProfileDataNewAsync(
            GetUserProfileRequest request)
        {
            if (null == request || string.IsNullOrEmpty(request.UserId) || string.IsNullOrEmpty(request.UserIdType))
            {
                _logger.LogError("Invalid Parameters Received");

                return new APIResponse("Invalid Parameters");
            }

            ProfileConfig config = new ProfileConfig();

            UserData userBasicProfile = new UserData();

            Accesstoken accessToken = null;

            var reqScopes = request.ProfileType.Split(',');

            try
            {
                accessToken = await _cacheClient.Get<Accesstoken>("AccessToken",
                    request.Token);
                if (null == accessToken)
                {
                    _logger.LogError("Access token not recieved from cache." +
                        "Expired or Invalid access token");
                    return new APIResponse("UnAuthorized");
                }
            }
            catch (CacheException ex)
            {
                _logger.LogError("Failed to get Access Token Record");
                ErrorResponseDTO error = new ErrorResponseDTO();
                error.error = _messageLocalizer.GetMessage(OIDCConstants.InternalError);
                error.error_description = _helper.GetRedisErrorMsg(
                    ex.ErrorCode, ErrorCodes.REDIS_ACCESS_TOKEN_GET_FAILED);
                return new APIResponse("Internal Error" + error.error_description);
            }

            userBasicProfile = await GetUserBasicDataAsync(request.UserId,
                        request.UserIdType);

            if (null == userBasicProfile)
            {
                _logger.LogError("GetUserBasicProfileAsync Failed");

                return new APIResponse("User not found");
            }

            var clientInDb = await _clientService.GetClientProfilesAndPurposesAsync(accessToken.ClientId);

            if (null == clientInDb)
            {
                _logger.LogError("Client not found");

                return new APIResponse("Client not found");
            }
            var eConsentClient = clientInDb.EConsentClients.FirstOrDefault(s => s.Status == "ACTIVE");

            if (null == eConsentClient)
            {
                _logger.LogError("EConsent Client not found");

                return new APIResponse("EConsent Client not found");
            }
            var transactionUid = Guid.NewGuid().ToString();

            var suid = userBasicProfile.SubscriberUid;

            List<string> scopesList = new List<string>();

            HashSet<string> requestedAttributes = new HashSet<string>();

            string[] Scopes = new string[1];

            if (!string.IsNullOrEmpty(request.ProfileType))
            {
                Scopes = request.ProfileType.Split(new char[] { ',', '\t' });

                foreach (var scope in Scopes)
                {
                    var profileId = await _scopeService.GetScopeIdByNameAsync(scope);

                    if (profileId == -1)
                    {
                        _logger.LogError("Profile not found");

                        return new APIResponse("Profile not found");
                    }
                    scopesList.Add(profileId.ToString());
                }

                request.ProfileType = string.Join(',', scopesList);
            }
            else
            {
                _logger.LogError("Empty Profiles");

                return new APIResponse("Invalid Profiles");
            }
            Scopes=scopesList.ToArray();
            if (!string.IsNullOrEmpty(request.Purpose))
            {
                var purposeId = await _purposeService.GetPurposeIdByNameAsync(request.Purpose);
                if (purposeId == -1)
                {
                    _logger.LogError("Purpose not found");

                    return new APIResponse("Purpose not found");
                }
                request.Purpose = purposeId.ToString();
            }

            var transactionResponse = await AddTransactionProfileRequest(transactionUid, clientInDb.Id, suid, JsonConvert.SerializeObject(request));

            if (!transactionResponse.Success)
            {
                _logger.LogError("Failed to Add Transactions");

                return new APIResponse("Internal Error");
            }

            var transactionId = transactionResponse.Result.Id;

            var profilesList = await _userClaimService.GetAttributes();

            foreach (var scope in scopesList)
            {
                var scopeinDb = await _unitOfWork.Scopes.GetByIdAsync(int.Parse(scope));

                if (scopeinDb != null)
                {
                    if (!scopeinDb.IsClaimsPresent)
                    {
                        _logger.LogError("Profile has no Attributes");

                        return new APIResponse("Profile Has No Attributes");
                    }
                    var claims = scopeinDb.ClaimsList.Split(new char[] { ' ', '\t' });
                    foreach (var claim in claims)
                    {
                        requestedAttributes.Add(claim);
                    }
                }
            }

            await AddTransactionProfileConsent(transactionId, "NA", "", string.Join(",", requestedAttributes), "");

            if (userBasicProfile.SubscriberStatus != StatusConstants.ACTIVE)
            {
                _logger.LogError("User account is not Active");

                await AddTransactionProfileStatus(transactionId, "FAILED", "User Account Not Active", "", -1);

                return new APIResponse("User Account is not Active");
            }

            var Purpose = request.Purpose;

            var PurposeConsentRequired = true;

            List<string> clientPurposes = new List<string>();

            if (eConsentClient.Purposes != null)
            {
                clientPurposes = eConsentClient.Purposes.Split(',').ToList();
            }

            var PurposesInDb = await _purposeService.GetPurposeListAsync();

            if (null == PurposesInDb)
            {
                _logger.LogError("Failed to get the purposes list");
                return new APIResponse("Failed to get the purposes list");
            }

            if (!string.IsNullOrEmpty(Purpose))
            {
                if (!ValidatePurposes(Purpose, clientPurposes))
                {
                    await AddTransactionProfileStatus(transactionId, "FAILED", "Invalid Purposes", "", -1);

                    return new APIResponse("Invalid Purpose");
                }
                PurposeConsentRequired = CheckPurposes(request.Purpose, PurposesInDb);
            }

            var tempAuthNSessId = string.Empty;

            try
            {
                tempAuthNSessId = EncryptionLibrary.KeyGenerator.GetUniqueKey();
            }
            catch (Exception error)
            {
                _logger.LogError("GetUniqueKey failed: {0}", error.Message);
                return new APIResponse(_messageLocalizer.GetMessage(OIDCConstants.InternalError));
            }

            // Validate client scopes

            var isTrue = ValidateProfiles(request.ProfileType, eConsentClient.Scopes);
            if (false == isTrue)
            {
                _logger.LogError(OIDCConstants.ClientScopesNotMatched.En);

                await AddTransactionProfileStatus(transactionId, "FAILED", _messageLocalizer.GetMessage(OIDCConstants.ClientScopesNotMatched), "", -1);

                return new APIResponse(_messageLocalizer.GetMessage(OIDCConstants.ClientScopesNotMatched));
            }

            // Requested Scopes


            // Get all scopes from Db
            var scopesinDB = await _unitOfWork.Scopes.ListAllScopeAsync();
            if (null == scopesinDB)
            {
                _logger.LogError("ListAllScopeAsync failed");

                await AddTransactionProfileStatus(transactionId, "FAILED", "Internal Error", "", -1);

                return new APIResponse(_messageLocalizer.GetMessage(OIDCConstants.InternalError));
            }
            var userProfileResponse = await GetProfile(reqScopes, userBasicProfile.IdDocNumber);

            if (!userProfileResponse.Success)
            {
                await AddTransactionProfileStatus(transactionId, "FAILED", userProfileResponse.Message, "", -1);
                return new APIResponse(userProfileResponse.Message);
            }

            var userProfile = (Dictionary<string, object>)userProfileResponse.Resource;
            // Get all Claims from Database
            var claimsinDb = await _unitOfWork.UserClaims.
                ListAllUserClaimAsync();
            if (null == claimsinDb)
            {
                _logger.LogError("ListAllUserClaimAsync failed");
                await AddTransactionProfileStatus(transactionId, "FAILED", "Internal Error", "", -1);
                return new APIResponse(_messageLocalizer.GetMessage(OIDCConstants.InternalError));
            }

            var deselectScopesAndClaims = true;

            var consentScopes = new List<ScopeInfo>() { };

            var daesClaim = new GetUserBasicProfileResult();

            var userApprovedClaims = new List<string>() { };

            HashSet<string> ApprovedClaims = new HashSet<string>();

            HashSet<string> requestedProfileAttributes = new HashSet<string>();

            var ProfilesSet = new Dictionary<string, UserProfilesConsent>();

            if (!PurposeConsentRequired)
            {
                userApprovedClaims = GetClaimsList(reqScopes, scopesinDB);
                foreach (var item in userApprovedClaims)
                {
                    ApprovedClaims.Add(item);
                }
            }
            else
            {
                foreach (var reqScope in Scopes)
                {
                    foreach (var scopeinDb in scopesinDB)
                    {
                        if (reqScope == scopeinDb.Id.ToString())
                        {
                            var userProfileConsent = await _unitOfWork.UserProfilesConsent.
                                GetUserProfilesConsentByProfileAsync(userBasicProfile.SubscriberUid, clientInDb.ClientId, scopeinDb.Id.ToString());

                            if (userProfileConsent != null)
                            {
                                ProfilesSet[scopeinDb.Name] = userProfileConsent;
                            }

                            ScopeInfo scopeInfo = new ScopeInfo();
                            scopeInfo.Name = scopeinDb.Name;
                            scopeInfo.DisplayName = scopeinDb.DisplayName;
                            scopeInfo.Description = scopeinDb.Description;
                            scopeInfo.Mandatory = scopeinDb.DefaultScope;

                            if (scopeinDb.IsClaimsPresent)
                            {
                                scopeInfo.ClaimsInfo = new List<ClaimInfo>() { };

                                var claims = scopeinDb.ClaimsList.Split(
                                    new char[] { ' ', '\t' });

                                List<Attributes> attributesList = new List<Attributes>();

                                if (userProfileConsent != null)
                                {
                                    attributesList = JsonConvert.DeserializeObject<List<Attributes>>(userProfileConsent.Attributes);
                                }
                                foreach (var claim in claims)
                                {
                                    foreach (var claiminDb in claimsinDb)
                                    {
                                        if (claim == claiminDb.Name)
                                        {
                                            Attributes AttributeinDb = null;
                                            foreach (var attribute in attributesList)
                                            {
                                                if (attribute.name == claim)
                                                {
                                                    AttributeinDb = attribute;
                                                }
                                            }
                                            if (AttributeinDb == null || !scopeinDb.SaveConsent)
                                            {
                                                ClaimInfo claimInfo = new ClaimInfo();
                                                claimInfo.Name = claiminDb.Name;
                                                claimInfo.DisplayName = claiminDb.DisplayName;
                                                claimInfo.Description = claiminDb.Description;
                                                claimInfo.Mandatory = claiminDb.DefaultClaim;
                                                scopeInfo.ClaimsInfo.Add(claimInfo);
                                                requestedProfileAttributes.Add(claiminDb.Id.ToString());
                                            }
                                            else
                                            {
                                                userApprovedClaims.Add(claim);
                                            }
                                        }
                                    }
                                }
                            }
                            if (scopeInfo.ClaimsInfo.Any())
                            {
                                scopeInfo.ClaimsPresent = scopeinDb.IsClaimsPresent;
                                consentScopes.Add(scopeInfo);
                            }
                        }
                    }
                }

                if (consentScopes.Count > 0)
                {
                    ClientDetails clientdetails = new ClientDetails();
                    clientdetails.AppName = clientInDb.ApplicationName;
                    clientdetails.ClientId = clientInDb.ClientId;

                    //var authScheme = configuration["Consent_Authentication"];
                    var authScheme = "USER_AUTH_SELECTION";

                    // Prepare temporary session object
                    TemporarySession temporarySession = new TemporarySession
                    {
                        TemporarySessionId = tempAuthNSessId,
                        UserId = userBasicProfile.SubscriberUid,
                        DisplayName = userBasicProfile.DisplayName,
                        PrimaryAuthNSchemeList = new List<string> { authScheme },
                        AuthNSuccessList = new List<string>(),
                        Clientdetails = clientdetails,
                        IpAddress = string.Empty,
                        UserAgentDetails = string.Empty,
                        TypeOfDevice = string.Empty,
                        MacAddress = DTInternalConstants.NOT_AVAILABLE,
                        withPkce = false,
                        AdditionalValue = DTInternalConstants.pending,
                        AllAuthNDone = false,
                        AuthNStartTime = DateTime.Now.ToString("s"),
                        CoRelationId = Guid.NewGuid().ToString(),
                        TransactionId = transactionId
                    };

                    // Create temporary session
                    var task = await _cacheClient.Add(CacheNames.TemporarySession,
                        tempAuthNSessId, temporarySession);
                    if (DTInternalConstants.S_OK != task.retValue)
                    {
                        _logger.LogError("_cacheClient.Add failed");
                        await AddTransactionProfileStatus(transactionId, "FAILED", "Internal Error", "", -1);
                        return new APIResponse(_messageLocalizer.GetMessage(OIDCConstants.InternalError));
                    }

                    // Send notification to mobile
                    var eConsentNotification = new EConsentNotification()
                    {
                        AuthnScheme = authScheme,
                        AuthnToken = tempAuthNSessId,
                        RegistrationToken = userBasicProfile.FcmToken,
                        ApplicationName = clientInDb.ApplicationName,
                        ConsentScopes = consentScopes,
                        DeselectScopesAndClaims = deselectScopesAndClaims
                    };
                    try
                    {
                        var result = await _pushNotificationClient.SendEConsentNotification(
                            eConsentNotification);
                        if (null == result)
                        {
                            _logger.LogError("_pushNotificationClient.SendAuthnNotification" +
                                " failed");
                            await AddTransactionProfileStatus(transactionId, "FAILED", "Internal Error", "", -1);
                            return new APIResponse("Failed to send Notification");
                        }
                    }
                    catch (Exception error)
                    {
                        _logger.LogError("_pushNotificationClient." +
                            "SendAuthnNotification " +
                            "failed : {0}", error.Message);
                        await AddTransactionProfileStatus(transactionId, "FAILED", "Internal Error", "", -1);
                        return new APIResponse("Failed to send Notification");
                    }

                    await UpdateTransactionProfileConsent(transactionId, string.Join(", ", requestedAttributes), "PENDING");

                    DateTime date1 = DateTime.Now;
                    DateTime date2 = DateTime.Now.AddMinutes(1);
                    bool isError = false;
                    string message = string.Empty;
                    TemporarySession tempSession = null;

                    while (0 > DateTime.Compare(date1, date2))
                    {
                        // Check whether the temporary session exists
                        var isExists = await _cacheClient.Exists(CacheNames.TemporarySession,
                            tempAuthNSessId);

                        if (CacheCodes.KeyExist != isExists.retValue)
                        {
                            _logger.LogError("Temporary Session Expired/Not Found");
                            message = _messageLocalizer.GetMessage(OIDCConstants.InternalError);
                            isError = true;
                            break;
                        }

                        // Get the temporary session object
                        tempSession = await _cacheClient.Get<TemporarySession>
                            (CacheNames.TemporarySession,
                            tempAuthNSessId);
                        if (null == tempSession)
                        {
                            _logger.LogError("Get Temporary Session Failed,Expired/Not Found");
                            await AddTransactionProfileStatus(transactionId, "FAILED", "Internal Error", "", -1);
                            message = _messageLocalizer.GetMessage(OIDCConstants.InternalError);
                            isError = true;
                            break;
                        }

                        if (!tempSession.AdditionalValue.Equals(DTInternalConstants.pending))
                            break;

                        // Wait 500 Milli Seconds(Half Second) without blocking
                        await Task.Delay(500);
                        date1 = DateTime.Now;
                    }

                    if (isError)
                    {
                        await AddTransactionProfileStatus(transactionId, "FAILED", message, "", -1);
                        return new APIResponse(message);
                    }
                    if (tempSession.AdditionalValue == DTInternalConstants.DeniedConsent)
                    {
                        await AddTransactionProfileStatus(transactionId, "SUCCESS", tempSession.AdditionalValue, "", -1);
                        return new APIResponse(tempSession.AdditionalValue);
                    }
                    if (tempSession.AdditionalValue == DTInternalConstants.pending)
                    {
                        await AddTransactionProfileStatus(transactionId, "FAILED", "Request Timed Out", "", -1);
                        return new APIResponse("Request Timed Out");
                    }
                    if (tempSession.AdditionalValue != "true")
                    {
                        await AddTransactionProfileStatus(transactionId, "FAILED", tempSession.AdditionalValue, "", -1);
                        //await UpdateTransactionProfileConsent(transactionId, "NA", "FAILED");
                        return new APIResponse(tempSession.AdditionalValue);

                    }
                    foreach (var reqScope in Scopes)
                    {
                        foreach (var approvedScope in tempSession.allowedScopesAndClaims)
                        {
                            List<Attributes> attributes = new List<Attributes>();
                            var scope = await _scopeService.GetScopeIdByNameAsync(approvedScope.name);
                            var id = scope.ToString();
                            if ((reqScope == id) &&
                                approvedScope.claimsPresent)
                            {
                                foreach (var claim in approvedScope.claims)
                                {
                                    userApprovedClaims.Add(claim);
                                }
                            }
                        }
                    }
                }
            }
            ApprovedClaims = new HashSet<string>(userApprovedClaims);

            await UpdateTransactionProfileConsent(transactionId, string.Join(",", ApprovedClaims), "");

            Dictionary<string, object> response = new Dictionary<string, object>();

            foreach (var item in ApprovedClaims)
            {
                if (userProfile.ContainsKey(item))
                {
                    response[item] = userProfile[item];
                }
            }
            APIResponse aPIResponse = new APIResponse()
            {
                Success = true,
                Result = response,
                Message = "Get User Profile Success"
            };

            await AddTransactionProfileStatus(transactionId, "SUCCESS", "", "", -1);
            _logger.LogDebug("<--GetUserProfileDataAsync");
            return aPIResponse;
        }

        public async Task<ServiceResult> GetProfile(string[] scopes, string documentNumber)
        {
            HttpClient client = _httpClientFactory.CreateClient();

            var raSubscriber = await GetUserBasicProfileAsync(documentNumber,
                    "PASSPORT", null);

            if (null == raSubscriber)
            {
                _logger.LogError("Subscriber details not found");
                return new ServiceResult(false, "User not Found");
            }

            var userDetails = new Dictionary<string, object>();

            var settings = new JsonSerializerSettings
            {
                ContractResolver = new CamelCasePropertyNamesContractResolver(),
                Formatting = Formatting.None
            };

            JObject keyValuePairs = JObject.Parse(JsonConvert.SerializeObject(raSubscriber, settings));

            foreach (var keyValue in keyValuePairs.Properties())
            {
                userDetails[keyValue.Name] = keyValue.Value.ToString();
            }

            if (scopes.Contains("kycprofile"))
            {
                var kycurl = configuration["KycUrl"];
                kycurl += documentNumber;
                HttpResponseMessage result;
                try
                {
                    result = await client.GetAsync(kycurl);
                    if (result.StatusCode != System.Net.HttpStatusCode.OK)
                    {
                        _logger.LogError($"Request to {kycurl} failed with status code {result.StatusCode}");

                        return new ServiceResult(false, "Internal error");
                    }
                }
                catch (Exception)
                {

                    _logger.LogError("Failed to connect Datapivot");
                    return new ServiceResult(false, "Internal error");
                }

                var responseString = await result.Content.ReadAsStringAsync();
                var apiResponse = JsonConvert.DeserializeObject<KycResponse>(responseString);
                if (apiResponse == null)
                {
                    return new ServiceResult(false, "Internal error");
                }
                if (!apiResponse.success)
                {
                    return new ServiceResult(false, apiResponse.message);
                }
                userDetails["kyc_document"] = apiResponse.result;
            }
            if (scopes.Contains("ekycprofile"))
            {
                var kycurl = configuration["EKycUrl"];

                kycurl += documentNumber;

                HttpResponseMessage result;
                try
                {
                    result = await client.GetAsync(kycurl);
                    if (result.StatusCode != System.Net.HttpStatusCode.OK)
                    {
                        _logger.LogError($"Request to {kycurl} failed with status code {result.StatusCode}");
                        return new ServiceResult(false, "Internal error");
                    }
                }

                catch (Exception)
                {

                    _logger.LogError("Failed to connect Datapivot");
                    return new ServiceResult(false, "Internal error");
                }

                var responseString = await result.Content.ReadAsStringAsync();
                var apiResponse = JsonConvert.DeserializeObject<KycResponse>(responseString);
                if (apiResponse == null)
                {
                    return new ServiceResult(false, "Internal error");
                }
                if (!apiResponse.success)
                {
                    return new ServiceResult(false, apiResponse.message);
                }
                userDetails["Ekyc_document"] = apiResponse.result;
            }
            return new ServiceResult(true, "Success", userDetails);
        }

        public async Task<ServiceResult> GetAgentDetails(GetAgentDetailsDTO request)
        {
            _logger.LogInformation("Get Agent Details Started");

            var raSubscriber = await _unitOfWork.Subscriber.GetSubscriberInfobyDocType(request.Agent);
            if (raSubscriber == null)
            {
                return new ServiceResult(false, "Agent Not Found");
            }

            var tempAuthNSessId = string.Empty;
            try
            {
                tempAuthNSessId = EncryptionLibrary.KeyGenerator.GetUniqueKey();
            }
            catch (Exception error)
            {
                _logger.LogError(error, error.Message);
                return new ServiceResult(false, _messageLocalizer.GetMessage(OIDCConstants.InternalError));
            }
            TemporarySession temporarySession = new TemporarySession
            {
                TemporarySessionId = tempAuthNSessId,
                UserId = raSubscriber.SubscriberUid,
                DisplayName = raSubscriber.DisplayName,
                PrimaryAuthNSchemeList = new List<string> { "DeviceAuthentication" },
                AuthNSuccessList = new List<string>(),
                Clientdetails = null,
                IpAddress = string.Empty,
                UserAgentDetails = string.Empty,
                TypeOfDevice = string.Empty,
                MacAddress = DTInternalConstants.NOT_AVAILABLE,
                withPkce = false,
                AdditionalValue = DTInternalConstants.pending,
                AllAuthNDone = false,
                AuthNStartTime = DateTime.Now.ToString("s"),
                CoRelationId = Guid.NewGuid().ToString()
            };

            var task = await _cacheClient.Add(CacheNames.TemporarySession,
                tempAuthNSessId, temporarySession);
            if (DTInternalConstants.S_OK != task.retValue)
            {
                return new ServiceResult(false, _messageLocalizer.GetMessage(OIDCConstants.InternalError));
            }
            var walletDelegationNotification = new WalletDelegationNotification
            {
                AuthnScheme = "DeviceAuthentication",
                AuthnToken = tempAuthNSessId,
                RegistrationToken = raSubscriber.FcmToken,
                Principal = request.Principal,
                DelegationPurpose = request.DelegationPurpose,
                NotaryInformation = request.NotaryInformation,
                ValidityPeriod = request.ValidityPeriod,
                Context = "POADelegation"
            };
            try
            {
                var result = _pushNotificationClient.SendWalletDelegationNotification(
                    walletDelegationNotification);
                if (null == result)
                {
                    return new ServiceResult(false, "Failed to send Notification");
                }
            }
            catch (Exception)
            {
                return new ServiceResult(false, "Failed to send Notification");
            }
            DateTime date1 = DateTime.Now;
            DateTime date2 = DateTime.Now.AddMinutes(2);
            bool isError = false;
            string message = string.Empty;
            TemporarySession tempSession = null;

            while (0 > DateTime.Compare(date1, date2))
            {
                // Check whether the temporary session exists
                var isExists = await _cacheClient.Exists(CacheNames.TemporarySession,
                    tempAuthNSessId);
                if (CacheCodes.KeyExist != isExists.retValue)
                {
                    _logger.LogError("Temporary Session Expired/Not Found");
                    message = _messageLocalizer.GetMessage(OIDCConstants.InternalError);
                    isError = true;
                    break;
                }

                tempSession = await _cacheClient.Get<TemporarySession>
                    (CacheNames.TemporarySession,
                    tempAuthNSessId);
                if (null == tempSession)
                {
                    _logger.LogError("Get Temporary Session Failed,Expired/Not Found");
                    message = _messageLocalizer.GetMessage(OIDCConstants.InternalError);
                    isError = true;
                    break;
                }

                if (!tempSession.AdditionalValue.Equals(DTInternalConstants.pending))
                {
                    _logger.LogInformation("Consent Status :" + temporarySession.AdditionalValue);
                    break;
                }


                await Task.Delay(500);
                date1 = DateTime.Now;
            }
            if (isError)
            {
                return new ServiceResult(false, "Internal Error");
            }
            if (tempSession.AdditionalValue == DTInternalConstants.pending)
            {
                return new ServiceResult(false, "Request Timed Out");
            }
            if (tempSession.AdditionalValue != DTInternalConstants.S_True)
            {
                return new ServiceResult(false, tempSession.AdditionalValue);
            }
            Dictionary<string, string> agentDetails = new Dictionary<string, string>();

            agentDetails["agentName"] = raSubscriber.DisplayName;

            agentDetails["agentEmail"] = raSubscriber.Email;

            agentDetails["agentSuid"] = raSubscriber.SubscriberUid;

            _logger.LogInformation("Get Agent Details Ended");

            return new ServiceResult(true, "Get Agent Details Success", agentDetails);
        }

        public async Task<ServiceResult> GetUserEmail(string suid)
        {
            try
            {
                var subscriber = await _unitOfWork.Subscriber.GetSubscriberDetailsBySuid(suid);

                if (subscriber == null)
                {
                    return new ServiceResult(false, "User Not Found");
                }

                var obj = new
                {
                    email = subscriber.Email
                };
                return new ServiceResult(true, "Get Email Success", obj);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return new ServiceResult(false, "Internal Error");
            }
        }
    }
}