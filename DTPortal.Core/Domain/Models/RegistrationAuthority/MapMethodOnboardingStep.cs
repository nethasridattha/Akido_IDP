using System;
using System.Collections.Generic;

namespace DTPortal.Core.Domain.Models.RegistrationAuthority;

public partial class MapMethodOnboardingStep
{
    public int MapMethodOnboardingStepId { get; set; }

    public string MethodName { get; set; }

    public int? TemplateId { get; set; }

    public string OnboardingStep { get; set; }

    public int? Sequence { get; set; }

    public int? OnboardingStepThreshold { get; set; }

    public string IntegrationUrl { get; set; }

    public DateTime? CreatedDate { get; set; }

    public int? IosTfliteThreshold { get; set; }

    public int? IosDttThreshold { get; set; }

    public int? AndroidTfliteThreshold { get; set; }

    public int? AndriodDttThreshold { get; set; }

    public virtual OnboardingStep OnboardingStepNavigation { get; set; }

    public virtual SubscriberOnboardingTemplate Template { get; set; }
}
