using System;
using System.Collections.Generic;

namespace DTPortal.Core.Domain.Models;

public partial class RoleActivity
{
    public int Id { get; set; }

    public int RoleId { get; set; }

    public int ActivityId { get; set; }

    public bool IsChecker { get; set; }

    public bool LocationOnlyAccess { get; set; }

    public bool NativeAccess { get; set; }

    public bool WebAccess { get; set; }

    public DateTime CreatedDate { get; set; }

    public DateTime ModifiedDate { get; set; }

    public string CreatedBy { get; set; }

    public string UpdatedBy { get; set; }

    public string Hash { get; set; }

    public string GeoLocCoordinates { get; set; }

    public bool? IsEnabled { get; set; }

    public virtual Activity Activity { get; set; }

    public virtual Role Role { get; set; }
}
