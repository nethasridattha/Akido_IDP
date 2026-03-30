using DTPortal.Core.Domain.Models;
using DTPortal.Core.Domain.Repositories;
using Microsoft.Extensions.Logging;

namespace DTPortal.Core.Persistence.Repositories
{
    public class SMTPRepository : GenericRepository<Smtp, idp_dtplatformContext>,
        ISMTPRepository
    {
        public SMTPRepository(idp_dtplatformContext context, ILogger logger) : base(context, logger)
        {
        }
    }
}
