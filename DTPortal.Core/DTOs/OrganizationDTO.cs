using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTPortal.Core.DTOs
{
    public class OrganizationDTO
    {
        public OrganizationDTO()
        {
            DocumentListCheckbox = new List<string>();
        }

        public int OrganizationId { get; set; }
        public string OrganizationUid { get; set; }
        public string OrganizationName { get; set; }
        public string OrganizationEmail { get; set; }
        public string EmailDomain { get; set; }
        public bool DomainStatus { get; set; } = false;
        public string ESealImage { get; set; }
        public string AuthorizedLetterForSignatories { get; set; }
        public string UniqueRegdNo { get; set; }
        public string TaxNo { get; set; }
        public string CorporateOfficeAddress { get; set; }
        public string Incorporation { get; set; }
        public string Tax { get; set; }
        public List<string> SubscriberEmailList { get; set; }
        public IList<OrganizationUser> OrgUserList { get; set; }
        public List<string> DirectorsEmailList { get; set; }
        public List<string> DocumentListCheckbox { get; set; }
        public List<int> TemplateId { get; set; }
        public string OtherLegalDocument { get; set; }
        public string SignedPdf { get; set; }
        public string Status { get; set; }
        public bool EnablePostPaidOption { get; set; }
        public string SpocUgpassEmail { get; set; }
        public string AgentUrl { get; set; }
        public string CreatedBy { get; set; }
        public string UpdatedBy { get; set; }
        public bool IsDetailsAvailable { get; set; } = false;
        public string IsPostPaid { get; set; }
        public string TransactionReferenceId { get; set; }

        public bool walletCertificateStatus { get; set; }
    }
}
