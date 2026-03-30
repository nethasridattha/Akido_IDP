using DTPortal.Core.Domain.Models;
using DTPortal.Core.Domain.Services;
using DTPortal.Core.Domain.Services.Communication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace DTPortal.IDP.Controllers.ApiControllers
{
    [ApiExplorerSettings(IgnoreApi = true)]
    [AllowAnonymous]
    [Route("api/[controller]")]
    public class ProfileController : BaseController
    {
        private readonly IScopeService _scopeService;
        public ProfileController(IScopeService scopeService)
        {
            _scopeService = scopeService;
        }

        [HttpGet]
        [Route("get-profile-list")]
        public async Task<APIResponse> GetProfileListAsync()
        {
            try
            {
                var scopesList = await _scopeService.ListScopeAsync();
                return new APIResponse(true, "Successfully received scopes list", scopesList);
            }
            catch (Exception ex)
            {
                return new APIResponse($"An error occurred while fetching profile list: {ex.Message}");
            }
        }

        [HttpGet]
        [Route("get-profile-by-id")]
        public async Task<APIResponse> GetProfileByIdAsync([FromQuery] int id)
        {
            try
            {
                if (id <= 0)
                {
                    return new APIResponse("Invalid profile ID provided.");
                }

                var scope = await _scopeService.GetScopeAsync(id);
                if (scope == null)
                {
                    return new APIResponse($"No profile found with ID: {id}");
                }
                return new APIResponse(true, "Successfully received profile", scope);
            }
            catch (Exception ex)
            {
                return new APIResponse($"An error occurred while fetching profile by ID: {ex.Message}");
            }
        }

        [HttpPost]
        [Route("add-profile")]
        public async Task<APIResponse> AddProfileAsync([FromBody] Scope request)
        {
            try
            {
                if (request == null)
                {
                    return new APIResponse("Profile data is required.");
                }

                if (string.IsNullOrWhiteSpace(request.Name))
                {
                    return new APIResponse("Profile name is required.");
                }

                var result = await _scopeService.CreateScopeAsync(request);
                if (!result.Success)
                {
                    return new APIResponse(result.Message);
                }
                return new APIResponse(true, "Profile added successfully", result.Result);
            }
            catch (Exception ex)
            {
                return new APIResponse($"An error occurred while adding profile: {ex.Message}");
            }
        }

        [HttpPut]
        [Route("update-profile")]
        public async Task<APIResponse> UpdateProfileAsync([FromBody] Scope request)
        {
            try
            {
                if (request == null || request.Id <= 0)
                {
                    return new APIResponse("Valid profile data with ID is required.");
                }

                var existingScope = await _scopeService.GetScopeAsync(request.Id);
                if (existingScope == null)
                {
                    return new APIResponse($"No profile found with ID: {request.Id}");
                }
                var result = await _scopeService.UpdateScopeAsync(request);
                if (!result.Success)
                {
                    return new APIResponse($"Failed to update profile: {result.Message}");
                }
                return new APIResponse(true, "Profile updated successfully", result.Result);
            }
            catch (Exception ex)
            {
                return new APIResponse($"An error occurred while updating profile: {ex.Message}");
            }
        }

        [HttpDelete]
        [Route("delete-profile")]
        public async Task<APIResponse> DeleteProfileAsync([FromQuery] int id, string UUID)
        {
            try
            {
                if (id <= 0)
                {
                    return new APIResponse("Invalid profile ID provided.");
                }

                if (string.IsNullOrWhiteSpace(UUID))
                {
                    return new APIResponse("UUID is required for deletion.");
                }

                var result = await _scopeService.DeleteScopeAsync(id, UUID);
                if (!result.Success)
                {
                    return new APIResponse($"Failed to delete profile: {result.Message}");
                }
                return new APIResponse(true, "Profile deleted successfully");
            }
            catch (Exception ex)
            {
                return new APIResponse($"An error occurred while deleting profile: {ex.Message}");
            }
        }
    }
}
