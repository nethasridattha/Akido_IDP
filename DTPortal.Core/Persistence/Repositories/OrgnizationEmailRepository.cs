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
    public class OrgnizationEmailRepository : GenericRepository<OrgSubscriberEmail, ra_0_2Context>,
        IOrgnizationEmailRepository
    {

        public OrgnizationEmailRepository(ra_0_2Context context, ILogger logger) : base(context, logger)
        {
        }

        public async Task<IList<OrgSubscriberEmail>> GetSubscriberOrgnizationByEmailAsync(string email)
        {
            return await Context.OrgSubscriberEmails.Where(s => s.EmployeeEmail.ToLower().Equals(email.ToLower()) && s.UgpassUserLinkApproved == true).AsNoTracking().ToListAsync();
        }

        public async Task<IList<OrgSubscriberEmail>> GetSubscriberOrgnizationBySuidAsync(string suid)
        {
            return await Context.OrgSubscriberEmails.Where(s => s.SubscriberUid.ToLower().Equals(suid.ToLower()) && s.UgpassUserLinkApproved == true).AsNoTracking().ToListAsync();
        }

        public async Task<IList<OrgSubscriberEmail>> GetSubscriberOrgnizationByPhoneNumberAsync(string PhoneNo)
        {
            return await Context.OrgSubscriberEmails.Where(s => s.MobileNumber.ToLower().Equals(PhoneNo.ToLower()) && s.UgpassUserLinkApproved == true).AsNoTracking().ToListAsync();
        }
    }
}
