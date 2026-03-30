using System;
using System.Collections.Generic;

namespace DTPortal.Core.Domain.Models.RegistrationAuthority;

public partial class OrganizationCertificateLifeCycle
{
    public int OrganizationCertificateLifeCycleId { get; set; }

    public string OrganizationUid { get; set; }

    public string CertificateSerialNumber { get; set; }

    public string CertificateStatus { get; set; }

    public string RevocationReason { get; set; }

    public DateTime? CreatedDate { get; set; }

    public string CertificateType { get; set; }

    public virtual OrganizationDetail OrganizationU { get; set; }
}
