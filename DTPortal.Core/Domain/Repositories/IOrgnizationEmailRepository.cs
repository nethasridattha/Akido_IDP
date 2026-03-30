using DTPortal.Core.Domain.Models.RegistrationAuthority;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTPortal.Core.Domain.Repositories
{
    public interface IOrgnizationEmailRepository : IGenericRepository<OrgSubscriberEmail>
    {
        Task<IList<OrgSubscriberEmail>> GetSubscriberOrgnizationByEmailAsync(string email);
        Task<IList<OrgSubscriberEmail>> GetSubscriberOrgnizationByPhoneNumberAsync(string PhoneNo);
        Task<IList<OrgSubscriberEmail>> GetSubscriberOrgnizationBySuidAsync(string suid);
    }
}
