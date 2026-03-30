using DTPortal.Core.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTPortal.Core.Domain.Repositories
{
    public interface ITransactionProfileConsentRepository: IGenericRepository<TransactionProfileConsent>
    {
        public Task<TransactionProfileConsent> GetByTransactionId(int transactionId);
    }
}
