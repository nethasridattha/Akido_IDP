using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DTPortal.Core.Domain.Models.RegistrationAuthority;
using DTPortal.Core.Domain.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace DTPortal.Core.Persistence.Repositories
{
    public class SubscriberStatusRepository : GenericRepository<SubscriberStatus,ra_0_2Context >,
        ISubscriberStatusRepository
    {

        public SubscriberStatusRepository(ra_0_2Context context, ILogger logger) : base(context, logger)
        {
        }

        public async Task<SubscriberStatus> GetSubscriberStatusBySuid(string suid)
        {
            return await Context.SubscriberStatuses.SingleOrDefaultAsync(s => s.SubscriberUid == suid);
        }
    }
}
