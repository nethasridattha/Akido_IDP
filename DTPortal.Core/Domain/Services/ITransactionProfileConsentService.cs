using DTPortal.Core.Domain.Models;
using DTPortal.Core.Domain.Services.Communication;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTPortal.Core.Domain.Services
{
    public interface ITransactionProfileConsentService
    {
        public Task<TransactionProfileConsentResponse> AddProfileConsent(TransactionProfileConsent transactionProfileConsent);

        public Task<IEnumerable<TransactionProfileConsent>> TransactionProfileConsentList();

        public Task<TransactionProfileConsent> GetTransactionProfileConsent(int Id);

        public Task<TransactionProfileConsent> GetTransactionProfileConsentbyTransactionId(int Id);

        public  Task<TransactionProfileConsentResponse> UpdateTransactionProfileConsent(TransactionProfileConsent transactionProfileConsent);
        public Task<TransactionProfileConsentResponse> UpdateTransactionConsent(TransactionProfileConsent transactionProfileConsent);
    }
}
