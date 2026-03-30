using System;
using System.Collections.Generic;

namespace DTPortal.Core.Domain.Models.RegistrationAuthority;

public partial class OrgClientAppConfig
{
    public int Id { get; set; }

    public string OrgId { get; set; }

    public string AppId { get; set; }

    public string ConfigValue { get; set; }

    public string Status { get; set; }

    public string CreatedOn { get; set; }

    public string UpdatedOn { get; set; }
}
