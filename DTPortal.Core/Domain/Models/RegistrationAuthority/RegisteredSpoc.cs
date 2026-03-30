using System;
using System.Collections.Generic;

namespace DTPortal.Core.Domain.Models.RegistrationAuthority;

public partial class RegisteredSpoc
{
    public int Id { get; set; }

    public string OrgName { get; set; }

    public string CeoTin { get; set; }

    public string CeoName { get; set; }

    public string SpocUgpassEmail { get; set; }

    public string OrgTin { get; set; }

    public string Status { get; set; }

    public string CreatedOn { get; set; }

    public string UpdatedOn { get; set; }
}
