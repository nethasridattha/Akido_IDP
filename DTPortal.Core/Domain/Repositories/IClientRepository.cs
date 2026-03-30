using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DTPortal.Core.Domain.Models;

namespace DTPortal.Core.Domain.Repositories
{
    public interface IClientRepository : IGenericRepository<Client>
    {
        Task<bool> IsClientExistsAsync(string clientName);
        Task<bool> IsClientExistsWithNameAsync(Client client);
        Task<bool> IsClientExistsWithRedirecturlAsync(Client client);
        Task<bool> IsClientExistsWithAppUrlAsync(Client client);
        Task<bool> IsClientExistsWithAppNameAsync(Client client);
        Task<Client> GetClientByClientIdAsync(string clientId);
        Task<Client> GetClientByAppNameAsync(string appName);

        Task<bool> IsRedirectUriExistsAsync(string redirectUri);
       // Task<Client> GetClientByClientIdWithSaml2Async(string clientId);
       // Task<Client> GetClientByIdWithSaml2Async(int id);

        Task<IEnumerable<Client>> ListClientByOrganizationIdAsync(string orgID);
        Task<IEnumerable<Client>> ListSaml2ClientAsync();
        Task<IEnumerable<Client>> ListOAuth2ClientAsync();
        Task<IEnumerable<Client>> ListAllClient();
        Task<string[]> GetAllowedOrigins();
        Task<string[]> GetAllClientAppNames(string request);
        Task<int> GetActiveClientsCount();
        Task<int> GetInActiveClientsCount();
        Task<IEnumerable<Client>> ListClientByOrgUidAsync(string OrgUid);
        Task<IEnumerable<Client>> GetClientsList();
        Task<Client> GetClientProfilesAndPurposesAsync(string clientId);
        Task<string[]> GetClientNamesAsync(string Value);
        Task<IEnumerable<Client>> GetKycClientsList();
        Task<IEnumerable<Client>> ListKycClientByOrgUidAsync(string OrgUid);
        Task<Client> ListClientByApplicationNameAsync(
            string OrgUid, string applicationName);
    }
}
