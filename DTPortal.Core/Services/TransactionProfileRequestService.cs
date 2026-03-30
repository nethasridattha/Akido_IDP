using DTPortal.Core.Domain.Models;
using DTPortal.Core.Domain.Repositories;
using DTPortal.Core.Domain.Services;
using DTPortal.Core.Domain.Services.Communication;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTPortal.Core.Services
{
    public class TransactionProfileRequestService : ITransactionProfileRequestService
    {
        private readonly ILogger _logger;
        private readonly IUnitOfWork _unitOfWork;
        public TransactionProfileRequestService(ILogger logger, IUnitOfWork unitOfWork)
        {
            _logger = logger;
            _unitOfWork = unitOfWork;
        }
        public async Task<TransactionProfileRequestResponse> AddProfileRequest(TransactionProfileRequest transactionProfileRequest)
        {
            try
            {
                await _unitOfWork.TransactionProfileRequests.AddAsync(transactionProfileRequest);
                await _unitOfWork.SaveAsync();
                _logger.LogInformation("<---AddProfileRequest Success");
                return new TransactionProfileRequestResponse(transactionProfileRequest, "Succcessfully added");
            }
            catch (Exception error)
            {
                _logger.LogError(error, "AddProfileRequest::Database exception");
                return new TransactionProfileRequestResponse("Failed to add");
            }
        }
        public async Task<IEnumerable<TransactionProfileRequest>> TransactionProfileRequestList()
        {
            try
            {
                return await _unitOfWork.TransactionProfileRequests.GetAllAsync();
            }
            catch (Exception error)
            {
                _logger.LogError(error, "TransactionProfileRequestList::Database exception");
                return null;
            }
        }

        public async Task<(IEnumerable<TransactionProfileRequest> Requests, int TotalCount)> TransactionProfileRequestLists(
            DateTime? startDate = null,
            DateTime? endDate = null,
            string userId = null,
            string applicationName = null,
            int page = 1,
            int perPage = 10)
        {
            try
            {
                var allRequests = await _unitOfWork.TransactionProfileRequests.GetAllAsync();

                var filteredRequests = allRequests
                    .Where(x => (!startDate.HasValue || x.CreatedDate >= startDate.Value) &&
                                (!endDate.HasValue || x.CreatedDate <= endDate.Value) &&
                                (string.IsNullOrEmpty(userId) || x.TransactionId == userId) &&
                                (string.IsNullOrEmpty(applicationName) ||
                                 x.Client != null && x.Client.ApplicationName.Contains(applicationName)))
                    .ToList();

                var totalCount = filteredRequests.Count;

                var paginatedData = filteredRequests
                    .Skip((page - 1) * perPage)
                    .Take(perPage)
                    .ToList();

                return (paginatedData, totalCount);
            }
            catch (Exception error)
            {
                _logger.LogError(error, "TransactionProfileRequestList::Database exception");
                return (null, 0);
            }
        }

        public async Task<IEnumerable<TransactionProfileRequest>> TransactionProfileRequestData(
            string startDate, string endDate, string userId = null, int clientId = 0, string profileType = null, int page = 1, int perPage = 10)
        {
            try
            {
                return await _unitOfWork.TransactionProfileRequests.GetConditionedList(startDate, endDate, userId, clientId, profileType, page, perPage);
            }
            catch (Exception error)
            {
                _logger.LogError(error, "TransactionProfileRequestList::Database exception");
                return null;
            }
        }



        public async Task<TransactionProfileRequest> GetTransactionProfileRequest(int Id)
        {
            try
            {
                return await _unitOfWork.TransactionProfileRequests.GetByIdAsync(Id);
            }
            catch (Exception error)
            {
                _logger.LogError(error, "GetTransactionProfileRequest::Database exception");
                return null;
            }
        }
        public async Task<int> GetIdByTransactionId(string TransactionId)
        {
            try
            {
                return await _unitOfWork.TransactionProfileRequests.GetIdByTransactionId(TransactionId);
            }
            catch (Exception error)
            {
                _logger.LogError(error, "GetIdByTransactionId::Database exception");
                return -1;
            }
        }
        public async Task<IEnumerable<TransactionProfileRequest>> TransactionProfileRequestListByOrgId(string orgId)
        {
            try
            {
                return await _unitOfWork.TransactionProfileRequests.GetListByOrgIdAsync(orgId);
            }
            catch (Exception error)
            {
                _logger.LogError(error, "TransactionProfileRequestList::Database exception");
                return null;
            }
        }
    }
}
