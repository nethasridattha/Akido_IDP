using System;
using System.Collections.Generic;

namespace DTPortal.Core.Domain.Models.RegistrationAuthority;

public partial class OnboardingLiveliness
{
    public int Id { get; set; }

    public string SubscriberUid { get; set; }

    public string File { get; set; }

    public string RecordedTime { get; set; }

    public string RecordedGeoLocation { get; set; }

    public string VerificationFirst { get; set; }

    public string VerificationSecond { get; set; }

    public string VerificationThird { get; set; }

    public string TypeOfService { get; set; }

    public string Url { get; set; }
}
