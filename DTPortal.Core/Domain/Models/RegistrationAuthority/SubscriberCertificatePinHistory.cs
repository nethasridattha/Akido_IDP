using System;
using System.Collections.Generic;

namespace DTPortal.Core.Domain.Models.RegistrationAuthority;

public partial class SubscriberCertificatePinHistory
{
    public int SubscriberCertificatePinHistoryId { get; set; }

    public string AuthPinList { get; set; }

    public DateTime? AuthPinSetDate { get; set; }

    public DateTime? SignPinSetDate { get; set; }

    public string SignPinList { get; set; }

    public string SubscriberUid { get; set; }
}
