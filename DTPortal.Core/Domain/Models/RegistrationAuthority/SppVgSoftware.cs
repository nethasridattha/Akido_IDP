using System;
using System.Collections.Generic;

namespace DTPortal.Core.Domain.Models.RegistrationAuthority;

public partial class SppVgSoftware
{
    public int SoftwareId { get; set; }

    public string FileName { get; set; }

    public string SoftwareVersion { get; set; }

    public string DownloadLink { get; set; }

    public string InstallManual { get; set; }

    public string SoftwareName { get; set; }

    public string SizeOfSoftware { get; set; }

    public string Status { get; set; }

    public string CreatedOn { get; set; }

    public string UpdatedOn { get; set; }

    public string PublishedOn { get; set; }
}
