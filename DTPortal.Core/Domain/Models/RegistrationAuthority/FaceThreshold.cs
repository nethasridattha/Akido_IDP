using System;
using System.Collections.Generic;

namespace DTPortal.Core.Domain.Models.RegistrationAuthority;

public partial class FaceThreshold
{
    public int MapMethodOnboardingStepId { get; set; }

    public string MethodName { get; set; }

    public int? TemplateId { get; set; }

    public string OnboardingStep { get; set; }

    public int? Sequence { get; set; }

    public int? OnboardingStepThreshold { get; set; }

    public string IntegrationUrl { get; set; }

    public DateTime? CreatedDate { get; set; }

    public double? IosTfliteThreshold { get; set; }

    public double? IosDttThreshold { get; set; }

    public double? AndroidTfliteThreshold { get; set; }

    public double? AndriodDttThreshold { get; set; }

    public double? WebDttThreshold { get; set; }

    public double? AndroidDlibThreshold { get; set; }

    public double? IosDlibThreshold { get; set; }
}
