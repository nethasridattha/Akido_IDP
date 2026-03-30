using System;
using System.Collections.Generic;

namespace DTPortal.Core.Domain.Models.RegistrationAuthority;

public partial class MoneyTransfer
{
    public int Id { get; set; }

    public string PrincipalName { get; set; }

    public string PrincipalEmail { get; set; }

    public string PrincipalIdDocNumber { get; set; }

    public string AgentName { get; set; }

    public string AgentEmail { get; set; }

    public string AgentIdDocNumber { get; set; }

    public string NotaryName { get; set; }

    public string NotaryEmail { get; set; }

    public string NotaryIdDocNumber { get; set; }

    public string PoaSignedDoc { get; set; }

    public DateTime? CreatedOn { get; set; }

    public DateTime? UpdatedOn { get; set; }

    public string ValidUpto { get; set; }
}
