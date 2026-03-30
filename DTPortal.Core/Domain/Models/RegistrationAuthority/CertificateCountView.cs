using System;
using System.Collections.Generic;

namespace DTPortal.Core.Domain.Models.RegistrationAuthority;

public partial class CertificateCountView
{
    public long? CertCount { get; set; }

    public long? ActiveCertCount { get; set; }

    public long? RevokeCertCount { get; set; }

    public long? ExpiredCertCount { get; set; }
}
