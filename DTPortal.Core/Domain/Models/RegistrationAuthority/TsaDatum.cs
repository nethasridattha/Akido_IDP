using System;
using System.Collections.Generic;

namespace DTPortal.Core.Domain.Models.RegistrationAuthority;

public partial class TsaDatum
{
    public int Id { get; set; }

    public byte[] Certificate { get; set; }

    public byte[] PrivateKey { get; set; }
}
