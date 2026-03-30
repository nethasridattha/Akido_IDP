using System.Threading.Tasks;
using System.Collections.Generic;

using DTPortal.Core.Domain.Lookups;
using DTPortal.Core.Domain.Models;


namespace DTPortal.Core.Domain.Repositories
{
    public interface IRoleRepository : IGenericRepository<Role>
    {
        Task<IEnumerable<RoleLookupItem>> GetRoleLookupItemsAsync();

        Task<Role> GetRoleByRoleIdWithActivities(int id);
        Task<bool> IsRoleExistsByName(Role role);
    }
}
