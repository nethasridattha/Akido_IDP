using System;
using System.Collections.Generic;

namespace DTPortal.Core.Domain.Models.RegistrationAuthority;

public partial class OrgFinancialAuditorDetail
{
    public int Id { get; set; }

    public int? OrgOnboardingFormId { get; set; }

    public string FinancialAuditorName { get; set; }

    public string FinancialAuditorIdDocumentNumber { get; set; }

    public string FinancialAuditorLicenseNum { get; set; }

    public string CreatedOn { get; set; }

    public string UpdatedOn { get; set; }

    public string FinancialAuditorUgpassEmail { get; set; }

    public string FinancialAuditorTinNumber { get; set; }

    public virtual OrganisationOnboardingForm OrgOnboardingForm { get; set; }
}
