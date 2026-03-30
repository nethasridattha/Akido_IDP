using System.Threading.Tasks;
using System.Collections.Generic;

using DTPortal.Core.Domain.Lookups;
using DTPortal.Core.Domain.Models;
using DTPortal.Core.Domain.Services.Communication;


namespace DTPortal.Core.Domain.Services
{
    public interface IRoleManagementService
    {
        Task<IEnumerable<RoleLookupItem>> GetRoleLookupItemsAsync();

        Task<Role> GetRoleAsync(int id);

        Task<RoleResponse> AddRoleAsync(Role role, IDictionary<int, bool> selectedActivityIds,
            bool makerCheckerFlag = false);

        Task<RoleResponse> DeleteRoleAsync(int id, string updatedBy,
            bool makerCheckerFlag = false);
        Task<RoleResponse> UpdateRoleState(int id, bool isApproved, string reason = null);
        Task<RoleResponse> ActivateRoleAsync(int id);
        Task<RoleResponse> DeActivateRoleAsync(int id);
    }
}
