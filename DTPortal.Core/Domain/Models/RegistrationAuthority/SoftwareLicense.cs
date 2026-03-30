using System;
using System.Collections.Generic;

namespace DTPortal.Core.Domain.Models.RegistrationAuthority;

public partial class SoftwareLicense
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

    public string FirstActivated { get; set; }

    public string LastActivated { get; set; }

    public string LicenceStatus { get; set; }

    public string ApplicationName { get; set; }

    public string OrgName { get; set; }
}
