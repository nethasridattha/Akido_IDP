using System;
using System.Collections.Generic;

namespace DTPortal.Core.Domain.Models.RegistrationAuthority;

public partial class PoaCredential
{
    public int Id { get; set; }

    public string AgentSuid { get; set; }

    public string AgentName { get; set; }

    public string AgentEmail { get; set; }

    public string AgentIdDocNumber { get; set; }

    public string PoaCredential1 { get; set; }

    public string CreatedOn { get; set; }

    public string UpdatedOn { get; set; }

    public string Status { get; set; }

    public string AgentPhoto { get; set; }
}
