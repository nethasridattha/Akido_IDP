using System;
using System.Collections.Generic;

namespace DTPortal.Core.Domain.Models.RegistrationAuthority;

public partial class UserKey
{
    public string UserIdentifier { get; set; }

    public string UserKey1 { get; set; }

    public string RevocationList { get; set; }

    public long Counter { get; set; }

    public string UserCert { get; set; }
}
