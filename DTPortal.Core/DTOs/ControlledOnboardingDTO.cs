using System.Collections.Generic;

namespace DTPortal.Core.DTOs
{
    public class ControlledOnboardingDTO
    {
        public IList<TrustedUserDetails> Emails { get; set; }

        public string CreatedBy { get; set; }

        public string UpdatedBy { get; set; }
    }
    public class TrustedUserDetails
    {
        public string Name { get; set; }

        public string Email { get; set; }

        public string MobileNo { get; set; }
    }
}
