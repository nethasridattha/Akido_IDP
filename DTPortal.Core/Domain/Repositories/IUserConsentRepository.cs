using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DTPortal.Core.Domain.Models;

namespace DTPortal.Core.Domain.Repositories
{
    public interface IUserConsentRepository:IGenericRepository<UserConsent>
    {
        Task<UserConsent> GetUserConsent(string suid,string clientId);
        Task<UserConsent> GetUserConsentByClientAsync(string suid, string clientId);
    }
}
