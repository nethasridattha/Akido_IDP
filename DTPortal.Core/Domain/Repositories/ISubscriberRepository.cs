using DTPortal.Core.Domain.Models.RegistrationAuthority;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DTPortal.Core.Domain.Repositories
{
    public interface ISubscriberRepository : IGenericRepository<SubscriberView>
    {
        Task<SubscriberView> GetSubscriberInfo(SubscriberView SubInfo);
        Task<SubscriberView> GetSubscriberInfoByEmail(string emailId);
        Task<IList<SubscriberView>> GetSubscriberInfoByOrgnizationEmail(string emailId);
        Task<SubscriberView> GetSubscriberInfoByPhone(string phoneNo);
        Task<SubscriberView> GetSubscriberInfoBySUID(string Suid);
        Task<SubscriberView> GetSubscriberInfoByEmiratesId(string emiratesId);
        Task<SubscriberView> GetSubscriberInfobyDocType(string docNumber);
        Task<List<SubscriberView>> GetSubscriberInfoBySUIDList(List<string> SuidList);
        Task<SubscriberRaDatum> GetSubscriberRaDatumBySuid(string suid);
        Task<SubscriberView> GetSubscriberDetailsBySuid(string suid);

        Task<SubscriberView> GetSubscriberDetailsByPassportNumber(string pnumber);

        Task<SubscriberView> GetSubscriberDetailsByEmiratesId(string eid);
    }
}
