using System;
using System.Text;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Serialization;

using DTPortal.Core.DTOs;
using DTPortal.Core.Constants;
using DTPortal.Core.Domain.Services;
using DTPortal.Core.Domain.Services.Communication;
using DTPortal.Core.Domain.Models;
using DTPortal.Core.Utilities;
using static DTPortal.Common.CommonResponse;
using DTPortal.Core.Domain.Repositories;
using DTPortal.Core.Domain.Models.RegistrationAuthority;

namespace DTPortal.Core.Services
{
    public class SubscriberService : ISubscriberService
    {
        private readonly IGoogleMapService _googleMapService;
        private readonly HttpClient _client;
        private readonly IConfiguration _configuration;
        private readonly ILogger<SubscriberService> _logger;
        private readonly IRAServiceClient _raServiceClient;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IGlobalConfiguration _globalConfiguration;
        private readonly MessageConstants Constants;
        private readonly ILogClient _LogClient;
        private readonly IMessageLocalizer _messageLocalizer;

        public SubscriberService(IGoogleMapService googleMapService,
            HttpClient httpClient,
            IConfiguration configuration,
            ILogger<SubscriberService> logger,
            IRAServiceClient rAServiceClient,
            IUnitOfWork unitOfWork,
            IGlobalConfiguration globalConfiguration,
            IMessageLocalizer messageLocalizer,
            ILogClient logClient
            )
        {
            _configuration = configuration;
            httpClient.BaseAddress = new Uri(_configuration["APIServiceLocations:RAServiceBaseAddress"]);
            httpClient.DefaultRequestHeaders.Add("Accept", "application/json");

            _googleMapService = googleMapService;
            _client = httpClient;
            _logger = logger;
            _raServiceClient = rAServiceClient;
            _unitOfWork = unitOfWork;
            _globalConfiguration = globalConfiguration;
            _LogClient = logClient;
            _messageLocalizer = messageLocalizer;

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
        }

        public async Task<IEnumerable<RevokeReasonDTO>> GetAllRevokeReasonsAsync()
        {
            try
            {
                HttpResponseMessage response = await _client.GetAsync("api/get/service/revoke-reasons");
                if (response.StatusCode == HttpStatusCode.OK)
                {
                    APIResponse apiResponse = JsonConvert.DeserializeObject<APIResponse>(await response.Content.ReadAsStringAsync());
                    if (apiResponse.Success)
                    {
                        return JsonConvert.DeserializeObject<IEnumerable<RevokeReasonDTO>>(apiResponse.Result.ToString());
                    }
                    else
                    {
                        _logger.LogError(apiResponse.Message);
                    }
                }
                else
                {
                    _logger.LogError($"The request with uri={response.RequestMessage.RequestUri} failed " +
                       $"with status code={response.StatusCode}");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
            }

            return null;
        }

        public async Task<string[]> GetSubscribersNamesAysnc(int type, string value)
        {
            try
            {
                HttpResponseMessage response = await _client.GetAsync($"api/get/service/subscriber/list/{type}/{value}");
                if (response.StatusCode == HttpStatusCode.OK)
                {
                    APIResponse apiResponse = JsonConvert.DeserializeObject<APIResponse>(await response.Content.ReadAsStringAsync());
                    if (apiResponse.Success)
                    {
                        return JsonConvert.DeserializeObject<string[]>(apiResponse.Result.ToString());
                    }
                    else
                    {
                        _logger.LogError(apiResponse.Message);
                    }
                }
                else
                {
                    _logger.LogError($"The request with uri={response.RequestMessage.RequestUri} failed " +
                       $"with status code={response.StatusCode}");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
            }

            return null;
        }

        public async Task<SubscribersCountDTO> GetSubscribersCountAsync()
        {
            try
            {
                HttpResponseMessage response = await _client.GetAsync("api/get/service/subscriber-certificate/count");
                if (response.StatusCode == HttpStatusCode.OK)
                {
                    APIResponse apiResponse = JsonConvert.DeserializeObject<APIResponse>(await response.Content.ReadAsStringAsync());
                    if (apiResponse.Success)
                    {
                        JObject result = (JObject)JToken.FromObject(apiResponse.Result);
                        return JsonConvert.DeserializeObject<SubscribersCountDTO>(result["subscriberCount"].ToString());
                    }
                    else
                    {
                        _logger.LogError(apiResponse.Message);
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

        public async Task<SubscribersAndCertificatesCountDTO> GetSubscribersAndCertificatesCountAsync()
        {
            try
            {
                HttpResponseMessage response = await _client.GetAsync("api/get/service/subscriber-certificate/count");
                if (response.StatusCode == HttpStatusCode.OK)
                {
                    APIResponse apiResponse = JsonConvert.DeserializeObject<APIResponse>(await response.Content.ReadAsStringAsync());
                    if (apiResponse.Success)
                    {
                        return JsonConvert.DeserializeObject<SubscribersAndCertificatesCountDTO>(apiResponse.Result.ToString());
                    }
                    else
                    {
                        _logger.LogError(apiResponse.Message);
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

        public async Task<SubscriberDetailsDTO> GetSubscriberDetailsAsync(int type, string value)
        {
            try
            {
                _logger.LogInformation("GetSubscriberDetailsAsync api call start");
                HttpResponseMessage response = await _client.GetAsync($"api/get/service/subscriber/details/{type}/{value}");
                _logger.LogInformation("GetSubscriberDetailsAsync api call end");
                if (response.StatusCode == HttpStatusCode.OK)
                {
                    APIResponse apiResponse = JsonConvert.DeserializeObject<APIResponse>(await response.Content.ReadAsStringAsync());
                    if (apiResponse.Success)
                    {
                        SubscriberDetailsDTO subscriberDetails = JsonConvert.DeserializeObject<SubscriberDetailsDTO>(apiResponse.Result.ToString());
                        if (Convert.ToBoolean(_configuration["CheckForLocation"]))
                        {
                            try
                            {
                                // Get subscriber address
                                string[] location = subscriberDetails.GeoLocation.Split(',');
                                subscriberDetails.Address = await GetSubscriberAddress(location[0], location[1]);
                            }
                            catch (Exception ex)
                            {
                                _logger.LogError(ex, ex.Message);
                            }
                        }
                        subscriberDetails.IsDetailsAvailable = true;
                        return subscriberDetails;
                    }
                    else
                    {
                        _logger.LogError(apiResponse.Message);
                        return new SubscriberDetailsDTO();
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

        private async Task<string> GetSubscriberAddress(string latitude, string longitude)
        {
            return await _googleMapService.GetAddressByLatitudeLongitude(latitude, longitude);
        }

        public async Task<IEnumerable<OrganizaionDetails>> GetOrganizationDetailsAsync(string suid)
        {
            try
            {
                _client.BaseAddress = new Uri(_configuration["APIServiceLocations:OrganizationOnboardingServiceBaseAddress"]);

                HttpResponseMessage response = await _client.GetAsync($"api/get/prepertry/status?email=" + suid);
                if (response.StatusCode == HttpStatusCode.OK)
                {
                    APIResponse apiResponse = JsonConvert.DeserializeObject<APIResponse>(await response.Content.ReadAsStringAsync());
                    if (apiResponse.Success)
                    {
                        return JsonConvert.DeserializeObject<IEnumerable<OrganizaionDetails>>(apiResponse.Result.ToString());
                    }
                    else
                    {
                        _logger.LogError(apiResponse.Message);
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

        public async Task<SubscriberOnboardingDetailsDTO> GetSubscriberOnboardingDetailsAsync(string identifier)
        {
            try
            {
                _client.BaseAddress = new Uri(_configuration["APIServiceLocations:ControlledOnboardingServiceBaseAddress"]);

                HttpResponseMessage response = await _client.GetAsync("api/get/subscriber/details?id=" + identifier);
                if (response.StatusCode == HttpStatusCode.OK)
                {
                    APIResponse apiResponse = JsonConvert.DeserializeObject<APIResponse>(await response.Content.ReadAsStringAsync());
                    if (apiResponse.Success)
                    {
                        //JObject result = (JObject)JToken.FromObject(apiResponse.Result);
                        return JsonConvert.DeserializeObject<SubscriberOnboardingDetailsDTO>(apiResponse.Result.ToString());
                    }
                    else
                    {
                        _logger.LogError(apiResponse.Message);
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

        public async Task<Response> CheckandUpdateSubscriber(string Suid)
        {
            var response = new Response();
            bool centralLog = false;
            var logMessage = "Subscriber Activated Successfully";
            var Subscriber = new SubscriberView();
            response.Message = "Subscriber is Active";
            var Status = LogClientServices.Success;
            DateTime StartTime = DateTime.Now;

            var subscriberStatus = StatusConstants.ACTIVE;
            try
            {
                try
                {
                    Subscriber = await _unitOfWork.Subscriber.GetSubscriberInfoBySUID(Suid);
                }
                catch (Exception error)
                {
                    _logger.LogError("GetSubscriberDetails" +
                                "failed : {0}", error.Message);
                    logMessage = "failed to get subscriber details";
                    response.Success = false;
                    Status = LogClientServices.Failure;
                    response.Message = "Internal Error";
                }

                if (Subscriber == null)
                {
                    logMessage = "failed to get subscriber details";
                    Status = LogClientServices.Failure;
                    response.Success = false;
                    response.Message = _messageLocalizer.GetMessage(Constants.SubscriberNotFound);
                    //return response;
                }

                subscriberStatus = Subscriber.SubscriberStatus;
                if (Subscriber != null && Subscriber.SubscriberStatus == StatusConstants.SUSPENDED)
                {
                    var statusUpdateRequest = new SubscriberStatusUpdateRequest();
                    statusUpdateRequest.description =
                        LogClientServices.SubscriberStatusUpdate;
                    statusUpdateRequest.subscriberStatus =
                        StatusConstants.ACTIVE;
                    statusUpdateRequest.subscriberUniqueId = Suid;

                    var statusResponse = await _raServiceClient.
                        SubscriberStatusUpdate(statusUpdateRequest);
                    if (null == statusResponse)
                    {
                        _logger.LogError("SubscriberStatusUpdate failed");
                        logMessage = "failed to activate subscriber";
                        Status = LogClientServices.Failure;
                        response.Success = false;
                        response.Message = "SubscriberStatusUpdate failled";
                    }
                    if (false == statusResponse.success)
                    {
                        _logger.LogError("SubscriberStatusUpdate failed, " +
                            "{0}", statusResponse.message);
                        logMessage = "failed to activate subscriber";
                        Status = LogClientServices.Failure;
                        response.Success = false;
                        response.Message = statusResponse.message;
                    }
                    UserLoginDetail userLoginDetails = await _unitOfWork.UserLoginDetail.
                            GetUserLoginDetailAsync(Suid);
                    if (null != userLoginDetails)
                    {
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
                        }
                    }
                    logMessage = "Subscriber Activated";
                    response.Success = true;
                    response.Message = "Successfully activated Subscriber";
                    //return response;
                }
            }
            catch (Exception error)
            {
                _logger.LogError("ActivatingSubscriber failed: " +
                       "{0}", error.Message);
                centralLog = true;
                logMessage = "Failed Activating Subscriber";
                response.Success = false;
                response.Message = "Internal error";
                Status = LogClientServices.Failure;
            }
            finally
            {
                if (Subscriber != null && subscriberStatus == StatusConstants.SUSPENDED)
                {
                    var logResponse = _LogClient.SendAuthenticationLogMessage(
                    StartTime,
                    Suid,
                    LogClientServices.AccountLocked,
                    logMessage,
                    Status,
                    LogClientServices.Business,
                    centralLog
                    );
                    if (false == logResponse.Success)
                    {
                        _logger.LogError("SendAuthenticationLogMessage failed: " +
                            "{0}", logResponse.Message);
                    }
                }
            }
            //response.Success = true;

            return response;
        }

        public async Task<ServiceResult> ActivateSubscriber(string Suid, string userName, bool makerCheckerFlag = false)
        {
            //var response = new ServiceResult();
            var Subscriber = await _unitOfWork.Subscriber.GetSubscriberInfoBySUID(Suid);

            if (Subscriber == null)
            {
                //response.Success = false;
                //response.Message = Constants.SubscriberNotFound;
                return new ServiceResult(false, _messageLocalizer.GetMessage(Constants.SubscriberNotFound));
            }
            if (Subscriber.SubscriberStatus == StatusConstants.SUSPENDED)
            {
                var statusUpdateRequest = new SubscriberStatusUpdateRequest();
                statusUpdateRequest.description =
                    LogClientServices.SubscriberStatusUpdate;
                statusUpdateRequest.subscriberStatus =
                    StatusConstants.ACTIVE;
                statusUpdateRequest.subscriberUniqueId = Suid;
                var statusResponse = await _raServiceClient.
                    SubscriberStatusUpdate(statusUpdateRequest);
                if (null == statusResponse)
                {
                    _logger.LogError("SubscriberStatusUpdate failed");
                    //response.Success = false;
                    //response.Message = "SubscriberStatusUpdate failled";
                    return new ServiceResult(false, "SubscriberStatusUpdate failed");
                }
                if (false == statusResponse.success)
                {
                    _logger.LogError("SubscriberStatusUpdate failed, " +
                        "{0}", statusResponse.message);
                    //response.Success = false;
                    //response.Message = statusResponse.message;
                    return new ServiceResult(false, statusResponse.message);
                }
                UserLoginDetail userLoginDetails = await _unitOfWork.UserLoginDetail.
                        GetUserLoginDetailAsync(Suid);
                if (null != userLoginDetails)
                {
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
                        //response.Success = false;
                        //response.Message = "SubscriberStatusUpdate failed";

                    }
                }
                //response.Success = true;
                //response.Message = "Successfully activated Subscriber";
                return new ServiceResult(true, "Successfully activated Subscriber");
            }
            //response.Success = true;
            //response.Message = "Successfully activated Subscriber";
            return new ServiceResult(true, "Successfully activated Subscriber");
        }

        public async Task<ServiceResult> GetDeviceHistory(string suid)
        {
            try
            {
                HttpClient _client1 = new HttpClient();
                _client1.BaseAddress = new Uri(_configuration["APIServiceLocations:DeviceHistoryBaseAddress"]);

                HttpResponseMessage response = await _client1.GetAsync($"api/get/subscriber-device-details/" + suid);
                if (response.StatusCode == HttpStatusCode.OK)
                {
                    APIResponse apiResponse = JsonConvert.DeserializeObject<APIResponse>(await response.Content.ReadAsStringAsync());

                    if (apiResponse.Success)
                    {
                        var subscriberDeviceHistoryDTO = JsonConvert.DeserializeObject<SubscriberDeviceHistoryDTO>(apiResponse.Result.ToString());

                        List<DateTime> deviceHistory = new List<DateTime>();



                        foreach (var item in subscriberDeviceHistoryDTO.SubscriberDeviceHistory)
                        {
                            deviceHistory.Add(item.Created_Date);
                        }

                        deviceHistory.Add(subscriberDeviceHistoryDTO.Subscriber.CreatedDate);

                        return new ServiceResult(true, "Device List Get Success", deviceHistory);

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
                    return new ServiceResult(false, "Internal Error");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return new ServiceResult(false, "Internal Error");
            }

        }

        public async Task<SubscriberView> GetSubscriberDetailsBySuid(string suid)
        {
            try
            {
                return await _unitOfWork.Subscriber.GetSubscriberDetailsBySuid(suid);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return null;
            }
        }
        public async Task<SubscriberView> GetSubscriberDetailsByPassportNumber(string pnumber)
        {
            try
            {
                return await _unitOfWork.Subscriber.GetSubscriberDetailsByPassportNumber(pnumber);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return null;
            }
        }

        public async Task<SubscriberView> GetSubscriberDetailsByEmiratedId(string eid)
        {
            try
            {
                return await _unitOfWork.Subscriber.GetSubscriberDetailsByEmiratesId(eid);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return null;
            }
        }
    }
}
