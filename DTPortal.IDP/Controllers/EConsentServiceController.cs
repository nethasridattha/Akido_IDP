using DTPortal.Core.Domain.Services;
using DTPortal.Core.Domain.Services.Communication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;

namespace DTPortal.IDP.Controllers
{
    [Route("api/[controller]")]
    [AllowAnonymous]
    [ApiController]
    public class EConsentServiceController : BaseController
    {
        private readonly IEConsentService _eConsentService;

        public EConsentServiceController(IEConsentService eConsentService)
        {
            _eConsentService = eConsentService;
        }


        [Route("GetClientProfiles")]
        [EnableCors("AllowedOrigins")]
        [HttpGet]
        public async Task<IActionResult> GetClientProfiles(
            [FromQuery]
            [Required]
            [StringLength(100,MinimumLength =5)]
            [RegularExpression(@"^[A-Za-z0-9._-]+$")]
            string clientId)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(new APIResponse
                {
                    Success = false,
                    Message = "Invalid clientId. Ensure it is between 5 and 100 characters and contains only letters, numbers, dots, underscores, or hyphens."
                });
            }
            try
            {
                APIResponse response = new APIResponse();
                var res = await _eConsentService.GetClientProfiles(clientId);

                response.Success = res.Success;
                response.Result = res.Resource;
                response.Message = res.Message;

                return Ok(response);
            }
            catch(Exception ex)
            {
                return Ok(new APIResponse
                {
                    Success = false,
                    Message = $"An error occurred while processing the request: {ex.Message}"
                });
            }
        }

        [Route("GetClientPurposes")]
        [EnableCors("AllowedOrigins")]
        [HttpGet]
        public async Task<IActionResult> GetClientPurposes(
            [FromQuery]
            [Required]
            [StringLength(100,MinimumLength =5)]
            [RegularExpression(@"^[A-Za-z0-9._-]+$")]
            string clientId)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new APIResponse
                {
                    Success = false,
                    Message = "Invalid clientId. Ensure it is between 5 and 100 characters and contains only letters, numbers, dots, underscores, or hyphens."
                });
            }
            try
            {
                APIResponse response = new APIResponse();
                var res = await _eConsentService.GetClientPurposes(clientId);

                response.Success = res.Success;
                response.Result = res.Resource;
                response.Message = res.Message;

                return Ok(response);
            }
            catch (Exception ex)
            {
                return Ok(new APIResponse
                {
                    Success = false,
                    Message = $"An error occurred while processing the request: {ex.Message}"
                });
            }
        }
    }
}
