using System;
using System.Collections.Generic;

namespace DTPortal.Core.Domain.Models.RegistrationAuthority;

public partial class TrustedUser
{
    public int Id { get; set; }

    public string Email { get; set; }

    public string Name { get; set; }

    public string Mobile { get; set; }

    public short Status { get; set; }
}
