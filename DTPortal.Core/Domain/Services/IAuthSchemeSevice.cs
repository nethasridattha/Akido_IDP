using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DTPortal.Core.Domain.Lookups;
using DTPortal.Core.Domain.Models;
using DTPortal.Core.Domain.Services.Communication;

namespace DTPortal.Core.Domain.Services
{
    public interface IAuthSchemeSevice
    {
        Task<AuthScheme> GetAuthSchemeAsync(string AuthSchemeName);
        Task<AuthSchemeResponse> CreateAuthSchemeAsync(AuthScheme authScheme,IList<string> primaryAuthSchemes);
        Task<AuthScheme> GetAuthSchemeByIdAsync(int id);
        Task<IEnumerable<string>> GetPrimaryAuthSchemesOfAuthScheme(int id);
        Task<AuthSchemeResponse> UpdateAuthSchemeAsync(AuthScheme authScheme, IList<string> primaryAuthSchemes);
        Task<AuthSchemeResponse> DeleteAuthSchemeAsync(int id);
        Task<IEnumerable<AuthScheme>> ListAuthSchemesAsync();
        Task<IEnumerable<AuthSchemesLookupItem>> GetAuthSchemesLookupItemsAsync();
        Task<List<string>> GetDefaultAuthScheme();
        Task<List<string>> GetAuthSchemesListById(int authSchemeId);
    }
}
