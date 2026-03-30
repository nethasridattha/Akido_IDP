using System;
using System.Collections.Generic;

namespace DTPortal.Core.Domain.Models.RegistrationAuthority;

public partial class OrganizationAdditionalDetail
{
    public int Id { get; set; }

    public byte[] OrganizationId { get; set; }

    public short EnablePostPaidOption { get; set; }

    public string SpocUgpassEmail { get; set; }

    public string CreatedOn { get; set; }

    public string UpdatedOn { get; set; }
}
