using System;
using System.Collections.Generic;

namespace DTPortal.Core.Domain.Models.RegistrationAuthority;

public partial class AgentUrl
{
    public int Id { get; set; }

    public string Dt { get; set; }

    public string Nita { get; set; }

    public string Prod { get; set; }
}
