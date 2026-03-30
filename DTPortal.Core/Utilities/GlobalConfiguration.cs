using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DTPortal.Core.Domain.Models;
using DTPortal.Core.Domain.Services;
using DTPortal.Core.Domain.Services.Communication;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace DTPortal.Core.Utilities
{
    public class GlobalConfiguration: IGlobalConfiguration
    {
        // Initialize logger.
        private readonly ILogger<GlobalConfiguration> _logger;
        private readonly IConfigurationService _configurationService;
        private readonly IConfiguration configuration;
        private static bool initLibrary = false;
        private static SSOConfig ssoConfiguration = null;
        private static idp_configuration idpConfiguration = null;
        private static string fcmConfiguration = null;
        private static string pkiLibConfiguration = null;
        private static ConsentConfiguration consentConfiguration = null;
        private static ErrorConfiguration errorConfiguration = null;
        private static string adminPortalClientId = null;
        private static string adminPortalClientSecret = null;
        private static string signingPortalClientId = null;
        private static ThresholdConfiguration thresholdConfiguration = new ThresholdConfiguration();

        public GlobalConfiguration(ILogger<GlobalConfiguration> logger,
            IConfigurationService configurationService, IConfiguration Configuration)
        {
            _logger = logger;
            _configurationService = configurationService;
            configuration = Configuration;

            _logger.LogDebug("-->GlobalConfiguration");

            if (false == initLibrary)
            {
                _logger.LogDebug("Initializing Global Configuration");

                // Get SSO Configuration
                ssoConfiguration = _configurationService.GetConfiguration
                    <SSOConfig>("SSO_Config");
                if (null == ssoConfiguration)
                {
                    _logger.LogError("Get SSO Configuration failed in global config");
                    throw new NullReferenceException();
                }
                _logger.LogError("ssoconfig - " + ssoConfiguration.log_config.central_log_config.connection_string.ToString());
                _logger.LogError("New " + ssoConfiguration.sso_config);

                if (configuration.GetValue<string>("IDP_TYPE").Equals("INTERNAL"))
                {
                    var adminportal_Config = _configurationService.GetConfiguration
                        <adminportal_config>("AdminPortal_SSOConfig");
                    if (null == adminportal_Config)
                    {
                        _logger.LogError("Get Admin Portal Configuration failed");
                        throw new NullReferenceException();
                    }

                    ssoConfiguration.sso_config.session_timeout = 
                        adminportal_Config.session_timeout;
                    ssoConfiguration.sso_config.ideal_timeout =
                        adminportal_Config.ideal_timeout;
                    ssoConfiguration.sso_config.access_token_timeout =
                        adminportal_Config.session_timeout;
                    ssoConfiguration.sso_config.temporary_session_timeout =
                        adminportal_Config.temporary_session_timeout;
                    ssoConfiguration.sso_config.active_sessions_per_user =
                        adminportal_Config.active_sessions_per_user;
                    ssoConfiguration.sso_config.account_lock_time =
                        adminportal_Config.account_lock_time;
                    ssoConfiguration.sso_config.wrong_pin = adminportal_Config.wrong_pin;
                    ssoConfiguration.sso_config.remoteSigning =
                        adminportal_Config.remoteSigning;
                    ssoConfiguration.sso_config.allowed_domain_users =
                        adminportal_Config.allowed_domain_users;
                }

                idpConfiguration = _configurationService.GetConfiguration
                    <idp_configuration>("IDP_Configuration");
                if(null == idpConfiguration)
                {
                    _logger.LogError("Get IDP Configuration failed");
                    throw new NullReferenceException();
                }

                // Get FCM Configuration
                var configObject = _configurationService.GetConfiguration
                    <JObject>("FCM_Config");
                if (null == configObject)
                {
                    _logger.LogError("Get FCM Configuration failed");
                    throw new NullReferenceException();
                }

                // Convert FCM Config object to string
                fcmConfiguration = JsonConvert.SerializeObject(configObject);
                if (fcmConfiguration == null)
                {
                    _logger.LogError("Convert FCM Config object to string");
                    throw new NullReferenceException();
                }

                // Get PKI Library Configuration
                configObject = _configurationService.GetConfiguration
                    <JObject>("PKILibrary_Config");
                if (null == configObject)
                {
                    _logger.LogError("Get PKI Library Configuration failed");
                    throw new NullReferenceException();
                }

                // Convert PKI Library Config object to string
                pkiLibConfiguration = JsonConvert.SerializeObject(configObject);
                if (pkiLibConfiguration == null)
                {
                    _logger.LogError("Convert PKI Library Config" +
                        "object to string Failed");
                    throw new NullReferenceException();
                }

                // Get Error Configuration
                errorConfiguration = _configurationService.GetPlainConfiguration
                    <ErrorConfiguration>("ErrorConfigurationIdp");
                if (null == configObject)
                {
                    _logger.LogError("Get PKI Library Configuration failed");
                    throw new NullReferenceException();
                }
                signingPortalClientId = configuration["SigningPortalClientId"];
                if (configuration.GetValue<string>("IDP_TYPE").Equals("EXTERNAL"))
                {
                    if (configuration.GetValue<bool>("EncryptionEnabled"))
                    {
                        signingPortalClientId = PKIMethods.Instance.
                                PKIDecryptSecureWireData(signingPortalClientId);
                    };
                }
                var Threshold = _configurationService.GetThreshold();
                if (Threshold == null)
                {
                    _logger.LogError("Get Threshold Configuration failed");
                    throw new NullReferenceException();
                }
                try
                {
                    thresholdConfiguration.Android_Threshold = (double)Threshold.AndroidDlibThreshold;

                    thresholdConfiguration.Ios_Threshold = (double)Threshold.IosDlibThreshold;
                }
                catch(Exception)
                {
                    _logger.LogError("Get Threshold Configuration Values failed");
                    throw new NullReferenceException();
                }

                initLibrary = true;

                _logger.LogDebug("Global Configuration Initialized");
            }

            _logger.LogDebug("<--GlobalConfiguration");
        }

        public SSOConfig GetSSOConfiguration()
        {
            return ssoConfiguration;
        }

        //public KafkaConfig GetKafkaConfiguration()
        //{
        //    var kafkaSection = Configuration.GetValue("KafkaConfig");
        //    return kafkaSection.Get<KafkaConfig>();
        //}


        public KafkaConfig GetKafkaConfiguration()
        {
            return new KafkaConfig
            {
                BootstrapServers = configuration.GetValue<string>("KafkaConfig:BootstrapServers"),
                CentralLogTopic = configuration.GetValue<string>("KafkaConfig:CentralLogTopic"),
                ServiceLogTopic= configuration.GetValue<string>("KafkaConfig:ServiceLogTopic"),
                AdminLogTopic= configuration.GetValue<string>("KafkaConfig:AdminLogTopic")
            };
        }
        public string GetPKIConfiguration()
        {
            return pkiLibConfiguration;
        }
        public string GetFCMConfiguration()
        {
            return fcmConfiguration;
        }

        public idp_configuration GetIDPConfiguration()
        {
            return idpConfiguration;
        }
        public ConsentConfiguration GetConsentConfiguration()
        {
            return consentConfiguration;
        }

        public ErrorConfiguration GetErrorConfiguration()
        {
            return errorConfiguration;
        }
        
        public WebConstants GetWebConstantsConfiguration()
        {
            return errorConfiguration.WebConstants;
        }
        public string AdminPortalClientId()
        {
            return adminPortalClientId;
        }
        public string AdminPortalClientSecret()
        {
            return adminPortalClientSecret;
        }
        public string SigningPortalClientId()
        {
            return signingPortalClientId;
        }

        public ThresholdConfiguration GetThresholdConfiguration()
        {
            return thresholdConfiguration;
        }
    }
}
