using DTPortal.Core.Domain.Models;
using DTPortal.Core.Domain.Models.RegistrationAuthority;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTPortal.Core.Domain.Repositories
{
    public interface ISubscriberCardDetailRepository : IGenericRepository<SubscriberCardDetail>
    {
        public Task<SubscriberCardDetail> GetSubscriberCard(string suid);
    }
}
