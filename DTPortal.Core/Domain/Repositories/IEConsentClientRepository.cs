using DTPortal.Core.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTPortal.Core.Domain.Repositories
{
    public interface IEConsentClientRepository : IGenericRepository<EConsentClient>
    {
        public Task<IEnumerable<EConsentClient>> ListAllConsentServicesAsync();
        //public Task<bool> IsConsentExistsWithNameAsync(string name);
        public Task<EConsentClient> GetConsentServiceByIdAsync(int catId);
        public Task<EConsentClient> GetConsentServiceByClientIdAsync(int clientId);

        public Task<IEnumerable<string>> GetProfilesByClientId(int clientId);

        public Task<IEnumerable<string>> GetPurposesByClientId(int clientId);

        
    }
}
