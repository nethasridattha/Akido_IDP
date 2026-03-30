using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DTPortal.Core.Constants;
using DTPortal.Core.Domain.Models;
using DTPortal.Core.Domain.Repositories;
using DTPortal.Core.Utilities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace DTPortal.Core.Persistence.Repositories
{
    public class ClientRepository : GenericRepository<Client, idp_dtplatformContext>,
            IClientRepository
    {
        private readonly ILogger _logger;
        public ClientRepository(idp_dtplatformContext context,
            ILogger logger) : base(context, logger)
        {
            _logger = logger;
        }
        public async Task<bool> IsClientExistsAsync(string clientName)
        {
            try
            {
                return await Context.Clients.AsNoTracking().AnyAsync(u => u.ClientId == clientName);
            }
            catch(Exception error)
            {
                _logger.LogError(error, "IsClientExistsAsync::Database exception");
                throw;
            }
        }

        public async Task<bool> IsClientExistsWithNameAsync(Client client)
        {
            try
            {
                return await Context.Clients.AsNoTracking().AnyAsync(u => u.ClientId == client.ClientId);
            }
            catch (Exception error)
            {
                _logger.LogError(error, "IsClientExistsAsync::Database exception");
                return false;
            }
        }

        public async Task<bool> IsClientExistsWithRedirecturlAsync(Client client)
        {
            try
            {
                return await Context.Clients.AsNoTracking().AnyAsync(u => u.RedirectUri == client.RedirectUri);
            }
            catch (Exception error)
            {
                _logger.LogError(error, "IsClientExistsAsync::Database exception");
                return false;
            }
        }

        public async Task<bool> IsClientExistsWithAppUrlAsync(Client client)
        {
            try
            {
                return await Context.Clients.AsNoTracking().AnyAsync(u => u.ApplicationUrl == client.ApplicationUrl);
            }
            catch (Exception error)
            {
                _logger.LogError(error, "IsClientExistsAsync::Database exception");
                return false;
            }
        }
        public async Task<bool> IsClientExistsWithAppNameAsync(Client client)
        {
            try
            {
                return await Context.Clients.AsNoTracking().AnyAsync(u => u.ApplicationName == client.ApplicationName);
            }
            catch (Exception error)
            {
                _logger.LogError(error, "IsClientExistsAsync::Database exception");
                return false;
            }
        }

        public async Task<bool> IsRedirectUriExistsAsync(string redirectUri)
        {
            try
            {
                return await Context.Clients.AsNoTracking()
                    .AnyAsync(c => c.RedirectUri == redirectUri);
            }
            catch (Exception error)
            {
             

                _logger.LogError(error, "IsRedirectUriExistsAsync::Database exception");

                return false;
            }
        }

        public async Task<bool> IsLogoutUriExistsAsync(string logoutUri)
        {
            try
            {
                return await Context.Clients.AsNoTracking()
                    .AnyAsync(c => c.LogoutUri == logoutUri);
            }
            catch (Exception error)
            {


                _logger.LogError(error, "IsRedirectUriExistsAsync::Database exception");

                return false;
            }
        }

        public async Task<Client> GetClientByClientIdAsync(string clientId)
        {
            try
            {
                return await Context.Clients.SingleOrDefaultAsync(u => u.ClientId == clientId);
            }
            catch(Exception error)
            {
                Monitor.SendMessage("GetClientByClientIdAsync" + error.ToString());
                _logger.LogError(error, "GetClientByClientIdAsync::Database exception");
                throw;
            }
        }

        public async Task<Client> GetClientProfilesAndPurposesAsync(string clientId)
        {
            try
            {
                return await Context.Clients.Include(c => c.EConsentClients).SingleOrDefaultAsync(u => u.ClientId == clientId);
            }
            catch (Exception error)
            {
                _logger.LogError(error, "GetClientByClientIdAsync::Database exception");
                throw;
            }
        }

        public async Task<Client> GetClientByAppNameAsync(string appName)
        {
            try
            {
                return await Context.Clients.AsNoTracking().SingleOrDefaultAsync(u => u.ApplicationName.ToLower() == appName.ToLower());
            }
            catch(Exception error)
            {
                _logger.LogError(error, "GetClientByAppNameAsync::Database exception");
                return null;
            }
        }

        //public async Task<Client> GetClientByClientIdWithSaml2Async(string clientId)
        //{
        //    try
        //    {
        //        return await Context.Clients.Include(x => x.ClientsSaml2s).AsNoTracking().SingleOrDefaultAsync(u => u.ClientId == clientId);
        //    }
        //    catch (Exception error)
        //    {
        //        _logger.LogError("GetClientByClientIdWithSaml2Async::Database exception: {0}", error);
        //        throw;
        //    }
        //}

        //public async Task<Client> GetClientByIdWithSaml2Async(int id)
        //{
        //    try
        //    {
        //        return await Context.Clients.Include(x => x.ClientsSaml2s).AsNoTracking().SingleOrDefaultAsync(u => u.Id == id);
        //    }
        //    catch (Exception error)
        //    {
        //        _logger.LogError("GetClientByClientIdWithSaml2Async::Database exception: {0}", error);
        //        return null;
        //    }

        //}

        public async Task<IEnumerable<Client>> ListClientByOrganizationIdAsync(string orgID)
        {
            try
            {
                return await Context.Clients.AsNoTracking().Where(u => u.OrganizationUid == orgID).ToListAsync();
            }
            catch (Exception error)
            {
                _logger.LogError(error, "ListClientByOrganizationIdAsync::Database exception");
                return null;
            }
        }
        public async Task<IEnumerable<Client>> ListSaml2ClientAsync()
        {
            try
            {
                return await Context.Clients.AsNoTracking().Where(u => u.Type == "SAML2" && u.Status != "DELETED").ToListAsync();
            }
            catch (Exception error)
            {
                _logger.LogError(error, "ListSaml2ClientAsync::Database exception");
                return null;
            }
        }

        public async Task<IEnumerable<Client>> ListOAuth2ClientAsync()
        {
            try
            {
                return await Context.Clients
                    .AsNoTracking()
                    .Where(u => u.Type == "OAUTH2"
                                && u.Status != "DELETED"
                                && u.IsKycApplication == false)
                    .OrderByDescending(u => u.CreatedDate)   // <-- Sort here
                    .ToListAsync();
            }
            catch (Exception error)
            {
                _logger.LogError(error, "ListOAuth2ClientAsync::Database exception");
                return null;
            }
        }


        public async Task<IEnumerable<Client>> ListAllClient()
        {
            try
            {
                return await Context.Clients.AsNoTracking().Where(u => u.Status == "DELETED").ToListAsync();
            }
            catch (Exception error)
            {
                _logger.LogError(error, "ListAllClient::Database exception");
                return null;
            }
        }

        public async Task<string[]> GetAllowedOrigins()
        {
            try
            {
                return await Context.Clients
                    .Where(x => x.Status != "DELETED")
                    .Select(x => x.ApplicationUrl).AsNoTracking().ToArrayAsync();
            }
            catch (Exception error)
            {
                _logger.LogError(error, "GetAllowedOrigins::Database exception");
                return null;
            }
        }

        public async Task<string[]> GetAllClientAppNames(string request)
        {
            try
            {
                return await Context.Clients
                    .Where(x => x.Status != "DELETED" && x.ApplicationName.Contains(request) && x.ClientId != DTInternalConstants.DTPortalClientId)
                    .Select(x => x.ApplicationName)
                    .ToArrayAsync();
            }
            catch (Exception error)
            {
                _logger.LogError(error, "GetAllClientAppNames::Database exception");
                return null;
            }
        }

        public async Task<int> GetActiveClientsCount()
        {
            try
            {
                return await Context.Clients
                    .Where(x => x.Status == "ACTIVE")
                    .AsNoTracking()
                    .CountAsync();
            }
            catch (Exception error)
            {
                _logger.LogError(error, "GetActiveClientsCount::Database exception");
                return default;
            }
        }

        public async Task<int> GetInActiveClientsCount()
        {
            try
            {
                return await Context.Clients
                    .Where(x => x.Status == "DEACTIVATED")
                    .AsNoTracking()
                    .CountAsync();
            }
            catch (Exception error)
            {
                _logger.LogError(error, "GetInActiveClientsCount::Database exception");
                return default;
            }
        }

        public async Task<IEnumerable<Client>> ListClientByOrgUidAsync(
            string OrgUid)
        {
            try
            {
                return await Context.Clients.AsNoTracking().Where(
                    u => u.OrganizationUid == OrgUid &&
                    u.Status != "DELETED").ToListAsync();
            }
            catch (Exception error)
            {
                _logger.LogError(error, "ListClientByOrgUidAsync::Database exception");
                return null;
            }
        }


        public async Task<Client> ListClientByApplicationNameAsync(
            string OrgUid,string applicationName)
        {
            try
            {
                return await Context.Clients.AsNoTracking().FirstOrDefaultAsync(
                    u => u.OrganizationUid == OrgUid &&
                    u.Status != "DELETED" &&
                    u.ApplicationName.ToLower() == applicationName.ToLower());
            }
            catch (Exception error)
            {
                _logger.LogError(error, "ListClientByOrgUidAsync::Database exception");
                return null;
            }
        }

        public async Task<IEnumerable<Client>> ListKycClientByOrgUidAsync(
            string OrgUid)
        {
            try
            {
                return await Context.Clients.AsNoTracking().Where(
                    u => u.OrganizationUid == OrgUid && u.IsKycApplication == true &&
                    u.Status != "DELETED").ToListAsync();
            }
            catch (Exception error)
            {
                _logger.LogError(error, "ListClientByOrgUidAsync::Database exception");
                return null;
            }
        }

        public async Task<IEnumerable<Client>> GetClientsList()
        {
            try
            {
                return await Context.Clients.AsNoTracking().Where(u => u.Status != "DELETED" && u.ClientId != DTInternalConstants.DTPortalClientId).ToListAsync();
            }
            catch (Exception error)
            {
                _logger.LogError(error, "ListAllClient::Database exception");
                return null;
            }
        }

        public async Task<string[]> GetClientNamesAsync(string Value)
        {
            try
            {
                return await Context.Clients
                    .AsNoTracking()
                    .Where(s =>  s.ApplicationName.Contains(Value))
                    .Select(c => c.ApplicationName)
                    .ToArrayAsync();
            }
            catch (Exception error)
            {
                _logger.LogError(error, "GetClientNamesAsync::Database exception");
                return null;
            }
        }

        public async Task<IEnumerable<Client>> GetKycClientsList()
        {
            try
            {
                return await Context.Clients.AsNoTracking().Where(u => u.Status != "DELETED" && u.ClientId != DTInternalConstants.DTPortalClientId && u.IsKycApplication==true).ToListAsync();
            }
            catch (Exception error)
            {
                _logger.LogError(error, "ListAllClient::Database exception");
                return null;
            }
        }
    }
}
