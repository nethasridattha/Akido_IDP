using DTPortal.Core.Constants;
using DTPortal.Core.Domain.Repositories;
using DTPortal.Core.Domain.Services;
using DTPortal.Core.Domain.Services.Communication;
using DTPortal.Core.Services;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;
using static DTPortal.Common.CommonResponse;

namespace DTPortal.Core.Utilities
{
    public class RAServiceClient : IRAServiceClient
    {
        private readonly SSOConfig ssoConfig;
        private readonly ILogger<RAServiceClient> _logger;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IGlobalConfiguration _globalConfiguration;
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly MessageConstants Constants;
        private readonly IHelper _helper;

        public RAServiceClient(ILogger<RAServiceClient> logger,
            IUnitOfWork unitOfWork,
            IGlobalConfiguration globalConfiguration,
            IHttpClientFactory httpClientFactory,
            IHelper helper)
        {
            _logger = logger;
            _unitOfWork = unitOfWork;
            _globalConfiguration = globalConfiguration;
            _httpClientFactory = httpClientFactory;
            _helper = helper;

            _logger.LogDebug("-->RAServiceClient");

            // Get SSO Configuration
            ssoConfig = _globalConfiguration.GetSSOConfiguration();
            if (null == ssoConfig)
            {
                _logger.LogError("Get SSO Configuration failed in ra service client");
                throw new NullReferenceException();
            }

            var errorConfiguration = _globalConfiguration.GetErrorConfiguration();
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

            _logger.LogDebug("<--RAServiceClient");
        }

        // Get signature for a random number by verifying user authentication pin
        public async Task<SubscriberStatusUpdateResponse> SubscriberStatusUpdate(
            SubscriberStatusUpdateRequest subscriberStatusUpdateRequest)
        {
            _logger.LogDebug("-->SubscriberStatusUpdate");

            // Variable Declaration
            SubscriberStatusUpdateResponse response = null;
            var errorMessage = string.Empty;

            // Validate input parameters
            if (null == subscriberStatusUpdateRequest)
            {
                _logger.LogError("Invalid Input Parameter");
                return response;
            }

            if (string.IsNullOrEmpty(ssoConfig.ra_service_config.base_address) ||
                 string.IsNullOrEmpty(ssoConfig.ra_service_config.status_update_uri) ||
                 null == subscriberStatusUpdateRequest)
            {
                _logger.LogError("Invalid Input Parameter");
                return response;
            }

            _logger.LogInformation("Base Address: {0}",
                ssoConfig.ra_service_config.base_address);
            _logger.LogInformation("Status Update Uri: {0}",
                ssoConfig.ra_service_config.status_update_uri);
            try
            {
                HttpClient client = _httpClientFactory.CreateClient();
                // Assign the base address
                client.BaseAddress = new Uri(
                    ssoConfig.ra_service_config.base_address);

                // Set Request Timeout
                client.Timeout = TimeSpan.FromSeconds(51);

                // Call the webservice with post method
                var result = await client.PutAsJsonAsync
                    <SubscriberStatusUpdateRequest>
                    (ssoConfig.ra_service_config.status_update_uri,
                    subscriberStatusUpdateRequest);

                // Check the status code
                if (result.IsSuccessStatusCode)
                {
                    // Read the response
                    response = await result.Content.ReadFromJsonAsync
                        <SubscriberStatusUpdateResponse>();
                }
                else
                {
                    _logger.LogError("SubscriberStatusUpdate failed" +
                        " returned status code : {0}",
                        result.StatusCode);
                    var errorCode = ErrorCodes.RA_USER_STATUS_UPDATE_FAILED;
                    if (HttpStatusCode.ServiceUnavailable == result.StatusCode)
                        errorCode = ErrorCodes.PKI_SERVICE_VERIFY_SIGNATURE_UNAVAILABLE;
                    response = new SubscriberStatusUpdateResponse();
                    errorMessage = _helper.GetErrorMsg(errorCode);
                    response.message = errorMessage;
                    return response;

                }
            }
            catch (TimeoutException error)
            {
                _logger.LogError("SubscriberStatusUpdate failed due to" +
                    " timeout exception: {0}", error.Message);
                response = new SubscriberStatusUpdateResponse();
                errorMessage = _helper.GetErrorMsg(
                    ErrorCodes.RA_USER_STATUS_UPDATE_TIMEOUT);
                response.message = errorMessage;
                return response;
            }
            catch (Exception error)
            {
                _logger.LogError("SubscriberStatusUpdate failed: {0}",
                    error.Message);
                response = new SubscriberStatusUpdateResponse();
                errorMessage = _helper.GetErrorMsg(
                    ErrorCodes.RA_USER_STATUS_UPDATE_FAILED);
                response.message = errorMessage;
                return response;
            }

            _logger.LogDebug("<--SubscriberStatusUpdate");
            return response;
        }

        public async Task<bool> RASubscriberStatusUpdate(string status, string identifier)
        {
            var statusUpdateRequest = new SubscriberStatusUpdateRequest();
            Response response = new Response();

            statusUpdateRequest.description =
                LogClientServices.SubscriberStatusUpdate;
            statusUpdateRequest.subscriberStatus = status;
            statusUpdateRequest.subscriberUniqueId = identifier;

            var statusResponse = await SubscriberStatusUpdate(statusUpdateRequest);
            if (statusResponse.success == false)
            {
                _logger.LogError("SubscriberStatusUpdate failed, " +
                    "{0}", statusResponse.message);
                return false;
            }

            return true;
        }

    }
}
