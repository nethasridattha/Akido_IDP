using DTPortal.Core.Domain.Models;
using DTPortal.Core.Domain.Services.Communication;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTPortal.Core.Domain.Services
{
    public interface ITransactionProfileStatusService
    {
        public Task<TransactionProfileStatusResponse> AddProfileStatus(TransactionProfileStatus transactionProfileStatus);

        public Task<IEnumerable<TransactionProfileStatus>> TransactionProfileStatusList();

        public Task<TransactionProfileStatus> GetTransactionProfileStatus(int Id);

        public Task<TransactionProfileRequest> GetTransactionProfileStatusbyTransactionId(int Id);
        public Task<TransactionProfileStatusResponse> UpdateProfileStatus(TransactionProfileStatus transactionProfileStatus);
    }
}
