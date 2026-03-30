using System;
using System.Collections.Generic;

namespace DTPortal.Core.Domain.Models.RegistrationAuthority;

public partial class PhotoFeature
{
    public string Suid { get; set; }

    public string SubscriberName { get; set; }

    public string SubscriberPhoto { get; set; }

    public byte[] PhotoFeatures { get; set; }

    public string SubscriberDataJson { get; set; }
}
