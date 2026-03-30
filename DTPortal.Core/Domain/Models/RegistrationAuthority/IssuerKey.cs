using System;
using System.Collections.Generic;

namespace DTPortal.Core.Domain.Models.RegistrationAuthority;

public partial class IssuerKey
{
    public string IssuerId { get; set; }

    public string IssuerKey1 { get; set; }

    public string IssuerCertificate { get; set; }

    public string CertificateChain { get; set; }
}
