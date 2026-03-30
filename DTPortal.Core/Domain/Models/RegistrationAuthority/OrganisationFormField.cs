using System;
using System.Collections.Generic;

namespace DTPortal.Core.Domain.Models.RegistrationAuthority;

public partial class OrganisationFormField
{
    public int Id { get; set; }

    public string FieldName { get; set; }

    public string CreatedOn { get; set; }

    public string UpdatedOn { get; set; }

    public string LabelName { get; set; }

    public virtual ICollection<OrganisationFieldMapping> OrganisationFieldMappings { get; set; } = new List<OrganisationFieldMapping>();
}
