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
using System.Transactions;

namespace DTPortal.Core.Services
{
    public class TransactionProfileStatusService : ITransactionProfileStatusService
    {
        private readonly ILogger _logger;
        private readonly IUnitOfWork _unitOfWork;
        public TransactionProfileStatusService(ILogger logger, IUnitOfWork unitOfWork)
        {
            _logger = logger;
            _unitOfWork = unitOfWork;
        }

        public async Task<TransactionProfileStatusResponse> AddProfileStatus(TransactionProfileStatus transactionProfileStatus)
        {
            try
            {
                var response = await UpdateProfileStatus(transactionProfileStatus);
                return response;
            }
            catch (Exception error)
            {
                _logger.LogError(error, "AddProfileStatus::Database exception");

                return new TransactionProfileStatusResponse("Failed to add");
            }
        }
        public async Task<IEnumerable<TransactionProfileStatus>> TransactionProfileStatusList()
        {
            try
            {
                return await _unitOfWork.TransactionProfileStatus.GetAllAsync();
            }
            catch (Exception error)
            {
                _logger.LogError(error, "TransactionProfileStatusList::Database exception");
                return null;
            }
        }
        public async Task<TransactionProfileStatus> GetTransactionProfileStatus(int Id)
        {
            try
            {
                return await _unitOfWork.TransactionProfileStatus.GetByIdAsync(Id);
            }
            catch (Exception error)
            {
                _logger.LogError(error, "GetTransactionProfileStatus::Database exception");
                return null;
            }
        }
        public async Task<TransactionProfileRequest> GetTransactionProfileStatusbyTransactionId(int Id)
        {
            try
            {
                return await _unitOfWork.TransactionProfileStatus.GetByTransactionId(Id);
            }
            catch (Exception error)
            {
                _logger.LogError(error, "GetTransactionProfileStatusbyTransactionId::Database exception");
                return null;
            }
        }
        public async Task<TransactionProfileStatusResponse> UpdateProfileStatus(TransactionProfileStatus transactionProfileStatus)
        {
            try
            {
                var transactionProfileStatusinDb = await GetTransactionProfileStatusbyTransactionId((int)transactionProfileStatus.TransactionId);

                if (transactionProfileStatusinDb == null)
                {
                    _logger.LogError("Failed to update Status");
                    return new TransactionProfileStatusResponse("Failed to update Status");
                }
                transactionProfileStatusinDb.UpdatedDate = DateTime.Now;
                transactionProfileStatusinDb.TransactionStatus = transactionProfileStatus.TransactionStatus;
                transactionProfileStatusinDb.FailedReason=transactionProfileStatus.FailedReason;
                _unitOfWork.TransactionProfileRequests.Update(transactionProfileStatusinDb);

                await _unitOfWork.SaveAsync();
                _logger.LogInformation("<---UpdateTransactionProfileStatus");
                return new TransactionProfileStatusResponse(transactionProfileStatusinDb, "Successfully added");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return new TransactionProfileStatusResponse("Failed to update");
            }
        }
    }
}
