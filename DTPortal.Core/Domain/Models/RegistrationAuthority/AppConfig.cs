using System;
using System.Collections.Generic;

namespace DTPortal.Core.Domain.Models.RegistrationAuthority;

public partial class AppConfig
{
    public int Id { get; set; }

    public string OsVersion { get; set; }

    public string LatestVersion { get; set; }

    public string MinimumVersion { get; set; }

    public string Updatelink { get; set; }
}
