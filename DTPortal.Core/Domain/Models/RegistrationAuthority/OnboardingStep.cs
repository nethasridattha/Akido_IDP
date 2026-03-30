using System;
using System.Collections.Generic;

namespace DTPortal.Core.Domain.Models.RegistrationAuthority;

public partial class OnboardingStep
{
    public int OnboardingStepId { get; set; }

    public string OnboardingStep1 { get; set; }

    public string OnboardingStepDisplayName { get; set; }

    public string IntegrationUrl { get; set; }

    public int? OnboardingStepThreshold { get; set; }

    public int? AndroidTfliteThreshold { get; set; }

    public int? AndriodDttThreshold { get; set; }

    public int? IosTfliteThreshold { get; set; }

    public int? IosDttThreshold { get; set; }

    public virtual ICollection<MapMethodOnboardingStep> MapMethodOnboardingSteps { get; set; } = new List<MapMethodOnboardingStep>();
}
