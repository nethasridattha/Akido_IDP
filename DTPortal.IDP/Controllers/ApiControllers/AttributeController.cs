using DTPortal.Core.Domain.Models;
using DTPortal.Core.Domain.Services;
using DTPortal.Core.Domain.Services.Communication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace DTPortal.IDP.Controllers.ApiControllers
{
    [ApiExplorerSettings(IgnoreApi = true)]
    [AllowAnonymous]
    [Route("api/[controller]")]
   
    public class AttributeController : BaseController
    {
        private readonly IUserClaimService _userClaimService;
        public AttributeController(IUserClaimService userClaimService)
        {
            _userClaimService = userClaimService;
        }

        [HttpGet]
        [Route("get-attribute-list")]
        public async Task<APIResponse> GetAttributeListAsync()
        {
            try
            {
                var attributeList = await _userClaimService.ListUserClaimAsync();
                return new APIResponse(true, "Successfully received attribute list", attributeList);
            }
            catch (System.Exception ex)
            {
                return new APIResponse($"An error occurred while fetching attribute list: {ex.Message}");
            }
        }

        [HttpGet]
        [Route("get-all-attribute-list")]
        public async Task<APIResponse> GetAllAttributeListAsync()
        {
            try
            {
                var attributeList = await _userClaimService.ListAllClaimAsync();
                return new APIResponse(true, "Successfully received attribute list", attributeList);
            }
            catch (System.Exception ex)
            {
                return new APIResponse($"An error occurred while fetching attribute list: {ex.Message}");
            }
        }

        [HttpGet]
        [Route("get-attribute-by-id")]
        public async Task<APIResponse> GetAttributeByIdAsync(int id)
        {
            try
            {
                if(id < 0)
                {
                    return new APIResponse("Invalid Id");
                }

                var attribute = await _userClaimService.GetUserClaimAsync(id);
                if (attribute == null)
                    return new APIResponse($"No attribute found with ID: {id}");
                return new APIResponse(true, "Successfully received attribute", attribute);
            }
            catch (System.Exception ex)
            {
                return new APIResponse($"An error occurred while fetching attribute by ID: {ex.Message}");
            }
        }

        [HttpPost]
        [Route("add-attribute")]
        public async Task<APIResponse> AddAttributeAsync([FromBody] UserClaim userClaim)
        {
            try
            {
                if (userClaim == null)
                {
                    return new APIResponse("Invalid attribute data provided");
                }

                var createResult = await _userClaimService.CreateUserClaimAsync(userClaim);
                if (!createResult.Success)
                    return new APIResponse(createResult.Message);
                return new APIResponse(true, "Successfully updated attribute", userClaim);
            }
            catch (System.Exception ex)
            {
                return new APIResponse($"An error occurred while creating attribute: {ex.Message}");
            }
        }

        [HttpPost]
        [Route("update-attribute")]
        public async Task<APIResponse> UpdateAttributeAsync([FromBody] UserClaim userClaim)
        {
            try
            {
                if (userClaim == null)
                {
                    return new APIResponse("Invalid attribute data provided");
                }

                var updateResult = await _userClaimService.UpdateUserClaimAsync(userClaim);
                if (!updateResult.Success)
                    return new APIResponse(updateResult.Message);
                return new APIResponse(true, "Successfully updated attribute", userClaim);
            }
            catch (System.Exception ex)
            {
                return new APIResponse($"An error occurred while updating attribute: {ex.Message}");
            }
        }

        [HttpDelete]
        [Route("delete-attribute")]
        public async Task<APIResponse> DeleteAttributeAsync(int id, string UUID)
        {
            try
            {
                if (id < 0)
                {
                    return new APIResponse("Invalid Id");
                }

                var deleteResult = await _userClaimService.DeleteUserClaimAsync(id, UUID);
                if (!deleteResult.Success)
                    return new APIResponse(deleteResult.Message);
                return new APIResponse(true, "Successfully deleted attribute");
            }
            catch (System.Exception ex)
            {
                return new APIResponse($"An error occurred while deleting attribute: {ex.Message}");
            }
        }
    }
}
