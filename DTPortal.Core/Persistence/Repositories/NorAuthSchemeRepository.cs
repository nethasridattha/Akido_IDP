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
    public class NorAuthSchemeRepository : GenericRepository<NorAuthScheme, idp_dtplatformContext>,
            INorAuthSchemeRepository
    {
        private readonly ILogger _logger;

        public NorAuthSchemeRepository(idp_dtplatformContext context,
            ILogger logger) : base(context, logger)
        {
            _logger = logger;
        }

        public async Task<IEnumerable<NorAuthScheme>> GetPrimaryAuthSchemeIds(int authSchemeId)
        {
            return await Context.NorAuthSchemes.Where(nor => nor.AuthSchId == authSchemeId).AsNoTracking().ToListAsync();

        }

        public async Task<IEnumerable<NorAuthScheme>>  GetNorAuthSchmIDsbyAuthSchm(int AuthSchmId)
        {
            return await Context.NorAuthSchemes.Where(nor => nor.AuthSchId == AuthSchmId).AsNoTracking().ToListAsync();
        }

        public async Task DeleteNorAuthSchmIDsbyAuthSchm(int AuthSchmId)
        {
            try
            {
                var recordsToDelete = await Context.NorAuthSchemes
                                               .Where(nor => nor.AuthSchId == AuthSchmId)
                                               .ToListAsync();

                if (recordsToDelete.Any())
                {
                    Context.NorAuthSchemes.RemoveRange(recordsToDelete);
                    await Context.SaveChangesAsync();
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "DeleteNorAuthSchmIDsbyAuthSchm::Database exception");
            }
        }
    }
}
