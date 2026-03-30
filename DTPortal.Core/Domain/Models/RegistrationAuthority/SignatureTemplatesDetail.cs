using System;
using System.Collections.Generic;

namespace DTPortal.Core.Domain.Models.RegistrationAuthority;

public partial class SignatureTemplatesDetail
{
    public string OrganizationUid { get; set; }

    public string OrgName { get; set; }

    public string Designation { get; set; }

    public string SignaturePhoto { get; set; }

    public int? TemplateId { get; set; }

    public string SamplePreview { get; set; }
}
