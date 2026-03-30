using System;
using System.Collections.Generic;

namespace DTPortal.Core.Domain.Models.RegistrationAuthority;

public partial class SubscriberCertificateLifeCycle
{
    public int SubscriberCertificateLifeCycleId { get; set; }

    public string CertificateSerialNumber { get; set; }

    public string CertificateStatus { get; set; }

    public string CertificateType { get; set; }

    public DateTime CreatedDate { get; set; }

    public string RevocationReason { get; set; }

    public string SubscriberUid { get; set; }
}
