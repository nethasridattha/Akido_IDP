using System;
using System.Collections.Generic;

namespace DTPortal.Core.Domain.Models.RegistrationAuthority;

public partial class SoftwareLicensesHistory
{
    public int Id { get; set; }

    public string Ouid { get; set; }

    public string SoftwareName { get; set; }

    public string CreatedDateTime { get; set; }

    public string UpdatedDateTime { get; set; }

    public string LicenseInfo { get; set; }

    public string IssuedOn { get; set; }

    public string ValidUpto { get; set; }

    public string LicenseType { get; set; }
}
