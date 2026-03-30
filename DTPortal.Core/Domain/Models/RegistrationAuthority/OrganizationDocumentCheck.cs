using System;
using System.Collections.Generic;

namespace DTPortal.Core.Domain.Models.RegistrationAuthority;

public partial class OrganizationDocumentCheck
{
    public int OrganizationdocCheckId { get; set; }

    public string OrganizationUid { get; set; }

    public string DocumentCheckBox { get; set; }

    public virtual OrganizationDetail OrganizationU { get; set; }
}
