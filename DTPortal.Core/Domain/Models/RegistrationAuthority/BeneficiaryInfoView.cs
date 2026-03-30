using System;
using System.Collections.Generic;

namespace DTPortal.Core.Domain.Models.RegistrationAuthority;

public partial class BeneficiaryInfoView
{
    public int? BeneficiaryId { get; set; }

    public string SponsorName { get; set; }

    public string BeneficiaryName { get; set; }

    public string SponsorDigitalId { get; set; }

    public string SponsorType { get; set; }

    public string SponsorExternalId { get; set; }

    public string BeneficiaryDigitalId { get; set; }

    public string BeneficiaryType { get; set; }

    public int? SponsorPaymentPriorityLevel { get; set; }

    public string BeneficiaryNin { get; set; }

    public string BeneficiaryPassport { get; set; }

    public string BeneficiaryMobileNumber { get; set; }

    public string BeneficiaryOfficeEmail { get; set; }

    public string BeneficiaryUgpassEmail { get; set; }

    public bool? BeneficiaryConsentAcquired { get; set; }

    public string SignaturePhoto { get; set; }

    public string Designation { get; set; }

    public string BeneficiaryStatus { get; set; }

    public string BeneficiaryCreatedOn { get; set; }

    public string BeneficiaryUpdatedOn { get; set; }

    public int? ValidityId { get; set; }

    public int? PrivilegeServiceId { get; set; }

    public bool? ValidityApplicable { get; set; }

    public string ValidFrom { get; set; }

    public string ValidUpto { get; set; }

    public string ValidityStatus { get; set; }

    public string ValidityCreatedOn { get; set; }

    public string ValidityUpdatedOn { get; set; }

    public int? PrivilegeId { get; set; }

    public string PrivilegeServiceName { get; set; }

    public string PrivilegeServiceDisplayName { get; set; }

    public string PrivilegeStatus { get; set; }

    public int? IsChargeable { get; set; }
}
