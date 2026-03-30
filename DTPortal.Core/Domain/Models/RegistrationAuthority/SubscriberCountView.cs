using System;
using System.Collections.Generic;

namespace DTPortal.Core.Domain.Models.RegistrationAuthority;

public partial class SubscriberCountView
{
    public long? ActiveSubscriberCount { get; set; }

    public long? InactiveSubscriberCount { get; set; }

    public long? OnboardedSubscriberCount { get; set; }

    public long? SubscriberCount { get; set; }

    public long? DisableSubscriberCount { get; set; }

    public long? CertRevokeSubscriberCount { get; set; }

    public long? RegisteredSubscriberCount { get; set; }

    public long? CertExpiredSubscriberCount { get; set; }
}
