using System;
using System.Collections.Generic;

namespace DTPortal.Core.Domain.Models.RegistrationAuthority;

public partial class WalletIssuer
{
    public string IssuerIdentifier { get; set; }

    public long IssuerCounter { get; set; }

    public string RevocationList { get; set; }

    public string Category { get; set; }
}
