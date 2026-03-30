using System;
using System.Collections.Generic;

namespace DTPortal.Core.Domain.Models;

public partial class Configuration
{
    public int Id { get; set; }

    public string Name { get; set; }

    public string Value { get; set; }

    public string Hash { get; set; }

    public DateTime CreatedDate { get; set; }

    public DateTime ModifiedDate { get; set; }

    public string CreatedBy { get; set; }

    public string UpdatedBy { get; set; }
}
