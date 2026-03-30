using System;
using System.Collections.Generic;

namespace DTPortal.Core.Domain.Models;

public partial class Purpose
{
    public int Id { get; set; }

    public string Name { get; set; }

    public string DisplayName { get; set; }

    public string Description { get; set; }

    public bool UserConsentRequired { get; set; }

    public DateTime CreatedDate { get; set; }

    public DateTime ModifiedDate { get; set; }

    public string CreatedBy { get; set; }

    public string UpdatedBy { get; set; }

    public string Status { get; set; }
}
