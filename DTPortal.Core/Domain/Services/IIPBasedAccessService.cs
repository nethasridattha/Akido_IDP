using DTPortal.Core.Domain.Models;
using DTPortal.Core.Domain.Services.Communication;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTPortal.Core.Domain.Services
{
    public interface IIPBasedAccessService
    {
        Task<IPBasedAccessResponse> CreateIPBasedAccessAsync(IpBasedAccess ipBasedAccess);
        Task<IpBasedAccess> GetIPBasedAccessAsync(int id);
        Task<IPBasedAccessResponse> UpdateIPBasedAccessAsync(IpBasedAccess ipBasedAccess);
        Task<IPBasedAccessResponse> DeleteIPBasedAccessAsync(int id);
        Task<IEnumerable<IpBasedAccess>> ListIPBasedAccessAsync();
    }
}
