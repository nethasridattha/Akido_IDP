using System;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using DTPortal.Core.Domain.Services.Communication;
using DTPortal.Core.Utilities;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace DTPortal.Core.Utilities
{
    public class MessageLocalizer : IMessageLocalizer

    {

        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly ILogger<MessageLocalizer> _logger;
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IConfiguration configuration;
        public MessageLocalizer(IHttpContextAccessor httpContextAccessor, IConfiguration Configuration, IHttpClientFactory httpClientFactory, ILogger<MessageLocalizer> logger)

        {
            _logger = logger;
            _httpClientFactory = httpClientFactory;
            configuration = Configuration;
            _httpContextAccessor = httpContextAccessor;

        }

        public string GetMessage(LocalizedMessage message)
        {
            var context = _httpContextAccessor.HttpContext;

            if (context == null || message == null)

                return message?.En;

            var acceptLanguage = context.Request.Headers["Accept-Language"].ToString();

            if (string.IsNullOrEmpty(acceptLanguage))

                return message.En;

            var language = acceptLanguage.Split(',').FirstOrDefault()?.Trim().ToLower();

            if (language != null && language.StartsWith("ar"))

                return message.Ar ?? message.En;

            return message.En;

        }

        public string GetLocalizedDisplayName(LocalizedDisplayName displayName)
        {
            var context = _httpContextAccessor.HttpContext;

            if (context == null || displayName == null)
                return displayName.DisplayNameEn;

            var acceptLanguage = context.Request.Headers["Accept-Language"].ToString();
            if (string.IsNullOrEmpty(acceptLanguage))
                return displayName.DisplayNameEn;

            var language = acceptLanguage.Split(',').FirstOrDefault()?.Trim().ToLower();
            if (language != null && language.StartsWith("ar"))
                return displayName.DisplayNameAr ?? displayName.DisplayNameEn;

            return displayName.DisplayNameEn;
        }

        public string GetLocalizedDisplayName(string displayNameEn, string displayNameAr)
        {
            var context = _httpContextAccessor.HttpContext;

            if (context == null)
                return displayNameEn;

            var acceptLanguage = context.Request.Headers["Accept-Language"].ToString();
            if (string.IsNullOrEmpty(acceptLanguage))
                return displayNameEn;

            var language = acceptLanguage.Split(',').FirstOrDefault()?.Trim().ToLower();
            if (language != null && language.StartsWith("ar"))
                return displayNameAr ?? displayNameEn;

            return displayNameEn;
        }


        public async Task<string> GetLocalizedDisplayNameFromPreferenceAsync(string displayNameEn, string displayNameAr, string suid)
        {
            try
            {
                var prefClient = _httpClientFactory.CreateClient();

                string url = configuration["APIServiceLocations:UserPreferenceBaseAddress"]
                             + "api/get/subscriber/preferences/by/suid/" + suid;

                var response = await prefClient.GetAsync(url);

                if (response.StatusCode == HttpStatusCode.OK)
                {
                    var responseString = await response.Content.ReadAsStringAsync();

                    APIResponse apiResponse = JsonConvert.DeserializeObject<APIResponse>(responseString);

                    if (apiResponse.Success && apiResponse.Result != null)
                    {
                        var jsonString = JObject.FromObject(apiResponse.Result);

                        var language = jsonString["languagePreferred"]?.ToString()?.ToLower() ?? "en";

                        if (language.StartsWith("ar"))
                            return displayNameAr ?? displayNameEn;
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Unable to retrieve user preferences for suid {suid}");
                _logger.LogError(ex.Message);
            }

            return displayNameEn;
        }

        public async Task<string> GetUserPreferredLanguageAsync(string suid)
        {
            try
            {
                var prefClient = _httpClientFactory.CreateClient();

                string url = configuration["APIServiceLocations:UserPreferenceBaseAddress"]
                             + "api/get/subscriber/preferences/by/suid/" + suid;

                var response = await prefClient.GetAsync(url);

                if (response.StatusCode == HttpStatusCode.OK)
                {
                    var responseString = await response.Content.ReadAsStringAsync();

                    APIResponse apiResponse = JsonConvert.DeserializeObject<APIResponse>(responseString);

                    if (apiResponse.Success && apiResponse.Result != null)
                    {
                        var jsonString = JObject.FromObject(apiResponse.Result);

                        return jsonString["languagePreferred"]?.ToString()?.ToLower() ?? "en";
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Unable to retrieve language preference for suid {suid}");
                _logger.LogError(ex.Message);
            }

            return "en";
        }

    }
}
