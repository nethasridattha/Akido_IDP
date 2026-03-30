using System;
using System.Collections.Generic;

namespace DTPortal.Core.Domain.Models.RegistrationAuthority;

public partial class OrganisationFieldMapping
{
    public int Id { get; set; }

    public int? OrgCategoryId { get; set; }

    public int? OrgFormFieldId { get; set; }

    public bool? Visibility { get; set; }

    public bool? Mandatory { get; set; }

    public string CreatedOn { get; set; }

    public string UpdatedOn { get; set; }

    public bool? Modifiable { get; set; }

    public string FieldName { get; set; }

    public string LabelName { get; set; }

    public virtual OrganisationCategory OrgCategory { get; set; }

    public virtual OrganisationFormField OrgFormField { get; set; }
}
