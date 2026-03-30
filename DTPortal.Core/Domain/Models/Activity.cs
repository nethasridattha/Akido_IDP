using System;
using System.Collections.Generic;

namespace DTPortal.Core.Domain.Models;

public partial class Activity
{
    public int Id { get; set; }

    public string Category { get; set; }

    public string Name { get; set; }

    public string DisplayName { get; set; }

    public int? ParentId { get; set; }

    public bool McEnabled { get; set; }

    public bool Enabled { get; set; }

    public string Hash { get; set; }

    public bool McSupported { get; set; }

    public bool? IsCritical { get; set; }

    public virtual ICollection<RoleActivity> RoleActivities { get; set; } = new List<RoleActivity>();
}
