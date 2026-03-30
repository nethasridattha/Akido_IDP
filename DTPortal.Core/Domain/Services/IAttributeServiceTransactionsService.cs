using DTPortal.Core.Domain.Services.Communication;
using DTPortal.Core.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTPortal.Core.Domain.Services
{
    public interface IAttributeServiceTransactionsService
    {
        public Task<IEnumerable<AttributeServiceTransactionListDTO>> GetAttributeServiceTransactionsList();
        public Task<AttributeServiceTransactionsDTO> GetDetails(int Id);
        public Task<IEnumerable<AttributeServiceTransactionListDTO>> GetAttributeServiceTransactionsListByOrgId(string OrgId);
        public Task<PaginatedList<AttributeServiceTransactionListDTO>> GetAttributeLogReportAsync(string startDate, string endDate, string userId, string userIdType, string applicationName, string profile, int perPage , int page = 1);
    }
}
