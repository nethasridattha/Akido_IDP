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
    public class UserProfilesConsentRepository : GenericRepository<UserProfilesConsent, idp_dtplatformContext>,
        IUserProfilesConsentRepository
    {
        private readonly ILogger _logger;
        public UserProfilesConsentRepository(idp_dtplatformContext context, ILogger logger) :
            base(context, logger)
        {
            _logger = logger;
        }


        public async Task<List<UserProfilesConsent>> GetUserProfilesConsentBySuidAsync(string suid)
        {
            try
            {
                var data =  await Context.UserProfilesConsents.Where(u => u.Status == "ACTIVE" && u.Suid == suid).ToListAsync();
                return data;

            }
            catch (Exception error)
            {
                _logger.LogError(error, "GetUserProfilesConsentBySuidAsync::Database exception");
                return null;
            }
        }
        public async Task<List<UserProfilesConsent>> GetUserProfilesConsentByIdAsync(string suid, string clientId)
        {
            try
            {
                return await Context.UserProfilesConsents.Where(u => u.Status == "ACTIVE" && u.Suid == suid && u.ClientId==clientId).ToListAsync();
            }
            catch (Exception error)
            {
                _logger.LogError(error, "GetUserProfilesConsentBySuidAsync::Database exception");
                return null;
            }
        }
        public async Task<UserProfilesConsent> GetUserProfilesConsentByProfileAsync(string suid, string clientId,string profile)
        {
            try
            {
                return await Context.UserProfilesConsents.FirstOrDefaultAsync(u => u.Status == "ACTIVE" && u.Suid == suid && u.ClientId == clientId && u.Profile.ToLower()==profile.ToLower());
            }
            catch (Exception error)
            {
                _logger.LogError(error, "GetUserProfilesConsentBySuidAsync::Database exception");
                return null;
            }
        }
    }
}
