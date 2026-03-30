using System;
using System.Collections.Generic;

namespace DTPortal.Core.Domain.Models.RegistrationAuthority;

public partial class SubscriberStatus
{
    public int SubscriberStatusId { get; set; }

    public string SubscriberUid { get; set; }

    public string SubscriberStatus1 { get; set; }

    public string SubscriberStatusDescription { get; set; }

    public string OtpVerifiedStatus { get; set; }

    public string CreatedDate { get; set; }

    public string UpdatedDate { get; set; }

    public virtual Subscriber SubscriberU { get; set; }
}
