using System;
using System.Collections.Generic;

namespace DTPortal.Core.Domain.Models.RegistrationAuthority;

public partial class SppVgTrustedUser
{
    public long Id { get; set; }

    public string CreatedOn { get; set; }

    public string Email { get; set; }

    public string Password { get; set; }

    public string UpdatedOn { get; set; }

    public string Name { get; set; }

    public string MobileNumber { get; set; }
}
