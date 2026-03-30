using System;
using System.Collections.Generic;

namespace DTPortal.Core.Domain.Models.RegistrationAuthority;

public partial class OrganisationOnboardingForm
{
    public int Id { get; set; }

    public string OrgName { get; set; }

    public string OrgRegIdNum { get; set; }

    public string OrgOfficialContactNum { get; set; }

    public string OrgWebUrl { get; set; }

    public string OrgTanTaxNum { get; set; }

    public string UrsbCert { get; set; }

    public string ApprovalLetter { get; set; }

    public string ObFormStatus { get; set; }

    public string OrgApprovalStatus { get; set; }

    public string OrgObRejectedReason { get; set; }

    public string SignApprByBrmStaff { get; set; }

    public string OrgUid { get; set; }

    public string OrgCategory { get; set; }

    public string CreatedOn { get; set; }

    public string UpdatedOn { get; set; }

    public string SpocSuid { get; set; }

    public bool? Flag { get; set; }

    public string OtpVerification { get; set; }

    public string Checksum { get; set; }

    public string SignedChecksum { get; set; }

    public string OrgCategoryId { get; set; }

    public string SpocEmail { get; set; }

    public string OrgCorporateAddress { get; set; }

    public bool? OrgAddedByAdmin { get; set; }

    public virtual ICollection<OrgCeoDetail> OrgCeoDetails { get; set; } = new List<OrgCeoDetail>();

    public virtual ICollection<OrgESeal> OrgESeals { get; set; } = new List<OrgESeal>();

    public virtual ICollection<OrgFinancialAuditorDetail> OrgFinancialAuditorDetails { get; set; } = new List<OrgFinancialAuditorDetail>();

    public virtual ICollection<OrganisationSpocDetail> OrganisationSpocDetails { get; set; } = new List<OrganisationSpocDetail>();

    public virtual ICollection<SoftwareSuggestionAndOrganisationReadiness> SoftwareSuggestionAndOrganisationReadinesses { get; set; } = new List<SoftwareSuggestionAndOrganisationReadiness>();
}
