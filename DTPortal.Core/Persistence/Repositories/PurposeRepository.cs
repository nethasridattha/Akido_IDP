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
    public class PurposeRepository : GenericRepository<Purpose, idp_dtplatformContext>, IPurposeRepository
    {
        private readonly ILogger _logger;
        public PurposeRepository(idp_dtplatformContext context, ILogger logger) :
            base(context, logger)
        {
            _logger = logger;
        }

        public async Task<bool> IsPurposeExistsWithNameAsync(string name)
        {
            try
            {
                return await Context.Purposes.AsNoTracking().AnyAsync(u => u.Name == name);
            }
            catch (Exception error)
            {
                _logger.LogError(error, "IsPurposeExistsWithNameAsync::Database exception");
                return false;
            }
        }

        public async Task<Purpose> GetPurposeByNameAsync(string name)
        {
            try
            {
                return await Context.Purposes.AsNoTracking().SingleOrDefaultAsync(u => u.Name.ToLower() == name.ToLower() && u.Status == "ACTIVE");
            }
            catch (Exception error)
            {
                _logger.LogError(error, "IsScopeExistsWithNameAsync::Database exception");
                return null;
            }
        }

        public async Task<IEnumerable<Purpose>> ListAllPurposeAsync()
        {
            try
            {
                return await Context.Purposes.Where(u => u.Status != "DELETED").OrderByDescending(p=> p.CreatedDate).ToListAsync();
            }
            catch (Exception error)
            {
                _logger.LogError(error, "ListAllPurposeAsync: :Database exception");
                return null;
            }
        }
        public async Task<Purpose> GetPurposeById(int id)
        {
            try
            {
                return await Context.Purposes.SingleOrDefaultAsync(u => u.Id == id);
            }
            catch (Exception error)
            {
                _logger.LogError(error, "GetpurposeAsync DatabaseException");
                return null;
            }
        }
    }
}
