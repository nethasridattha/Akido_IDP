using DTPortal.Core.Domain.Models.RegistrationAuthority;
using DTPortal.Core.Domain.Repositories;
using DTPortal.Core.Domain.Services;
using DTPortal.Core.Domain.Services.Communication;
using DTPortal.Core.DTOs;
using Google.Apis.Logging;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace DTPortal.Core.Services
{
    public class UserDataService:IUserDataService
    {
        private readonly IConfiguration _configuration;
        private readonly ILogger<UserDataService> _logger;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IHttpClientFactory _httpClientFactory;
        public UserDataService(
            IConfiguration configuration,
            IUnitOfWork unitOfWork,
            ILogger<UserDataService> logger,
            IHttpClientFactory httpClientFactory)
        {
            _configuration = configuration;
            _logger = logger;
            _unitOfWork = unitOfWork;
            _httpClientFactory = httpClientFactory;
        }
        public async Task<string> GetFaceByUrl(string SelfieUri)
        {
            _logger.LogDebug("-->GetSubscriberPhoto");

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

        public async Task<ServiceResult> GetSocialBenefitCardDetails
            (string userId)
        {
            try
            {
                var raSubscriber = new SubscriberView();

                raSubscriber = await _unitOfWork.Subscriber.
                                 GetSubscriberInfobyDocType(userId);
                if (null == raSubscriber)
                {
                    _logger.LogError("Subscriber details not found");

                    return new ServiceResult(false, "Failed to get subscriber Details");
                }
                HttpClient _client = new HttpClient();

                var url = _configuration["BeneficiaryCardUrl"] + userId;

                HttpResponseMessage result;

                result = await _client.GetAsync(url);
                if (result.StatusCode != System.Net.HttpStatusCode.OK)
                {
                    _logger.LogError($"Request to {url} failed with status code {result.StatusCode}");
                    return new ServiceResult(false, "Internal error");
                }

                var responseString = await result.Content.ReadAsStringAsync();
                var apiResponse = JsonConvert.DeserializeObject<APIResponse>(responseString);
                if (apiResponse == null)
                {
                    return new ServiceResult(false, "Internal error");
                }
                if (!apiResponse.Success)
                {
                    return new ServiceResult(false, apiResponse.Message);
                }
                var socialBenefitCard = JsonConvert.DeserializeObject<SocialBenefitCardDTO>(apiResponse.Result.ToString());

                var photo = await GetFaceByUrl(raSubscriber.SelfieUri);

                if (photo != null)
                {
                    socialBenefitCard.photo = photo;
                }
                return new ServiceResult(true, apiResponse.Message, socialBenefitCard);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.ToString());
                return new ServiceResult(false, ex.Message);
            }
        }

        public async Task<ServiceResult> GetMdlProfile(string userId)
        {
            try
            {
                HttpClient _client = new HttpClient();

                var url = _configuration["MdlUrl"] + userId;

                HttpResponseMessage result;

                result = await _client.GetAsync(url);
                if (result.StatusCode != System.Net.HttpStatusCode.OK)
                {
                    _logger.LogError($"Request to {url} failed with status code {result.StatusCode}");
                    return new ServiceResult(false, "Internal error");
                }

                var responseString = await result.Content.ReadAsStringAsync();
                var apiResponse = JsonConvert.DeserializeObject<APIResponse>(responseString);
                if (apiResponse == null)
                {
                    return new ServiceResult(false, "Internal error");
                }
                if (!apiResponse.Success)
                {
                    return new ServiceResult(false, apiResponse.Message);
                }
                return new ServiceResult(true,apiResponse.Message, apiResponse.Result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.ToString());
                return new ServiceResult(false, ex.Message);
            }
        }

        public async Task<ServiceResult> GetPidProfile(string userId)
        {
            try
            {
                HttpClient _client = new HttpClient();

                var url = _configuration["PidUrl"] + userId;

                HttpResponseMessage result;

                result = await _client.GetAsync(url);
                if (result.StatusCode != System.Net.HttpStatusCode.OK)
                {
                    _logger.LogError($"Request to {url} failed with status code {result.StatusCode}");
                    return new ServiceResult(false, "Internal error");
                }

                var responseString = await result.Content.ReadAsStringAsync();
                var apiResponse = JsonConvert.DeserializeObject<APIResponse>(responseString);
                if (apiResponse == null)
                {
                    return new ServiceResult(false, "Internal error");
                }
                if (!apiResponse.Success)
                {
                    return new ServiceResult(false, apiResponse.Message);
                }
                return new ServiceResult(true, apiResponse.Message, apiResponse.Result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.ToString());
                return new ServiceResult(false, ex.Message);
            }
        }
        public async Task<ServiceResult> GetProfile(string url)
        {
            try
            {
                HttpClient _client = new HttpClient();

                //var url = _configuration["PidUrl"] + userId;

                HttpResponseMessage result;

                result = await _client.GetAsync(url);
                if (result.StatusCode != System.Net.HttpStatusCode.OK)
                {
                    _logger.LogError($"Request to {url} failed with status code {result.StatusCode}");
                    return new ServiceResult(false, "Internal error");
                }

                var responseString = await result.Content.ReadAsStringAsync();
                var apiResponse = JsonConvert.DeserializeObject<APIResponse>(responseString);
                if (apiResponse == null)
                {
                    return new ServiceResult(false, "Internal error");
                }
                if (!apiResponse.Success)
                {
                    return new ServiceResult(false, apiResponse.Message);
                }
                return new ServiceResult(true, apiResponse.Message, apiResponse.Result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.ToString());
                return new ServiceResult(false, ex.Message);
            }
        }
    }
}
