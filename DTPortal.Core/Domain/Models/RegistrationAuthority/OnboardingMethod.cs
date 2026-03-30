using System;
using System.Collections.Generic;

namespace DTPortal.Core.Domain.Models.RegistrationAuthority;

public partial class OnboardingMethod
{
    public string OnboardingMethod1 { get; set; }

    public string LevelOfAssurance { get; set; }

    public string IdDocumentType { get; set; }

    public string SubscriberType { get; set; }

    public int OnboardingMethodId { get; set; }
}
