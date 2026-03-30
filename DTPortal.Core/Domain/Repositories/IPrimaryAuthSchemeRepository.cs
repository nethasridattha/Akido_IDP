using DTPortal.Core.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTPortal.Core.Domain.Repositories
{
    public interface IPrimaryAuthSchemeRepository : IGenericRepository<PrimaryAuthScheme>
    {
        Task<PrimaryAuthScheme> GetPrimaryAuthSchemeByPrimaryAuthSchemeAsync(string PrimaryAuthSchemeName);
        Task<PrimaryAuthScheme> GetPrimaryAuthSchemeByIdAsync(int PrimaryAuthSchemeId);
        Task<bool> IsPrimaryAuthSchemeExists(PrimaryAuthScheme primaryAuthScheme);
        Task<IList<string>> GetPrimaryAuthSchmsbyAuthSchmName(string AuthSchmName);
    }
}
