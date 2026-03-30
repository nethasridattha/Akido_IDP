using System;
using System.Collections.Generic;

namespace DTPortal.Core.Domain.Models.RegistrationAuthority;

public partial class SubmitCheckboxConfig
{
    public int Id { get; set; }

    public string FeildDescription { get; set; }

    public string OrgCategory { get; set; }

    public bool? Isenable { get; set; }

    public string OrgCategoryId { get; set; }
}
