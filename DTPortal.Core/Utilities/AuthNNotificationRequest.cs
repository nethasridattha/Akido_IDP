using DTPortal.Core.Domain.Services.Communication;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTPortal.Core.Utilities
{
    public class AuthnNotification
    {
        public string RegistrationToken { get; set; }
        public string AuthnToken { get; set; }
        public string AuthnScheme { get; set; }
        public string RandomCodes { get; set; }
        public string ApplicationName { get; set; }
        public string DeviceName { get; set; }
        public string Timestamp { get; set; }
        public string Language { get; set; }
    }

    public class EConsentNotification
    {
        public string RegistrationToken { get; set; }
        public string AuthnToken { get; set; }
        public string AuthnScheme { get; set; }
        public string ApplicationName { get; set; }
        public List<ScopeInfo> ConsentScopes { get; set; }
        public bool DeselectScopesAndClaims { get; set; }
    }

    public class WalletDelegationNotification
    {
        public string RegistrationToken { get; set; }
        public string AuthnToken { get; set; }
        public string AuthnScheme { get; set; }
        public string Context { get; set; }
        public string Principal { get; set; }
        public List<string> DelegationPurpose { get; set; }
        public string NotaryInformation { get; set; }
        public string ValidityPeriod { get; set; }
    }
}
