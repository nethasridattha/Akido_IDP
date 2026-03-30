using DTPortal.Core.Domain.Models;
using DTPortal.Core.Domain.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Polly;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTPortal.Core.Persistence.Repositories
{
    public class EConsentClientRepository : GenericRepository<EConsentClient, idp_dtplatformContext>,
        IEConsentClientRepository
    {
        private readonly ILogger _logger;
        public EConsentClientRepository(idp_dtplatformContext context, ILogger logger) :
            base(context, logger)
        {
            _logger = logger;
        }
        public async Task<IEnumerable<EConsentClient>> ListAllConsentServicesAsync()
        {
            try
            {
                return await Context.EConsentClients
                    .Where(u => u.Status != "DELETED").ToListAsync();
            }
            catch (Exception error)
            {
                _logger.LogError(error, "ListAllConsentServicesAsync::Database exception");
                return null;
            }
        }

        public async Task<EConsentClient> GetConsentServiceByIdAsync(int consentId)
        {
            try
            {
                return await Context.EConsentClients.FirstOrDefaultAsync(u => u.Status != "DELETED" && u.Id == consentId);
            }
            catch (Exception error)
            {
                _logger.LogError(error, "GetConsentServiceByIdAsync::Database exception");
                return null;
            }
        }

        public async Task<EConsentClient> GetConsentServiceByClientIdAsync(int clientId)
        {
            try
            {
                return await Context.EConsentClients.FirstOrDefaultAsync(u => u.Status != "DELETED" && u.ClientId == clientId);
            }
            catch (Exception error)
            {
                _logger.LogError(error, "GetConsentServiceByIdAsync::Database exception");
                return null;
            }
        }

        public async Task<IEnumerable<string>> GetProfilesByClientId(int clientId)
        {
            try
            {
                var eConsentClient = await Context.EConsentClients.FirstOrDefaultAsync(u => u.Status != "DELETED" && u.ClientId == clientId);

                if (eConsentClient != null && !string.IsNullOrWhiteSpace(eConsentClient.Scopes))
                {
                    IEnumerable<string> scopes = eConsentClient.Scopes.Split(',', StringSplitOptions.RemoveEmptyEntries);
                    return scopes;
                }

                return Enumerable.Empty<string>();
            }
            catch (Exception error)
            {
                _logger.LogError(error, "GetProfilesByClientId::Database exception");
                return null;
            }
        }

        public async Task<IEnumerable<string>> GetPurposesByClientId(int clientId)
        {
            try
            {
                var eConsentClient = await Context.EConsentClients.FirstOrDefaultAsync(u => u.Status != "DELETED" && u.ClientId == clientId);

                if (eConsentClient != null && !string.IsNullOrWhiteSpace(eConsentClient.Purposes))
                {
                    IEnumerable<string> purposes = eConsentClient.Purposes.Split(',', StringSplitOptions.RemoveEmptyEntries);
                    return purposes;
                }

                return Enumerable.Empty<string>();
            }
            catch (Exception error)
            {
                _logger.LogError(error, "GetPurposesByClientId::Database exception");
                return null;
            }
        }

        //public async Task<bool> IsConsentExistsWithNameAsync(string name)
        //{
        //    try
        //    {
        //        return await Context.EConsentClients.AsNoTracking().AnyAsync(u => u.ApplicationName == name);
        //    }
        //    catch (Exception error)
        //    {
        //        _logger.LogError("IsConsentExistsWithNameAsync::Database exception: {0}", error);
        //        return false;
        //    }
        //}

    }
}
