using System;
using System.Collections.Generic;

namespace DTPortal.Core.Domain.Models.RegistrationAuthority;

public partial class OrganizationPrivilege
{
    public int Id { get; set; }

    public string OrganizationId { get; set; }

    public string OrganizationName { get; set; }

    public string Privilege { get; set; }

    public string CreatedBy { get; set; }

    public string CreatedOn { get; set; }

    public string Status { get; set; }

    public string ModifiedBy { get; set; }

    public string ModifiedOn { get; set; }
}
