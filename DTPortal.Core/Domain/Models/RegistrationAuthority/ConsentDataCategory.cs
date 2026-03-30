using System;
using System.Collections.Generic;

namespace DTPortal.Core.Domain.Models.RegistrationAuthority;

public partial class ConsentDataCategory
{
    public long ConsentId { get; set; }

    public string Category { get; set; }

    public virtual ConsentRecord Consent { get; set; }
}
