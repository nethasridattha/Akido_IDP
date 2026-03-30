using DTPortal.Core.Domain.Models;
using System.Collections.Generic;

namespace DTPortal.Core.Domain.Services.Communication
{
    public class UserConsentResponse : BaseResponse<UserConsent>
    {
        public UserConsentResponse(UserConsent category) : base(category) { }

        public UserConsentResponse(string message) : base(message) { }
    }

    public class ConsentDetails
    {
        public IList<string> Scopes { get; set; }
        public string Suid { get; set; }
        public string ApplicationName { get; set; }
    }
    public class CheckConsentResponse
    {
        public bool Success { get; set; }
        public string Message { get; set; }
        public ConsentDetails Result { get; set; }
    }
}
