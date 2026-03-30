using System;
using System.Collections.Generic;

namespace DTPortal.Core.Domain.Models.RegistrationAuthority;

public partial class SubscriberFcmToken
{
    public int SubscriberFcmTokenId { get; set; }

    public string SubscriberUid { get; set; }

    public string FcmToken { get; set; }

    public string CreatedDate { get; set; }

    public virtual Subscriber SubscriberU { get; set; }
}
