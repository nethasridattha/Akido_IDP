using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DTPortal.Core.Domain.Models;
using DTPortal.Core.Domain.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace DTPortal.Core.Persistence.Repositories
{
    public class ScopeRepository : GenericRepository<Scope, idp_dtplatformContext>,IScopeRepository
    {
        private readonly ILogger _logger;
        public ScopeRepository(idp_dtplatformContext context, ILogger logger) :
            base(context, logger)
        {
            _logger = logger;
        }

        public async Task<bool> IsScopeExistsWithNameAsync(string name)
        {
            try
            {
                return await Context.Scopes.AsNoTracking().AnyAsync(u => u.Name == name);
            }
            catch (Exception error)
            {
                _logger.LogError(error, "IsScopeExistsWithNameAsync::Database exception");
                return false;
            }
        }
        public async Task<Scope> GetScopeByNameAsync(string name)
        {
            try
            {
                return await Context.Scopes.AsNoTracking().SingleOrDefaultAsync(u => u.Name.ToLower() == name.ToLower() && u.Status=="ACTIVE");
            }
            catch (Exception error)
            {
                _logger.LogError(error, "IsScopeExistsWithNameAsync::Database exception");
                return null;
            }
        }

        public async Task<IEnumerable<Scope>> ListAllScopeAsync()
        {
            try
            {
                return await Context.Scopes
                    .Where(u => u.Status != "DELETED").OrderByDescending(s=> s.CreatedDate).ToListAsync();
            }
            catch (Exception error)
            {
                _logger.LogError(error, "ListAllScopeAsync::Database exception");
                return null;
            }
        }

        public async Task<Scope> GetScopeByIdWithClaims(int id)
        {
            try
            {
                return await Context.Scopes
                    .SingleOrDefaultAsync(x => x.Id == id);
            }
            catch (Exception error)
            {
                _logger.LogError(error, "GetRoleByRoleIdWithActivities::Database exception");
                return null;
            }
        }

        public async Task<string[]> GetScopesNamesAsync(string Value)
        {
            try
            {
                return await Context.Scopes
                    .Where(s => s.Name.Contains(Value))
                    .OrderByDescending(s => s.CreatedDate)
                    .Select(s => s.Name)
                    .ToArrayAsync();
            }
            catch (Exception error)
            {
                _logger.LogError(error, "ListAllScopeAsync::Database exception");
                return null;
            }
        }
    }
}