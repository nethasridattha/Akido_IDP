using Microsoft.AspNetCore.Http;

namespace DTPortal.Core.DTOs
{
    public class ConsentRequestBodyDTO
    {
        public int ConsentId { get; set; }

        public string Consent { get; set; }

        public string PrivacyConsent { get; set; }

        public string ConsentType { get; set; }

        public bool ConsentRequired { get; set; }

        public IFormFile DataPrivacy { get; set; }
        public IFormFile TermsAndConditions { get; set; }
    }
}
