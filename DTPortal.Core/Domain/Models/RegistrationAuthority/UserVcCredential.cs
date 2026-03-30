using System;
using System.Collections.Generic;

namespace DTPortal.Core.Domain.Models.RegistrationAuthority;

public partial class UserVcCredential
{
    public int Id { get; set; }

    public string Suid { get; set; }

    public string DocType { get; set; }

    public string VcData { get; set; }

    public long Index { get; set; }

    public string RlcUrl { get; set; }
}
