using System;
using System.Collections.Generic;

namespace DTPortal.Core.Domain.Models.RegistrationAuthority;

public partial class Organizationsview
{
    public int? OrganizationDetailsId { get; set; }

    public string Ouid { get; set; }

    public string OrgName { get; set; }

    public string OrganizationEmail { get; set; }

    public string ESealImage { get; set; }

    public string AuthorizedLetterForSignatories { get; set; }

    public string UniqueRegdNo { get; set; }

    public string TaxNo { get; set; }

    public string CorporateOfficeAddress { get; set; }

    public string IncorporationFile { get; set; }

    public string TaxFile { get; set; }

    public string OtherLegalDocument { get; set; }

    public string OtherEsealDocument { get; set; }

    public string OrganizationStatus { get; set; }

    public bool? EnablePostPaidOption { get; set; }

    public string SpocUgpassEmail { get; set; }

    public string CreatedOn { get; set; }

    public string UpdatedOn { get; set; }

    public DateTime? CertificateIssueDate { get; set; }

    public DateTime? CertificateExpiryDate { get; set; }

    public DateTime? ESealCreatedDate { get; set; }

    public string CertificateStatus { get; set; }

    public DateTime? WalletCertificateIssueDate { get; set; }

    public DateTime? WalletCertificateExpiryDate { get; set; }
}
