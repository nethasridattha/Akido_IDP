using DTPortal.Core.Domain.Services;
using DTPortal.Core.Domain.Services.Communication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;

namespace DTPortal.IDP.Controllers.ApiControllers
{
    [ApiExplorerSettings(IgnoreApi = true)]
    [Route("api/[controller]")]
    public class SessionController : BaseController
    {
        private readonly ISessionService _sessionService;
        public SessionController(ISessionService sessionService)
        {
            _sessionService = sessionService;
        }

        [HttpGet]
        [Route("get-session/{searchValue}/{searchType}")]
        public async Task<APIResponse> GetAllRAUserSessions(
            [FromRoute]
            [Required]
            [StringLength(100)]
            [RegularExpression(@"^[A-Za-z0-9@._-]{1,100}$", ErrorMessage = "Invalid search value")]
            string searchValue,

            [FromRoute]
            [Range(1, 5, ErrorMessage = "Invalid search type")]
            int searchType)
        {
            try
            {
                if (string.IsNullOrEmpty(searchValue))
                {
                    return new APIResponse($"Search value cannot be null.");
                }

                if (searchType < 0)
                {
                    return new APIResponse("Invalid search type.");
                }

                var session = await _sessionService.GetAllRAUserSessions(searchValue, searchType);

                return new APIResponse(true, "Successfully received session", session);

            }
            catch (Exception ex)
            {
                return new APIResponse($"An error occurred while fetching session: {ex.Message}");
            }
        }

        [HttpPost]
        [Route("revoke-session")]
        public async Task<APIResponse> RevokeSession([FromHeader(Name = "X-Session-Id")] string sessionId)
        {
            try
            {
                if (string.IsNullOrEmpty(sessionId))
                {
                    return new APIResponse($"Session ID cannot be null.");
                }
                var data = new LogoutUserRequest
                {
                    GlobalSession = sessionId
                };
                var result = await _sessionService.LogoutUser(data);
                
                return new APIResponse(result.Success, result.Message, result.Result);
            }
            catch (Exception ex)
            {
                return new APIResponse($"An error occurred while revoking session: {ex.Message}");
            }
        }
    }
}
