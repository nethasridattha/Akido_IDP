using System;
using System.Collections.Generic;

namespace DTPortal.Core.Domain.Models.RegistrationAuthority;

public partial class SppVgOrganizationDetail
{
    public long Id { get; set; }

    public string CreatedOn { get; set; }

    public string TaxNumber { get; set; }

    public string OrgName { get; set; }

    public string RegNo { get; set; }

    public string OrgType { get; set; }

    public string OrganizationLetter { get; set; }

    public string Ouid { get; set; }

    public string SpocAuthorizationLetter { get; set; }

    public string Status { get; set; }

    public string UpdatedOn { get; set; }

    public string OrgEmail { get; set; }

    public string Address { get; set; }
}
