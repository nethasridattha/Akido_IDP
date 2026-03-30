using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DTPortal.Core.Domain.Lookups;
using DTPortal.Core.Domain.Models;
using DTPortal.Core.Domain.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace DTPortal.Core.Persistence.Repositories
{
    public class AuthSchemeRepository : GenericRepository<AuthScheme, idp_dtplatformContext>,
            IAuthSchemeRepository
    {
        private readonly ILogger _logger;
        public AuthSchemeRepository(idp_dtplatformContext context,
            ILogger logger) : base(context, logger)
        {
            _logger = logger;
        }

        public async Task<IEnumerable<AuthScheme>> ListAuthSchemesAsync()
        {
            try
            {
                return await Context.AuthSchemes.AsNoTracking().Where(u => u.Status != "DELETED").ToListAsync();
            }
            catch (Exception error)
            {
                _logger.LogError(error, "ListOAuth2ClientAsync::Database exception");
                return null;
            }
        }

        public async Task<AuthScheme> GetAuthSchemeByNameAsync(string AuthSchemeName)
        {

            return await Context.AuthSchemes.AsNoTracking().SingleOrDefaultAsync(u => u.Name == AuthSchemeName);
        }

        public async Task<IEnumerable<AuthSchemesLookupItem>> GetAuthSchemeLookupItemsAsync()
        {
            return await Context.Roles
                .Where(x => x.Status != "Deleted")
                .Select(x =>
               new AuthSchemesLookupItem
               {
                   Id = x.Id,
                   DisplayName = x.DisplayName
               }).AsNoTracking().ToListAsync();
        }
    }
}
