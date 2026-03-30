using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DTPortal.Core.Domain.Models;
using DTPortal.Core.Domain.Repositories;
using DTPortal.Core.Domain.Services.Communication;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace DTPortal.Core.Persistence.Repositories
{
    public class UserConsentRepository : GenericRepository<UserConsent, idp_dtplatformContext>,
            IUserConsentRepository
    {
        private readonly ILogger _logger;
        public UserConsentRepository(idp_dtplatformContext context, ILogger logger) : base(context, logger)
        {
            _logger = logger;
        }

        public async Task<UserConsent> GetUserConsent(string suid, string clientId)
        {
            try
            {
                return await Context.UserConsents.SingleOrDefaultAsync(uc => uc.Suid == suid && uc.ClientId == clientId);
            }
            catch (Exception error)
            {
                _logger.LogError(error, "GetRoleByRoleIdWithActivities::Database exception");
                return null;
            }
        }

        public async Task<UserConsent> GetUserConsentByClientAsync(string suid, string clientId)
        {
            try
            {
                return await Context.UserConsents.SingleOrDefaultAsync(uc => uc.Suid == suid && uc.ClientId == clientId);
            }
            catch (Exception error)
            {
                _logger.LogError(error, "GetUserConsentByClientAsync::Database exception");
                throw;
            }
        }

    }
}
