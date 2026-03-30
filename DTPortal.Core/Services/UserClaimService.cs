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
    public class UserClaimService : IUserClaimService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogClient _LogClient;
        private readonly ILogger<ClientService> _logger;
        private readonly IConfigurationService _configurationService;
        private readonly ICacheClient _cacheClient;

        public UserClaimService(ILogger<ClientService> logger,
            IConfigurationService configurationService,
            IUnitOfWork unitOfWork, ILogClient logClient,
            ICacheClient cacheClient)
        {
            _logger = logger;
            _unitOfWork = unitOfWork;
            _configurationService = configurationService;
            _LogClient = logClient;
            _cacheClient = cacheClient;
        }

        public async Task<UserClaimResponse> CreateUserClaimAsync(UserClaim userClaim,
            bool makerCheckerFlag = false)
        {
            _logger.LogDebug("--->CreateUserClaimAsync");

            var isExists = await _unitOfWork.UserClaims.IsUserClaimExistsWithNameAsync(
                userClaim.Name);
            if (true == isExists)
            {
                _logger.LogError("UserClaim already exists with given name");
                return new UserClaimResponse("Attribute already exists with given Name");
            }
            userClaim.CreatedDate = DateTime.SpecifyKind(DateTime.Now, DateTimeKind.Unspecified);
            userClaim.ModifiedDate = DateTime.SpecifyKind(DateTime.Now, DateTimeKind.Unspecified);
            userClaim.Status = StatusConstants.ACTIVE;
            try
            {
                await _unitOfWork.UserClaims.AddAsync(userClaim);
                await _unitOfWork.SaveAsync();
                return new UserClaimResponse(userClaim, "Attribute created successfully");
            }
            catch
            {
                _logger.LogError("UserClaim AddAsync failed");
                return new UserClaimResponse("An error occurred while creating the Attribute." +
                    " Please contact the admin.");
            }
        }

        public async Task<UserClaim> GetUserClaimAsync(int id)
        {
            _logger.LogDebug("--->GetUserClaimAsync");

            var userClaim = await _unitOfWork.UserClaims.GetByIdAsync(id);
            if (null == userClaim)
            {
                _logger.LogError("UserClaims GetByIdAsync() Failed");
                return null;
            }

            return userClaim;
        }

        public async Task<UserClaimResponse> UpdateUserClaimAsync(UserClaim userClaim,
            bool makerCheckerFlag = false)
        {
            _logger.LogDebug("--->UpdateUserClaimAsync");

            // Check whether the userClaim exists or not
            var userClaimInDb = await _unitOfWork.UserClaims.GetByIdAsync(userClaim.Id);
            if (null == userClaimInDb)
            {
                _logger.LogError("userClaim not found");
                return new UserClaimResponse("Attribute not found");
            }

            if (userClaimInDb.Status == "DELETED")
            {
                _logger.LogError("UserClaim is already deleted");
                return new UserClaimResponse("Attribute is already deleted");
            }

            // Check wheter the userClaim already exists other than the given userClaim
            var allUserClaims = await _unitOfWork.UserClaims.GetAllAsync();
            foreach (var item in allUserClaims)
            {
                if (item.Id != userClaim.Id)
                {
                    if (item.Name == userClaim.Name)
                    {
                        _logger.LogError("UserClaim already exists with given Name");
                        return new UserClaimResponse("Attribute already exists with given Name");
                    }
                }
            }

            userClaimInDb.Name = userClaim.Name;
            userClaimInDb.DisplayName = userClaim.DisplayName;
            userClaimInDb.DisplayNameArabic = userClaim.DisplayNameArabic;
            userClaimInDb.Description = userClaim.Description;
            userClaimInDb.DefaultClaim = userClaim.DefaultClaim;
            userClaimInDb.UserConsent = userClaim.UserConsent;
            userClaimInDb.MetadataPublish = userClaim.MetadataPublish;
            userClaimInDb.UpdatedBy = userClaim.UpdatedBy;
            userClaimInDb.ModifiedDate = DateTime.Now;

            try
            {
                _unitOfWork.UserClaims.Update(userClaimInDb);
                await _unitOfWork.SaveAsync();
                return new UserClaimResponse(userClaimInDb, "Attribute updated successfully");
            }
            catch
            {
                _logger.LogError("UserClaim Update failed");
                return new UserClaimResponse("An error occurred while updating the Attribute." +
                    " Please contact the admin.");
            }
        }

        public async Task<IEnumerable<UserClaim>> ListUserClaimAsync()
        {
            return await _unitOfWork.UserClaims.ListAllUserClaimAsync();
        }

        public async Task<IEnumerable<UserClaim>> ListAllClaimAsync()
        {
            return await _unitOfWork.UserClaims.GetAllAsync();
        }

        public async Task<UserClaimResponse> DeleteUserClaimAsync(int id, string updatedBy,
                bool makerCheckerFlag = false)
        {
            var userClaimInDb = await _unitOfWork.UserClaims.GetByIdAsync(id);
            if (null == userClaimInDb)
            {
                return new UserClaimResponse("Attribute not found");
            }

            try
            {
                _unitOfWork.UserClaims.Remove(userClaimInDb);
                await _unitOfWork.SaveAsync();

                return new UserClaimResponse(userClaimInDb,
                    "Attribute deleted successfully");
            }
            catch (Exception error)
            {
                _logger.LogError("DeleteUserClaimAsync failed : {0}", error.Message);
                return new UserClaimResponse(
                    "An error occurred,Please contact the admin.");
            }
        }

        public async Task<Dictionary<string,string>> GetAttributes()
        {
            var attributesPair= new Dictionary<string, string>();

            var claimsList = await _unitOfWork.UserClaims.ListAllUserClaimAsync();

            foreach(var claim in claimsList)
            {
                attributesPair[claim.Name] = claim.Id.ToString();
            }

            return attributesPair;
        }

        public async Task<Dictionary<string, LocalizedDisplayName>> GetAttributeNameDisplayNameAsync()
        {
            var attributesPair = new Dictionary<string, LocalizedDisplayName>();

            var claimsList = await _unitOfWork.UserClaims.ListAllUserClaimAsync();

            foreach (var claim in claimsList)
            {
                LocalizedDisplayName localizedDisplayName = new LocalizedDisplayName()
                {
                    DisplayNameEn = claim.DisplayName,
                    DisplayNameAr = claim.DisplayNameArabic
                };
                attributesPair[claim.Name] = localizedDisplayName;
            }

            return attributesPair;
        }

        public async Task<Dictionary<string, bool>> GetAttributeNameMandatoryAsync()
        {
            var attributesPair = new Dictionary<string, bool>();

            var claimsList = await _unitOfWork.UserClaims.ListAllUserClaimAsync();

            foreach (var claim in claimsList)
            {
                attributesPair[claim.Name] = claim.DefaultClaim;
            }

            return attributesPair;
        }

    }
}
