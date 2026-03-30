using DTPortal.Core.Domain.Services.Communication;
using DTPortal.Core.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static DTPortal.Common.CommonResponse;

namespace DTPortal.Core.Domain.Services
{
    public interface IPushNotificationService
    {
        Task<Response> SendNotification(PushNotificationDTO request);

        Task<Response> SendNotificationDelegationRequest(DelegationPushNotificationDTO request);
    }
}
