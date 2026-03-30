using DTPortal.Core.Domain.Services;
using DTPortal.Core.Domain.Services.Communication;
using DTPortal.Core.DTOs;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text.Json.Nodes;
using System.Threading.Tasks;
using static DTPortal.Common.CommonResponse;

namespace DTPortal.Core.Services
{
    public class SoftwareService : ISoftwareService
    {
        private readonly ILogger<SoftwareService> _logger;
        private readonly HttpClient _client;
        private readonly IConfiguration _configuration;
        public SoftwareService(ILogger<SoftwareService> logger,
            HttpClient httpClient,
            IConfiguration configuration)
        {
            _logger = logger;
            _client = httpClient;
            _configuration = configuration;
            _client.Timeout = TimeSpan.FromMinutes(10);

            httpClient.BaseAddress = new Uri(configuration["APIServiceLocations:SelfServiceOnboardingBaseAddress"]);
            httpClient.DefaultRequestHeaders.Add("Accept", "application/json");
        }
        public async Task<IEnumerable<SoftwareNewListDTO>> GetAllSoftwareListNewAsync()
        {
            try
            {
                HttpResponseMessage response = await _client.GetAsync($"get/all-software-list");
                if (response.StatusCode == HttpStatusCode.OK)
                {
                    APIResponse apiResponse = JsonConvert.DeserializeObject<APIResponse>(await response.Content.ReadAsStringAsync());
                    if (apiResponse.Success)
                    {
                        return JsonConvert.DeserializeObject<IList<SoftwareNewListDTO>>(apiResponse.Result.ToString());
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
        public async Task<IEnumerable<SoftwareListDTO>> GetAllSoftwareListAsync()
        {
            try
            {
                HttpResponseMessage response = await _client.GetAsync($"get/all-software-list");
                if (response.StatusCode == HttpStatusCode.OK)
                {
                    APIResponse apiResponse = JsonConvert.DeserializeObject<APIResponse>(await response.Content.ReadAsStringAsync());
                    if (apiResponse.Success)
                    {
                        return JsonConvert.DeserializeObject<IList<SoftwareListDTO>>(apiResponse.Result.ToString());
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
        public async Task<ServiceResult> PublishUnpublishSoftwareAsync(int id, string status)
        {
            try
            {
                _logger.LogInformation(
                    "Calling change software status API. SoftwareId={Id}, Status={Status}", id, status);

                using var content = new MultipartFormDataContent();
                content.Add(new StringContent(id.ToString()), "softwareId");
                content.Add(new StringContent(status), "status");

                var request = new HttpRequestMessage(
                    HttpMethod.Get,
                    "change/software/status")
                {
                    Content = content
                };

                HttpResponseMessage response = await _client.SendAsync(request);

                var responseText = await response.Content.ReadAsStringAsync();

                _logger.LogInformation(
                    "API Response: StatusCode={StatusCode}, Body={Body}",
                    response.StatusCode, responseText);

                if (!response.IsSuccessStatusCode)
                {
                    return new ServiceResult(false, "Failed to change software status");
                }

                var apiResponse = JsonConvert.DeserializeObject<APIResponse>(responseText);

                if (apiResponse?.Success == true)
                {
                    return new ServiceResult(true, apiResponse.Message);
                }

                return new ServiceResult(false, apiResponse?.Message ?? "Operation failed");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Exception while changing software status");
                return new ServiceResult(false, "Unexpected server error");
            }
        }


        public async Task<ServiceResult> UploadSoftwareAsync(UploadSoftwareDTO uploadSoftware)
        {
            try
            {
                var data = new ModelData
                {
                    softwareName = uploadSoftware.SoftwareName,
                    softwareVersion = uploadSoftware.SoftwareVersion
                };

                HttpResponseMessage response = null;

                using (var multipartFormContent = new MultipartFormDataContent())
                {
                    // Add the PDF file
                    //StreamContent pdfFileStreamContent = new StreamContent(uploadSoftware.Mannual.OpenReadStream());
                    //pdfFileStreamContent.Headers.ContentType = new MediaTypeHeaderValue("application/pdf");
                    //multipartFormContent.Add(pdfFileStreamContent, name: "pdfFile", fileName: uploadSoftware.Mannual.FileName);

                    // Add the ZIP file
                    StreamContent zipFileStreamContent = new StreamContent(uploadSoftware.SoftwareZip.OpenReadStream());
                    zipFileStreamContent.Headers.ContentType = new MediaTypeHeaderValue("application/zip");
                    multipartFormContent.Add(zipFileStreamContent, name: "zipFile", fileName: uploadSoftware.SoftwareZip.FileName);

                    // Add other form data
                    multipartFormContent.Add(new StringContent(JsonConvert.SerializeObject(data)), "model");

                    _logger.LogInformation("Upload software api call start");
                    response = await _client.PostAsync("post/upload/software/by/admin", multipartFormContent);
                    _logger.LogInformation("Upload software api call end");
                }

                if (response.StatusCode == HttpStatusCode.OK)
                {
                    APIResponse apiResponse = JsonConvert.DeserializeObject<APIResponse>(await response.Content.ReadAsStringAsync());
                    if (apiResponse.Success)
                    {
                        return new ServiceResult(true, apiResponse.Message);
                    }
                    else
                    {
                        _logger.LogError(apiResponse.Message);
                        return new ServiceResult(false, apiResponse.Message);
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
            }
            return new ServiceResult(false, "Failed to upload software");
        }
    }
}
