using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DTPortal.Core.Domain.Models;
using DTPortal.Core.Domain.Services.Communication;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace DTPortal.Core.Domain.Services
{
    public interface IClientService
    {
        Task<ClientResponse> CreateClientAsync(Client client, bool makerCheckerFlag = false);
        Task<Client> GetClientAsync(int id);
        Task<Client> GetClientByAppNameAsync(string appName);
        Task<Client> GetClientByClientIdAsync(string clientId);
        Task<ClientResponse> UpdateClientAsync(Client client,
            ClientsSaml2 clientsSaml2,
            bool makerCheckerFlag = false);
        Task<ClientResponse> DeleteClientAsync(int id, string updatedBy,
            bool makerCheckerFlag = false);
        Task<bool> IsRedirectUriExistsAsync(string redirectUri);
        Task<IEnumerable<Client>> ListClientAsync();
        Task<ClientResponse> UpdateClientState(int id, bool isApproved, string reason = null);
        Task<ClientResponse> DeActivateClientAsync(int id);
        Task<ClientResponse> ActivateClientAsync(int id);

        Task<IEnumerable<Client>> ListClientByOrganizationIdAsync(string orgID);
        Task<IEnumerable<Client>> ListKycClientByOrgUidAsync(string OrgUid);
        Task<IEnumerable<Client>> ListSaml2ClientAsync();
        Task<IEnumerable<Client>> ListOAuth2ClientAsync();
        //Task<string> GetSaml2Config(string clientId);
        Task<string[]> GetAllowedOrigins(string origin);
        Task<string[]> GetAllClientAppNames(string request);
        Task<ClientsCount> GetAllClientsCount();
        Task<Dictionary<string, string>> EnumClientIds();
        Task<IEnumerable<Client>> ListClientByOrgUidAsync(string OrgUid);
        Task<Dictionary<string, string>> GetClientsByName(string value);
        Task<List<SelectListItem>> GetApplicationsList();
        Task<List<SelectListItem>> GetApplicationsListByOuid(string Ouid);
        Task<List<string>> GetApplicationsListByOrgId(string orgId);
        Task<Client> GetClientProfilesAndPurposesAsync(string clientId);
        Task<string[]> GetClientNamesAsync(string Value);
        Task<Dictionary<string, string>> GetApplicationsDictionary();
        Task<ClientResponse> GetClientByApplicationName
            (string ApplicationName, string orgId);

        Task<ClientResponse> DeleteClientByClientId(string clientId);
        Task<Dictionary<string, ApplicationInfo>> GetClientOrgApplicationMap();
    }
}
