using DTPortal.Core.Domain.Models;
using DTPortal.Core.Domain.Models.RegistrationAuthority;
using DTPortal.Core.Domain.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTPortal.Core.Persistence.Repositories
{
    public class SubscriberCardDetailRepository : GenericRepository<SubscriberCardDetail, ra_0_2Context>,
            ISubscriberCardDetailRepository
    {
        private readonly ILogger _logger;
        public SubscriberCardDetailRepository(ra_0_2Context context, ILogger logger) : base(context, logger)
        {
            _logger = logger;
        }
        public async Task<SubscriberCardDetail> GetSubscriberCard(string suid)
        {
            try
            {
                _logger.LogInformation("Get Subscriber Card Started");
                return await Context.SubscriberCardDetails.AsNoTracking().FirstOrDefaultAsync(u => u.SubscriberUid == suid);
            }
            catch (Exception error)
            {
                _logger.LogError(error, "Get SubscriberCardDetail::Database exception");
                return null;
            }
        }
    }
}
