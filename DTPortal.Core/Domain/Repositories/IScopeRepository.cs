using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DTPortal.Core.Domain.Models;

namespace DTPortal.Core.Domain.Repositories
{
    public interface IScopeRepository : IGenericRepository<Scope>
    {
        public Task<bool> IsScopeExistsWithNameAsync(string name);

        public Task<IEnumerable<Scope>> ListAllScopeAsync();

        public Task<Scope> GetScopeByIdWithClaims(int id);
        public Task<Scope> GetScopeByNameAsync(string name);
        public Task<string[]> GetScopesNamesAsync(string Value);
    }
}
