using DTPortal.Core.Domain.Models;
using DTPortal.Core.Domain.Services.Communication;
using FirebaseAdmin.Messaging;
using Google.Apis.Logging;
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
using System.Threading;
using System.Threading.Tasks;

namespace DTPortal.Core.Utilities
{
    public class PushNotificationClient:IPushNotificationClient
    {
        private readonly IConfiguration _configuration;
        private readonly ILogger<PushNotificationClient> _logger;
        private readonly HttpClient _client;
        public PushNotificationClient(
            IConfiguration configuration,
            HttpClient httpClient,
            ILogger<PushNotificationClient> logger
            ) 
        {
            httpClient.BaseAddress = new Uri(configuration["APIServiceLocations:NotificationServiceBaseAddress"]);
            httpClient.DefaultRequestHeaders.Add("Accept", "application/json");
            _configuration = configuration;
            _logger = logger;
            _client = httpClient;
        }
        public async Task<string> SendAuthnNotification(
            AuthnNotification authnNotification)
        {
            try
            {
                string json = JsonConvert.SerializeObject(authnNotification,
                    new JsonSerializerSettings { ContractResolver = new CamelCasePropertyNamesContractResolver() });
                StringContent content = new StringContent(json, Encoding.UTF8, "application/json");

                HttpResponseMessage response = await _client.PostAsync("api/push-notification/send-authn-notification", content);
                if (response.StatusCode == HttpStatusCode.OK)
                {
                    APIResponse apiResponse = JsonConvert.DeserializeObject<APIResponse>(await response.Content.ReadAsStringAsync());
                    if (apiResponse.Success)
                    {
                        return apiResponse.Message;
                    }
                    else
                    {
                        _logger.LogError(apiResponse.Message);
                        return null;
                    }
                }
                else
                {
                    _logger.LogError($"The request with URI={response.RequestMessage.RequestUri} failed " +
                           $"with status code={response.StatusCode}");
                    return null;
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Unexpected error: {ex}");
                return null;
            }
        }
        public async Task<string> SendEConsentNotification(
            EConsentNotification eConsentNotification)
        {
            try
            {
                string json = JsonConvert.SerializeObject(eConsentNotification,
                    new JsonSerializerSettings { ContractResolver = new CamelCasePropertyNamesContractResolver() });
                StringContent content = new StringContent(json, Encoding.UTF8, "application/json");

                HttpResponseMessage response = await _client.PostAsync("api/push-notification/send-econsent-notification", content);
                if (response.StatusCode == HttpStatusCode.OK)
                {
                    APIResponse apiResponse = JsonConvert.DeserializeObject<APIResponse>(await response.Content.ReadAsStringAsync());
                    if (apiResponse.Success)
                    {
                        return apiResponse.Message;
                    }
                    else
                    {
                        _logger.LogError(apiResponse.Message);
                        return null;
                    }
                }
                else
                {
                    _logger.LogError($"The request with URI={response.RequestMessage.RequestUri} failed " +
                           $"with status code={response.StatusCode}");
                    return null;
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Unexpected error: {ex}");
                return null;
            }
        }
        public async Task<string> SendNotification(
            PushNotificationRequest request)
        {
            try
            {
                string json = JsonConvert.SerializeObject(request,
                    new JsonSerializerSettings { ContractResolver = new CamelCasePropertyNamesContractResolver() });
                StringContent content = new StringContent(json, Encoding.UTF8, "application/json");

                HttpResponseMessage response = await _client.PostAsync("api/push-notification/send-push-notification", content);
                if (response.StatusCode == HttpStatusCode.OK)
                {
                    APIResponse apiResponse = JsonConvert.DeserializeObject<APIResponse>(await response.Content.ReadAsStringAsync());
                    if (apiResponse.Success)
                    {
                        return apiResponse.Message;
                    }
                    else
                    {
                        _logger.LogError(apiResponse.Message);
                        return null;
                    }
                }
                else
                {
                    _logger.LogError($"The request with URI={response.RequestMessage.RequestUri} failed " +
                           $"with status code={response.StatusCode}");
                    return null;
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Unexpected error: {ex}");
                return null;
            }
        }
        public string SendWalletDelegationNotification(
            WalletDelegationNotification request)
        {
            _logger.LogDebug("-->SendEConsentNotification");

            string result = null;

            if (null == request)
            {
                _logger.LogError("Invalid Input Parameter");
                return result;
            }

            var message = new FirebaseAdmin.Messaging.Message()
            {
                Token = request.RegistrationToken,
                Data = new Dictionary<string, string>()
                {
                    ["AuthNToken"] = request.AuthnToken,
                    ["AuthNScheme"] = request.AuthnScheme,
                    ["Context"] = request.Context,
                    ["Principal"] = request.Principal,
                    ["NotaryInformation"] = request.NotaryInformation,
                    ["ValidityPeriod"] = request.ValidityPeriod,
                    ["DelegationPurpose"] = JsonConvert.SerializeObject(request.DelegationPurpose),
                    ["Title"] = "POA Delegation Request",
                    ["Body"] = "Please approve or deny"
                },

                Android = new AndroidConfig()
                {
                    Priority = Priority.High,
                },

                Apns = new ApnsConfig()
                {
                    Aps = new Aps()
                    {
                        Sound = "default",

                        Alert = new ApsAlert()
                        {
                            Title = "POA Delegation Request",
                            Body = "Please approve or deny",
                        },

                        MutableContent = true,

                    },
                },
            };

            try
            {
                _logger.LogInformation("Sending Push Notification");
                if (null == result)
                {
                    _logger.LogError("Send Notification Failed");
                    return result;
                }
            }
            catch (Exception ex)
            {
                _logger.LogError("SendEConsentNotification Failed: {0}",
                    ex.Message);
                return result;
            }

            _logger.LogInformation("Send Notification Response:{0}", result);
            _logger.LogDebug("<--SendEConsentNotification");
            return result;
        }
    }
}