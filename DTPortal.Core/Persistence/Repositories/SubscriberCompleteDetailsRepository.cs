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
    public class SubscriberCompleteDetailsRepository : GenericRepository<SubscriberCompleteDetail, ra_0_2Context>,
            ISubscriberCompleteDetailsRepository
    {
        private readonly ILogger _logger;
        public SubscriberCompleteDetailsRepository(ra_0_2Context context, ILogger logger) : base(context, logger)
        {
            _logger = logger;
        }

        public async Task<SubscriberCompleteDetail> GetSubscriberCompleteDetailsBySuid(string suid)
        {
            try
            {
                _logger.LogInformation("Get Subscriber Complete Details Started");
                return await Context.SubscriberCompleteDetails.AsNoTracking().FirstOrDefaultAsync(u => u.SubscriberUid == suid);
            }
            catch (Exception error)
            {
                _logger.LogError(error, "Get SubscriberCompleteDetailsBySuid::Database exception");
                return null;
            }
        }

        public async Task<SubscriberCompleteDetail> GetSubscriberCompleteDetailsByDocId(string docid)
        {
            try
            {
                _logger.LogInformation("Get Subscriber Complete Details Started");
                return await Context.SubscriberCompleteDetails.AsNoTracking().FirstOrDefaultAsync(u => u.IdDocNumber == docid);
            }
            catch (Exception error)
            {
                _logger.LogError(error, "Get SubscriberCompleteDetailsByDocId::Database exception");
                return null;
            }
        }
    }
}
