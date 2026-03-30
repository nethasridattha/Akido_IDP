using DTPortal.Core.Domain.Models;
using DTPortal.Core.Domain.Services.Communication;
using DTPortal.Core.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTPortal.Core.Domain.Services
{
    public interface IScopeService
    {
        public Task<ScopeResponse> CreateScopeAsync(Scope scope,
            bool makerCheckerFlag = false);
        public Task<Scope> GetScopeAsync(int id);

        public Task<int> GetScopeIdByNameAsync(string name);

        public Task<ScopeResponse> UpdateScopeAsync(Scope scope,
            bool makerCheckerFlag = false);

        public Task<IEnumerable<Scope>> ListScopeAsync();

        public Task<IList<string>> GetScopesListAsync();

        public Task<ScopeResponse> DeleteScopeAsync(int id, string updatedBy,
        bool makerCheckerFlag = false);

        public Task<bool> isScopehaveSaveConsent(int scopeId);

        public Task<bool> isScopehaveSaveConsentByName(string scopename);

        public Task<string[]> GetScopesNamesAsync(string Value);

        public Task<IList<UserClaimDto>> ListAttributeDisplayNames(string fieldsString);

        public Task<Dictionary<string, LocalizedDisplayName>> GetScopeNameDisplayNameAsync();
    }
}
