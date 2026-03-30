using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DTPortal.Core.Domain.Models;
using DTPortal.Core.Domain.Services;
using DTPortal.Core.Domain.Repositories;
using DTPortal.Core.Domain.Services.Communication;
using DTPortal.Core.Utilities;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using DTPortal.Core.Constants;
using Newtonsoft.Json.Linq;

namespace DTPortal.Core.Services
{
    public class PurposeService : IPurposeService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogClient _LogClient;
        private readonly ILogger<ClientService> _logger;
        public PurposeService(IUnitOfWork unitOfWork, ILogClient logClient, ILogger<ClientService> logger)
        {
            _unitOfWork = unitOfWork;
            _LogClient = logClient;
            _logger = logger;
        }
        public async Task<PurposeResponse> CreatePurposeAsync(Purpose purpose)
        {
            var isExist = await _unitOfWork.Purpose.IsPurposeExistsWithNameAsync(purpose.Name);
            if (true == isExist)
            {
                _logger.LogError("purpose with given name already exist");
                return null;
            }
            purpose.CreatedDate = DateTime.Now;
            purpose.ModifiedDate = DateTime.Now;
            purpose.Status = "ACTIVE";
            try
            {
                await _unitOfWork.Purpose.AddAsync(purpose);
                await _unitOfWork.SaveAsync();
                return new PurposeResponse(purpose, "purpose added successfully");
            }
            catch (Exception)
            {
                _logger.LogError("Failed adding purpose");
                return new PurposeResponse("Error occured adding purpose");
            }
        }


        public async Task<Purpose> GetPurposeAsync(int id)
        {
            var Purpose = await _unitOfWork.Purpose.GetPurposeById(id);
            if (Purpose == null)
            {
                return null;
            }
            return Purpose;
        }

        public async Task<int> GetPurposeIdByNameAsync(string name)
        {
            var purposeId = await _unitOfWork.Purpose.GetPurposeByNameAsync(name);
            if (purposeId == null)
            {
                return -1;
            }
            return purposeId.Id;
        }


        public async Task<PurposeResponse> UpdatePurposeAsync(Purpose purpose)
        {
            var PurposeInDb = await _unitOfWork.Purpose.GetPurposeById(purpose.Id);
            if (PurposeInDb == null)
            {
                return new PurposeResponse("Purpose not found");
            }
            if (purpose.Status == "DELETED")
            {
                return new PurposeResponse("Purpose is deleted");
            }
            var PurposeList = await _unitOfWork.Purpose.GetAllAsync();
            foreach (var item in PurposeList)
            {
                if (item.Id != purpose.Id)
                {
                    if (item.Name == purpose.Name)
                    {
                        _logger.LogError("Purpose Name already exist");
                        return new PurposeResponse("Purpose name already exist");
                    }
                }
            }
            PurposeInDb.Name = purpose.Name;
            PurposeInDb.DisplayName = purpose.DisplayName;
            PurposeInDb.UserConsentRequired = purpose.UserConsentRequired;
            PurposeInDb.ModifiedDate = DateTime.Now;
            PurposeInDb.UpdatedBy = purpose.UpdatedBy;
            try
            {
                _unitOfWork.Purpose.Update(PurposeInDb);
                await _unitOfWork.SaveAsync();
                return new PurposeResponse(PurposeInDb, "Purpose updated Successfully");
            }
            catch (Exception)
            {
                _logger.LogError("Failed updating the Purpose");
                return new PurposeResponse("Failed Updating the purpose");
            }

        }


        public async Task<IEnumerable<Purpose>> GetPurposeListAsync()
        {
            var PurposeList = await _unitOfWork.Purpose.ListAllPurposeAsync();
            return PurposeList;
        }

        public async Task<IEnumerable<string>> GetPurposesListAsync()
        {
            var list = await _unitOfWork.Purpose.ListAllPurposeAsync();
            var purposelist = new List<string>();
            foreach (var purpose in list)
            {
                purposelist.Add(purpose.Name);
            }
            return purposelist;
        }

        public async Task<PurposeResponse> DeletePurposeAsync(int id,string UUID)
        {
            var PurposeInDb=await _unitOfWork.Purpose.GetByIdAsync(id);
            if (PurposeInDb == null)
            {
                return new PurposeResponse("Purpose not found");
            }
            try
            {
                PurposeInDb.Status = "DELETED";
                PurposeInDb.ModifiedDate = DateTime.Now;
                PurposeInDb.UpdatedBy = UUID;
                _unitOfWork.Purpose.Update(PurposeInDb);
                await _unitOfWork.SaveAsync();
                return new PurposeResponse(PurposeInDb,"Purpose Deleted Successfully");
            }
            catch (Exception ex)
            {
                _logger.LogError("Failed to delete the Purpose : {0}", ex.Message);
                return new PurposeResponse("Error Occured while deleting the purpose");
            }
        }
    }
}
