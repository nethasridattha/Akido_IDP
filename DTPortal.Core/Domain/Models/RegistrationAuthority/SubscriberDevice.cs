using System;
using System.Collections.Generic;

namespace DTPortal.Core.Domain.Models.RegistrationAuthority;

public partial class SubscriberDevice
{
    public int SubscriberDeviceId { get; set; }

    public string SubscriberUid { get; set; }

    public string DeviceUid { get; set; }

    public string DeviceStatus { get; set; }

    public string CreatedDate { get; set; }

    public string UpdatedDate { get; set; }

    public virtual Subscriber SubscriberU { get; set; }
}
