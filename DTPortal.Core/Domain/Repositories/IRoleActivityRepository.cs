using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DTPortal.Core.Domain.Lookups;
using DTPortal.Core.Domain.Models;

namespace DTPortal.Core.Domain.Repositories
{
    public interface IRoleActivityRepository : IGenericRepository<RoleActivity>
    {
        Task<RoleActivity> GetRoleActivityByRoleIdActivityId(int roleId, int activityId);
    }
}
