using System;
using System.Collections.Generic;

namespace DTPortal.Core.Domain.Models.RegistrationAuthority;

public partial class QrIssuer
{
    public string IssuerId { get; set; }

    public string IssuerKey { get; set; }
}
