using System;
using System.Collections.Generic;

namespace DTPortal.Core.Domain.Models.RegistrationAuthority;

public partial class OrgESeal
{
    public int Id { get; set; }

    public int? OrgOnboardingFormId { get; set; }

    public string ESealLogo { get; set; }

    public string ESealPaymentReferenceId { get; set; }

    public string ESealApplyOrRenewConsent { get; set; }

    public string ESealAuthorisationLetter { get; set; }

    public string ESealLastIssuedOn { get; set; }

    public string ESealValidUpTo { get; set; }

    public string CreatedOn { get; set; }

    public string UpdtaedOn { get; set; }

    public virtual OrganisationOnboardingForm OrgOnboardingForm { get; set; }
}
