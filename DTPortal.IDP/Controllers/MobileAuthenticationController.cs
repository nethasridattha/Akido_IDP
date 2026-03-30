using DTPortal.Core.Constants;
using DTPortal.Core.Domain.Services;
using DTPortal.Core.Domain.Services.Communication;
using DTPortal.Core.DTOs;
using DTPortal.Core.Utilities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.DotNet.Scaffolding.Shared;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Net.Http.Headers;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace DTPortal.IDP.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MobileAuthenticationController : BaseController
    {
        private readonly IMobileAuthenticationService _mobileAuthenticationService;
        private readonly IConfiguration _configuration;
        private readonly ILogger<MobileAuthenticationController> _logger;
        private readonly IClientService _clientService;
        private readonly IOrganizationService _organizationService;
        private readonly IMessageLocalizer _messageLocalizer;
        private readonly ICredentialService _credentialService;
        private readonly MessageConstants Constants;
        private readonly OIDCConstants OIDCConstants;
        private readonly IGlobalConfiguration _globalConfiguration;
        public MobileAuthenticationController
            (IMobileAuthenticationService mobileAuthenticationService,
            ICredentialService credentialService,
            IMessageLocalizer messageLocalizer,
            IConfiguration configuration,
            IClientService clientService,
            IGlobalConfiguration globalConfiguration,
            ILogger<MobileAuthenticationController> logger,
            IOrganizationService organizationService)
        {
            _mobileAuthenticationService = mobileAuthenticationService;
            _configuration = configuration;
            _clientService = clientService;
            _messageLocalizer = messageLocalizer;
            _credentialService = credentialService;
            _globalConfiguration = globalConfiguration;
            _logger = logger;
            _organizationService = organizationService;

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
            OIDCConstants = errorConfiguration.OIDCConstants;
            if (null == OIDCConstants)
            {
                _logger.LogError("Get Error Configuration failed");
                throw new NullReferenceException();
            }
        }


        [HttpGet("GetConsentDetails")]
        public async Task<IActionResult> GetConsentDetails(
            [Required]
            [StringLength(100, MinimumLength = 5)]
            [RegularExpression(@"^[A-Za-z0-9\-]+$",
            ErrorMessage = "SessionId can contain only letters, numbers, and hyphen.")]
            string sessionId)
        {
            var serviceResult = await _mobileAuthenticationService
                .GetConsentDetailsAsync(sessionId);

            return Ok(new APIResponse()
            {
                Success = serviceResult.Success,
                Message = serviceResult.Message,
                Result = serviceResult.Resource

            });
        }

        [HttpPost("VerifyClientDetails")]
        [Consumes("application/json")]
        public async Task<IActionResult> VerifyClientDetails
            ([FromBody][Required] AuthorizationRequest request)
        {
            if (request == null)
                return BadRequest(new { error = "Request body is required." });

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                var result = await _mobileAuthenticationService.
                VerifyClientDetails(request);

                if (!result.Success)
                {
                    return Ok(new APIResponse()
                    {
                        Success = result.Success,
                        Message = result.Message,
                    });
                }
                var sessionId = (string)result.Resource;

                var Res = new
                {
                    SessionId = sessionId
                };

                return Ok(new APIResponse()
                {
                    Success = true,
                    Message = _messageLocalizer.GetMessage(Constants.VerifiedClientDetails),
                    Result = Res
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in VerifyClientDetails");
                return Ok(new APIResponse()
                {
                    Success = false,
                    Message = _messageLocalizer.GetMessage(Constants.InternalError),
                    Result = null
                });
            }
        }

        [HttpPost("VerifyUserAuthenticationData")]
        [Consumes("application/json")]
        public async Task<IActionResult> VerifyUserAuthenticationData
            ([FromBody] [Required] AuthenticateUserRequest request)
        {
            if (request == null)
                return BadRequest(new { error = "Request body is required." });

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                var response = await _mobileAuthenticationService
                .AuthenticateUserAsync(request);

                if (!response.Success)
                {
                    return Ok(new APIResponse()
                    {
                        Success = response.Success,
                        Message = response.Message,
                        Result = response.Resource
                    });
                }

                var result = await _mobileAuthenticationService
                    .GetAuthorizationCode(request.SessionId);

                if (!response.Success)
                {
                    return Ok(new APIResponse()
                    {
                        Success = result.Success,
                        Message = result.Message
                    });
                }
                var Res = new
                {
                    AuthorizationCode = result.AuthorizationCode
                };
                return Ok(new APIResponse()
                {
                    Success = result.Success,
                    Message = result.Message,
                    Result = Res
                });
            }
            catch(Exception ex)
            {
                _logger.LogError(ex, "Error in VerifyUserAuthenticationData");
                return Ok(new APIResponse()
                {
                    Success = false,
                    Message = _messageLocalizer.GetMessage(Constants.InternalError),
                    Result = null
                });
            }
        }

        [HttpGet("GetCredentialOffer")]
        public async Task<IActionResult> GetCredentialOffer()
        {
            var authHeader = Request.Headers[_configuration["AccessTokenHeaderName"]];
            if (string.IsNullOrEmpty(authHeader))
            {
                ErrorResponseDTO errResponse = new ErrorResponseDTO();
                errResponse.error = _messageLocalizer.GetMessage(OIDCConstants.InvalidToken);
                errResponse.error_description = _messageLocalizer.GetMessage(OIDCConstants.InvalidToken);
                return Unauthorized(errResponse);
            }

            // Parse the authorization header
            var authHeaderVal = AuthenticationHeaderValue.Parse(authHeader);
            if (null == authHeaderVal.Scheme || null == authHeaderVal.Parameter)
            {
                ErrorResponseDTO errResponse = new ErrorResponseDTO();
                errResponse.error = _messageLocalizer.GetMessage(OIDCConstants.InvalidToken);
                errResponse.error_description = _messageLocalizer.GetMessage(OIDCConstants.InvalidToken);
                return Unauthorized(errResponse);
            }

            // Check the authorization is of Bearer type
            if (!authHeaderVal.Scheme.Equals("bearer",
                 StringComparison.OrdinalIgnoreCase))
            {
                ErrorResponseDTO errResponse = new ErrorResponseDTO();
                errResponse.error = _messageLocalizer.GetMessage(OIDCConstants.InvalidToken);
                errResponse.error_description = _messageLocalizer.GetMessage(OIDCConstants.InvalidToken);
                return Unauthorized(errResponse);
            }
            var credentialId = _configuration["MobileAuthentication:CredentialId"];
            var response = await _credentialService.
                GetCredentialOfferByUidAsync(credentialId, authHeaderVal.Parameter);

            var apiResponse = new APIResponse()
            {
                Success = response.Success,
                Message = response.Message,
                Result = response.Resource
            };

            return Ok(apiResponse);
        }

        [HttpPost("AddWalletTransactionLog")]
        [Consumes("application/json")]
        [ProducesResponseType(typeof(APIResponse), 200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> AddWalletTransactionLog(
            [FromBody] [Required] WalletTransactionRequestDTO walletTransactionRequestDTO)
        {
            if (walletTransactionRequestDTO == null)
                return BadRequest(new APIResponse
                {
                    Success = false,
                    Message = "Request body is required.",
                    Result = null
                });

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                var result = await _mobileAuthenticationService
                    .AddTransactionLog(walletTransactionRequestDTO);

                return Ok(new APIResponse
                {
                    Success = result.Success,
                    Message = result.Message,
                    Result = result.Resource
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in AddWalletTransactionLog");

                return Ok(new APIResponse
                {
                    Success = false,
                    Message = _messageLocalizer.GetMessage(Constants.InternalError),
                    Result = null
                });
            }
        }

        [HttpPost("GetTransactionLog")]
        [Consumes("application/json")]
        public async Task<IActionResult> GetTransactionLog
            ([FromBody] [Required] AuthenticationTransactionRequest request)
        {
            if (request == null)
                return BadRequest(new { error = "Request body is required." });

            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            try
            {
                var result = await _mobileAuthenticationService.
                GetAuthenticationTransactionLog(request.suid, request.pageNumber);

                if (result == null)
                {
                    return Ok(new APIResponse()
                    {
                        Success = false,
                        Message = _messageLocalizer.GetMessage(Constants.InternalError),
                        Result = null
                    });
                }

                var clientDictionary = await _clientService.GetClientOrgApplicationMap();

                if (clientDictionary == null)
                {
                    return Ok(new APIResponse()
                    {
                        Success = false,
                        Message = _messageLocalizer.GetMessage(Constants.InternalError),
                        Result = null
                    });
                }

                var organizationDictionary = await _organizationService
                    .GetOrganizationsListDictionary();

                if (organizationDictionary == null)
                {
                    return Ok(new APIResponse()
                    {
                        Success = false,
                        Message = _messageLocalizer.GetMessage(Constants.InternalError),
                        Result = null
                    });
                }

                AuthenticationTransactionResponse resultLogs = new AuthenticationTransactionResponse();

                List<AuthenticationTransaction> logs = new List<AuthenticationTransaction>();

                if (clientDictionary != null)
                {
                    foreach (var log in result)
                    {
                        var resultLog = new AuthenticationTransaction
                        {
                            dateTime = log.EndTime,
                            authenticationStatus = log.AuthenticationType,
                            serviceName = log.ServiceName,
                            Id = log._id,
                        };

                        if (log.userActivityType == LogClientServices.IdpAuthentication
                            || log.userActivityType == LogClientServices.WalletAuthentication)
                        {
                            resultLog.activityType = _messageLocalizer.GetMessage(Constants.AUTHENTICATION);
                        }
                        else if(log.userActivityType == LogClientServices.IdpRegistration
                            || log.userActivityType == LogClientServices.WalletRegistration)
                        {
                            resultLog.activityType = _messageLocalizer.GetMessage(Constants.REGISTRATION);
                        }
                        else if(log.userActivityType == LogClientServices.DataSharing)
                        {
                            resultLog.activityType = _messageLocalizer.GetMessage(Constants.SHARING);
                        }
                        else
                        {
                            resultLog.activityType = "N/A";
                        }

                        if (clientDictionary.TryGetValue(log.ServiceProviderAppName, out ApplicationInfo appInfo))
                        {
                            resultLog.serviceProviderAppName = _messageLocalizer.
                                GetLocalizedDisplayName(appInfo.ApplicationName, appInfo.ApplicationNameAr);

                            var orgDict = organizationDictionary.ToDictionary(x => x.Ouid);


                            if (orgDict.TryGetValue(appInfo.OrganizationUid, out OrganizationListResponse value))
                            {
                                resultLog.serviceProviderName = _messageLocalizer.GetLocalizedDisplayName(value.en, value.ar);
                            }
                            else
                            {
                                resultLog.serviceProviderName = "N/A";
                            }
                        }
                        else
                        {
                            resultLog.serviceProviderAppName = "N/A";
                            resultLog.serviceProviderName = "N/A";
                        }

                        logs.Add(resultLog);
                    }
                }

                resultLogs.authenticationTransactions = logs;
                resultLogs.hasMoreResults = result.HasNextPage;
                resultLogs.totalResults = result.TotalCount;

                return Ok(new APIResponse()
                {
                    Success = true,
                    Message = _messageLocalizer.GetMessage(Constants.TransactionLogsFetchedSuccessfully),
                    Result = resultLogs
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in GetTransactionLog");
                return Ok(new APIResponse()
                {
                    Success = false,
                    Message = _messageLocalizer.GetMessage(Constants.InternalError),
                    Result = null
                });
            }

        }

        [HttpGet("GetServiceProviderAppDetails")]
        public async Task<IActionResult> GetServiceProviderAppDetails()
        {
            var authHeader = Request.Headers[_configuration["AccessTokenHeaderName"]];
            if (string.IsNullOrEmpty(authHeader))
            {
                ErrorResponseDTO errResponse = new ErrorResponseDTO();
                errResponse.error = _messageLocalizer.GetMessage(OIDCConstants.InvalidToken);
                errResponse.error_description = _messageLocalizer.GetMessage(OIDCConstants.InvalidToken);
                return Unauthorized(errResponse);
            }

            // Parse the authorization header
            var authHeaderVal = AuthenticationHeaderValue.Parse(authHeader);
            if (null == authHeaderVal.Scheme || null == authHeaderVal.Parameter)
            {
                ErrorResponseDTO errResponse = new ErrorResponseDTO();
                errResponse.error = _messageLocalizer.GetMessage(OIDCConstants.InvalidToken);
                errResponse.error_description = _messageLocalizer.GetMessage(OIDCConstants.InvalidToken);
                return Unauthorized(errResponse);
            }

            // Check the authorization is of Bearer type
            if (!authHeaderVal.Scheme.Equals("bearer",
                 StringComparison.OrdinalIgnoreCase))
            {
                ErrorResponseDTO errResponse = new ErrorResponseDTO();
                errResponse.error = _messageLocalizer.GetMessage(OIDCConstants.InvalidToken);
                errResponse.error_description = _messageLocalizer.GetMessage(OIDCConstants.InvalidToken);
                return Unauthorized(errResponse);
            }

            var response = await _mobileAuthenticationService.
                GetServiceProviderAppDetails(authHeaderVal.Parameter);

            var apiResponse = new APIResponse()
            {
                Success = response.Success,
                Message = response.Message,
                Result = response.Resource
            };

            return Ok(apiResponse);
        }

        [HttpGet("GetLogDetails")]
        public async Task<IActionResult> GetLogDetails(
            [Required]
            [StringLength(100, MinimumLength = 3)]
            [RegularExpression(@"^[A-Za-z0-9\-]+$", ErrorMessage = "Identifier contains invalid characters.")]
            string Identifier)        {
            try
            {
                var response = await _mobileAuthenticationService
                .GetLogDetailsAsync(Identifier);

                if (response == null || !response.Success)
                {
                    return Ok(new APIResponse()
                    {
                        Success = response.Success,
                        Message = _messageLocalizer.GetMessage(Constants.InternalError),
                        Result = response.Resource
                    });
                }
                string callStack = null;

                string ActivityType = string.Empty;


                var logDetails = (LogMessage)response.Resource;

                if (logDetails.userActivityType == LogClientServices.IdpAuthentication
                    || logDetails.userActivityType == LogClientServices.WalletAuthentication)
                {
                    ActivityType = _messageLocalizer.GetMessage(Constants.AUTHENTICATION);
                }
                else
                {
                    ActivityType = _messageLocalizer.GetMessage(Constants.REGISTRATION);
                }

                if (logDetails.serviceName != LogClientServices.walletAuthenticationLog)
                {
                    var log = await _mobileAuthenticationService.GetAuthenticationDetailsAsync
                        (logDetails.correlationID, logDetails.serviceProviderAppName);

                    if (!log.Success)
                    {
                        return Ok(new APIResponse()
                        {
                            Success = log.Success,
                            Message = _messageLocalizer.GetMessage(Constants.InternalError),
                            Result = log.Resource
                        });
                    }
                    callStack = ((LogReportDTO)log.Resource).CallStack;
                }
                else
                {
                    callStack = logDetails.callStack;
                }

                var scopes = await _mobileAuthenticationService.
                        GetScopeDetailsAsync(callStack, logDetails.serviceName, ActivityType);

                return Ok(new APIResponse()
                {
                    Success = scopes.Success,
                    Message = scopes.Message,
                    Result = scopes.Resource
                });
            }
            catch(Exception ex)
            {
                _logger.LogError(ex, "Error in GetLogDetails");
                return Ok(new APIResponse()
                {
                    Success = false,
                    Message = _messageLocalizer.GetMessage(Constants.InternalError),
                    Result = null
                });
            }
        }

        [HttpGet("GetAuthenticationLogsCount")]
        public async Task<IActionResult> GetAuthenticationLogsCount(
            [Required]
            [StringLength(100, MinimumLength = 5)]
            [RegularExpression(@"^[A-Za-z0-9\-]+$", ErrorMessage = "Invalid SUID format.")]
            string suid)
        {
            var result = await _mobileAuthenticationService.
                GetTransactionLogCount(suid);
            if (result == null)
            {
                return Ok(new APIResponse()
                {
                    Success = false,
                    Message = _messageLocalizer.GetMessage(Constants.InternalError),
                    Result = null
                });
            }
            return Ok(new APIResponse()
            {
                Success = true,
                Message = _messageLocalizer.GetMessage(Constants.TransactionLogsCountFetchedSuccessfully),
                Result = result.Resource
            });
        }

        [HttpPost("AddMultipleWalletTransactionLogs")]
        [Consumes("application/json")]
        public async Task<IActionResult> AddMultipleWalletTransactionLogs
            ([FromBody][Required,MinLength(1)] List<WalletTransactionRequestDTO> walletTransactionRequestDTO)
        {
            try
            {
                if (walletTransactionRequestDTO == null)
                    return BadRequest(new APIResponse
                    {
                        Success = false,
                        Message = "Request body is required.",
                        Result = null
                    });

                if (!walletTransactionRequestDTO.Any())
                {
                    return BadRequest(new APIResponse
                    {
                        Success = false,
                        Message = "At least one transaction log entry is required.",
                        Result = null
                    });
                }

                if (walletTransactionRequestDTO.Count == 0)
                    return Ok(new APIResponse
                    {
                        Success = false,
                        Message = "At least one transaction log entry is required.",
                        Result = null
                    });
                if (walletTransactionRequestDTO.Any(x => x == null))
                {
                    return BadRequest(new
                    {
                        success = false,
                        message = "Validation failed",
                        errors = new Dictionary<string, IEnumerable<string>>
                        {
                                { "walletTransactionRequestDTO", new[] { "All log entries must be valid objects." } }
                        }
                    });
                }

                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                ServiceResult result = null;
                foreach (var request in walletTransactionRequestDTO)
                {
                    result = await _mobileAuthenticationService.
                    AddTransactionLog(request);
                }
                return Ok(new APIResponse()
                {
                    Success = result.Success,
                    Message = result.Message,
                    Result = result.Resource
                });
            }
            catch (Exception)
            {
                return Ok(new APIResponse()
                {
                    Success = false,
                    Message = _messageLocalizer.GetMessage(Constants.InternalError),
                    Result = null
                });
            }
        }
    }
}
