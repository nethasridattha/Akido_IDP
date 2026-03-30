using AppShieldRestAPICore.Filters;
using Base32;
using DTPortal.Common;
using DTPortal.Core.Constants;
using DTPortal.Core.Domain.Models;
using DTPortal.Core.Domain.Services;
using DTPortal.Core.Domain.Services.Communication;
using DTPortal.Core.DTOs;
using DTPortal.Core.Services;
using DTPortal.Core.Utilities;
using DTPortal.IDP.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Org.BouncyCastle.Asn1.Ocsp;
using OtpSharp;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace DTPortal.IDP.Controllers
{
    [ApiController]
    [AllowAnonymous]
    [Route("api/[controller]")]
    public class UserInfoController : BaseController
    {
        private readonly IConfiguration Configuration;
        private readonly ILogger<UserInfoController> _logger;
        private readonly IUserInfoService _userInfoService;
        private readonly IGlobalConfiguration _globalConfiguration;
        private readonly OIDCConstants OIDCConstants;
        private readonly IUserProfileService _userProfileService;
        private readonly IMessageLocalizer _messageLocalizer;

        public UserInfoController(ILogger<UserInfoController> logger,
            IUserInfoService userInfoService,
            IGlobalConfiguration globalConfiguration,
            IConfiguration configuration,
            IUserProfileService userProfileService,
            IMessageLocalizer messageLocalizer)
        {
            _logger = logger;
            _userInfoService = userInfoService;
            _globalConfiguration = globalConfiguration;
            Configuration = configuration;

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
            _userProfileService = userProfileService;
            _messageLocalizer = messageLocalizer;
        }
        // ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
        [Route("userinfo")]
        [EnableCors("AllowedOrigins")]
        [ProducesResponseType(typeof(APIResponse), 200)]
        [ProducesResponseType(typeof(DTOs.ErrorResponseDTO), 200)]
        [ProducesResponseType(typeof(object), 200)]
        [HttpGet]
        public async Task<IActionResult> GetUserInfo()
        {
            try
            {
                GetUserInfoResponse response = new GetUserInfoResponse();

                // Check the value of authorization header
                var authHeader = Request.Headers[Configuration["AccessTokenHeaderName"]];
                if (string.IsNullOrEmpty(authHeader))
                {
                    IDP.DTOs.ErrorResponseDTO errResponse = new IDP.DTOs.ErrorResponseDTO();
                    errResponse.error = _messageLocalizer.GetMessage(OIDCConstants.InvalidToken);
                    errResponse.error_description = _messageLocalizer.
                        GetMessage(OIDCConstants.InvalidAuthZHeader);
                    return Unauthorized(errResponse);
                }

                // Parse the authorization header
                var authHeaderVal = AuthenticationHeaderValue.Parse(authHeader);
                if (null == authHeaderVal.Scheme || null == authHeaderVal.Parameter)
                {
                    _logger.LogError("Invalid scheme or parameter in Authorization header");
                    IDP.DTOs.ErrorResponseDTO errResponse = new IDP.DTOs.ErrorResponseDTO();
                    errResponse.error = _messageLocalizer.GetMessage(OIDCConstants.InvalidToken);
                    errResponse.error_description = _messageLocalizer.
                        GetMessage(OIDCConstants.InvalidAuthZHeader);
                    return Unauthorized(errResponse);
                }

                // Check the authorization is of Bearer type
                if (!authHeaderVal.Scheme.Equals("bearer",
                     StringComparison.OrdinalIgnoreCase))
                {
                    _logger.LogError($"Token is not Bearer token type.Recieved {0} type",
                        authHeaderVal.Scheme);
                    IDP.DTOs.ErrorResponseDTO errResponse = new IDP.DTOs.ErrorResponseDTO();
                    errResponse.error = _messageLocalizer.GetMessage(OIDCConstants.InvalidToken);
                    errResponse.error_description = _messageLocalizer.
                        GetMessage(OIDCConstants.UnsupportedAuthSchm);
                    return Unauthorized(errResponse);
                }

                bool signed = false;
                if (Request.Headers.TryGetValue("Accept", out var header))
                {
                    if (header.Contains("application/jwt"))
                        signed = true;
                }

                //var result = await _userInfoService.GetUserInfo(authHeaderVal.Parameter, signed);
                var result = await _userInfoService.UserProfile(authHeaderVal.Parameter);
                if (null == result)
                {
                    IDP.DTOs.ErrorResponseDTO errResponse = new IDP.DTOs.ErrorResponseDTO();
                    errResponse.error = _messageLocalizer.GetMessage(OIDCConstants.InternalError);
                    errResponse.error_description = _messageLocalizer.
                        GetMessage(OIDCConstants.InternalError);
                    return Unauthorized(errResponse);
                }
                if ("ErrorResponseDTO" == result.GetType().Name)
                {
                    return Unauthorized(result);
                }

                _logger.LogDebug("GetUserInfo response : {0}", result);

                if (true == signed)
                {
                    Response.ContentType = "application/jwt";
                    Response.StatusCode = 200;
                    await Response.WriteAsync(result.ToString());
                    return Ok();
                }

                _logger.LogDebug("<--GetUserInfo");
                return Ok(result);
            }
            catch(Exception ex)
            {
                _logger.LogError(ex, "An error occurred while getting user info.");
                APIResponse errResponse = new APIResponse()
                {
                    Success = false,
                    Message = "An error occurred while processing the request.",
                };
                return Ok(errResponse);
            }
        }
        // ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~

        [Route("GetUserImage")]
        [ProducesResponseType(typeof(DTOs.ErrorResponseDTO), 200)]
        [ProducesResponseType(typeof(APIResponse), 200)]
        [EnableCors("AllowedOrigins")]
        [HttpGet]
        public async Task<IActionResult> GetUserImage()
        {
            IDP.DTOs.ErrorResponseDTO errResponse = new IDP.DTOs.ErrorResponseDTO();
            GetUserInfoResponse response = new GetUserInfoResponse();

            // Check the value of authorization header
            var authHeader = Request.Headers[Configuration["AccessTokenHeaderName"]];
            if (string.IsNullOrEmpty(authHeader))
            {
                errResponse.error = _messageLocalizer.GetMessage(OIDCConstants.InvalidToken);
                errResponse.error_description = _messageLocalizer.
                    GetMessage(OIDCConstants.InvalidAuthZHeader);
                return Unauthorized(errResponse);
            }

            _logger.LogInformation($"Authorization header recieved ");

            // Parse the authorization header
            var authHeaderVal = AuthenticationHeaderValue.Parse(authHeader);
            if (null == authHeaderVal.Scheme || null == authHeaderVal.Parameter)
            {
                _logger.LogError("Invalid scheme or parameter in Authorization header");
                errResponse.error = _messageLocalizer.GetMessage(OIDCConstants.InvalidToken);
                errResponse.error_description = _messageLocalizer.
                    GetMessage(OIDCConstants.InvalidAuthZHeader);
                return Unauthorized(errResponse);
            }

            // Check the authorization is of Bearer type
            if (!authHeaderVal.Scheme.Equals("bearer",
                 StringComparison.OrdinalIgnoreCase))
            {
                _logger.LogError($"Token is not Bearer token type.Recieved {0} type",
                    authHeaderVal.Scheme);
                errResponse.error = _messageLocalizer.GetMessage(OIDCConstants.InvalidToken);
                errResponse.error_description = _messageLocalizer.
                    GetMessage(OIDCConstants.UnsupportedAuthSchm);
                return Unauthorized(errResponse);
            }

            var result = await _userInfoService.GetUserImage(authHeaderVal.Parameter);

            return Ok(result);
        }

        // ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~

        [HttpGet("GetUserEmail")]
        [EnableCors("AllowedOrigins")]
        public async Task<IActionResult> GetUserEmail(
            [FromQuery][Required][RegularExpression("^[a-zA-Z0-9_-]{6,50}$")] string suid)
        {
            try
            {
                var response = await _userProfileService.GetUserEmail(suid);
                return Ok(new APIResponse()
                {
                    Success = response.Success,
                    Message = response.Message,
                    Result = response.Resource
                });
            }
            catch(Exception ex)
            {
                _logger.LogError(ex, "An error occurred while getting user email.");
                return Ok(new APIResponse()
                {
                    Success = false,
                    Message = "An error occurred while processing the request.",
                });
            }
        }
    }
}
