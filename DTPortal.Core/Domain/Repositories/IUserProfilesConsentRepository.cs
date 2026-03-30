using DTPortal.Core.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTPortal.Core.Domain.Repositories
{
    public interface IUserProfilesConsentRepository : IGenericRepository<UserProfilesConsent>
    {
        public Task<List<UserProfilesConsent>> GetUserProfilesConsentBySuidAsync(string suid);
        public Task<List<UserProfilesConsent>> GetUserProfilesConsentByIdAsync(string suid, string clientId);
        public Task<UserProfilesConsent> GetUserProfilesConsentByProfileAsync(string suid, string clientId, string profile);

    }
}
