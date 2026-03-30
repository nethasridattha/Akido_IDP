using DTPortal.Core.Domain.Services.Communication;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace DTPortal.Core.Utilities
{
    public class EmailSender:IEmailSender
    {
        private readonly ILogger<EmailSender> _logger;
        private readonly HttpClient _client;
        private readonly IConfiguration _configuration;
        public EmailSender(
            ILogger<EmailSender> logger,
            HttpClient httpClient,
            IConfiguration configuration)
        {
            _logger = logger;
            httpClient.BaseAddress = new Uri(configuration["APIServiceLocations:NotificationServiceBaseAddress"]);
            httpClient.DefaultRequestHeaders.Add("Accept", "application/json");
            _client = httpClient;
            _configuration= configuration;
        }
        public async Task<int> SendEmail(Message message)
        {
            try
            {
                List<string> emails = message.To.Select(m => m.Address).ToList();
                EmailDto emailDto = new EmailDto
                {
                    To = emails,
                    Subject = message.Subject,
                    Content = message.Content
                };
                string json = JsonConvert.SerializeObject(emailDto,
                    new JsonSerializerSettings { ContractResolver = new CamelCasePropertyNamesContractResolver() });
                StringContent content = new StringContent(json, Encoding.UTF8, "application/json");

                HttpResponseMessage response = await _client.PostAsync("api/email/send-email", content);
                if (response.StatusCode == HttpStatusCode.OK)
                {
                    APIResponse apiResponse = JsonConvert.DeserializeObject<APIResponse>(await response.Content.ReadAsStringAsync());
                    if (apiResponse.Success)
                    {
                        return 0;
                    }
                    else
                    {
                        _logger.LogError(apiResponse.Message);
                        return -1;
                    }
                }
                else
                {
                    _logger.LogError($"The request with URI={response.RequestMessage.RequestUri} failed " +
                           $"with status code={response.StatusCode}");
                    return -1;
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Unexpected error: {ex}");
                return -1;
            }
        }
    }
}
