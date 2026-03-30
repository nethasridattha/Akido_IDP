using DTPortal.Core.Domain.Services;
using DTPortal.Core.Domain.Services.Communication;
using Google.Apis.Logging;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;

namespace DTPortal.IDP.Controllers
{
    [ApiExplorerSettings(IgnoreApi = true)]
    [Route("api/[controller]")]
    [ApiController]

    public class SDKLoginController : BaseController
    {
        private readonly ILogger<SDKLoginController> _logger;
        private readonly ISDKAuthenticationService _sdkAuthenticationService;
        public SDKLoginController(ILogger<SDKLoginController> logger,
            ISDKAuthenticationService sDKAuthenticationService)
        {
            _logger = logger;
            _sdkAuthenticationService = sDKAuthenticationService;
        }

        [Route("VerifyUser")]
        [EnableCors("AllowedOrigins")]
        [Consumes("application/json")]
        [HttpPost]
        public async Task<IActionResult> VerifyUser(
            [FromBody] [Required] SDKVerifyUserRequest requestObj)
        {
            if (requestObj == null)
                return BadRequest(new
                {
                    errors = new { request = new[] { "Request body is required." } }
                });

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _sdkAuthenticationService.VerifyUser(requestObj);

            if (result == null)
                return NotFound(new
                {
                    success = false,
                    message = "User not found",
                    result = (object)null
                });

            return Ok(new APIResponse()
            {
                Success=result.Success,
                Message=result.Message,
                Result=result.Result
            });
        }

        [Route("VerifyUserAuthData")]
        [EnableCors("AllowedOrigins")]
        [Consumes("application/json")]
        [HttpPost]
        public async Task<IActionResult> VerifyUserAuthData(
            [FromBody] [Required] VerifyUserAuthDataRequest requestObj)
        {
            if (requestObj == null)
                return BadRequest(new { error = "Request body is required." });

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                var result = await _sdkAuthenticationService.VerifyUserAuthData(requestObj);
                return Ok(new APIResponse()
                {
                    Success = result.Success,
                    Message = result.Message,
                    Result = result.Result
                });
            }
            catch(Exception ex)
            {
                _logger.LogError(ex, "Error verifying user authentication data");
                return Ok(new APIResponse()
                {
                    Success = false,
                    Message = "An error occurred while verifying user authentication data",
                    Result = (object)null
                });
            }
        }

        [Route("GetVerifierUrl")]
        [EnableCors("AllowedOrigins")]
        [Consumes("application/json")]
        [ProducesResponseType(typeof(ServiceResult), 200)]
        [HttpGet]
        public async Task<IActionResult> GetVerifierUrl()
        {
            var result = await _sdkAuthenticationService.GetVerificationUrl();
            return Ok(result);
        }

        [Route("IsUserVerifiedQrCode")]
        [Consumes("application/json")]
        [EnableCors("AllowedOrigins")]
        [ProducesResponseType(typeof(VerifyUserAuthDataResponse), 200)]
        [HttpPost]
        public async Task<IActionResult> IsUserVerifiedQrCode(
            [FromBody][Required] VerifyQrRequest verifyQrCodeRequest)
        {
            if (verifyQrCodeRequest == null)
                return BadRequest(new { error = "Request body is required" });

            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            try
            {
                var result = await _sdkAuthenticationService
               .IsUserVerifiedQrCode(verifyQrCodeRequest);

                if (result == null)
                    return Ok(new
                    {
                        success = false,
                        message = "QR code not found or expired",
                        result = (object)null
                    });

                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error verifying QR code");
                return Ok(new APIResponse()
                {
                    Success = false,
                    Message = "An error occurred while verifying the QR code",
                    Result = (object)null
                });
            }
        }
    }
}
