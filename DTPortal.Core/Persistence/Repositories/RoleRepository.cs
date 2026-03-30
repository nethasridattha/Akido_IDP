using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

using DTPortal.Core.Domain.Models;
using DTPortal.Core.Domain.Lookups;
using DTPortal.Core.Domain.Repositories;
using Microsoft.Extensions.Logging;
using System;

namespace DTPortal.Core.Persistence.Repositories
{
    public class RoleRepository : GenericRepository<Role, idp_dtplatformContext>,
            IRoleRepository
    {
        private readonly ILogger _logger;
        public RoleRepository(idp_dtplatformContext context, ILogger logger) : base(context, logger)
        {
            _logger = logger;
        }

        public async Task<IEnumerable<RoleLookupItem>> GetRoleLookupItemsAsync()
        {
            try
            {
                return await Context.Roles
                    .Where(x => x.Status != "DELETED")
                    .OrderByDescending(x => x.CreatedDate)
                    .Select(x => new RoleLookupItem
                    {
                        Id = x.Id,
                        Name = x.Name,
                        DisplayName = x.DisplayName,
                        Description = x.Description,
                        Status = x.Status,
                        CreatedDate = x.CreatedDate

                    })
                    .AsNoTracking()
                    .ToListAsync();

            }
            catch (Exception error)
            {
                _logger.LogError(error, "GetRoleLookupItemsAsync::Database exception");
                return null;
            }
        }

        public async Task<Role> GetRoleByRoleIdWithActivities(int id)
        {
            try
            {
                return await Context.Roles
                    .Include(x => x.RoleActivities)
                    .SingleOrDefaultAsync(x => x.Id == id);
            }
            catch (Exception error)
            {
                _logger.LogError(error, "GetRoleByRoleIdWithActivities::Database exception");
                return null;
            }
        }

        //public async Task<bool> IsRoleExistsByName(Role role)
        //{
        //    try
        //    {
        //        return await Context.Roles.AsNoTracking().AnyAsync(u => u.Name == role.Name
        //        && u.DisplayName == role.DisplayName);
        //    }
        //    catch (Exception error)
        //    {
        //        _logger.LogError("IsRoleExistsByName::Database exception: {0}", error);
        //        return false;
        //    }
        //}

        public async Task<bool> IsRoleExistsByName(Role role)
        {
            try
            {
                var roleName = role.Name.Trim();
                var displayName = role.DisplayName.Trim();

                return await Context.Roles.AsNoTracking().AnyAsync(u =>
                    u.Name == roleName && u.DisplayName == displayName);
            }
            catch (Exception error)
            {
                _logger.LogError(error, "IsRoleExistsByName::Database exception");
                return false;
            }
        }


    }
}
