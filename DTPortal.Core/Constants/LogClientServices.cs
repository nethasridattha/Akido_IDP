using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTPortal.Core.Constants
{
    public static class LogClientServices
    {
        public const string AuthenticationSuccess = "SUBSCRIBER_AUTHENTICATED";
        public const string AuthenticationFailed = "SUBSCRIBER_AUTHENTICATION_FAILED";
        public const string SPOnboarded = "SERVICE_PROVIDER_ONBOARDED";

        public const string AccountLocked = "SUBSCRIBER_ACCOUNT_LOCKED";
        public const string Other = "OTHER";

        public const string SubscriberStatusUpdate = "SUBSCRIBER_STATUS_UPDATED";
        public const string SubscriberLogOut = "SUBSCRIBER_LOGOUT";
        public const string AuthenticationInitiated = "AUTHENTICATION_INITIATED";
        public const string PinVerification = "PIN_VERIFICATION";
        public const string Request = "Request";
        public const string IDP = "IDP";
        public const string Business = "BUSINESS";
        public const string walletAuthenticationLog = "WALLET_AUTHENTICATION";

        public const string Success = "SUCCESS";
        public const string Error = "ERROR";
        public const string Info = "INFO";
        public const string Warning = "WARNING";
        public const string Failure = "FAILURE";

        public const string Approved = "APPROVED";
        public const string Declined = "DECLINED";
        public const string Failed = "FAILED";

        public const string IdpAuthentication = "IDP_AUTHENTICATION";
        public const string IdpRegistration = "IDP_REGISTRATION";
        public const string WalletAuthentication = "WALLET_AUTHENTICATION";
        public const string WalletRegistration = "WALLET_REGISTRATION";
        public const string DataSharing = "DATA_SHARING";
    }
}
