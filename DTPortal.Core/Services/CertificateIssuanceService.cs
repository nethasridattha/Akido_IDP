using DTPortal.Core.Domain.Services;
using DTPortal.Core.Domain.Services.Communication;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace DTPortal.Core.Services
{
    public class CertificateIssuanceService : ICertificateIssuanceService
    {
        private readonly ILogger<CertificateIssuanceService> _logger;
        private readonly HttpClient _client;
        private readonly IConfiguration _configuration;
        public CertificateIssuanceService(
            ILogger<CertificateIssuanceService> logger,
            IConfiguration configuration,
            HttpClient httpClient)
        {
            _logger = logger;
            httpClient.BaseAddress = new Uri(configuration["APIServiceLocations:CertificateIssuanceBaseAddress"]);
            _client = httpClient;
            _configuration = configuration;
        }

        public async Task<ServiceResult> IssueCertificateNew
            (CertificateIssueRequest certificateIssueRequest)
        {
            try
            {
                string json = JsonConvert.SerializeObject(certificateIssueRequest,
                    new JsonSerializerSettings
                    { ContractResolver = new CamelCasePropertyNamesContractResolver() });

                StringContent content = new StringContent(json, Encoding.UTF8, "application/json");

                HttpResponseMessage response = await _client.PostAsync("IssueCertificate", content);

                if (response.StatusCode == HttpStatusCode.OK)
                {
                    APIResponse apiResponse = JsonConvert.DeserializeObject<APIResponse>
                        (await response.Content.ReadAsStringAsync());

                    if (apiResponse.Success)
                    {
                        var certificateResponse = JsonConvert.DeserializeObject<CertificateResult>
                            (apiResponse.Result.ToString());

                        return new ServiceResult(true, apiResponse.Message,certificateResponse);
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
            return new ServiceResult(false, "An error occurred while Generating Certificate. Please try later.");
        }

        public async Task<ServiceResult> GenerateSignatureAsync
            (SignDataRequest signDataRequest)
        {
            try
            {
                string json = JsonConvert.SerializeObject(signDataRequest,
                    new JsonSerializerSettings
                    { ContractResolver = new CamelCasePropertyNamesContractResolver() });

                StringContent content = new StringContent(json, Encoding.UTF8, "application/json");

                HttpResponseMessage response = await _client.PostAsync("SignDataRSA", content);

                if (response.StatusCode == HttpStatusCode.OK)
                {
                    APIResponse apiResponse = JsonConvert.DeserializeObject<APIResponse>
                        (await response.Content.ReadAsStringAsync());

                    if (apiResponse.Success)
                    {

                        return new ServiceResult(true, apiResponse.Message, apiResponse.Result.ToString());
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
            return new ServiceResult(false, "An error occurred while Generating Certificate. Please try later.");
        }

    }
}
