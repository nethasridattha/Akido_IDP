using DTPortal.Core.Domain.Services;
using FirebaseAdmin;
using FirebaseAdmin.Messaging;
using Google.Apis.Auth.OAuth2;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace DTPortal.Core.Utilities
{
    public class PushNotificationClientOld
    {
        // Initialize logger.
        private readonly ILogger<PushNotificationClient> _logger;
        private static FirebaseMessaging firebaseMessaging;
        private readonly IGlobalConfiguration _globalConfiguration;
        private readonly IConfiguration configuration;
        public static bool initLibrary = false;

        public PushNotificationClientOld(ILogger<PushNotificationClient> logger,
            IGlobalConfiguration globalConfiguration, IConfiguration Configuration)
        {
            _logger = logger;
            _globalConfiguration = globalConfiguration;
            configuration = Configuration;
            _logger.LogDebug("-->PushNotificationClient");

            if (false == initLibrary)
            {
                _logger.LogInformation("Library not Initialized");
                try
                {
                    var defaultApp = FirebaseApp.Create(new AppOptions()
                    {
                        Credential = GoogleCredential.FromJson(
                            _globalConfiguration.GetFCMConfiguration()),
                    });
                }
                catch(Exception ex)
                {
                    _logger.LogError("Failed to initialize PushNotification"+
                        " Library: {0}",
                        ex.Message);
                    throw;
                }

                firebaseMessaging = FirebaseMessaging.DefaultInstance;
                initLibrary = true;
                _logger.LogInformation("PushNotification Library Initialized");
            }

            _logger.LogDebug("<--PushNotificationClient");
        }

        public async Task<string> SendAuthnNotification(
            AuthnNotification authnNotification)
        {
            _logger.LogDebug("-->SendAuthnNotification");

            string result = null;

            // Validate input parameters
            if (null == authnNotification)
            {
                _logger.LogError("Invalid Input Parameter");
                return result;
            }

            _logger.LogDebug("AuthnNotification request:{0}",
                JsonConvert.SerializeObject(authnNotification));

            var message = new Message()
            {
                Token = authnNotification.RegistrationToken,
                Data = new Dictionary<string, string>()
                {
                    ["AuthNToken"] = authnNotification.AuthnToken,
                    ["AuthNScheme"] = authnNotification.AuthnScheme,
                    ["RandomCodes"] = authnNotification.RandomCodes,
                    ["ApplicationName"] = authnNotification.ApplicationName,
                    ["Title"] = "Authentication Request",
                    ["Body"] = "Please approve or deny",
                    ["urlImage"] = configuration.GetValue<string>("LogoUrl")
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
                            Title = "Authentication Request",
                            Body = "Please approve or deny",
                        },
                        
                        MutableContent = true
                        
                    },
                },
            };

            try
            {
                _logger.LogDebug("Sending Push Notification");
                result = await firebaseMessaging.SendAsync(message);
                if (null == result)
                {
                    _logger.LogError("Send Notification Failed");
                    return result;
                }
            }
            catch (Exception ex)
            {
                _logger.LogError("SendAuthnNotification Failed: {0}",
                    ex.Message);
                return result;
            }

            _logger.LogInformation("Send Notification Response:{0}",result);
            _logger.LogDebug("<--SendAuthnNotification");
            return result;
        }

        public async Task<string> SendNotification(
            PushNotificationRequest request)
        {
            _logger.LogDebug("-->SendNotification");

            string result = null;

            // Validate input parameters
            if (null == request)
            {
                _logger.LogError("Invalid Input Parameter");
                return result;
            }

            var message = new Message()
            {
                Token = request.RegistrationToken,
                Data = new Dictionary<string, string>()
                {
                    ["Title"] = request.Title,
                    ["Body"] = request.Body,
                    ["Context"] = request.Context,
                    ["ConsentData"] = request.Text,
                    ["urlImage"] = configuration.GetValue<string>("LogoUrl")
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
                            Title = request.Title,
                            Body = request.Body,
                        },

                        MutableContent = true,
                    },
                },
            };

            try
            {
                _logger.LogDebug("Sending Push Notification");
                result = await firebaseMessaging.SendAsync(message);
                if (null == result)
                {
                    _logger.LogError("Send Notification Failed");
                    return result;
                }
            }
            catch (Exception ex)
            {
                _logger.LogError("SendNotification Failed: {0}",
                    ex.Message);
                return result;
            }

            _logger.LogInformation("SendNotification Response:{0}", result);
            _logger.LogDebug("<--SendNotification");
            return result;
        }

        public async Task<string> SendEConsentNotification(
            EConsentNotification eConsentNotification)
        {
            _logger.LogDebug("-->SendEConsentNotification");

            string result = null;

            // Validate input parameters
            if (null == eConsentNotification)
            {
                _logger.LogError("Invalid Input Parameter");
                return result;
            }

            _logger.LogDebug("eConsentNotification request:{0}",
                JsonConvert.SerializeObject(eConsentNotification));

            string DeselectScopesAndClaims = string.Empty;
            if (eConsentNotification.DeselectScopesAndClaims)
                DeselectScopesAndClaims = "true";
            else
                DeselectScopesAndClaims = "false";


            var consentScopes = JsonConvert.SerializeObject(
                eConsentNotification.ConsentScopes);

            var message = new Message()
            {
                Token = eConsentNotification.RegistrationToken,
                //Token = "e8XxUkt9yEhhtoXXq9HXAs:APA91bG45vfIvxn3TvAsvat6zqe-P628oZrfxUiil6i7SaInIQ2kc8Xu-Nww0K3JZ0oP1rM5kjIepxRepfVjDByaBH_YcvM0SmPxPTSqMl3eegoMySEdbaZlFqNaKh3uS8lMjvQuoNRu",
                Data = new Dictionary<string, string>()
                {
                    ["AuthNToken"] = eConsentNotification.AuthnToken,
                    ["AuthNScheme"] = eConsentNotification.AuthnScheme,
                    ["ApplicationName"] = eConsentNotification.ApplicationName,
                    ["ConsentScopes"] = consentScopes,
                    ["DeselectScopesAndClaims"] = DeselectScopesAndClaims,
                    ["Title"] = "Consent Request",
                    ["Body"] = "Please approve or deny",
                    ["urlImage"] = configuration.GetValue<string>("LogoUrl")
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
                            Title = "Consent Request",
                            Body = "Please approve or deny",
                        },

                        MutableContent = true,

                    },
                },
            };

            try
            {
                _logger.LogInformation("Sending Push Notification");
                result = await firebaseMessaging.SendAsync(message);
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

        public async Task<string> SendWalletDelegationNotification(
            WalletDelegationNotification request)
        {
            _logger.LogDebug("-->SendEConsentNotification");

            string result = null;

            if (null == request)
            {
                _logger.LogError("Invalid Input Parameter");
                return result;
            }

            var message = new Message()
            {
                Token = request.RegistrationToken,
                //Token = "e8XxUkt9yEhhtoXXq9HXAs:APA91bG45vfIvxn3TvAsvat6zqe-P628oZrfxUiil6i7SaInIQ2kc8Xu-Nww0K3JZ0oP1rM5kjIepxRepfVjDByaBH_YcvM0SmPxPTSqMl3eegoMySEdbaZlFqNaKh3uS8lMjvQuoNRu",
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
                result = await firebaseMessaging.SendAsync(message);
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
