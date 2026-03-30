using System;
using System.Collections.Generic;

namespace DTPortal.Core.Domain.Models.RegistrationAuthority;

public partial class CycleMembership
{
    public int Id { get; set; }

    public int? ProgramId { get; set; }

    public int? RegistrantId { get; set; }

    public int? CycleId { get; set; }

    public string Status { get; set; }

    public virtual Cycle Cycle { get; set; }

    public virtual Program Program { get; set; }

    public virtual RegistrantDetail Registrant { get; set; }
}
