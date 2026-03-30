using System.Threading.Tasks;
using DTPortal.Core.Domain.Models;
using DTPortal.Core.Domain.Services;
using DTPortal.Core.Domain.Repositories;
using DTPortal.Core.Domain.Services.Communication;
using System;
using System.Collections.Generic;
using Microsoft.Extensions.Logging;

namespace DTPortal.Core.Services
{
    public class PrimaryAuthSchemeService : IPrimaryAuthSchemeService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<PrimaryAuthSchemeService> _logger;

        public PrimaryAuthSchemeService(IUnitOfWork unitOfWork,
             ILogger<PrimaryAuthSchemeService> logger)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
        }

        public async Task<PrimaryAuthScheme> GetPrimaryAuthSchemeAsync(string primaryAuthSchemeName)
        {
            return await _unitOfWork.PrimaryAuthScheme.GetPrimaryAuthSchemeByPrimaryAuthSchemeAsync(primaryAuthSchemeName);
        }

        public async Task<PrimaryAuthSchemeResponse> CreatePrimaryAuthSchemeAsync(PrimaryAuthScheme primaryAuthScheme, int supportsProivisioning)
        {

            await _unitOfWork.PrimaryAuthScheme.AddAsync(primaryAuthScheme);
            try
            {
                await _unitOfWork.SaveAsync();
            }
            catch
            {
                return new PrimaryAuthSchemeResponse("An error occurred while creating the primary authentication scheme. Please contact the admin.");
            }

            var primaryAuthSchemeinDb = await _unitOfWork.PrimaryAuthScheme.GetPrimaryAuthSchemeByPrimaryAuthSchemeAsync(primaryAuthScheme.Name);
            if(null == primaryAuthSchemeinDb)
            {
                return new PrimaryAuthSchemeResponse("An error occurred while creating the primary authentication scheme. Please contact the admin.");
            }

            var authScheme = new AuthScheme
            {
                Name = primaryAuthScheme.Name,
                IsPrimaryAuthscheme = true,
                Guid = primaryAuthScheme.Guid,
                Description = primaryAuthScheme.Description,
                CreatedDate = DateTime.Now,
                ModifiedDate = DateTime.Now,
                PriAuthSchCnt = 1,
                CreatedBy = "system",
                UpdatedBy = "system",
                Hash = "not available",
                DisplayName = primaryAuthScheme.DisplayName,
                Status = "ACTIVE",
                SupportsProvisioning = (int)supportsProivisioning
                //BlockedReason = ""
            };
            
            await _unitOfWork.AuthScheme.AddAsync(authScheme);
            try
            {
                await _unitOfWork.SaveAsync();
            }
            catch
            {
                return new PrimaryAuthSchemeResponse("An error occurred while creating the primary authentication scheme. Please contact the admin.");
            }

            var authSchemeinDb = await _unitOfWork.AuthScheme.GetAuthSchemeByNameAsync(primaryAuthScheme.Name);
            if (null == authSchemeinDb)
            {
                return new PrimaryAuthSchemeResponse("An error occurred while creating the primary authentication scheme. Please contact the admin.");
            }

            try
            {
                await _unitOfWork.SaveAsync();
            }
            catch
            {
                return new PrimaryAuthSchemeResponse("An error occurred while creating the primary authentication scheme. Please contact the admin.");
            }

            var norAuthScheme = new NorAuthScheme
            {
                PriAuthSchId = primaryAuthSchemeinDb.Id,
                AuthSchId = authSchemeinDb.Id
            };
            await _unitOfWork.NorAuthScheme.AddAsync(norAuthScheme);

            try
            {
                await _unitOfWork.SaveAsync();
                return new PrimaryAuthSchemeResponse(primaryAuthScheme, "Primary authentication scheme created successfully");
            }
            catch(Exception)
            {
                return new PrimaryAuthSchemeResponse("An error occurred while creating the primary authentication scheme. Please contact the admin.");
            }

        }

        public async Task<PrimaryAuthScheme> GetPrimaryAuthSchemeByIdAsync(int id)
        {
            return await _unitOfWork.PrimaryAuthScheme.GetByIdAsync(id);
        }

        public async Task<PrimaryAuthSchemeResponse> UpdatePrimaryAuthSchemeAsync(PrimaryAuthScheme primaryAuthScheme, int supportsProivisioning)
        {

            var isExists = await _unitOfWork.PrimaryAuthScheme.IsPrimaryAuthSchemeExists(primaryAuthScheme);
            if(false == isExists)
            {
                return new PrimaryAuthSchemeResponse("Primary authentication scheme not found. Please contact the admin.");
            }

            var primaryAuthSchemeInDb = await _unitOfWork.PrimaryAuthScheme.GetByIdAsync(primaryAuthScheme.Id);
            if(null == primaryAuthSchemeInDb)
            {
                return new PrimaryAuthSchemeResponse("Primary authentication scheme not found. Please contact the admin.");
            }

            primaryAuthSchemeInDb.Id = primaryAuthScheme.Id;
            primaryAuthSchemeInDb.Name = primaryAuthScheme.Name;
            primaryAuthSchemeInDb.Description = primaryAuthScheme.Description;
            primaryAuthSchemeInDb.DisplayName = primaryAuthScheme.DisplayName;
            primaryAuthSchemeInDb.ModifiedDate = DateTime.Now;
            primaryAuthSchemeInDb.UpdatedBy = primaryAuthScheme.UpdatedBy;
            primaryAuthSchemeInDb.ClientVerify = primaryAuthScheme.ClientVerify;
            primaryAuthSchemeInDb.StrngMatch = primaryAuthScheme.StrngMatch;
            primaryAuthSchemeInDb.RandPresent = primaryAuthScheme.RandPresent;

            _unitOfWork.PrimaryAuthScheme.Update(primaryAuthSchemeInDb);
            try
            {
                await _unitOfWork.SaveAsync();
            }
            catch (Exception)
            {
                return new PrimaryAuthSchemeResponse("An error occurred while creating the primary authentication scheme. Please contact the admin.");
            }

            var authSchemeInDb = await _unitOfWork.AuthScheme.GetAuthSchemeByNameAsync(primaryAuthScheme.Name);

            authSchemeInDb.Name = primaryAuthScheme.Name;
            authSchemeInDb.IsPrimaryAuthscheme = true;
            authSchemeInDb.Description = primaryAuthScheme.Description;
            authSchemeInDb.CreatedDate = DateTime.Now;
            authSchemeInDb.ModifiedDate = DateTime.Now;
            authSchemeInDb.PriAuthSchCnt = 1;
            authSchemeInDb.CreatedBy = "system";
            authSchemeInDb.UpdatedBy = "system";
            authSchemeInDb.Hash = "not available";
            authSchemeInDb.DisplayName = primaryAuthScheme.DisplayName;
            authSchemeInDb.Status = "ACTIVE";
            authSchemeInDb.SupportsProvisioning = supportsProivisioning;
            //authSchemeInDb.BlockedReason = "";

            _unitOfWork.AuthScheme.Update(authSchemeInDb);

            try
            {
                await _unitOfWork.SaveAsync();
                return new PrimaryAuthSchemeResponse(primaryAuthScheme, "Primary authentication scheme updated successfully");
            }
            catch (Exception)
            {
                return new PrimaryAuthSchemeResponse("An error occurred while creating the primary authentication scheme. Please contact the admin.");
            }
        }

        public async Task<PrimaryAuthSchemeResponse> DeletePrimaryAuthSchemeAsync(int id)
        {
            var primaryAuthSchemeInDb = await _unitOfWork.PrimaryAuthScheme.GetByIdAsync(id);
            if (null == primaryAuthSchemeInDb)
            {
                return new PrimaryAuthSchemeResponse("Primary authentication scheme not found. Please contact the admin.");
            }


            try
            {
                _unitOfWork.PrimaryAuthScheme.Update(primaryAuthSchemeInDb);
                await _unitOfWork.SaveAsync();

                return new PrimaryAuthSchemeResponse(primaryAuthSchemeInDb, "Primary authentication scheme deleted successfully");
            }
            catch
            {
                return new PrimaryAuthSchemeResponse("Primary authentication scheme delete failed. Please contact the admin.");
            }


            //var authSchemeInDb = await _unitOfWork.AuthScheme.GetAuthSchemeByNameAsync(primaryAuthSchemeInDb.Name);

            //var isAssigned = await _unitOfWork.AuthScheme.IsAuthSchemeAssignedAsync(authSchemeInDb.Id);
            //if(true == isAssigned)
            //{
            //    return new PrimaryAuthSchemeResponse("Primary authentication could not be deleted, as it is assigned to operation.");
            //}

            //_unitOfWork.AuthScheme.Remove(authSchemeInDb);

            //try
            //{
            //    await _unitOfWork.SaveAsync();

            //    return new PrimaryAuthSchemeResponse(primaryAuthSchemeInDb);
            //}
            //catch
            //{
            //    return new PrimaryAuthSchemeResponse("Primary authentication scheme delete failed. Please contact the admin.");
            //}
        }

        public async Task<IEnumerable<PrimaryAuthScheme>> ListPrimaryAuthSchemesAsync()
        {
            return await _unitOfWork.PrimaryAuthScheme.GetAllAsync();
        }
    }
}
