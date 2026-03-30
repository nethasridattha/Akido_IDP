using System;
using System.Collections.Generic;

namespace DTPortal.Core.Domain.Models.RegistrationAuthority;

public partial class SppMetaSoftwareName
{
    public long Id { get; set; }

    public string DisplayName { get; set; }

    public string Value { get; set; }
}
