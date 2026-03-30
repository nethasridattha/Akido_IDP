using System;
using System.Collections.Generic;

namespace DTPortal.Core.Domain.Models.RegistrationAuthority;

public partial class OrganisationSpocDetail
{
    public int Id { get; set; }

    public int? OrgOnboardingFormId { get; set; }

    public string SpocOfficeEmail { get; set; }

    public string SpocUgpassEmail { get; set; }

    public string SpocUgpassMobNum { get; set; }

    public string SpocIdDocumentNumber { get; set; }

    public string SpocTaxNum { get; set; }

    public bool? SpocOtpVerfyStatus { get; set; }

    public bool? SpocFaceMatchStatus { get; set; }

    public string OrgUid { get; set; }

    public string SpocSuid { get; set; }

    public string SpocFaceCaptured { get; set; }

    public string SpocFaceFromUgpass { get; set; }

    public string CreatedOn { get; set; }

    public string UpdatedOn { get; set; }

    public string SpocName { get; set; }

    public virtual OrganisationOnboardingForm OrgOnboardingForm { get; set; }
}
