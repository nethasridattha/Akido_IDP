using DTPortal.Core.Domain.Models.RegistrationAuthority;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTPortal.Core.Domain.Repositories
{
    public interface ISubscriberCompleteDetailsRepository : IGenericRepository<SubscriberCompleteDetail>
    {
        public Task<SubscriberCompleteDetail> GetSubscriberCompleteDetailsBySuid(string suid);
        public Task<SubscriberCompleteDetail> GetSubscriberCompleteDetailsByDocId(string docid);
    }
}
