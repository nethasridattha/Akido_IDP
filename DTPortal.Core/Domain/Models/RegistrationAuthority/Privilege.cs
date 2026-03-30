using System;
using System.Collections.Generic;

namespace DTPortal.Core.Domain.Models.RegistrationAuthority;

public partial class Privilege
{
    public int PrivilegeId { get; set; }

    public string PrivilegeServiceName { get; set; }

    public string PrivilegeServiceDisplayName { get; set; }

    public string Status { get; set; }

    public int? IsChargeable { get; set; }

    public virtual ICollection<BeneficiaryValidity> BeneficiaryValidities { get; set; } = new List<BeneficiaryValidity>();
}
