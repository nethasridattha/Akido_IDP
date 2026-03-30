using DTPortal.Core.Domain.Models;
using DTPortal.Core.Domain.Repositories;
using DTPortal.Core.DTOs;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTPortal.Core.Persistence.Repositories
{
    public class UserClaimRepository : GenericRepository<UserClaim, idp_dtplatformContext>,
        IUserClaimRepository
    {
        private readonly ILogger _logger;
        public UserClaimRepository(idp_dtplatformContext context, ILogger logger) :
            base(context, logger)
        {
            _logger = logger;
        }

        public async Task<bool> IsUserClaimExistsWithNameAsync(string name)
        {
            try
            {
                return await Context.UserClaims.AsNoTracking().AnyAsync(u => u.Name == name);
            }
            catch (Exception error)
            {
                _logger.LogError(error, "IsUserClaimExistsWithNameAsync::Database exception");
                return false;
            }
        }

        public async Task<IEnumerable<UserClaim>> ListAllUserClaimAsync()
        {
            try
            {
                return await Context.UserClaims.AsNoTracking().
                    Where(u => u.Status != "DELETED").ToListAsync();
            }
            catch (Exception error)
            {
                _logger.LogError(error, "ListAllUserClaimAsync::Database exception");
                return null;
            }
        }

        public async Task<List<UserClaimDto>> GetUserClaimByNameAsync(string attributesList)
        {
            try
            {
                var attributeNames = attributesList
                    .Split(' ', StringSplitOptions.RemoveEmptyEntries);

                var claims = await Context.UserClaims
                    .Where(x => attributeNames.Contains(x.Name))
                    .Select(x => new UserClaimDto
                    {
                        Name = x.Name,
                        DisplayName = x.DisplayName
                    })
                    .ToListAsync();

                return claims;
            }
            catch (Exception error)
            {
                _logger.LogError(error, "GetUserClaimByNameAsync::Database exception");
                return new List<UserClaimDto>();
            }
        }


    }
}