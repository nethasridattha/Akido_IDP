using System;
using System.Collections.Generic;

namespace DTPortal.Core.Domain.Models.RegistrationAuthority;

public partial class OrganisationCategory
{
    public int Id { get; set; }

    public string CategoryName { get; set; }

    public string CreatedOn { get; set; }

    public string UpdatedOn { get; set; }

    public string Status { get; set; }

    public string LabelName { get; set; }

    public virtual ICollection<OrganisationFieldMapping> OrganisationFieldMappings { get; set; } = new List<OrganisationFieldMapping>();
}
