using DTPortal.Core.Domain.Models;
using DTPortal.Core.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTPortal.Core.Domain.Repositories
{
    public interface IUserClaimRepository : IGenericRepository<UserClaim>
    {
        public Task<bool> IsUserClaimExistsWithNameAsync(string name);

        public Task<IEnumerable<UserClaim>> ListAllUserClaimAsync();
        public Task<List<UserClaimDto>> GetUserClaimByNameAsync(string attributesList);

    }
}
