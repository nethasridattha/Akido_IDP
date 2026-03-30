using System;
using System.IO;
using System.Text;
using System.Net;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Serialization;

using DTPortal.Core.DTOs;
using DTPortal.Core.Utilities;
using DTPortal.Core.Exceptions;
using DTPortal.Core.Domain.Services;
using DTPortal.Core.Domain.Services.Communication;
using System.Transactions;

namespace DTPortal.Core.Services
{
    public class LogReportService : ILogReportService
    {
        private readonly IConfiguration _configuration;
        private readonly ILogger<LogReportService> _logger;
        private readonly IHttpClientFactory _httpClientFactory;

        public LogReportService(IConfiguration configuration,
            ILogger<LogReportService> logger,
            IHttpClientFactory httpClientFactory)
        {
            _configuration = configuration;
            _logger = logger;
            _httpClientFactory = httpClientFactory;
        }

        public async Task<IEnumerable<AdminActivity>> GetAdminLogReportAsync(int page = 1)
        {
            HttpClient client = _httpClientFactory.CreateClient("ignoreSSL");
            client.BaseAddress = new Uri(_configuration["APIServiceLocations:AdminLogServiceBaseAddress"]);
            client.Timeout = TimeSpan.FromSeconds(30);

            try
            {
                HttpResponseMessage response = await client.GetAsync($"api/audit-logs/{page}");
                if (response.StatusCode == HttpStatusCode.OK)
                {
                    APIResponse apiResponse = JsonConvert.DeserializeObject<APIResponse>(await response.Content.ReadAsStringAsync());
                    if (apiResponse.Success)
                    {
                        JObject logsArray = (JObject)JToken.FromObject(apiResponse.Result);
                        return JsonConvert.DeserializeObject<IEnumerable<AdminActivity>>(logsArray["logs"].ToString());
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

        public async Task<PaginatedList<LogReportDTO>> GetRegistrationAuthorityLogReportAsync(string startDate, string endDate, string identifier = null,
            string serviceName = null, string transactionType = null, string signatureType = null, bool eSealUsed = false, int page = 1, int perPage = 10)
        {
            HttpClient client = _httpClientFactory.CreateClient("ignoreSSL");
            client.BaseAddress = new Uri(_configuration["APIServiceLocations:RALogServiceBaseAddress"]);

            try
            {
                string json = JsonConvert.SerializeObject(
                    new
                    {
                        Identifier = identifier,
                        ServiceName = serviceName,
                        TransactionType = transactionType,
                        SignatureType = signatureType,
                        ESealUsed = eSealUsed,
                        StartDate = startDate,
                        EndDate = endDate,
                        PerPage = perPage
                    }, new JsonSerializerSettings { ContractResolver = new CamelCasePropertyNamesContractResolver() });
                StringContent content = new StringContent(json, Encoding.UTF8, "application/json");

                HttpResponseMessage response = await client.PostAsync($"api/audit-logs/{page}", content);
                if (response.StatusCode == HttpStatusCode.OK)
                {
                    APIResponse apiResponse = JsonConvert.DeserializeObject<APIResponse>(await response.Content.ReadAsStringAsync());
                    if (apiResponse.Success)
                    {
                        JObject result = (JObject)JToken.FromObject(apiResponse.Result);
                        var logs = JsonConvert.DeserializeObject<IEnumerable<LogReportDTO>>(result["data"].ToString());
                        return new PaginatedList<LogReportDTO>(logs, Convert.ToInt32(result["currentPage"]), perPage, Convert.ToInt32(result["totalPages"]), Convert.ToInt32(result["totalCount"]));
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

        public async Task<PaginatedList<LogReportDTO>> GetOnboardingLogReportAsync(string startDate, string endDate, string identifier = null,
            string serviceName = null, string transactionType = null, int page = 1, int perPage = 10)
        {
            HttpClient client = _httpClientFactory.CreateClient("ignoreSSL");
            client.BaseAddress = new Uri(_configuration["APIServiceLocations:OnboardingLogServiceBaseAddress"]);

            try
            {
                string json = JsonConvert.SerializeObject(
                    new
                    {
                        Identifier = identifier,
                        ServiceName = serviceName,
                        TransactionType = transactionType,
                        StartDate = startDate,
                        EndDate = endDate,
                        PerPage = perPage
                    }, new JsonSerializerSettings { ContractResolver = new CamelCasePropertyNamesContractResolver() });
                StringContent content = new StringContent(json, Encoding.UTF8, "application/json");

                HttpResponseMessage response = await client.PostAsync($"api/audit-logs/{page}", content);
                if (response.StatusCode == HttpStatusCode.OK)
                {
                    APIResponse apiResponse = JsonConvert.DeserializeObject<APIResponse>(await response.Content.ReadAsStringAsync());
                    if (apiResponse.Success)
                    {
                        JObject result = (JObject)JToken.FromObject(apiResponse.Result);
                        var logs = JsonConvert.DeserializeObject<IEnumerable<LogReportDTO>>(result["data"].ToString());
                        return new PaginatedList<LogReportDTO>(logs, Convert.ToInt32(result["currentPage"]), perPage, Convert.ToInt32(result["totalPages"]), Convert.ToInt32(result["totalCount"]));
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

        public async Task<PaginatedList<LogReportDTO>> GetSigningServiceLogReportAsync(string startDate, string endDate, string identifier = null,
            string serviceName = null, string transactionType = null, string signatureType = null, bool eSealUsed = false, int page = 1, int perPage = 10)
        {
            HttpClient client = _httpClientFactory.CreateClient("ignoreSSL");
            client.BaseAddress = new Uri(_configuration["APIServiceLocations:SigningLogServiceBaseAddress"]);

            try
            {
                string json = JsonConvert.SerializeObject(
                     new
                     {
                         Identifier = identifier,
                         ServiceName = serviceName,
                         TransactionType = transactionType,
                         SignatureType = signatureType,
                         ESealUsed = eSealUsed,
                         StartDate = startDate,
                         EndDate = endDate,
                         PerPage = perPage
                     }, new JsonSerializerSettings { ContractResolver = new CamelCasePropertyNamesContractResolver() });
                StringContent content = new StringContent(json, Encoding.UTF8, "application/json");

                HttpResponseMessage response = await client.PostAsync($"api/audit-logs/{page}", content);
                if (response.StatusCode == HttpStatusCode.OK)
                {
                    APIResponse apiResponse = JsonConvert.DeserializeObject<APIResponse>(await response.Content.ReadAsStringAsync());
                    if (apiResponse.Success)
                    {
                        JObject result = (JObject)JToken.FromObject(apiResponse.Result);
                        var logs = JsonConvert.DeserializeObject<IEnumerable<LogReportDTO>>(result["data"].ToString());
                        return new PaginatedList<LogReportDTO>(logs, Convert.ToInt32(result["currentPage"]), perPage, Convert.ToInt32(result["totalPages"]), Convert.ToInt32(result["totalCount"]));
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

        public async Task<PaginatedList<LogReportDTO>> GetDigitalAuthenticationLogReportAsync(string startDate, string endDate, string identifier = null,
            string serviceName = null, string transactionType = null, int page = 1, int perPage = 10)
        {
            HttpClient client = _httpClientFactory.CreateClient("ignoreSSL");
            client.BaseAddress = new Uri(_configuration["APIServiceLocations:IDPLogServiceBaseAddress"]);

            try
            {
                string json = JsonConvert.SerializeObject(
                    new
                    {
                        Identifier = identifier,
                        ServiceName = serviceName,
                        TransactionType = transactionType,
                        StartDate = startDate,
                        EndDate = endDate,
                        PerPage = perPage
                    }, new JsonSerializerSettings { ContractResolver = new CamelCasePropertyNamesContractResolver() });
                StringContent content = new StringContent(json, Encoding.UTF8, "application/json");

                HttpResponseMessage response = await client.PostAsync($"api/audit-logs/{page}", content);
                if (response.StatusCode == HttpStatusCode.OK)
                {
                    APIResponse apiResponse = JsonConvert.DeserializeObject<APIResponse>(await response.Content.ReadAsStringAsync());
                    if (apiResponse.Success)
                    {
                        JObject result = (JObject)JToken.FromObject(apiResponse.Result);
                        var logs = JsonConvert.DeserializeObject<IEnumerable<LogReportDTO>>(result["data"].ToString());
                        return new PaginatedList<LogReportDTO>(logs, Convert.ToInt32(result["currentPage"]), perPage, Convert.ToInt32(result["totalPages"]), Convert.ToInt32(result["totalCount"]));
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

        public async Task<PaginatedList<LogReportDTO>> GetCentralLogReportAsync(string startDate, string endDate, string identifier = null,
            string serviceName = null, string transactionType = null, string signatureType = null, bool eSealUsed = false, int page = 1, int perPage = 10)
        {
            HttpClient client = _httpClientFactory.CreateClient("ignoreSSL");
            client.BaseAddress = new Uri(_configuration["APIServiceLocations:CentralLogServiceBaseAddress"]);

            try
            {
                string json = JsonConvert.SerializeObject(
                    new
                    {
                        Identifier = identifier,
                        ServiceName = serviceName,
                        TransactionType = transactionType,
                        SignatureType = signatureType,
                        ESealUsed = eSealUsed,
                        StartDate = startDate,
                        EndDate = endDate,
                        PerPage = perPage
                    }, new JsonSerializerSettings { ContractResolver = new CamelCasePropertyNamesContractResolver() });
                StringContent content = new StringContent(json, Encoding.UTF8, "application/json");

                HttpResponseMessage response = await client.PostAsync($"api/audit-logs/{page}", content);
                if (response.StatusCode == HttpStatusCode.OK)
                {
                    APIResponse apiResponse = JsonConvert.DeserializeObject<APIResponse>(await response.Content.ReadAsStringAsync());
                    if (apiResponse.Success)
                    {
                        JObject result = (JObject)JToken.FromObject(apiResponse.Result);
                        var logs = JsonConvert.DeserializeObject<IEnumerable<LogReportDTO>>(result["data"].ToString());
                        return new PaginatedList<LogReportDTO>(logs, Convert.ToInt32(result["currentPage"]), perPage, Convert.ToInt32(result["totalPages"]), Convert.ToInt32(result["totalCount"]));
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

        public async Task<PaginatedList<AdminLogReportDTO>> GetAdminLogReportAsync(string startDate, string endDate, string userName = null,
            string moduleName = null, int page = 1, int perPage = 10)
        {
            HttpClient client = _httpClientFactory.CreateClient("ignoreSSL");
            client.BaseAddress = new Uri(_configuration["APIServiceLocations:AdminLogServiceBaseAddress"]);

            try
            {
                string json = JsonConvert.SerializeObject(
                    new
                    {
                        UserName = userName,
                        ModuleName = moduleName,
                        StartDate = startDate,
                        EndDate = endDate,
                        PerPage = perPage
                    }, new JsonSerializerSettings { ContractResolver = new CamelCasePropertyNamesContractResolver() });
                StringContent content = new StringContent(json, Encoding.UTF8, "application/json");

                using (HttpResponseMessage response = await client.PostAsync($"api/audit-logs/{page}", content))
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
                                        var logs = JsonConvert.DeserializeObject<IEnumerable<AdminLogReportDTO>>(result["data"].ToString());
                                        return new PaginatedList<AdminLogReportDTO>(logs, Convert.ToInt32(result["currentPage"]), perPage, Convert.ToInt32(result["totalPages"]), Convert.ToInt32(result["totalCount"]));
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

        public async Task<PaginatedList<OnboardingFailedLogReportDTO>> GetOnboardingFailedLogReportAsync(string startDate, string endDate, string documentNumber, int page = 1, int perPage = 10)
        {
            HttpClient client = _httpClientFactory.CreateClient("ignoreSSL");
            client.BaseAddress = new Uri(_configuration["APIServiceLocations:OnboardingLogServiceBaseAddress"]);

            try
            {
                string json = JsonConvert.SerializeObject(
                    new
                    {
                        StartDate = startDate,
                        EndDate = endDate,
                        DocumentNumber = documentNumber,
                        PerPage = perPage
                    }, new JsonSerializerSettings { ContractResolver = new CamelCasePropertyNamesContractResolver() });
                StringContent content = new StringContent(json, Encoding.UTF8, "application/json");

                using (HttpResponseMessage response = await client.PostAsync($"api/nira-log/{page}", content))
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
                                        var logs = JsonConvert.DeserializeObject<IEnumerable<OnboardingFailedLogReportDTO>>(result["data"].ToString());
                                        return new PaginatedList<OnboardingFailedLogReportDTO>(logs, Convert.ToInt32(result["currentPage"]), perPage, Convert.ToInt32(result["totalPages"]), Convert.ToInt32(result["totalCount"]));
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

        public async Task<PaginatedList<LogReportDTO>> GetSigningFailedLogReportAsync(string startDate, string endDate, int page = 1, int perPage = 10)
        {
            HttpClient client = _httpClientFactory.CreateClient("ignoreSSL");
            client.BaseAddress = new Uri(_configuration["APIServiceLocations:SigningFailedLogBaseAddress"]);

            try
            {
                string json = JsonConvert.SerializeObject(
                    new
                    {
                        StartDate = startDate,
                        EndDate = endDate,
						TransactionStatus = "FAILED",
                        TransactionType = "SIGNING",
                        PerPage = perPage
                    }, new JsonSerializerSettings { ContractResolver = new CamelCasePropertyNamesContractResolver() });
                StringContent content = new StringContent(json, Encoding.UTF8, "application/json");

                HttpResponseMessage response = await client.PostAsync($"api/audit-logs/onboarding-failure/{page}", content);
                if (response.StatusCode == HttpStatusCode.OK)
                {
                    APIResponse apiResponse = JsonConvert.DeserializeObject<APIResponse>(await response.Content.ReadAsStringAsync());
                    if (apiResponse.Success)
                    {
                        JObject result = (JObject)JToken.FromObject(apiResponse.Result);
                        var logs = JsonConvert.DeserializeObject<IEnumerable<LogReportDTO>>(result["data"].ToString());
                        return new PaginatedList<LogReportDTO>(logs, Convert.ToInt32(result["currentPage"]), 10, Convert.ToInt32(result["totalPages"]), Convert.ToInt32(result["totalCount"]));
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

                //using (HttpResponseMessage response = await client.PostAsync($"api/audit-logs/onboarding-failure/{page}", content))
                //{
                //    if (response.StatusCode == HttpStatusCode.OK)
                //    {
                //        using (Stream stream = await response.Content.ReadAsStreamAsync())
                //        {
                //            using (StreamReader streamReader = new StreamReader(stream))
                //            {
                //                using (JsonReader reader = new JsonTextReader(streamReader))
                //                {
                //                    JsonSerializer serializer = new JsonSerializer();
                //                    APIResponse apiResponse = serializer.Deserialize<APIResponse>(reader);
                //                    if (apiResponse.Success)
                //                    {
                //                        JObject result = (JObject)JToken.FromObject(apiResponse.Result);
                //                        var logs = JsonConvert.DeserializeObject<IEnumerable<LogReportDTO>>(result["data"].ToString());
                //                        return new PaginatedList<LogReportDTO>(logs, Convert.ToInt32(result["currentPage"]), perPage, Convert.ToInt32(result["totalPages"]), Convert.ToInt32(result["totalCount"]));
                //                    }
                //                    else
                //                    {
                //                        _logger.LogError(apiResponse.Message);
                //                    }
                //                }
                //            }
                //        }
                //    }
                //    else
                //    {
                //        _logger.LogError($"The request with URI={response.RequestMessage.RequestUri} failed " +
                //                   $"with status code={response.StatusCode}");
                //    }
                //}
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
            }

            return null;
        }

        public async Task<PaginatedList<LogReportDTO>> GetAuthenticationFailedLogReportAsync(string startDate, string endDate, int page = 1, int perPage = 10)
		{
			HttpClient client = _httpClientFactory.CreateClient("ignoreSSL");
			client.BaseAddress = new Uri(_configuration["APIServiceLocations:AuthenticationFailedLogBaseAddress"]);

			try
			{
				string json = JsonConvert.SerializeObject(
					new
					{
						StartDate = startDate,
						EndDate = endDate,
						TransactionStatus = "FAILED",
						TransactionType = "AUTHENTICATION",
						PerPage = perPage
					}, new JsonSerializerSettings { ContractResolver = new CamelCasePropertyNamesContractResolver() });
				StringContent content = new StringContent(json, Encoding.UTF8, "application/json");

                HttpResponseMessage response = await client.PostAsync($"api/audit-logs/onboarding-failure/{page}", content);
                if (response.StatusCode == HttpStatusCode.OK)
                {
                    APIResponse apiResponse = JsonConvert.DeserializeObject<APIResponse>(await response.Content.ReadAsStringAsync());
                    if (apiResponse.Success)
                    {
                        JObject result = (JObject)JToken.FromObject(apiResponse.Result);
                        var logs = JsonConvert.DeserializeObject<IEnumerable<LogReportDTO>>(result["data"].ToString());
                        return new PaginatedList<LogReportDTO>(logs, Convert.ToInt32(result["currentPage"]), 10, Convert.ToInt32(result["totalPages"]), Convert.ToInt32(result["totalCount"]));
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

                //using (HttpResponseMessage response = await client.PostAsync($"api/audit-logs/onboarding-failure/{page}", content))
                //{
                //	if (response.StatusCode == HttpStatusCode.OK)
                //	{
                //		using (Stream stream = await response.Content.ReadAsStreamAsync())
                //		{
                //			using (StreamReader streamReader = new StreamReader(stream))
                //			{
                //				using (JsonReader reader = new JsonTextReader(streamReader))
                //				{
                //					JsonSerializer serializer = new JsonSerializer();
                //					APIResponse apiResponse = serializer.Deserialize<APIResponse>(reader);
                //					if (apiResponse.Success)
                //					{
                //						JObject result = (JObject)JToken.FromObject(apiResponse.Result);
                //						var logs = JsonConvert.DeserializeObject<IEnumerable<LogReportDTO>>(result["data"].ToString());
                //						return new PaginatedList<LogReportDTO>(logs, Convert.ToInt32(result["currentPage"]), perPage, Convert.ToInt32(result["totalPages"]), Convert.ToInt32(result["totalCount"]));
                //					}
                //					else
                //					{
                //						_logger.LogError(apiResponse.Message);
                //					}
                //				}
                //			}
                //		}
                //	}
                //	else
                //	{
                //		_logger.LogError($"The request with URI={response.RequestMessage.RequestUri} failed " +
                //				   $"with status code={response.StatusCode}");
                //	}
                //}
            }
			catch (Exception ex)
			{
				_logger.LogError(ex, ex.Message);
			}

			return null;
		}

		public async Task<PaginatedList<LogReportDTO>> GetRegistrationAuthorityLogReportByCorrelationIDAsync(string correlationID)
        {
            HttpClient client = _httpClientFactory.CreateClient("ignoreSSL");
            client.BaseAddress = new Uri(_configuration["APIServiceLocations:RALogServiceBaseAddress"]);

            try
            {
                HttpResponseMessage response = await client.GetAsync($"api/audit-logs/correlationID={correlationID}");
                if (response.StatusCode == HttpStatusCode.OK)
                {
                    APIResponse apiResponse = JsonConvert.DeserializeObject<APIResponse>(await response.Content.ReadAsStringAsync());
                    if (apiResponse.Success)
                    {
                        JObject result = (JObject)JToken.FromObject(apiResponse.Result);
                        var logs = JsonConvert.DeserializeObject<IEnumerable<LogReportDTO>>(result["data"].ToString());
                        return new PaginatedList<LogReportDTO>(logs, Convert.ToInt32(result["currentPage"]), 10, Convert.ToInt32(result["totalPages"]), Convert.ToInt32(result["totalCount"]));
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

        public async Task<PaginatedList<LogReportDTO>> GetOnboardingLogReportByCorrelationIDAsync(string correlationID)
        {
            HttpClient client = _httpClientFactory.CreateClient("ignoreSSL");
            client.BaseAddress = new Uri(_configuration["APIServiceLocations:OnboardingLogServiceBaseAddress"]);

            try
            {
                HttpResponseMessage response = await client.GetAsync($"api/audit-logs/correlationID={correlationID}");
                if (response.StatusCode == HttpStatusCode.OK)
                {
                    APIResponse apiResponse = JsonConvert.DeserializeObject<APIResponse>(await response.Content.ReadAsStringAsync());
                    if (apiResponse.Success)
                    {
                        JObject result = (JObject)JToken.FromObject(apiResponse.Result);
                        var logs = JsonConvert.DeserializeObject<IEnumerable<LogReportDTO>>(result["data"].ToString());
                        return new PaginatedList<LogReportDTO>(logs, Convert.ToInt32(result["currentPage"]), 10, Convert.ToInt32(result["totalPages"]), Convert.ToInt32(result["totalCount"]));
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

        public async Task<PaginatedList<LogReportDTO>> GetSigningServiceLogReportByCorrelationIDAsync(string correlationID, int page = 1, int perPage = 10)
        {
            HttpClient client = _httpClientFactory.CreateClient("ignoreSSL");
            client.BaseAddress = new Uri(_configuration["APIServiceLocations:SigningLogServiceBaseAddress"]);

            try
            {
                string json = JsonConvert.SerializeObject(
                    new
                    {
                        CorrelationID = correlationID,
                    }, new JsonSerializerSettings { ContractResolver = new CamelCasePropertyNamesContractResolver() });
                StringContent content = new StringContent(json, Encoding.UTF8, "application/json");
                _logger.LogInformation($"Request JSON: {json}");
                using (HttpResponseMessage response = await client.PostAsync($"api/audit-logs/{page}", content))
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
                                        return new PaginatedList<LogReportDTO>(logs, Convert.ToInt32(result["currentPage"]), 10, Convert.ToInt32(result["totalPages"]), Convert.ToInt32(result["totalCount"]));
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

        public async Task<PaginatedList<LogReportDTO>> GetDigitalAuthenticationLogReportByCorrelationIDAsync(string correlationID, int page = 1, int perPage = 10)
        {
            HttpClient client = _httpClientFactory.CreateClient("ignoreSSL");
            client.BaseAddress = new Uri(_configuration["APIServiceLocations:IDPLogServiceBaseAddress"]);

            try
            {
                string json = JsonConvert.SerializeObject(
                    new
                    {
                        CorrelationID = correlationID,
                    }, new JsonSerializerSettings { ContractResolver = new CamelCasePropertyNamesContractResolver() });
                StringContent content = new StringContent(json, Encoding.UTF8, "application/json");
                _logger.LogInformation($"Request JSON: {json}");
                using (HttpResponseMessage response = await client.PostAsync($"api/audit-logs/{page}", content))
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
                                        return new PaginatedList<LogReportDTO>(logs, Convert.ToInt32(result["currentPage"]), 10, Convert.ToInt32(result["totalPages"]), Convert.ToInt32(result["totalCount"]));
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

        public async Task<PaginatedList<LogReportDTO>> GetSubscriberSpecificDigitalAuthenticationLogReportAsync(string startDate, string endDate, string identifier, string transactionStatus, int page = 1, int perPage = 10)
        {
            HttpClient client = _httpClientFactory.CreateClient("ignoreSSL");
            client.BaseAddress = new Uri(_configuration["APIServiceLocations:CentralLogServiceBaseAddress"]);

            try
            {
                string json = JsonConvert.SerializeObject(
                    new
                    {
                        Identifier = identifier,
                        StartDate = startDate,
                        EndDate = endDate,
                        TransactionStatus = transactionStatus,
                        PerPage = perPage
                    }, new JsonSerializerSettings { ContractResolver = new CamelCasePropertyNamesContractResolver() });
                StringContent content = new StringContent(json, Encoding.UTF8, "application/json");

                using (HttpResponseMessage response = await client.PostAsync($"api/audit-logs/authn/{page}", content))
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
                                        return new PaginatedList<LogReportDTO>(logs, Convert.ToInt32(result["currentPage"]), perPage, Convert.ToInt32(result["totalPages"]), Convert.ToInt32(result["totalCount"]));
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

        public async Task<PaginatedList<LogReportDTO>> GetSubscriberSpecificWalletTransactionLogReportAsync(string startDate, string endDate, string identifier, string transactionStatus, int page = 1, int perPage = 10)
        {
            HttpClient client = _httpClientFactory.CreateClient("ignoreSSL");
            client.BaseAddress = new Uri(_configuration["APIServiceLocations:CentralLogServiceBaseAddress"]);

            try
            {
                string json = JsonConvert.SerializeObject(
                    new
                    {
                        Identifier = identifier,
                        StartDate = startDate,
                        EndDate = endDate,
                        TransactionStatus = transactionStatus,
                        PerPage = perPage
                    }, new JsonSerializerSettings { ContractResolver = new CamelCasePropertyNamesContractResolver() });
                StringContent content = new StringContent(json, Encoding.UTF8, "application/json");

                using (HttpResponseMessage response = await client.PostAsync($"api/audit-logs/wallet/authentication/{page}", content))
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
                                        return new PaginatedList<LogReportDTO>(logs, Convert.ToInt32(result["currentPage"]), perPage, Convert.ToInt32(result["totalPages"]), Convert.ToInt32(result["totalCount"]));
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

        public async Task<PaginatedList<LogReportDTO>> GetSubscriberSpecificSigningLogReportAsync(string startDate, string endDate, string identifier, string transactionStatus, int page = 1, int perPage = 10)
        {
            HttpClient client = _httpClientFactory.CreateClient("ignoreSSL");
            client.BaseAddress = new Uri(_configuration["APIServiceLocations:CentralLogServiceBaseAddress"]);

            try
            {
                string json = JsonConvert.SerializeObject(
                    new
                    {
                        Identifier = identifier,
                        StartDate = startDate,
                        EndDate = endDate,
                        TransactionStatus = transactionStatus,
                        PerPage = perPage
                    }, new JsonSerializerSettings { ContractResolver = new CamelCasePropertyNamesContractResolver() });
                StringContent content = new StringContent(json, Encoding.UTF8, "application/json");

                using (HttpResponseMessage response = await client.PostAsync($"api/audit-logs/signing/{page}", content))
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
                                        return new PaginatedList<LogReportDTO>(logs, Convert.ToInt32(result["currentPage"]), perPage, Convert.ToInt32(result["totalPages"]), Convert.ToInt32(result["totalCount"]));
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

        public async Task<PaginatedList<LogReportDTO>> GetSubscriberSpecificOnboardingLogReportAsync(string email, string identifier)
        {
            HttpClient client = _httpClientFactory.CreateClient("ignoreSSL");
            client.BaseAddress = new Uri(_configuration["APIServiceLocations:OnboardingLogServiceBaseAddress"]);

            try
            {
                string json = JsonConvert.SerializeObject(
                    new
                    {
                        Suid = identifier,
                        Email = email
                    }, new JsonSerializerSettings { ContractResolver = new CamelCasePropertyNamesContractResolver() });
                StringContent content = new StringContent(json, Encoding.UTF8, "application/json");

                using (HttpResponseMessage response = await client.PostAsync($"api/audit-logs/onboarding/log", content))
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
                                        var otpLogs = JsonConvert.DeserializeObject<IEnumerable<LogReportDTO>>(result["otpLog"].ToString());
                                        var niraAPILogs = JsonConvert.DeserializeObject<IEnumerable<LogReportDTO>>(result["niraApiLog"].ToString());
                                        var obLogs = JsonConvert.DeserializeObject<IEnumerable<LogReportDTO>>(result["onboardingLog"].ToString());
                                        return new PaginatedList<LogReportDTO>(obLogs.Concat(niraAPILogs).Concat(otpLogs), 0, 0, 0, 0);
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

        public async Task<PaginatedList<LogReportDTO>> GetSubscriberSpecificCertIssuanceLogReportAsync(string email, string identifier)
        {
            HttpClient client = _httpClientFactory.CreateClient("ignoreSSL");
            client.BaseAddress = new Uri(_configuration["APIServiceLocations:CentralLogServiceBaseAddress"]);

            try
            {
                string json = JsonConvert.SerializeObject(
                    new
                    {
                        Identifier = identifier,
                    }, new JsonSerializerSettings { ContractResolver = new CamelCasePropertyNamesContractResolver() });
                StringContent content = new StringContent(json, Encoding.UTF8, "application/json");

                using (HttpResponseMessage response = await client.PostAsync($"api/audit-logs/cert-log", content))
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
                                        var logs = JsonConvert.DeserializeObject<IEnumerable<LogReportDTO>>(apiResponse.Result.ToString());
                                        return new PaginatedList<LogReportDTO>(logs, 0, 0, 0, 0);
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
        public async Task<PaginatedList<LogReportDTO>> GetSubscriberSpecificAllLogReportAsync(string startDate, string endDate, string identifier, string transactionStatus, int page = 1, int perPage = 10)
        {
            HttpClient client = _httpClientFactory.CreateClient("ignoreSSL");
            client.BaseAddress = new Uri(_configuration["APIServiceLocations:CentralLogServiceBaseAddress"]);

            try
            {
                string json = JsonConvert.SerializeObject(
                    new
                    {
                        Identifier = identifier,
                        StartDate = startDate,
                        EndDate = endDate,
                        TransactionStatus = transactionStatus,
                        PerPage = perPage
                    }, new JsonSerializerSettings { ContractResolver = new CamelCasePropertyNamesContractResolver() });
                StringContent content = new StringContent(json, Encoding.UTF8, "application/json");

                using (HttpResponseMessage response = await client.PostAsync($"api/audit-logs/ALL/{page}", content))
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
                                        return new PaginatedList<LogReportDTO>(logs, Convert.ToInt32(result["currentPage"]), perPage, Convert.ToInt32(result["totalPages"]), Convert.ToInt32(result["totalCount"]));
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

        public string AddChecksum(object value)
        {
            if (value == null)
                throw new ArgumentNullException(nameof(value));

            try
            {
                return PKIMethods.Instance.AddChecksum(
                     JsonConvert.SerializeObject(value,
                     new JsonSerializerSettings { ContractResolver = new CamelCasePropertyNamesContractResolver() }));
            }
            catch (PKIException ex)
            {
                _logger.LogError(ex, ex.Message);
            }

            return null;
        }

        public ServiceResult VerifyChecksum(object logReport)
        {
            if (logReport == null)
                throw new ArgumentNullException(nameof(logReport));

            try
            {
                var result = PKIMethods.Instance.VerifyChecksum(
                     JsonConvert.SerializeObject(logReport,
                     new JsonSerializerSettings { ContractResolver = new CamelCasePropertyNamesContractResolver() }));
                if (result == true)
                    return new ServiceResult(true, "Integrity check verified successfully");
                else
                    return new ServiceResult(false, "Failed to verify Integrity check");
            }
            catch (PKIException ex)
            {
                _logger.LogError(ex, ex.Message);
            }

            return new ServiceResult(false, "An error occurred while verifying the checksum");
        }
    }
}
