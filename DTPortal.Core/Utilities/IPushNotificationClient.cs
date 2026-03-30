using System.Threading.Tasks;

namespace DTPortal.Core.Utilities
{
    public interface IPushNotificationClient
    {
        public Task<string> SendAuthnNotification(
            AuthnNotification authnNotification);

        public Task<string> SendNotification(
            PushNotificationRequest request);
        public Task<string> SendEConsentNotification(
            EConsentNotification eConsentNotification);

        string SendWalletDelegationNotification(
            WalletDelegationNotification request);
    }
}