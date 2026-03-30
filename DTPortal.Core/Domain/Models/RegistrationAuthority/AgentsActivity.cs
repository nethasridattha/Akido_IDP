using System;
using System.Collections.Generic;

namespace DTPortal.Core.Domain.Models.RegistrationAuthority;

public partial class AgentsActivity
{
    public int Id { get; set; }

    public string AgentUgpassSuid { get; set; }

    public string AgentsName { get; set; }

    public string AssistedOnboardedSuid { get; set; }

    public string AssistedOnboardedName { get; set; }

    public DateTime? CreatedOn { get; set; }

    public DateTime? UpdatedOn { get; set; }
}
