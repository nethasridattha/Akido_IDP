using DTPortal.Core.Domain.Models;
using DTPortal.Core.Domain.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTPortal.Core.Persistence.Repositories
{
    public class TransactionProfileRequestsRepository : GenericRepository<TransactionProfileRequest, idp_dtplatformContext>, ITransactionProfileRequestsRepository
    {
        private readonly ILogger _logger;
        public TransactionProfileRequestsRepository(idp_dtplatformContext context, ILogger logger) : base(context, logger)
        {
            _logger= logger;
        }
        public async Task<TransactionProfileRequest> GetByTransactionId(string transactionId)
        {
            try
            {
                return await Context.TransactionProfileRequests.SingleOrDefaultAsync(x => x.TransactionId == transactionId);
            }
            catch(Exception ex)
            {
                _logger.LogError(ex, "GetByTransactionId:: Database Exception");
                return null;
            }
        }

        public async Task<int> GetIdByTransactionId(string transactionId)
        {
            try
            {
                return await Context.TransactionProfileRequests.Where(p => p.TransactionId == transactionId).Select(p => p.Id).SingleOrDefaultAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "GetByTransactionId:: Database Exception");
                return 0;
            }
        }
        public async Task<List<TransactionProfileRequest>> GetList()
        {
            try
            {
                return await Context.TransactionProfileRequests.OrderByDescending(x => x.Id).ToListAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "GetList:: Database Exception");
                return null;
            }
        }

        public async Task<IEnumerable<TransactionProfileRequest>> GetConditionedList(
    string startDate,
    string endDate,
    string userId = null,
    int clientId = 0,
    string profileType = null,
    int page = 1,
    int perPage = 10)
        {
            try
            {
                // Ensure page and perPage are valid
                if (page < 1) page = 1; // Default to first page
                if (perPage < 1) perPage = 10; // Default to 10 items per page

                var startDateTime = DateTime.Parse(startDate);
                var endDateTime = DateTime.Parse(endDate);

                var query = Context.TransactionProfileRequests
                    .Where(log => log.CreatedDate >= startDateTime && log.CreatedDate <= endDateTime);

                if (!string.IsNullOrEmpty(userId))
                {
                    query = query.Where(log => log.Suid == userId);
                }

                if (clientId > 0)
                {
                    query = query.Where(log => log.ClientId == clientId);
                }

                if (!string.IsNullOrEmpty(profileType))
                {
                    query = query.Where(log => EF.Functions.Like(log.RequestDetails, $"%\"ProfileType\":\"{profileType}\"%"));
                }

                //return await query
                //    .OrderByDescending(log => log.CreatedDate)
                //    .ToListAsync();

                return await query
                    .OrderByDescending(log => log.CreatedDate)
                    .Skip((page - 1) * perPage)
                    .Take(perPage)
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                // Handle exceptions gracefully
                _logger.LogError(ex, "GetConditionedList:: Database Exception");
                return Enumerable.Empty<TransactionProfileRequest>();
            }
        }


        public int GetConditionedListCount(
    string startDate,
    string endDate,
    string userId = null,
    int clientId = 0,
    string profileType = null,
    int page = 1,
    int perPage = 10)
        {
            try
            {
                // Ensure page and perPage are valid
                if (page < 1) page = 1; // Default to first page
                if (perPage < 1) perPage = 10; // Default to 10 items per page

                var startDateTime = DateTime.Parse(startDate);
                var endDateTime = DateTime.Parse(endDate);

                var query = Context.TransactionProfileRequests
                    .Where(log => log.CreatedDate >= startDateTime && log.CreatedDate <= endDateTime);

                if (!string.IsNullOrEmpty(userId))
                {
                    query = query.Where(log => log.Suid == userId);
                }

                if (clientId > 0)
                {
                    query = query.Where(log => log.ClientId == clientId);
                }

                if (!string.IsNullOrEmpty(profileType))
                {
                    query = query.Where(log => EF.Functions.Like(log.RequestDetails, $"%\"ProfileType\":\"{profileType}\"%"));
                }

                //return await query
                //    .OrderByDescending(log => log.CreatedDate)
                //    .ToListAsync();

                return query.Count();
            }
            catch (Exception ex)
            {
                // Handle exceptions gracefully
                _logger.LogError(ex, "GetConditionedListCount:: Database Exception");
                return 0;
            }
        }


        public async Task<List<TransactionProfileRequest>> GetListByOrgIdAsync(string orgId)
        {
            try
            {
                return await Context.TransactionProfileRequests
                    .Include(x => x.Client)
                    .Where(x => x.Client != null && x.Client.OrganizationUid != null && x.Client.OrganizationUid == orgId)
                    .OrderByDescending(x => x.Id)
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "GetListByOrgIdAsync:: Database Exception");
                return null;
            }
        }
    }
}
