using System;
using System.Collections.Generic;

namespace DTPortal.Core.Domain.Models.RegistrationAuthority;

public partial class OrgSignatureTemplate
{
    public int Id { get; set; }

    public string OrganizationUid { get; set; }

    public int? TemplateId { get; set; }

    public string Type { get; set; }

    public virtual OrganizationDetail OrganizationU { get; set; }
}
