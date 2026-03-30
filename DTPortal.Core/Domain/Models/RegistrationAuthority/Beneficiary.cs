using System;
using System.Collections.Generic;

namespace DTPortal.Core.Domain.Models.RegistrationAuthority;

public partial class Beneficiary
{
    public int Id { get; set; }

    public string SponsorDigitalId { get; set; }

    public string SponsorName { get; set; }

    public string SponsorType { get; set; }

    public string SponsorExternalId { get; set; }

    public string BeneficiaryDigitalId { get; set; }

    public string BeneficiaryName { get; set; }

    public string BeneficiaryType { get; set; }

    public int? SponsorPaymentPriorityLevel { get; set; }

    public string BeneficiaryNin { get; set; }

    public string BeneficiaryPassport { get; set; }

    public string BeneficiaryMobileNumber { get; set; }

    public string BeneficiaryOfficeEmail { get; set; }

    public string BeneficiaryUgpassEmail { get; set; }

    public bool BeneficiaryConsentAcquired { get; set; }

    public string SignaturePhoto { get; set; }

    public string Designation { get; set; }

    public string Status { get; set; }

    public string CreatedOn { get; set; }

    public string UpdatedOn { get; set; }

    public virtual ICollection<BeneficiaryValidity> BeneficiaryValidities { get; set; } = new List<BeneficiaryValidity>();
}
