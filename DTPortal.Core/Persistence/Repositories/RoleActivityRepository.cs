using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

using DTPortal.Core.Domain.Models;
using DTPortal.Core.Domain.Lookups;
using DTPortal.Core.Domain.Repositories;
using Microsoft.Extensions.Logging;

namespace DTPortal.Core.Persistence.Repositories
{
    public class RoleActivityRepository : GenericRepository<RoleActivity, idp_dtplatformContext>,
            IRoleActivityRepository
    {
        private readonly ILogger _logger;
        public RoleActivityRepository(idp_dtplatformContext context, ILogger logger) : base(context, logger)
        {
            _logger = logger;
        }

        public async Task<RoleActivity> GetRoleActivityByRoleIdActivityId(int roleId, int activityId)
        {
            try
            {
                return await Context.RoleActivities.SingleOrDefaultAsync(x => x.RoleId == roleId && x.ActivityId == activityId);
            }
            catch(Exception error)
            {
                _logger.LogError(error, "GetRoleActivityByRoleIdActivityId::Database exception");
                return null;
            }
        }
    }
}
