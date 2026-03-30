using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;
using DTPortal.Core.Domain.Repositories;
using DTPortal.Core.Domain.Services.Communication;
using DTPortal.Core.Services;
using Microsoft.Extensions.Logging;
using static DTPortal.Common.CommonResponse;

namespace DTPortal.Core.Utilities
{
    public class AssertionValidationClient : IAssertionValidationClient
    {
        private readonly SSOConfig ssoConfig;
        private readonly ILogger<AssertionValidationClient> _logger;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IGlobalConfiguration _globalConfiguration;
        private readonly IHttpClientFactory _httpClientFactory;

        public AssertionValidationClient(ILogger<AssertionValidationClient> logger,
            IUnitOfWork unitOfWork,
            IGlobalConfiguration globalConfiguration,
            IHttpClientFactory httpClientFactory)
        {
            _logger = logger;
            _unitOfWork = unitOfWork;
            _globalConfiguration = globalConfiguration;
            _httpClientFactory = httpClientFactory;

            _logger.LogDebug("-->AssertionValidationClient");

            // Get SSO Configuration
            ssoConfig = _globalConfiguration.GetSSOConfiguration();
            if (null == ssoConfig)
            {
                _logger.LogError("Get SSO Configuration failed in assertion validation");
                throw new NullReferenceException();
            }

            _logger.LogDebug("<--AssertionValidationClient");
        }

        public async Task<Response> ValidateAssertion(string address,
            string requestUri,
            string assertion)
        {
            _logger.LogDebug("-->GenerateSignature");

            // Local Variable Declaration
            Response response = null;

            if (string.IsNullOrEmpty(address) ||
                string.IsNullOrEmpty(requestUri) ||
                null == assertion)
            {
                _logger.LogError("Invalid Input Parameter");
                return response;
            }

            _logger.LogInformation("Base Address: {0}", address?.SanitizeForLogging());
            _logger.LogInformation("Generate Signature Uri: {0}", requestUri?.SanitizeForLogging());

            try
            {
                //using (var client = new HttpClient())
                //{
                HttpClient client = _httpClientFactory.CreateClient();

                // Assign the base address
                client.BaseAddress = new Uri(address);

                // Set Request Timeout
                client.Timeout = TimeSpan.FromSeconds(51);

                string safeAssertion = WebUtility.HtmlEncode(assertion);

                // Pass the 'safe' version to the StringContent
                StringContent content = new StringContent(safeAssertion, Encoding.UTF8, "application/json");

                // Call the webservice with post method
                var result = await client.PostAsync(requestUri, content);

                // Check the status code
                if (result.IsSuccessStatusCode)
                {
                    // Read the response
                    response = await result.Content.ReadFromJsonAsync
                        <Response>();
                }
                else
                {
                    // Use structured logging and sanitize the status code/message
                    _logger.LogError("GenerateSignature failed. StatusCode: {StatusCode}", (int)result.StatusCode);
                }
                //}
            }
            catch (TimeoutException error)
            {
                _logger.LogError("VerifySignature failed due to timeout: {0}", error.Message?.SanitizeForLogging());
                return null;
            }
            catch (Exception error)
            {
                _logger.LogError("GenerateSignature failed: {0}", error.Message?.SanitizeForLogging());
                return null;
            }

            _logger.LogDebug("<--GenerateSignature");
            return response;
        }
    }
}
