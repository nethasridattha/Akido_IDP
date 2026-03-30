using System;
using System.Collections.Generic;

namespace DTPortal.Core.Domain.Models.RegistrationAuthority;

public partial class Cycle
{
    public int Id { get; set; }

    public int ProgramId { get; set; }

    public DateOnly? CycleStartDate { get; set; }

    public DateOnly? CycleEndDate { get; set; }

    public string CycleName { get; set; }

    public string Status { get; set; }

    public virtual ICollection<CycleMembership> CycleMemberships { get; set; } = new List<CycleMembership>();

    public virtual ICollection<Entitlement> Entitlements { get; set; } = new List<Entitlement>();

    public virtual Program Program { get; set; }
}
