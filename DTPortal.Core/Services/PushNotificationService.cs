using DTPortal.Core.Constants;
using DTPortal.Core.Domain.Models.RegistrationAuthority;
using DTPortal.Core.Domain.Repositories;
using DTPortal.Core.Domain.Services;
using DTPortal.Core.Domain.Services.Communication;
using DTPortal.Core.DTOs;
using DTPortal.Core.Exceptions;
using DTPortal.Core.Utilities;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static DTPortal.Common.CommonResponse;

namespace DTPortal.Core.Services
{
    public class PushNotificationService: IPushNotificationService
    {
        private readonly ILogger<PushNotificationService> _logger;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IPushNotificationClient _pushNotificationClient;
        private readonly MessageConstants Constants;
        private readonly IGlobalConfiguration _globalConfiguration;
        private readonly IConfiguration configuration;
        private readonly IHelper _helper;
        private readonly ICacheClient _cacheClient;
        private readonly IMessageLocalizer _messageLocalizer;

        public PushNotificationService(
            ILogger<PushNotificationService> logger,
            IUnitOfWork unitOfWork,
            IPushNotificationClient pushNotificationClient,
            IGlobalConfiguration globalConfiguration,
            IConfiguration Configuration,
            IHelper helper,
            ICacheClient cacheClient,
            IMessageLocalizer messageLocalizer
            )
        {
            _logger = logger;
            _unitOfWork = unitOfWork;
            _pushNotificationClient = pushNotificationClient;
            configuration = Configuration;
            _helper = helper;
            _globalConfiguration = globalConfiguration;
            _cacheClient = cacheClient;
            _messageLocalizer = messageLocalizer;

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

        public async Task<Response> SendNotification(PushNotificationDTO request)
        {
            Response response = null;

            _logger.LogInformation($"Push Notification Request {0}:",JsonConvert.SerializeObject(request));

            // Validate input
            if (
                (null == request) ||
                (string.IsNullOrEmpty(request.Suid)) ||
                (string.IsNullOrEmpty(request.Title)) ||
                (string.IsNullOrEmpty(request.Body))
                )
            {
                _logger.LogError(_messageLocalizer.
                    GetMessage(Constants.InvalidArguments));
                response = new Response
                {
                    Success = false,
                    Message = _messageLocalizer.
                    GetMessage(Constants.InvalidArguments)
                };
                return response;
            }

            var errorMessage = string.Empty;
            Accesstoken accessToken = null;
            try
            {
                // Get the access token record
                accessToken = await _cacheClient.Get<Accesstoken>("AccessToken",
                    request.AccessToken);
                if (null == accessToken)
                {
                    _logger.LogError("Access token not recieved from cache." +
                        "Expired or Invalid access token");
                    response = new Response
                    {
                        Success = false,
                        Message = "Expired or Invalid access token"
                    };
                    return response;
                }
            }
            catch (CacheException ex)
            {
                _logger.LogError("Failed to get Access Token Record");
                response = new Response
                {
                    Success = false,
                    Message = _helper.GetRedisErrorMsg(ex.ErrorCode,
                    ErrorCodes.REDIS_ACCESS_TOKEN_GET_FAILED)
                };
                return response;
            }

            var encryptionEnabled = configuration.GetValue<bool>("EncryptionEnabled");

            var signingPortalClientId = configuration["SigningPortalClientId"];

            var mobileSigningClientId = configuration["MobileSigningPortalClientId"];

            if (encryptionEnabled)
            {
                signingPortalClientId= PKIMethods.Instance.
                PKIDecryptSecureWireData(signingPortalClientId);

                mobileSigningClientId= PKIMethods.Instance.
                PKIDecryptSecureWireData(mobileSigningClientId);
            }

            if (accessToken.ClientId != signingPortalClientId && accessToken.ClientId!= mobileSigningClientId)
            {
                _logger.LogWarning("Client is not Authorized to send Notification");
                response = new Response
                {
                    Success = false,
                    Message = "UnAuthorized Client"
                };
                return response;
            }

            var raSubscriber = new SubscriberView();
            try
            {
                raSubscriber = await _unitOfWork.Subscriber.GetSubscriberInfoBySUID(
                    request.Suid);
                if (null == raSubscriber)
                {
                    _logger.LogError("Subscriber details not found");
                    response = new Response
                    {
                        Success = false,
                        Message = "Subscriber details not found"
                    };
                    return response;
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                response = new Response
                {
                    Success = false,
                    Message = _helper.GetErrorMsg(ErrorCodes.DB_ERROR)
                };
                return response;
            }
            var context = configuration["NotificationContext"];
            // Send notification to mobile
            var notificationRequest = new PushNotificationRequest()
            {
                Title = request.Title,
                Body = request.Body,
                Context = context,
                RegistrationToken = raSubscriber.FcmToken,
            };

            var result = await _pushNotificationClient.SendNotification(
                notificationRequest);
            if (null == result)
            {
                _logger.LogError("SendNotification failed");
                response = new Response
                {
                    Success = false,
                    Message = _messageLocalizer.
                    GetMessage(Constants.NotificationSendFailed)
                };
                return response;
            }

            response = new Response
            {
                Success = true,
                Result = "Push Notification Sent Successfully"
            };
            return response;
        }

        public async Task<Response> SendNotificationDelegationRequest(DelegationPushNotificationDTO request)
        {
            Response response = null;

            // Validate input
            if (
                (null == request) ||
                (request.DelegateeList.Count == 0 )||
                (string.IsNullOrEmpty(request.Title)) ||
               // (string.IsNullOrEmpty(request.AccessToken)) ||
                (string.IsNullOrEmpty(request.Body))
                )
            {
                _logger.LogError(Constants.InvalidArguments.En);
                response = new Response
                {
                    Success = false,
                    Message = _messageLocalizer.GetMessage(Constants.InvalidArguments)
                };
                return response;
            }

            //var errorMessage = string.Empty;
            //Accesstoken accessToken = null;
            //try
            //{
            //    // Get the access token record
            //    accessToken = await _cacheClient.Get<Accesstoken>("AccessToken",
            //        request.AccessToken);
            //    if (null == accessToken)
            //    {
            //        _logger.LogError("Access token not recieved from cache." +
            //            "Expired or Invalid access token");
            //        response = new Response
            //        {
            //            Success = false,
            //            Message = "Expired or Invalid access token"
            //        };
            //        return response;
            //    }
            //}
            //catch (CacheException ex)
            //{
            //    _logger.LogError("Failed to get Access Token Record");
            //    response = new Response
            //    {
            //        Success = false,
            //        Message = _helper.GetRedisErrorMsg(ex.ErrorCode,
            //        ErrorCodes.REDIS_ACCESS_TOKEN_GET_FAILED)
            //    };
            //    return response;
            //}

            //if (accessToken.ClientId != configuration["SigningPortalClientId"])
            //{
            //    _logger.LogWarning("Client is not Authorized to send Notification");
            //    response = new Response
            //    {
            //        Success = false,
            //        Message = "UnAuthorized Client"
            //    };
            //    return response;
            //}

            var raSubscriber = new List<SubscriberView>();
            try
            {
                raSubscriber = await _unitOfWork.Subscriber.GetSubscriberInfoBySUIDList(
                    request.DelegateeList);
                if (null == raSubscriber || raSubscriber.Count == 0)
                {
                    _logger.LogError("Subscriber details not found");
                    response = new Response
                    {
                        Success = false,
                        Message = "Subscriber details not found"
                    };
                    return response;
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                response = new Response
                {
                    Success = false,
                    Message = _helper.GetErrorMsg(ErrorCodes.DB_ERROR)
                };
                return response;
            }

            foreach(var subscriber in raSubscriber)
            {
                var context = "SIGNATURE_DELEGATION";
                if (request.isDelegator)
                {
                    context = "SIGNATURE_DELEGATION_DELEGATOR";
                    if (request.isIdle)
                    {
                        context = "SIGNATURE_DELEGATEE_ACTION";
                    }
                }
               
                // Send notification to mobile
                var notificationRequest = new PushNotificationRequest()
                {
                    Title = request.Title,
                    Body = request.Body,
                    Context = context,
                    Text = request.ConsentData,
                    RegistrationToken = subscriber.FcmToken,
                };

                var result = await _pushNotificationClient.SendNotification(
                    notificationRequest);
                if (null == result)
                {
                    _logger.LogError("SendNotification failed");
                    response = new Response
                    {
                        Success = false,
                        Message = _messageLocalizer.
                        GetMessage(Constants.NotificationSendFailed)
                    };
                    return response;
                }
            }
            

            response = new Response
            {
                Success = true,
                Result = "Push Notification Sent Successfully"
            };
            return response;
        }
    }
}