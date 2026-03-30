using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DTPortal.Core.Domain.Models;
using DTPortal.Core.Domain.Services.Communication;

namespace DTPortal.Core.Domain.Services
{
    public interface IUserConsentService
    {
        Task<UserConsentResponse> ModifyUserConsent(UserConsent consent);
        Task<CheckConsentResponse> CheckUserConsent(string clientId, string globalSession,
            IList<string> scopes);
        Task<UserConsentResponse> AddUserConsentAsync(UserConsent consent);
        Task<UserConsentResponse> UpdateUserConsentAsync(UserConsent consent);
    }
}
