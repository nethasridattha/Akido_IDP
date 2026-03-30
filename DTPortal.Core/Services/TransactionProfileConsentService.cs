using DTPortal.Core.Domain.Models;
using DTPortal.Core.Domain.Repositories;
using DTPortal.Core.Domain.Services;
using DTPortal.Core.Domain.Services.Communication;
using Microsoft.Extensions.Logging;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTPortal.Core.Services
{
    public class TransactionProfileConsentService : ITransactionProfileConsentService
    {
        private readonly ILogger _logger;
        private readonly IUnitOfWork _unitOfWork;
        public TransactionProfileConsentService(ILogger logger,IUnitOfWork unitOfWork) 
        {
            _logger = logger;
            _unitOfWork = unitOfWork;
        }
        public async Task<TransactionProfileConsentResponse> AddProfileConsent(TransactionProfileConsent transactionProfileConsent)
        {
            try
            {
                await _unitOfWork.TransactionProfileConsent.AddAsync(transactionProfileConsent);
                await _unitOfWork.SaveAsync();
                _logger.LogInformation("<---AddProfileConsent");
                return new TransactionProfileConsentResponse(transactionProfileConsent, "Successfully added");
            }
            catch (Exception)
            {
                _logger.LogError("Failed to add Profile Consent");
                return new TransactionProfileConsentResponse("failed to add");
            }
        }
        public async Task<IEnumerable<TransactionProfileConsent>> TransactionProfileConsentList()
        {
            try
            {
                return await _unitOfWork.TransactionProfileConsent.GetAllAsync();
            }
            catch (Exception)
            {
                _logger.LogError("failed to get list");
                return null;
            }
        }
        public async Task<TransactionProfileConsent> GetTransactionProfileConsent(int Id)
        {
            try
            {
                return await _unitOfWork.TransactionProfileConsent.GetByIdAsync(Id);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return null;
            }
        }
        public async Task<TransactionProfileConsentResponse> UpdateTransactionProfileConsent(TransactionProfileConsent transactionProfileConsent)
        {
            try
            {
                var transactionProfileConsentinDb = await GetTransactionProfileConsentbyTransactionId((int)transactionProfileConsent.TransactionId);

                if(transactionProfileConsentinDb == null)
                {
                    _logger.LogError("Failed to update consent");
                    return new TransactionProfileConsentResponse("Failed to update Consent");
                }

                transactionProfileConsentinDb.ApprovedProfileAttributes = transactionProfileConsent.ApprovedProfileAttributes;

                transactionProfileConsentinDb.UpdatedDate = DateTime.Now;

                _unitOfWork.TransactionProfileConsent.Update(transactionProfileConsentinDb);

                await _unitOfWork.SaveAsync();
                _logger.LogInformation("<---UpdateTransactionProfileConsent");
                return new TransactionProfileConsentResponse(transactionProfileConsent, "Successfully added");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return new TransactionProfileConsentResponse("Failed to update");
            }
        }

        public async Task<TransactionProfileConsentResponse> UpdateTransactionConsent(TransactionProfileConsent transactionProfileConsent)
        {
            try
            {
                var transactionProfileConsentinDb = await GetTransactionProfileConsentbyTransactionId((int)transactionProfileConsent.TransactionId);

                if (transactionProfileConsentinDb == null)
                {
                    _logger.LogError("Failed to update consent");
                    return new TransactionProfileConsentResponse("Failed to update Consent");
                }

                transactionProfileConsentinDb.ConsentStatus = transactionProfileConsent.ConsentStatus;

                transactionProfileConsentinDb.UpdatedDate = DateTime.Now;

                _unitOfWork.TransactionProfileConsent.Update(transactionProfileConsentinDb);

                await _unitOfWork.SaveAsync();
                _logger.LogInformation("<---UpdateTransactionProfileConsent");
                return new TransactionProfileConsentResponse(transactionProfileConsent, "Successfully added");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return new TransactionProfileConsentResponse("Failed to update");
            }
        }
        public async Task<TransactionProfileConsent> GetTransactionProfileConsentbyTransactionId(int Id)
        {
            try
            {
                return await _unitOfWork.TransactionProfileConsent.GetByTransactionId(Id);
            }
            catch (Exception error)
            {
                _logger.LogError(error.Message);
                return null;
            }
        }
    }
}
