using System;
using System.Collections.Generic;

namespace DTPortal.Core.Domain.Models.RegistrationAuthority;

public partial class OrgEmailDomain
{
    public int OrgDomainId { get; set; }

    public string OrganizationUid { get; set; }

    public string EmailDomain { get; set; }

    public bool Status { get; set; }

    public string CreatedOn { get; set; }

    public string UpdatedOn { get; set; }

    public virtual OrganizationDetail OrganizationU { get; set; }
}
