using System;
using System.Collections.Generic;

namespace DTPortal.Core.Domain.Models.RegistrationAuthority;

public partial class BeneficiaryValidity
{
    public int Id { get; set; }

    public int BeneficiaryId { get; set; }

    public int? PrivilegeServiceId { get; set; }

    public bool? ValidityApplicable { get; set; }

    public string ValidFrom { get; set; }

    public string ValidUpto { get; set; }

    public string Status { get; set; }

    public string CreatedOn { get; set; }

    public string UpdatedOn { get; set; }

    public virtual Beneficiary Beneficiary { get; set; }

    public virtual Privilege PrivilegeService { get; set; }
}
