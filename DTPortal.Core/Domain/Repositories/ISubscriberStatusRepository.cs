using DTPortal.Core.Domain.Models.RegistrationAuthority;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTPortal.Core.Domain.Repositories
{
    public interface ISubscriberStatusRepository: IGenericRepository<SubscriberStatus>
    {
        Task<SubscriberStatus> GetSubscriberStatusBySuid(string suid);
    }
}
