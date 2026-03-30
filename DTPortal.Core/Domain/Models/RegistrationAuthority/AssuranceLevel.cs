using System;
using System.Collections.Generic;

namespace DTPortal.Core.Domain.Models.RegistrationAuthority;

public partial class AssuranceLevel
{
    public string AssuranceLevel1 { get; set; }

    public int? AssuranceLevelValue { get; set; }

    public DateTime? CreatedDate { get; set; }
}
