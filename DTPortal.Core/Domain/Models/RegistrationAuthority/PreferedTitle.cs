using System;
using System.Collections.Generic;

namespace DTPortal.Core.Domain.Models.RegistrationAuthority;

public partial class PreferedTitle
{
    public int Id { get; set; }

    public string PreferedTitles { get; set; }
}
