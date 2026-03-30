using System;
using System.Collections.Generic;

namespace DTPortal.Core.Domain.Models.RegistrationAuthority;

public partial class SchemaDatum
{
    public string IssuerId { get; set; }

    public string ProfileType { get; set; }

    public string Schema { get; set; }
}
