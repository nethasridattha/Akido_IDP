using System;
using System.Collections.Generic;

namespace DTPortal.Core.Domain.Models.RegistrationAuthority;

public partial class OrganizationWrappedKey
{
    public string CertificateSerialNumber { get; set; }

    public string WrappedKey { get; set; }
}
