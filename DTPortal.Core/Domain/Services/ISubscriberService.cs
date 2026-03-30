using System.Threading.Tasks;
using System.Collections.Generic;

using DTPortal.Core.DTOs;
using DTPortal.Core.Domain.Services.Communication;
using static DTPortal.Common.CommonResponse;

namespace DTPortal.Core.Domain.Services
{
    public interface ISubscriberService
    {
        Task<IEnumerable<RevokeReasonDTO>> GetAllRevokeReasonsAsync();

        Task<string[]> GetSubscribersNamesAysnc(int type, string value);

        Task<SubscribersCountDTO> GetSubscribersCountAsync();

        Task<SubscribersAndCertificatesCountDTO> GetSubscribersAndCertificatesCountAsync();

        Task<SubscriberDetailsDTO> GetSubscriberDetailsAsync(int type, string value);

        Task<SubscriberOnboardingDetailsDTO> GetSubscriberOnboardingDetailsAsync(string identifier);

        Task<IEnumerable<OrganizaionDetails>> GetOrganizationDetailsAsync(string suid);

        Task<Response> CheckandUpdateSubscriber(string Suid);

        Task<ServiceResult> ActivateSubscriber(string Suid, string userName, bool makerCheckerFlag = false);
        Task<ServiceResult> GetDeviceHistory(string SubscriberUniqueId);
    }
}
