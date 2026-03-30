using System;
using System.Collections.Generic;

namespace DTPortal.Core.Domain.Models.RegistrationAuthority;

public partial class OrgCeoDetail
{
    public int Id { get; set; }

    public int? OrgOnboardingFormId { get; set; }

    public string OrgUid { get; set; }

    public string CeoPanTaxNum { get; set; }

    public string CeoName { get; set; }

    public string CeoEmail { get; set; }

    public string CeoIdDocumentNumber { get; set; }

    public virtual OrganisationOnboardingForm OrgOnboardingForm { get; set; }
}
