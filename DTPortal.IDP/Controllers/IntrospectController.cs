using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DTPortal.Core.Domain.Models;
using DTPortal.Core.Domain.Services;
using DTPortal.Common;
using DTPortal.IDP.DTOs;
using DTPortal.Core.Domain.Services.Communication;
using System.Net.Http.Headers;
using DTPortal.Core.Services;
using Microsoft.Extensions.Logging;
using DTPortal.Core.Domain.Repositories;
using DTPortal.Core.Utilities;
using static DTPortal.Common.CommonResponse;
using System.Text;
using DTPortal.Core.Constants;
using Microsoft.Extensions.Configuration;
using DTPortal.Core.Exceptions;
using System.ComponentModel.DataAnnotations;

namespace DTPortal.IDP.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class IntrospectController : BaseController
    {
        // Initialize logger
        private readonly ILogger<IntrospectController> _logger;

        // Initialize Db
        private readonly IUnitOfWork _unitOfWork;
        private readonly ICacheClient _cacheClient;
        private readonly IGlobalConfiguration _globalConfiguration;
        private readonly MessageConstants Constants;
        private readonly OIDCConstants OIDCConstants;
        private readonly IConfiguration Configuration;
        private readonly IHelper _helper;
        private readonly IClientService _clientService;
        private readonly IMessageLocalizer _messageLocalizer;
        public IntrospectController(ILogger<IntrospectController> logger,
            IUnitOfWork unitofWork, IGlobalConfiguration globalConfiguration,
            ICacheClient cacheClient, IConfiguration configuration,
            IHelper helper, IClientService clientService,
            IMessageLocalizer messageLocalizer)
        {
            _logger = logger;
            _unitOfWork = unitofWork;
            _cacheClient = cacheClient;
            _globalConfiguration = globalConfiguration;
            Configuration = configuration;
            _helper = helper;
            _clientService = clientService;

            var errorConfiguration = _globalConfiguration.
                GetErrorConfiguration();
            if (null == errorConfiguration)
            {
                _logger.LogError("Get Error Configuration failed");
                throw new NullReferenceException();
            }

            Constants = errorConfiguration.Constants;
            OIDCConstants = errorConfiguration.OIDCConstants;
            _messageLocalizer = messageLocalizer;
        }
        // ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
        [Route("introspect")]
        [HttpPost]
        [Consumes("application/x-www-form-urlencoded")]
        [Produces("application/json", "application/problem+json")]
        [RequestSizeLimit(10000)]
        [ProducesResponseType(typeof(object), StatusCodes.Status200OK)]
        [IgnoreAntiforgeryToken]
        public async Task<IActionResult> VerifyToken(
            [FromForm][Required] VerifyTokenReq verifyTokenReq, [FromHeader(Name = "Authorization")] string authHeader)
        {
            _logger.LogDebug("-->VerifyToken");

            try
            {
                if (verifyTokenReq == null)
                {
                    return BadRequest(new ErrorResponse
                    {
                        error = _messageLocalizer.GetMessage(OIDCConstants.InvalidInput),
                        error_description = _messageLocalizer.GetMessage(OIDCConstants.InvalidInput)
                    });
                }

                var allowedKeys = new HashSet<string>
                {
                    "token"
                };

                var incomingKeys = Request.Form.Keys;

                var unknownKeys = incomingKeys
                    .Where(k => !allowedKeys.Contains(k))
                    .ToList();

                if (unknownKeys.Any())
                {
                    return BadRequest(new DTOs.ErrorResponseDTO
                    {
                        error = _messageLocalizer.GetMessage(OIDCConstants.InvalidInput),
                        error_description = _messageLocalizer.GetMessage(OIDCConstants.InvalidInput)
                    });
                }

                if (!ModelState.IsValid)
                {
                    return ValidationProblem(ModelState);
                }

                if(string.IsNullOrEmpty(verifyTokenReq.token))
                {
                    return BadRequest(new ErrorResponse
                    {
                        error = _messageLocalizer.GetMessage(OIDCConstants.InvalidInput),
                        error_description = _messageLocalizer.GetMessage(OIDCConstants.InvalidInput)
                    });
                }

                if (string.IsNullOrEmpty(authHeader))
                {
                    return Unauthorized(new ErrorResponse
                    {
                        error = _messageLocalizer.GetMessage(OIDCConstants.InvalidToken),
                        error_description = _messageLocalizer.GetMessage(OIDCConstants.InvalidAuthZHeader)
                    });
                }

                if (!AuthenticationHeaderValue.TryParse(authHeader, out var authHeaderVal))
                {
                    return Unauthorized(new ErrorResponse
                    {
                        error = _messageLocalizer.GetMessage(OIDCConstants.InvalidToken),
                        error_description = _messageLocalizer.GetMessage(OIDCConstants.InvalidAuthZHeader)
                    });
                }

                if (!authHeaderVal.Scheme.Equals("Basic", StringComparison.OrdinalIgnoreCase))
                {
                    return Unauthorized(new ErrorResponse
                    {
                        error = _messageLocalizer.GetMessage(OIDCConstants.InvalidToken),
                        error_description = _messageLocalizer.GetMessage(OIDCConstants.UnsupportedAuthSchm)
                    });
                }

                if (string.IsNullOrEmpty(authHeaderVal.Parameter))
                {
                    return Unauthorized(new ErrorResponse
                    {
                        error = _messageLocalizer.GetMessage(OIDCConstants.InvalidClient),
                        error_description = _messageLocalizer.GetMessage(OIDCConstants.InvalidCredentials)
                    });
                }

                var result = await VerifyClientAndScope(
                    authHeaderVal.Parameter,
                    OAuth2Constants.VerifyToken);

                if (result != "Success" && result != "InvalidScope")
                {
                    return Unauthorized(new ErrorResponse
                    {
                        error = _messageLocalizer.GetMessage(OIDCConstants.InvalidClient),
                        error_description = _messageLocalizer.GetMessage(OIDCConstants.InvalidCredentials)
                    });
                }

                if (OIDCConstants.ClientNotActive.En == result)
                {
                    return Unauthorized(new ErrorResponse
                    {
                        error = _messageLocalizer.GetMessage(OIDCConstants.ClientNotActive),
                        error_description = _messageLocalizer.GetMessage(OIDCConstants.ClientNotActive)
                    });
                }

                if (result == "InvalidScope")
                {
                    return Unauthorized(new ErrorResponse
                    {
                        error = _messageLocalizer.GetMessage(OIDCConstants.insufficientScope),
                        error_description = _messageLocalizer.GetMessage(OIDCConstants.insufficientScope)
                    });
                }

                VerifyTokenInActiveRes inactiveResponse = new VerifyTokenInActiveRes();

                Accesstoken accessToken;

                try
                {
                    accessToken = await _cacheClient.Get<Accesstoken>(
                        "AccessToken", verifyTokenReq.token);

                    if (accessToken == null)
                    {
                        inactiveResponse.active = false;
                        return Ok(inactiveResponse);
                    }
                }
                catch (CacheException ex)
                {
                    _logger.LogError(ex, "Redis access token fetch failed");

                    return StatusCode(StatusCodes.Status500InternalServerError,
                        new ErrorResponse
                        {
                            error = _messageLocalizer.GetMessage(OIDCConstants.InternalError),
                            error_description = _messageLocalizer.GetMessage(OIDCConstants.InternalError)
                        });
                }

                var client = await _unitOfWork.Client
                    .GetClientByClientIdAsync(accessToken.ClientId);

                var organizationId = client?.OrganizationUid ?? string.Empty;

                var activeResponse = new VerifyTokenActiveRes
                {
                    active = true,
                    client_id = accessToken.ClientId,
                    username = accessToken.UserId,
                    scope = accessToken.Scopes,
                    org_id = organizationId
                };

                return Ok(activeResponse);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Exception occurred in VerifyToken");

                return StatusCode(StatusCodes.Status500InternalServerError,
                    new ErrorResponse
                    {
                        error = _messageLocalizer.GetMessage(OIDCConstants.InternalError),
                        error_description = _messageLocalizer.GetMessage(OIDCConstants.InternalError)
                    });
            }
        }
        // ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
        [NonAction]
        // Validate the client credentials and scope
        private async Task<string> VerifyClientAndScope(
            string credentials, string scope)
        {
            _logger.LogDebug("-->VerifyClientAndScope");

            // Validate input
            if ((null == credentials) || (null == scope))
            {
                _logger.LogError(OIDCConstants.InvalidInput.En);
                return "Failed";
            }

            try
            {
                var encoding = Encoding.GetEncoding("iso-8859-1");
                credentials = encoding.GetString(
                    Convert.FromBase64String(credentials));
            }
            catch (Exception error)
            {
                _logger.LogError("GetEncoding failed: {0}", error.Message);
                return "Failed";
            }

            int separator = credentials.IndexOf(':');
            if (-1 == separator)
            {
                _logger.LogError("credentials not found");
                return "Failed";
            }

            string clientId = credentials.Substring(0, separator);
            string clientSecret = credentials.Substring(separator + 1);
            _logger.LogInformation($"Client Id : {clientId},Client Secret : {clientSecret}");

            Client client = null;
            try
            {
                // Get Client details from database
                client = await _unitOfWork.Client.GetClientByClientIdAsync(clientId);
                if (null == client)
                {
                    _logger.LogError("_unitOfWork.Client.GetClientByClientId failed");
                    return "Failed";
                }
            }
            catch (Exception)
            {
                var errorMessage = _helper.GetErrorMsg(ErrorCodes.DB_ERROR);
                return "Failed";
            }

            if (StatusConstants.ACTIVE != client.Status)
            {
                _logger.LogError("Client status is not active: {0}", client.Status);
                return _messageLocalizer.GetMessage(OIDCConstants.ClientNotActive);
            }

            if (client.ClientSecret != clientSecret)
            {
                _logger.LogError("Client secret not matched");
                return "Failed";
            }

            // Parse the space seperated scopes
            var scopes = client.Scopes.Split(' ');
            if (0 == scope.Length)
            {
                _logger.LogError("Client scopes not found");
                return "Failed";
            }

            // Check for verify token scope
            if (!scopes.Contains(scope))
            {
                _logger.LogError($"The scope is not there in access token scopes {0}",
                    scope);
                return "InvalidScope";
            }

            _logger.LogDebug("<--VerifyClientAndScope");
            return "Success";
        }
        // ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
    }
}
