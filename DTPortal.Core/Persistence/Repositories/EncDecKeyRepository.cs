using DTPortal.Core.Domain.Models;
using DTPortal.Core.Domain.Repositories;
using Microsoft.Extensions.Logging;

namespace DTPortal.Core.Persistence.Repositories
{
    public class EncDecKeyRepository : GenericRepository<EncDecKey, idp_dtplatformContext>,
            IEncDecKeyRepository
    {
        public EncDecKeyRepository(idp_dtplatformContext context, ILogger logger) : base(context, logger)
        {
        }
    }
}
