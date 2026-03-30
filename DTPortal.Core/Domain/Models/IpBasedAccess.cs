using System;
using System.Collections.Generic;

namespace DTPortal.Core.Domain.Models;

public partial class IpBasedAccess
{
    public int Id { get; set; }

    public string Name { get; set; }

    public string Type { get; set; }

    public string Description { get; set; }

    public string Ip { get; set; }

    public bool? Permission { get; set; }

    public string CreatedBy { get; set; }

    public DateTime? CreatedDate { get; set; }

    public string ModifiedBy { get; set; }

    public DateTime? ModifiedDate { get; set; }

    public string Hash { get; set; }
}
