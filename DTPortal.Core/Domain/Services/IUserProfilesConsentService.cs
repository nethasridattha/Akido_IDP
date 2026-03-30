using DTPortal.Core.Domain.Services.Communication;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTPortal.Core.Domain.Services
{
    public interface IUserProfilesConsentService
    {
        public Task<ServiceResult> GetUserProfilesConsentbySuidAsync(string suid);
        public Task<ServiceResult> GetUserProfilesConsentByClientNameAsync(string suid, string applicationName);
        public Task<ServiceResult> GetUserProfilesConsentByProfileAsync(string suid, string applicationName, string profile);
        public Task<ServiceResult> RevokeUserProfilesConsentByProfileAsync(string suid, string applicationName, string profile);
    }
}
