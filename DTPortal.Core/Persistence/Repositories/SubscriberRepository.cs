using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;

using DTPortal.Core.Domain.Repositories;
using DTPortal.Core.Domain.Models.RegistrationAuthority;
using System.Linq;
using System.Collections.Generic;

namespace DTPortal.Core.Persistence.Repositories
{
    public class SubscriberRepository : GenericRepository<SubscriberView, ra_0_2Context>,
        ISubscriberRepository
    {
        private readonly ILogger _logger;
        public SubscriberRepository(ra_0_2Context context,
            ILogger logger) : base(context, logger)
        {
            _logger = logger;
        }

        public async Task<SubscriberView> GetSubscriberInfo(SubscriberView SubInfo)
        {
            try
            {
                return await Context.SubscriberViews.AsNoTracking().SingleOrDefaultAsync(ss => ss.SubscriberUid == SubInfo.SubscriberUid);
            }
            catch (Exception error)
            {
                _logger.LogError(error, "GetSubscriberInfo::Database exception");
                return null;
            }
        }

        public async Task<SubscriberView> GetSubscriberInfoByEmail(string emailId)
        {
            try
            {
                return await Context.SubscriberViews.AsNoTracking().SingleOrDefaultAsync(ss => ss.Email == emailId);
            }
            catch (Exception error)
            {
                _logger.LogError(error, "GetSubscriberInfoByEmail::Database exception");
                throw;
            }
        }

        public async Task<SubscriberView> GetSubscriberInfoByEmiratesId(string emiratesId)
        {
            try
            {
                return await Context.SubscriberViews.AsNoTracking().SingleOrDefaultAsync(ss => ss.NationalId == emiratesId);
            }
            catch (Exception error)
            {
                _logger.LogError(error, "GetSubscriberInfoByEmail::Database exception");
                throw;
            }
        }

        public async Task<IList<SubscriberView>> GetSubscriberInfoByOrgnizationEmail(string emailId)
        {
            try
            {
                return await Context.SubscriberViews.Where(ss => ss.OrgEmailsList.ToLower().Contains(emailId.ToLower())).AsNoTracking().ToListAsync();
            }
            catch (Exception error)
            {
                _logger.LogError(error, "GetSubscriberInfoByEmail::Database exception");
                throw;
            }
        }

        public async Task<SubscriberView> GetSubscriberInfoByPhone(string phoneNo)
        {
            try
            {
                return await Context.SubscriberViews.AsNoTracking().SingleOrDefaultAsync(ss => ss.MobileNumber == phoneNo);
            }
            catch (Exception error)
            {
                _logger.LogError(error, "GetSubscriberInfoByPhone::Database exception");
                throw;
            }
        }

        public async Task<SubscriberView> GetSubscriberInfoBySUID(string Suid)
        {
            try
            {
                return await Context.SubscriberViews.AsNoTracking().SingleOrDefaultAsync(ss => ss.SubscriberUid == Suid);
            }
            catch (Exception error)
            {
                _logger.LogError(error, "GetSubscriberInfoBySUID::Database exception");
                throw;
            }
        }

        public async Task<List<SubscriberView>> GetSubscriberInfoBySUIDList(List<string> SuidList)
        {
            try
            {
                return await Context.SubscriberViews.Where<SubscriberView>(ss => SuidList.Contains(ss.SubscriberUid)).AsNoTracking().ToListAsync();
            }
            catch (Exception error)
            {
                _logger.LogError(error, "GetSubscriberInfoBySUID::Database exception");
                throw;
            }
        }

        public async Task<SubscriberView> GetSubscriberInfobyDocType(string docNumber)
        {
            try
            {
                return await Context.SubscriberViews.AsNoTracking().SingleOrDefaultAsync(ss => ss.IdDocNumber == docNumber);
            }
            catch (Exception error)
            {
                _logger.LogError(error, "GetSubscriberInfobyDocType::Database exception");
                return null;
            }
        }

        public async Task<SubscriberRaDatum> GetSubscriberRaDatumBySuid(string suid)
        {
            try
            {
                return await Context.SubscriberRaData.AsNoTracking().SingleOrDefaultAsync(ss => ss.SubscriberUid == suid);
            }
            catch (Exception error)
            {
                _logger.LogError(error, "GetSubscriberRaDatumBySuid::Database exception");
                return null;
            }
        }

        public async Task<SubscriberView> GetSubscriberDetailsBySuid(string suid)
        {
            try
            {
                return await Context.SubscriberViews.SingleOrDefaultAsync(s => s.SubscriberUid == suid);
            }
            catch (Exception error)
            {
                _logger.LogError(error, "GetSubscriberDetailsBySuid::Database exception");
                return null;
            }
        }

        public async Task<SubscriberView> GetSubscriberDetailsByPassportNumber(string pnumber)
        {
            try
            {
                return await Context.SubscriberViews.SingleOrDefaultAsync(s => s.IdDocNumber == pnumber);
            }
            catch (Exception error)
            {
                _logger.LogError(error, "GetSubscriberDetailsByPassportNumber::Database exception");
                return null;
            }
        }

        public async Task<SubscriberView> GetSubscriberDetailsByEmiratesId(string eid)
        {
            try
            {
                return await Context.SubscriberViews.SingleOrDefaultAsync(s => s.NationalId == eid && s.IdDocType == "1");
            }
            catch (Exception error)
            {
                _logger.LogError(error, "GetSubscriberDetailsByEmiratesId::Database exception");
                return null;
            }
        }
    }
}
