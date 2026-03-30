using System;
using System.Collections.Generic;

namespace DTPortal.Core.Domain.Models.RegistrationAuthority;

public partial class OrgCertificateEmailCounter
{
    public string OrganizationUid { get; set; }

    public string OrganizationName { get; set; }

    public string SpocUgpassEmail { get; set; }

    public string Counter15Day { get; set; }

    public string Counter10Day { get; set; }

    public string Counter5Day { get; set; }

    public string Counter0Day { get; set; }
}
