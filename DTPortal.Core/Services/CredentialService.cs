using DTPortal.Core.Constants;
using DTPortal.Core.Domain.Models;
using DTPortal.Core.Domain.Models.RegistrationAuthority;
using DTPortal.Core.Domain.Repositories;
using DTPortal.Core.Domain.Services;
using DTPortal.Core.Domain.Services.Communication;
using DTPortal.Core.DTOs;
using DTPortal.Core.Enums;
using DTPortal.Core.Exceptions;
using DTPortal.Core.Utilities;
using Google.Apis.Auth.OAuth2;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Serialization;
using OtpNet;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;
using static System.Runtime.InteropServices.JavaScript.JSType;
using Attribute = DTPortal.Core.DTOs.Attribute;

namespace DTPortal.Core.Services
{
    public class CredentialService : ICredentialService
    {
        private readonly ILogger<CredentialService> _logger;
        private readonly HttpClient _client;
        private readonly Helper _helper;
        private readonly MessageConstants Constants;
        private readonly IMessageLocalizer _messageLocalizer;
        private readonly IGlobalConfiguration _globalConfiguration;
        private readonly string _accessTokenHeaderName;
        public CredentialService(ILogger<CredentialService> logger,
            HttpClient httpClient,
            IConfiguration configuration,
            IHttpClientFactory httpClientFactory,
            IWebHostEnvironment environment,
            IGlobalConfiguration globalConfiguration,
            IMessageLocalizer messageLocalizer,
            Helper helper)
        {
            _logger = logger;
            httpClient.BaseAddress = new Uri(configuration["APIServiceLocations:CredentialOfferBaseAddress"]);
            httpClient.DefaultRequestHeaders.Add("Accept", "application/json");
            _accessTokenHeaderName = configuration["AccessTokenHeaderName"];
            _client = httpClient;
            _globalConfiguration = globalConfiguration;
            _messageLocalizer = messageLocalizer;
            _helper = helper;

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
        public async Task<ServiceResult> GetCredentialOfferByUidAsync(string Id, string token)
        {
            try
            {
                if (_client.DefaultRequestHeaders.Contains(_accessTokenHeaderName))
                {
                    _client.DefaultRequestHeaders.Remove(_accessTokenHeaderName);
                }

                _client.DefaultRequestHeaders.Add(_accessTokenHeaderName, $"Bearer {token}");
                HttpResponseMessage response = await _client.GetAsync($"api/Credential/GetCredentialOfferByUid/{Id}");
                if (response.StatusCode == HttpStatusCode.OK)
                {
                    APIResponse apiResponse = JsonConvert.DeserializeObject<APIResponse>(await response.Content.ReadAsStringAsync());
                    if (apiResponse.Success)
                    {
                        return new ServiceResult(true, _messageLocalizer.GetMessage(Constants.CredentialListSuccess), apiResponse.Result);
                    }
                    else
                    {
                        _logger.LogError(apiResponse.Message);
                        return new ServiceResult(false, _messageLocalizer.GetMessage(Constants.InternalError));
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
                return new ServiceResult(false, _messageLocalizer.GetMessage(Constants.InternalError));
              
            }
            return null;

        }
    }
}