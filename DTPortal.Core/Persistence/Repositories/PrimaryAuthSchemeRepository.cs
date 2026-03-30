using DTPortal.Core.Domain.Models;
using DTPortal.Core.Domain.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTPortal.Core.Persistence.Repositories
{
    public class PrimaryAuthSchemeRepository : GenericRepository<PrimaryAuthScheme, idp_dtplatformContext>,
            IPrimaryAuthSchemeRepository
    {
        private readonly ILogger _logger;

        public PrimaryAuthSchemeRepository(idp_dtplatformContext context,
            ILogger logger) : base(context, logger)
        {
            _logger = logger;
        }

        public async Task<PrimaryAuthScheme> GetPrimaryAuthSchemeByPrimaryAuthSchemeAsync(string PrimaryAuthSchemeName)
        {
            return await Context.PrimaryAuthSchemes.AsNoTracking().SingleOrDefaultAsync(p=>p.Name == PrimaryAuthSchemeName);
        }

        public async Task<PrimaryAuthScheme> GetPrimaryAuthSchemeByIdAsync(int PrimaryAuthSchemeId)
        {
            return await Context.PrimaryAuthSchemes.AsNoTracking().SingleOrDefaultAsync(p => p.Id == PrimaryAuthSchemeId);
        }

        public async Task<IList<string>> GetPrimaryAuthSchmsbyAuthSchmName(string AuthSchmName)
        {
            var query = await Context.AuthSchemes
                .AsNoTracking()
                .SingleOrDefaultAsync(pri => pri.Name == AuthSchmName);

            var authschems = await Context.NorAuthSchemes
                .Where(aa => aa.AuthSchId == query.Id)
                .ToListAsync();

            var authSchmList = new List<string>();

            foreach (var item in authschems)
            {
                var primAuthSchm = await Context.PrimaryAuthSchemes
                    .AsNoTracking()
                    .SingleOrDefaultAsync(pr => pr.Id == item.PriAuthSchId);

                authSchmList.Add(primAuthSchm.Name);
            }

            return authSchmList;
        }

        public async Task<bool> IsPrimaryAuthSchemeExists(PrimaryAuthScheme primaryAuthScheme)
        {
            return await Context.PrimaryAuthSchemes
                .AsNoTracking().AnyAsync(u => u.Id == primaryAuthScheme.Id || u.Name == primaryAuthScheme.Name);
        }
    }
}
