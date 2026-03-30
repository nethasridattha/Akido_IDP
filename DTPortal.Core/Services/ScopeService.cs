using DTPortal.Core.Constants;
using DTPortal.Core.Domain.Models;
using DTPortal.Core.Domain.Repositories;
using DTPortal.Core.Domain.Services;
using DTPortal.Core.Domain.Services.Communication;
using DTPortal.Core.DTOs;
using DTPortal.Core.Utilities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTPortal.Core.Services
{
    public class ScopeService : IScopeService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogClient _LogClient;
        private readonly ILogger<ClientService> _logger;
        private readonly IConfigurationService _configurationService;
        private readonly ICacheClient _cacheClient;

        public ScopeService(ILogger<ClientService> logger,
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

        public async Task<ScopeResponse> CreateScopeAsync(Scope scope,
            bool makerCheckerFlag = false)
        {
            _logger.LogDebug("--->CreateScopeAsync");

            var isExists = await _unitOfWork.Scopes.IsScopeExistsWithNameAsync(
                scope.Name);
            if (true == isExists)
            {
                _logger.LogError("Profile already exists with given name");
                return new ScopeResponse("Profile already exists with given Name");
            }
            scope.CreatedDate = DateTime.Now;
            scope.ModifiedDate = DateTime.Now;
            scope.Status = StatusConstants.ACTIVE;
            try
            {
                await _unitOfWork.Scopes.AddAsync(scope);
                await _unitOfWork.SaveAsync();
                return new ScopeResponse(scope, "Profile created successfully");
            }
            catch
            {
                _logger.LogError("Profile AddAsync failed");
                return new ScopeResponse("An error occurred while creating the Profile." +
                    " Please contact the admin.");
            }
        }

        public async Task<Scope> GetScopeAsync(int id)
        {
            _logger.LogDebug("--->GetScopeAsync");

            var scope = await _unitOfWork.Scopes.GetScopeByIdWithClaims(id);
            if (null == scope)
            {
                _logger.LogError("Profile GetByIdAsync() Failed");
                return null;
            }

            return scope;
        }

        public async Task<int> GetScopeIdByNameAsync(string name)
        {
            _logger.LogDebug("--->GetScopeAsync");

            var scope = await _unitOfWork.Scopes.GetScopeByNameAsync(name);
            if (null == scope)
            {
                _logger.LogError("Profile GetByIdAsync() Failed");
                return -1;
            }

            return scope.Id;
        }

        public async Task<ScopeResponse> UpdateScopeAsync(Scope scope,
            bool makerCheckerFlag = false)
        {
            _logger.LogDebug("--->UpdateScopeAsync");

            // Check whether the scope exists or not
            var scopeInDb = await _unitOfWork.Scopes.GetByIdAsync(scope.Id);
            if (null == scopeInDb)
            {
                _logger.LogError("Profile not found");
                return new ScopeResponse("Profile not found");
            }

            if (scopeInDb.Status == "DELETED")
            {
                _logger.LogError("Profile is already deleted");
                return new ScopeResponse("Profile is already deleted");
            }

            // Check wheter the scope already exists other than the given scope
            var allScopes = await _unitOfWork.Scopes.GetAllAsync();
            foreach (var item in allScopes)
            {
                if (item.Id != scope.Id)
                {
                    if (item.Name == scope.Name)
                    {
                        _logger.LogError("Profile already exists with given Name");
                        return new ScopeResponse("Profile already exists with given Name");
                    }
                }
            }

            scopeInDb.Name = scope.Name;
            scopeInDb.DisplayName = scope.DisplayName;
            scopeInDb.DisplayNameArabic = scope.DisplayNameArabic;
            scopeInDb.Description = scope.Description;
            scopeInDb.DefaultScope = scope.DefaultScope;
            scopeInDb.UserConsent = scope.UserConsent;
            scopeInDb.MetadataPublish = scope.MetadataPublish;
            scopeInDb.UpdatedBy = scope.UpdatedBy;
            scopeInDb.ModifiedDate = DateTime.Now;
            scopeInDb.ClaimsList = scope.ClaimsList;
            scopeInDb.IsClaimsPresent = scope.IsClaimsPresent;

            try
            {
                _unitOfWork.Scopes.Update(scopeInDb);
                await _unitOfWork.SaveAsync();
                return new ScopeResponse(scopeInDb, "Profile updated successfully");
            }
            catch
            {
                _logger.LogError("Profile Update failed");
                return new ScopeResponse("An error occurred while updating the Profile." +
                    " Please contact the admin.");
            }
        }

        public async Task<IEnumerable<Scope>> ListScopeAsync()
        {
            return await _unitOfWork.Scopes.ListAllScopeAsync();
        }

        public async Task<IList<string>> GetScopesListAsync()
        {
            var list = await _unitOfWork.Scopes.ListAllScopeAsync();
            var scopeList = new List<string>();
            foreach (var scope in list)
            {
                scopeList.Add(scope.Name);
            }
            return scopeList;
        }

        public async Task<IList<UserClaimDto>> ListAttributeDisplayNames(string fieldsString)
        {
            var attributes = await _unitOfWork.UserClaims
                .GetUserClaimByNameAsync(fieldsString);

            return attributes;
        }


        public async Task<bool> isScopehaveSaveConsent(int scopeId)
        {
            var scope = await _unitOfWork.Scopes.GetScopeByIdWithClaims(scopeId);
            if(scope == null)
            {
                return false;
            }
            return scope.SaveConsent;
        }

        public async Task<bool> isScopehaveSaveConsentByName(string scopename)
        {
            var scope = await _unitOfWork.Scopes.GetScopeByNameAsync(scopename);
            if (scope == null)
            {
                return false;
            }
            return scope.SaveConsent;
        }

        public async Task<ScopeResponse> DeleteScopeAsync(int id, string updatedBy,
                bool makerCheckerFlag = false)
        {
            var scopeInDb = await _unitOfWork.Scopes.GetByIdAsync(id);
            if (null == scopeInDb)
            {
                return new ScopeResponse("Profile not found");
            }

            try
            {
                _unitOfWork.Scopes.Remove(scopeInDb);
                await _unitOfWork.SaveAsync();

                return new ScopeResponse(scopeInDb, "Profile deleted successfully");
            }
            catch (Exception error)
            {
                _logger.LogError("DeleteScopeAsync failed : {0}", error.Message);
                return new ScopeResponse(
                    "An error occurred,Please contact the admin.");
            }
        }

        public async Task<string[]> GetScopesNamesAsync(string Value)
        {
            var scopesNames=await _unitOfWork.Scopes.GetScopesNamesAsync(Value);

            return scopesNames;
        }

        public async Task<Dictionary<string, LocalizedDisplayName>> GetScopeNameDisplayNameAsync()
        {
            var scopesPair = new Dictionary<string, LocalizedDisplayName>();

            var scopesList = await _unitOfWork.Scopes.ListAllScopeAsync();

            foreach (var scope in scopesList)
            {
                LocalizedDisplayName localizedDisplayName = new LocalizedDisplayName()
                {
                    DisplayNameAr = scope.DisplayNameArabic,
                    DisplayNameEn = scope.DisplayName
                };
                scopesPair[scope.Name] = localizedDisplayName;
            }

            return scopesPair;
        }
    }
}
