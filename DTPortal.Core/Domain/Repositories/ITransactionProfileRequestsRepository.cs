using DTPortal.Core.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTPortal.Core.Domain.Repositories
{
    public interface ITransactionProfileRequestsRepository : IGenericRepository<TransactionProfileRequest>
    {
        public Task<int> GetIdByTransactionId(string transactionId);
        public Task<TransactionProfileRequest> GetByTransactionId(string transactionId);
        public Task<List<TransactionProfileRequest>> GetListByOrgIdAsync(string orgId);
        public Task<IEnumerable<TransactionProfileRequest>> GetConditionedList(string startDate, string endDate, string userId = null, int clientId = 0 , string profileType = null, int page = 1, int perPage = 10);

        public int GetConditionedListCount(
    string startDate,
    string endDate,
    string userId = null,
    int clientId = 0,
    string profileType = null,
    int page = 1,
    int perPage = 10);
    }
}
