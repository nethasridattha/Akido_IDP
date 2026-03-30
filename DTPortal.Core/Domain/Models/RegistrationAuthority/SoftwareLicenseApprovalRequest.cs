using System;
using System.Collections.Generic;

namespace DTPortal.Core.Domain.Models.RegistrationAuthority;

public partial class SoftwareLicenseApprovalRequest
{
    public int Id { get; set; }

    public string Ouid { get; set; }

    public string SoftwareName { get; set; }

    public string LicenseType { get; set; }

    public string CreatedDateTime { get; set; }

    public string ApprovalStatus { get; set; }

    public string UpdatedDateTime { get; set; }
}
