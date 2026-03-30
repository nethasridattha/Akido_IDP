namespace DTPortal.Core.DTOs
{
    public class OrganizationUser
    {
        public string EmployeeEmail { get; set; }

        public bool Signatory { get; set; } = true;

        public bool ESealSignatory { get; set; }

        public bool ESealPrepatory { get; set; }

        public bool Template { get; set; }

        public bool Bulksign { get; set; }

        public bool Digitalforms { get; set; }

        public bool DigitalFormPrivilege { get; set; }

        public bool Delegate { get; set; }

        public string Designation { get; set; }

        public string SignaturePhoto { get; set; }

        public string Initial { get; set; }

        public string UgpassEmail { get; set; }

        public string PassportNumber { get; set; }

        public string NationalIdNumber { get; set; }

        public string MobileNumber { get; set; }

        public string SubscriberUid { get; set; }

        public string Status { get; set; }

        public bool UgpassUserLinkApproved { get; set; } = false;
    }
}
