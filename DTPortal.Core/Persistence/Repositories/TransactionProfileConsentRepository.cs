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
    public class TransactionProfileConsentRepository: GenericRepository<TransactionProfileConsent, idp_dtplatformContext>, ITransactionProfileConsentRepository
    {
        private readonly ILogger _logger;
        public TransactionProfileConsentRepository(idp_dtplatformContext context, ILogger logger) : base(context, logger)
        {
            _logger = logger;
        }
        public async Task<TransactionProfileConsent> GetByTransactionId(int transactionId)
        {
            try
            {
                return await Context.TransactionProfileConsents.SingleOrDefaultAsync(x => x.TransactionId == transactionId);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "GetByTransactionId:: Database Exception");
                return null;
            }
        }
    }
}
