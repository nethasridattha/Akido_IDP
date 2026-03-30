namespace DTPortal.Core.DTOs
{
    public class ConsentDTO
    {
        public int ConsentId { get; set; }

        public string Consent { get; set; }

        public string PrivacyConsent { get; set; }
        public bool ConsentRequired { get; set; }

        public string CreatedOn { get; set; }

        public string UpdatedOn { get; set; }

        public string ConsentType { get; set; }

        public string Status { get; set; }
    }
}
