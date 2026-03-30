using System;
using System.Collections.Generic;

namespace DTPortal.Core.Domain.Models.RegistrationAuthority;

public partial class Config
{
    public int Id { get; set; }

    public int VerificationId { get; set; }

    public string VerificationIdName { get; set; }

    public string Config1 { get; set; }

    public string Connector { get; set; }

    public short? Active { get; set; }
}
