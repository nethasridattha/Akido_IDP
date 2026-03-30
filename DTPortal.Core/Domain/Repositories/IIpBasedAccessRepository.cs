using DTPortal.Core.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTPortal.Core.Domain.Repositories
{
    public interface IIpBasedAccessRepository : IGenericRepository<IpBasedAccess>
    {
        Task<string> GetActiveAllowedSingleIps();
        Task<string> GetActiveAllowedMaskedIps();
        Task<string> GetActiveDeniedSingleIps();
        Task<string> GetActiveDeniedMaskedIps();
        Task<bool> IsIPAccessActive();
    }
}
