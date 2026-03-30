using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DTPortal.Core.Domain.Models;
using DTPortal.Core.Domain.Services.Communication;

namespace DTPortal.Core.Domain.Services
{
    public interface IPrimaryAuthSchemeService
    {
        Task<PrimaryAuthScheme> GetPrimaryAuthSchemeAsync(string primaryAuthSchemeName);
        Task<PrimaryAuthSchemeResponse> CreatePrimaryAuthSchemeAsync(PrimaryAuthScheme primaryAuthScheme, int supportsProivisioning);
        Task<PrimaryAuthScheme> GetPrimaryAuthSchemeByIdAsync(int id);
        Task<PrimaryAuthSchemeResponse> UpdatePrimaryAuthSchemeAsync(PrimaryAuthScheme primaryAuthScheme, int supportsProivisioning);
        Task<PrimaryAuthSchemeResponse> DeletePrimaryAuthSchemeAsync(int id);

        Task<IEnumerable<PrimaryAuthScheme>> ListPrimaryAuthSchemesAsync();

    }
}
