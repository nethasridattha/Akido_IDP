using System;
using System.Collections.Generic;

namespace DTPortal.Core.Domain.Models.RegistrationAuthority;

public partial class PoaCredentialView
{
    public string AgentEmail { get; set; }

    public string AgentName { get; set; }

    public string AgentSuid { get; set; }

    public string AgentIdDocNumber { get; set; }

    public string PoaCredential { get; set; }
}
