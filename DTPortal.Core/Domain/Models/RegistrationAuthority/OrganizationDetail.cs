using System;
using System.Collections.Generic;

namespace DTPortal.Core.Domain.Models.RegistrationAuthority;

public partial class OrganizationDetail
{
    public int OrganizationDetailsId { get; set; }

    public string OrganizationUid { get; set; }

    public string OrgName { get; set; }

    public string OrganizationEmail { get; set; }

    public string ESealImage { get; set; }

    public string AuthorizedLetterForSignatories { get; set; }

    public string UniqueRegdNo { get; set; }

    public string TaxNo { get; set; }

    public string CorporateOfficeAddress { get; set; }

    public string IncorporationFile { get; set; }

    public string TaxFile { get; set; }

    public string OrganizationSegments { get; set; }

    public string OtherLegalDocument { get; set; }

    public string OtherEsealDocument { get; set; }

    public string SignedPdf { get; set; }

    public string CreatedBy { get; set; }

    public string UpdatedBy { get; set; }

    public string OrganizationStatus { get; set; }

    public string CreatedOn { get; set; }

    public string UpdatedOn { get; set; }

    public bool? EnablePostPaidOption { get; set; }

    public string SpocUgpassEmail { get; set; }

    public string AgentUrl { get; set; }

    public bool? ManageByAdmin { get; set; }

    public virtual ICollection<OrgEmailDomain> OrgEmailDomains { get; set; } = new List<OrgEmailDomain>();

    public virtual ICollection<OrgSignatureTemplate> OrgSignatureTemplates { get; set; } = new List<OrgSignatureTemplate>();

    public virtual ICollection<OrgSubscriberEmailOld> OrgSubscriberEmailOlds { get; set; } = new List<OrgSubscriberEmailOld>();

    public virtual ICollection<OrgSubscriberEmail> OrgSubscriberEmails { get; set; } = new List<OrgSubscriberEmail>();

    public virtual ICollection<OrganizationCertificateLifeCycle> OrganizationCertificateLifeCycles { get; set; } = new List<OrganizationCertificateLifeCycle>();

    public virtual ICollection<OrganizationCertificate> OrganizationCertificates { get; set; } = new List<OrganizationCertificate>();

    public virtual ICollection<OrganizationDirector> OrganizationDirectors { get; set; } = new List<OrganizationDirector>();

    public virtual ICollection<OrganizationDocumentCheck> OrganizationDocumentChecks { get; set; } = new List<OrganizationDocumentCheck>();

    public virtual ICollection<OrganizationDocument> OrganizationDocuments { get; set; } = new List<OrganizationDocument>();

    public virtual ICollection<OrganizationStatus> OrganizationStatuses { get; set; } = new List<OrganizationStatus>();

    public virtual ICollection<WalletCertificate> WalletCertificates { get; set; } = new List<WalletCertificate>();
}
