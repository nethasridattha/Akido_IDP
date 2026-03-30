using System;
using System.Collections.Generic;

namespace DTPortal.Core.Domain.Models.RegistrationAuthority;

public partial class SppVgPasswordResetToken
{
    public long Id { get; set; }

    public string Token { get; set; }

    public string Email { get; set; }

    public string ExpiryTime { get; set; }

    public bool? Used { get; set; }
}
