using DTPortal.Core.Domain.Services;
using DTPortal.Core.Domain.Services.Communication;
using Microsoft.AspNetCore.Mvc;
using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace DTPortal.IDP.Controllers
{
    [ApiExplorerSettings(IgnoreApi = true)]
    [ApiController]
    [Route("api/[controller]")]
    public class UserConsentController : BaseController
    {
        private readonly IUserProfilesConsentService _userProfilesConsentService;
        public UserConsentController(
            IUserProfilesConsentService userProfilesConsentService)
        {
            _userProfilesConsentService = userProfilesConsentService;
        }

        [Route("GetUserConsents")]
        [HttpGet]
        public async Task<IActionResult> GetUserConsents(
            [RegularExpression(@"^[A-Za-z0-9\-]{5,50}$", ErrorMessage = "Invalid SUID format")]
            string suid)
        {
            try
            {
                var res = await _userProfilesConsentService.
                GetUserProfilesConsentbySuidAsync(suid);
                APIResponse response = new APIResponse();
                response.Success = res.Success;
                response.Message = res.Message;
                response.Result = res.Resource;
                return Ok(response);
            }
            catch(Exception ex)
            {
                APIResponse response = new APIResponse();
                response.Success = false;
                response.Message = ex.Message;
                response.Result = null;
                return Ok(response);
            }
        }

        [Route("GetUserConsentsByClientName")]
        [HttpGet]
        public async Task<IActionResult> GetUserConsentsByClientName(
            [FromQuery]
            [Required]
            [RegularExpression(@"^[A-Za-z0-9\-]{5,50}$", ErrorMessage = "Invalid SUID format.")]
            string suid,

            [FromQuery]
            [Required]
            [MinLength(1)]
            [MaxLength(100)]
            [RegularExpression(@"^[a-zA-Z0-9 _\-.]+$", ErrorMessage = "Application name contains invalid characters.")]
            string applicationName)
        {
            try
            {
                var res = await _userProfilesConsentService.
                GetUserProfilesConsentByClientNameAsync(suid, applicationName);
                APIResponse response = new APIResponse();
                response.Success = res.Success;
                response.Message = res.Message;
                response.Result = res.Resource;
                return Ok(response);
            }
            catch (Exception ex)
            {
                APIResponse response = new APIResponse();
                response.Success = false;
                response.Message = ex.Message;
                response.Result = null;
                return Ok(response);
            }
        }

        [Route("GetUserConsentsByProfile")]
        [HttpGet]
        public async Task<IActionResult> GetUserConsentsByProfile(

            [FromQuery]
            [Required]
            [RegularExpression(@"^[A-Za-z0-9\-]{5,50}$", ErrorMessage = "Invalid SUID format.")]
            string suid,

            [FromQuery]
            [Required]
            [StringLength(100, MinimumLength = 2)]
            [RegularExpression(@"^[A-Za-z0-9][A-Za-z0-9\s\-_\.]{1,99}$", ErrorMessage = "Application name contains invalid characters.")]
            string applicationName,

            [Required]
            [StringLength(50)]
            [RegularExpression(@"^[A-Za-z0-9._-]+$")]
             string profile)
        {
            if(!ModelState.IsValid)
            {
                APIResponse response = new APIResponse();
                response.Success = false;
                response.Message = "Validation failed: " + string.Join("; ", ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage));
                response.Result = null;
                return BadRequest(response);
            }
            try
            {
                var res = await _userProfilesConsentService.
                GetUserProfilesConsentByProfileAsync(suid, applicationName, profile);
                APIResponse response = new APIResponse();
                response.Success = res.Success;
                response.Message = res.Message;
                response.Result = res.Resource;
                return Ok(response);
            }
            catch (Exception ex)
            {
                APIResponse response = new APIResponse();
                response.Success = false;
                response.Message = ex.Message;
                response.Result = null;
                return Ok(response);
            }
        }

        [Route("RevokeUserConsentsByProfile")]
        [HttpGet]
        public async Task<IActionResult> RevokeUserConsentsByProfile(
            [FromQuery]
            [Required]
            [RegularExpression(@"^[A-Za-z0-9\-]{5,50}$", ErrorMessage = "Invalid SUID format.")]
            string suid,

            [FromQuery]
            [Required]
            [MinLength(1)]
            [MaxLength(100)]
            [RegularExpression(@"^[a-zA-Z0-9 _\-.]+$", ErrorMessage = "Application name contains invalid characters.")]
            string applicationName,

            [FromQuery]
            [Required]
            [StringLength(50, MinimumLength = 2)]
            [RegularExpression(@"^[A-Za-z0-9_\-\.]+$", ErrorMessage = "Profile contains invalid characters.")]
            string profile)
        {
            try
            {
                var res = await _userProfilesConsentService.
                RevokeUserProfilesConsentByProfileAsync(suid, applicationName, profile);
                APIResponse response = new APIResponse();
                response.Success = res.Success;
                response.Message = res.Message;
                response.Result = res.Resource;
                return Ok(response);
            }
            catch(Exception ex)
            {
                APIResponse response = new APIResponse();
                response.Success = false;
                response.Message = ex.Message;
                response.Result = null;
                return Ok(response);
            }
        }
    }
}
