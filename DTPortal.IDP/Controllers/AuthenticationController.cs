using DTPortal.Core.Domain.Services;
using DTPortal.Core.Domain.Services.Communication;
using DTPortal.Core.Utilities;
using DTPortal.IDP.DTOs;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Org.BouncyCastle.Asn1.Ocsp;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace DTPortal.IDP.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : BaseController
    {
        private readonly IAuthenticationService _authenticationService;
        private readonly IClientService _clientService;
        private readonly ILogger<AuthenticationController> _logger;
        private readonly IConfiguration Configuration;
        private readonly OIDCConstants OIDCConstants;
        private readonly WebConstants WebConstants;
        private readonly IGlobalConfiguration _globalConfiguration;
        private readonly ISubscriberService _subscriberService;
        private readonly IMessageLocalizer _messageLocalizer;
        public AuthenticationController(IAuthenticationService authenticationService,
            IClientService clientService,
            ILogger<AuthenticationController> logger,
            IConfiguration configuration,
            IGlobalConfiguration globalConfiguration,
            ISubscriberService subscriberService,
            IMessageLocalizer messageLocalizer)
        {
            _authenticationService = authenticationService;
            _clientService = clientService;
            _logger = logger;
            Configuration = configuration;
            _globalConfiguration = globalConfiguration;
            _subscriberService = subscriberService;
            _messageLocalizer = messageLocalizer;
            var errorConfiguration = _globalConfiguration.
            GetErrorConfiguration();
            if (null == errorConfiguration)
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
            WebConstants = errorConfiguration.WebConstants;
            if (null == WebConstants)
            {
                _logger.LogError("Get Error Configuration failed");
                throw new NullReferenceException();
            }
        }

        // ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
        // Endpoint for ValidateSession
        [Route("VerifyUserAuthData")]
        [Consumes("application/json")]
        [ProducesResponseType(typeof(APIResponse), 200)]
        [HttpPost]
        public async Task<IActionResult> VerifyUserAuthData
            ([FromBody][Required] VerifyUserAuthDataRequest requestObj)
        {
            _logger.LogDebug("---->VerifyUserAuthData");
            try
            {
                if (requestObj == null)
                    return BadRequest(new { error = "Request body is required." });

                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                var result = await _authenticationService.VerifyUserAuthData(requestObj);

                if (!result.Success)
                {
                    // Failure
                    var response = new APIResponse
                    {
                        Success = result.Success,
                        Message = result.Message
                    };
                    return Ok(response);
                }
                else
                {
                    return Ok(new APIResponse()
                    {
                        Success = result.Success,
                        Message = result.Message,
                        Result = result.Result
                    });
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Exception Occurred in VerifyUserAuthData");
                var response = new APIResponse
                {
                    Success = false,
                    Message = _messageLocalizer.GetMessage(WebConstants.InternalServerError)
                };
                return Ok(response);
            }
        }
        
        // ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~

        [Route("token")]
        [EnableCors("AllowedOrigins")]
        [HttpPost]
        [Consumes("application/x-www-form-urlencoded")]
        [Produces("application/json", "application/problem+json")]
        [RequestSizeLimit(10000)]

        [ProducesResponseType(typeof(object), StatusCodes.Status200OK)]

        [ProducesResponseType(typeof(object), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(object), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(object), StatusCodes.Status415UnsupportedMediaType)]
        [ProducesResponseType(typeof(object), StatusCodes.Status500InternalServerError)]

        [IgnoreAntiforgeryToken]
        public async Task<IActionResult> GetAccessToken(
            [FromForm][Required] GetAccessTokenRequest request)
        {
            _logger.LogDebug("--->GetAccessToken");

            try
            {
                if (!Request.HasFormContentType)
                {
                    return StatusCode(StatusCodes.Status415UnsupportedMediaType,
                        new DTOs.ErrorResponseDTO
                        {
                            error = "invalid_request",
                            error_description = "Content-Type must be application/x-www-form-urlencoded"
                        });
                }
                var allowedKeys = new HashSet<string>
                {
                    "grant_type",
                    "client_id",
                    "client_secret",
                    "code",
                    "redirect_uri",
                    "refresh_token",
                    "code_verifier",
                    "scope",
                    "assertion",
                    "client_assertion",
                    "client_assertion_type"
                };

                var incomingKeys = Request.Form.Keys;

                var unknownKeys = incomingKeys
                    .Where(k => !allowedKeys.Contains(k))
                    .ToList();

                if (unknownKeys.Any())
                {
                    return BadRequest(new DTOs.ErrorResponseDTO
                    {
                        error = "invalid_request",
                        error_description = $"Unknown parameters: {string.Join(", ", unknownKeys)}"
                    });
                }

                if (request == null)
                {
                    return BadRequest(new DTOs.ErrorResponseDTO
                    {
                        error = "invalid_request",
                        error_description = "Request cannot be null"
                    });
                }

                if (!ModelState.IsValid)
                {
                    return ValidationProblem(ModelState);
                }

                var grantTypesSupported =
                    Configuration.GetSection("grant_types_supported")
                    .Get<string[]>();

                if (grantTypesSupported == null)
                {
                    _logger.LogError("grant_types_supported missing in configuration");

                    return Ok(
                        new DTOs.ErrorResponseDTO
                        {
                            error = _messageLocalizer.GetMessage(WebConstants.InternalServerError),
                            error_description = _messageLocalizer.GetMessage(WebConstants.InternalServerError)
                        });
                }

                if (string.IsNullOrWhiteSpace(request.grant_type))
                {
                    return BadRequest(new DTOs.ErrorResponseDTO
                    {
                        error = _messageLocalizer.GetMessage(OIDCConstants.InvalidGrant),
                        error_description = _messageLocalizer.GetMessage(OIDCConstants.InvalidGrant)
                    });
                }

                if (string.IsNullOrWhiteSpace(request.client_id))
                {
                    return BadRequest(new DTOs.ErrorResponseDTO
                    {
                        error = _messageLocalizer.GetMessage(OIDCConstants.InvalidClient),
                        error_description = _messageLocalizer.GetMessage(OIDCConstants.InvalidClient)
                    });
                }

                if (!grantTypesSupported.Contains(request.grant_type)){
                    return Ok(new DTOs.ErrorResponseDTO
                    {
                        error = _messageLocalizer.GetMessage(OIDCConstants.InvalidGrant),
                        error_description = _messageLocalizer.GetMessage(OIDCConstants.InvalidGrant),
                    });
                }

                var idpConfiguration = _globalConfiguration.GetIDPConfiguration();

                if (idpConfiguration == null)
                {
                    _logger.LogError("IDP configuration missing");

                    return Ok(
                        new DTOs.ErrorResponseDTO
                        {
                            error = _messageLocalizer.GetMessage(WebConstants.InternalServerError),
                            error_description = _messageLocalizer.GetMessage(WebConstants.InternalServerError),
                        });
                }

                var openIdCommonConfig =
                    JsonConvert.DeserializeObject<Core.Domain.Services.Communication.Common>(
                        idpConfiguration.common.ToString());

                if (openIdCommonConfig == null)
                {
                    return Ok(
                        new DTOs.ErrorResponseDTO
                        {
                            error = _messageLocalizer.GetMessage(WebConstants.InternalServerError),
                            error_description = _messageLocalizer.GetMessage(WebConstants.InternalServerError)
                        });
                }

                var credential = string.Empty;
                var type = string.Empty;

                if (openIdCommonConfig.TokenEndPointReqSigning)
                {
                    if (string.IsNullOrEmpty(request.client_assertion) ||
                        string.IsNullOrEmpty(request.client_assertion_type) ||
                        request.client_assertion_type !=
                        "urn:ietf:params:oauth:client-assertion-type:jwt-bearer")
                    {
                        return Ok(new DTOs.ErrorResponseDTO
                        {
                            error = _messageLocalizer.GetMessage(OIDCConstants.InvalidInput),
                            error_description = _messageLocalizer.GetMessage(OIDCConstants.InvalidInput)
                        });
                    }

                    if (string.IsNullOrEmpty(request.code) ||
                        string.IsNullOrEmpty(request.redirect_uri) ||
                        string.IsNullOrEmpty(request.client_id))
                    {
                        return BadRequest(new DTOs.ErrorResponseDTO
                        {
                            error = _messageLocalizer.GetMessage(OIDCConstants.InvalidInput),
                            error_description = _messageLocalizer.GetMessage(OIDCConstants.InvalidInput)
                        });
                    }
                }

                if (request.client_assertion_type == null)
                {
                    var authHeader =
                        Request.Headers[Configuration["AccessTokenHeaderName"]];

                    if (string.IsNullOrEmpty(authHeader))
                    {
                        return Unauthorized(new DTOs.ErrorResponseDTO
                        {
                            error = _messageLocalizer.GetMessage(OIDCConstants.InvalidAuthZHeader),
                            error_description = _messageLocalizer.GetMessage(OIDCConstants.InvalidAuthZHeader)
                        });
                    }

                    if (!AuthenticationHeaderValue.TryParse(authHeader,
                        out var authHeaderVal))
                    {
                        return Unauthorized(new DTOs.ErrorResponseDTO
                        {
                            error = _messageLocalizer.GetMessage(OIDCConstants.InvalidAuthZHeader),
                            error_description = _messageLocalizer.GetMessage(OIDCConstants.InvalidAuthZHeader)
                        });
                    }

                    if (authHeaderVal.Scheme.Equals("Basic",
                        StringComparison.OrdinalIgnoreCase))
                    {
                        credential = authHeaderVal.Parameter;
                        type = "client_secret_basic";

                        try
                        {
                            var decoded =
                                Encoding.UTF8.GetString(
                                Convert.FromBase64String(credential));

                            if (!decoded.Contains(":"))
                            {
                                return Unauthorized(new DTOs.ErrorResponseDTO
                                {
                                    error = _messageLocalizer.GetMessage(OIDCConstants.InvalidCredentials),
                                    error_description = _messageLocalizer.GetMessage(OIDCConstants.InvalidCredentials)
                                });
                            }
                        }
                        catch
                        {
                            return Unauthorized(new DTOs.ErrorResponseDTO
                            {
                                error = _messageLocalizer.GetMessage(OIDCConstants.InvalidCredentials),
                                error_description = _messageLocalizer.GetMessage(OIDCConstants.InvalidCredentials),
                            });
                        }
                    }
                }

                if (!string.IsNullOrEmpty(request.client_assertion_type) &&
                    request.client_assertion_type.Contains(
                    "urn:ietf:params:oauth:client-assertion-type:jwt-bearer"))
                {
                    credential = request.client_assertion;
                    type = "private_key_jwt";
                }

                var result = await _authenticationService
                    .GetAccessToken(request, credential, type);

                if (!result.Success)
                {
                    return Ok(new DTOs.ErrorResponseDTO
                    {
                        error = result.error,
                        error_description = result.error_description
                    });
                }

                if (result.scopes != null &&
                    result.scopes.Contains("openid"))
                {
                    if (string.IsNullOrEmpty(result.refresh_token))
                    {
                        return Ok(new AccessTokenOpenIdResponseDTO
                        {
                            access_token = result.access_token,
                            expires_in = result.expires_in,
                            scopes = result.scopes,
                            token_type = result.token_type,
                            id_token = result.id_token
                        });
                    }

                    return Ok(new AccessTokenOpenIdRefreshTokenDTO
                    {
                        access_token = result.access_token,
                        expires_in = result.expires_in,
                        scopes = result.scopes,
                        token_type = result.token_type,
                        id_token = result.id_token,
                        refresh_token = result.refresh_token
                    });
                }

                if (string.IsNullOrEmpty(result.refresh_token))
                {
                    return Ok(new AccessTokenOAuthResponseDTO
                    {
                        access_token = result.access_token,
                        expires_in = result.expires_in,
                        scopes = result.scopes,
                        token_type = result.token_type
                    });
                }

                return Ok(new AccessTokenOAuthRefreshTokenDTO
                {
                    access_token = result.access_token,
                    expires_in = result.expires_in,
                    scopes = result.scopes,
                    token_type = result.token_type,
                    refresh_token = result.refresh_token
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Exception occurred in GetAccessToken");

                return Ok(
                    new DTOs.ErrorResponseDTO
                    {
                        error = _messageLocalizer.GetMessage(WebConstants.InternalServerError),
                        error_description = _messageLocalizer.GetMessage(WebConstants.InternalServerError)
                    });
            }
        }

        // ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~

        [Route("GetJourneyToken")]
        [Consumes("application/json")]
        [HttpPost]
        [ProducesResponseType(typeof(ServiceResult), StatusCodes.Status200OK)]
        public async Task<IActionResult> GenerateICPJourneyToken(
            [FromBody][Required] ICPAuthRequest request)
        {
            _logger.LogDebug("----> GenerateICPJourneyToken");

            try
            {
                if (request == null)
                    return BadRequest(new APIResponse
                    {
                        Success = false,
                        Message = "Request body is required"
                    });

                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                var result = await _authenticationService.ICPLoginVerify(request);

                if (result == null)
                {
                    _logger.LogError("GenerateICPJourneyToken failed");

                    return StatusCode(500, new APIResponse
                    {
                        Success = false,
                        Message = _messageLocalizer.GetMessage(WebConstants.InternalServerError)
                    });
                }

                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Exception occurred in GenerateICPJourneyToken");

                return StatusCode(500, new APIResponse
                {
                    Success = false,
                    Message = _messageLocalizer.GetMessage(WebConstants.InternalServerError)
                });
            }
        }
        // ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~

        [Route("GenerateLoginJourneyTokenBySuid")]
        [ProducesResponseType(typeof(ServiceResult), StatusCodes.Status200OK)]
        [Consumes("application/json")]
        [HttpPost]
        public async Task<IActionResult> GenerateLoginJourneyTokenBySuid(
            [FromBody] ICPAuthSuidRequest request)
        {
            _logger.LogDebug("----> GenerateLoginJourneyTokenBySuid");

            try
            {
                var result = await _authenticationService.ICPLoginSuidVerify(request);

                if (result == null)
                {
                    _logger.LogWarning("Subscriber not found for Suid: {Suid}", request.Suid?.SanitizeForLogging());

                    return NotFound(new APIResponse
                    {
                        Success = false,
                        Message = "Subscriber not found"
                    });
                }

                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Exception in GenerateLoginJourneyTokenBySuid");

                return StatusCode(StatusCodes.Status500InternalServerError, new APIResponse
                {
                    Success = false,
                    Message = _messageLocalizer.GetMessage(WebConstants.InternalServerError)
                });
            }
        }

        [Route("ActivateSubscriber/{id}")]
        [HttpPost]
        public async Task<IActionResult> CheckandUpdateSubscriber(string id)
        {
            if (string.IsNullOrWhiteSpace(id))
                return BadRequest("Subscriber Id is required.");

            var result = await _subscriberService.CheckandUpdateSubscriber(id);

            var response = new ResponseDTO
            {
                Success = result.Success,
                Message = result.Message
            };

            return Ok(response);
        }
    }
}
