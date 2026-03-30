using System;
using System.Collections.Generic;

namespace DTPortal.Core.Domain.Models.RegistrationAuthority;

public partial class SubscriberCertificateView
{
    public string Email { get; set; }

    public string CertificateSerialNumber { get; set; }

    public string SubscriberUid { get; set; }
}
