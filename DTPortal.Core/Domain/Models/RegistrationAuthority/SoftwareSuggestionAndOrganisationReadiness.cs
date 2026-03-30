using System;
using System.Collections.Generic;

namespace DTPortal.Core.Domain.Models.RegistrationAuthority;

public partial class SoftwareSuggestionAndOrganisationReadiness
{
    public int Id { get; set; }

    public int? OrgOnboardingFormId { get; set; }

    public string BrmSuggestionToOrganisation { get; set; }

    public string SpocOptedFor { get; set; }

    public string DigitalCertificateForEnterpriseGateway { get; set; }

    public string InfraReadinessChecklistSubmittedBySpoc { get; set; }

    public string CreatedOn { get; set; }

    public string UpdatedOn { get; set; }

    public virtual OrganisationOnboardingForm OrgOnboardingForm { get; set; }
}
