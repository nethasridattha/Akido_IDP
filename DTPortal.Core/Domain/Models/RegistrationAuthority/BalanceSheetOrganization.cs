using System;
using System.Collections.Generic;

namespace DTPortal.Core.Domain.Models.RegistrationAuthority;

public partial class BalanceSheetOrganization
{
    public int Id { get; set; }

    public double TotalCredits { get; set; }

    public double TotalDebits { get; set; }

    public int ServiceId { get; set; }

    public string OrganizationId { get; set; }

    public DateTime CreatedOn { get; set; }

    public DateTime UpdatedOn { get; set; }

    public virtual ServicesDefinition Service { get; set; }
}
