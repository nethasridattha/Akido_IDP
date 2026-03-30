using DTPortal.Core.Domain.Models;
using DTPortal.Core.Domain.Services.Communication;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTPortal.Core.Domain.Services
{
    public interface ITransactionProfileRequestService
    {
        public Task<TransactionProfileRequestResponse> AddProfileRequest(TransactionProfileRequest transactionProfileRequest);

        public Task<IEnumerable<TransactionProfileRequest>> TransactionProfileRequestList();

        public Task<TransactionProfileRequest> GetTransactionProfileRequest(int Id);
        public Task<int> GetIdByTransactionId(string TransactionId);
        public Task<IEnumerable<TransactionProfileRequest>> TransactionProfileRequestListByOrgId(string orgId);
        public Task<(IEnumerable<TransactionProfileRequest> Requests, int TotalCount)> TransactionProfileRequestLists(
    DateTime? startDate = null,
    DateTime? endDate = null,
    string userId = null,
    string applicationName = null,
    int page = 1,
    int perPage = 10);

        public Task<IEnumerable<TransactionProfileRequest>> TransactionProfileRequestData(
            string startDate, string endDate, string userId = null, int clientId = 0, string profileType = null, int page = 1, int perPage = 10);

    }
}
