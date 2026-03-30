using DTPortal.Core.Services;
using System.Threading.Tasks;

namespace DTPortal.Core.Utilities
{
    public interface IRAServiceClient
    {
        public Task<SubscriberStatusUpdateResponse> SubscriberStatusUpdate(
            SubscriberStatusUpdateRequest subscriberStatusUpdateRequest);
        Task<bool> RASubscriberStatusUpdate(string status, string identifier);
    }
}
