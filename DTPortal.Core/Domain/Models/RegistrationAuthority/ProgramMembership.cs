using System;
using System.Collections.Generic;

namespace DTPortal.Core.Domain.Models.RegistrationAuthority;

public partial class ProgramMembership
{
    public int Id { get; set; }

    public int? RegistrantId { get; set; }

    public int? ProgramId { get; set; }

    public string Status { get; set; }

    public virtual Program Program { get; set; }

    public virtual RegistrantDetail Registrant { get; set; }
}
