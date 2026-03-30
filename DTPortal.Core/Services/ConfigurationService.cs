using DTPortal.Core.Constants;
using DTPortal.Core.Domain.Models;
using DTPortal.Core.Domain.Models.RegistrationAuthority;
using DTPortal.Core.Domain.Repositories;
using DTPortal.Core.Domain.Services;
using DTPortal.Core.Domain.Services.Communication;
using DTPortal.Core.Utilities;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static DTPortal.Common.CommonResponse;

namespace DTPortal.Core.Services
{
    public class ConfigurationService : IConfigurationService
    {
        private IUnitOfWork _unitOfWork;
        // Initialize logger.
        private readonly ILogger<ConfigurationService> _logger;

        public ConfigurationService(IUnitOfWork unitOfWork,
            ILogger<ConfigurationService> logger)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
        }

        public async Task<IList<string>> GetAllScopes()
        {
            _logger.LogDebug("-->GetAllScopes");

            // Get Configuration
            var configObject = await GetConfigurationAsync<JObject>
                ("IDP_Configuration");
            if (null == configObject)
            {
                _logger.LogError("GetConfigurationAsync Failed");
                return null;
            }

            // Get supported scopes list
            var openidconnect = configObject.SelectToken("openidconnect");
            if (null == openidconnect)
            {
                _logger.LogError("Get scopes_supported Failed");
                return null;
            }


            // Get supported scopes list
            var scopes_supported = openidconnect.SelectToken("scopes_supported")
                .Values<string>().ToList();
            if (null == scopes_supported)
            {
                _logger.LogError("Get scopes_supported Failed");
                return null;
            }

            return scopes_supported;
        }

        public async Task<IList<string>> GetAllGrantTypes()
        {
            _logger.LogDebug("-->GetAllGrantTypes");

            // Get Configuration
            var configObject = await GetConfigurationAsync<JObject>
                ("IDP_Configuration");
            if (null == configObject)
            {
                _logger.LogError("GetConfigurationAsync Failed");
                return null;
            }

            // Get supported scopes list
            var openidconnect = configObject.SelectToken("openidconnect");
            if (null == openidconnect)
            {
                _logger.LogError("Get scopes_supported Failed");
                return null;
            }

            // Get supported grant types
            var grantTypesSupported = openidconnect.SelectToken("grant_types_supported")
                .Values<string>().ToList();
            if (null == grantTypesSupported)
            {
                _logger.LogError("Get scopes_supported Failed");
                return null;
            }

            _logger.LogDebug("<--GetAllGrantTypes");
            return grantTypesSupported;
        }
        public T GetPlainConfiguration<T>(string configName)
        {
            _logger.LogDebug("-->GetConfiguration");

            // Get Configuration Record
            var configRecord = _unitOfWork.Configuration.
                GetConfigurationByName(configName);
            if (null == configRecord || null == configRecord.Value)
            {
                _logger.LogError("Get Configuration Record Failed in GetPlainConfiguration");
                return default;
            }


            // Convert Plain data string to object
            T config = JsonConvert.DeserializeObject<T>(configRecord.Value);
            if (EqualityComparer<T>.Default.Equals(config, default(T)))
            {
                _logger.LogError("Convert Plain data string to object Failed");
                return default;
            }

            _logger.LogDebug("<--GetConfiguration");
            return config;
        }
        public T GetConfiguration<T>(string configName)
        {
            _logger.LogDebug("-->GetConfiguration");

            // Get Configuration Record
            var configRecord = _unitOfWork.Configuration.
                GetConfigurationByName(configName);
            if (null == configRecord || null == configRecord.Value)
            {
                _logger.LogError("ConfigName - "+configName.ToString());
                _logger.LogError("Get Configuration Record Failed in GetConfiguration");
                return default;
            }

            // Get Plain data from secured data
            var plainData = PKIMethods.Instance.
                PKIDecryptSecureWireData(configRecord.Value);
            if (null == plainData)
            {
                _logger.LogError("PKIDecryptSecureWireData Failed");
                return default;
            }

            // Convert Plain data string to object
            T config = JsonConvert.DeserializeObject<T>(plainData);
            if (EqualityComparer<T>.Default.Equals(config, default(T)))
            {
                _logger.LogError("Convert Plain data string to object Failed");
                return default;
            }

            _logger.LogDebug("<--GetConfiguration");
            return config;
        }

        public async Task<T> GetConfigurationAsync<T>(string configName)
        {
            _logger.LogDebug("-->GetConfiguration");

            // Get Configuration Record
            var configRecord = await _unitOfWork.Configuration.
                GetConfigurationByNameAsync(configName);
            if (null == configRecord || null == configRecord.Value)
            {
                _logger.LogError("Get Configuration Record Failed in GetConfigurationAsync");
                return default;
            }

            // Get Plain data from secured data
            var plainData = PKIMethods.Instance.
                PKIDecryptSecureWireData(configRecord.Value);
            if (null == plainData)
            {
                _logger.LogError("PKIDecryptSecureWireData Failed");
                return default;
            }

            // Convert Plain data string to object
            T config = JsonConvert.DeserializeObject<T>(plainData);
            if (EqualityComparer<T>.Default.Equals(config, default(T)))
            {
                _logger.LogError("Convert Plain data string to object Failed");
                return default;
            }

            _logger.LogDebug("<--GetConfiguration");
            return config;
        }

        public ConfigurationResponse SetConfiguration(
                string configName, object config)
        {
            _logger.LogDebug("-->SetConfiguration");

            // Get Configuration Record
            var configRecord = _unitOfWork.Configuration.
                GetConfigurationByName(configName);
            if (null == configRecord || null == configRecord.Value)
            {
                _logger.LogError("Get Configuration Record Failed in set configuration");
                return null;
            }

            // Convert Configuration Object to string
            var serializedConfig = JsonConvert.SerializeObject(config);
            if (null == serializedConfig)
            {
                _logger.LogError("Convert Configuration Object to string Failed");
                return null;
            }

            // Create Secure data from plain data
            var secureData = PKIMethods.Instance.
                PKICreateSecureWireData(serializedConfig);
            if (null == secureData)
            {
                _logger.LogError("PKICreateSecureWireData Failed");
                return null;
            }

            // Keep the updated data
            configRecord.Value = secureData;

            try
            {
                _unitOfWork.Configuration.Update(configRecord);
                _unitOfWork.Save();
                return new ConfigurationResponse(configRecord);
            }
            catch
            {
                return null;
            }
        }

        public async Task<ConfigurationResponse> SetConfigurationAsync(
                string configName, object config, string updatedBy,
                bool makerCheckerFlag = false)
        {
            _logger.LogDebug("-->SetConfiguration");
            var secureData = string.Empty;

            // Get Configuration Record
            var configRecord = await _unitOfWork.Configuration.
                GetConfigurationByNameAsync(configName);
            if (null == configRecord || null == configRecord.Value)
            {
                _logger.LogError("Get Configuration Record Failed in set config async");
                return null;
            }

            try
            {
                // Convert Configuration Object to string
                var serializedConfig = JsonConvert.SerializeObject(config);
                if (null == serializedConfig)
                {
                    _logger.LogError("Convert Configuration Object to string Failed");
                    return null;
                }

                // Create Secure data from plain data
                secureData = PKIMethods.Instance.
                    PKICreateSecureWireData(serializedConfig);
                if (null == secureData)
                {
                    _logger.LogError("PKICreateSecureWireData Failed");
                    return null;
                }

                // Keep the updated data
                configRecord.Value = secureData;
                configRecord.UpdatedBy = updatedBy;
                configRecord.ModifiedDate = DateTime.Now;

                _unitOfWork.Configuration.Update(configRecord);
                await _unitOfWork.SaveAsync();
                return new ConfigurationResponse(configRecord, "Configuration updated successfully");
            }
            catch
            {
                return null;
            }
        }

        public FaceThreshold GetThreshold()
        {
            var Threshold = _unitOfWork.Threshold.GetThreshold();
            return Threshold;
        }
        
        public async Task<string> GetActiveAuthenticationId()
        {
            var res = await _unitOfWork.Configuration.GetConfigurationByNameAsync("DEFAULT_AUTH_SCHEME");
            if(res == null)
            {
                return "";
            }
            return res.Value;
        }
        public async Task<ConfigurationResponse> UpdateDefaultAuthScheme(string Id)
        {
            try
            {
                var configuration = await _unitOfWork.Configuration.GetConfigurationByNameAsync("DEFAULT_AUTH_SCHEME");

                configuration.Value = Id;

                _unitOfWork.Configuration.Update(configuration);

                await _unitOfWork.SaveAsync();

                return new ConfigurationResponse(configuration, "Configuration updated successfully");
            }
            catch(Exception)
            {
                _logger.LogError("Failed to update configuration");
                return new ConfigurationResponse("Failed to update Configuration");
            }
        }
    }
}
