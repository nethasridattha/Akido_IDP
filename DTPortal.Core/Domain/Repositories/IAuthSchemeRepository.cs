using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DTPortal.Core.Domain.Lookups;
using DTPortal.Core.Domain.Models;

namespace DTPortal.Core.Domain.Repositories
{
    public interface IAuthSchemeRepository : IGenericRepository<AuthScheme>
    {
        Task<AuthScheme> GetAuthSchemeByNameAsync(string AuthSchemeName);
        Task<IEnumerable<AuthScheme>> ListAuthSchemesAsync();
        Task<IEnumerable<AuthSchemesLookupItem>> GetAuthSchemeLookupItemsAsync();
    }
}
