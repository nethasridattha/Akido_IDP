using System;
using System.Collections.Generic;

namespace DTPortal.Core.Domain.Models;

public partial class TimeBasedAccess
{
    public int Id { get; set; }

    public string Name { get; set; }

    public string Description { get; set; }

    public string AccessDenyTimeZone { get; set; }

    public TimeOnly? StartTime { get; set; }

    public TimeOnly? EndTime { get; set; }

    public DateOnly? StartDate { get; set; }

    public DateOnly? EndDate { get; set; }

    public bool? Deny { get; set; }

    public string ApplicableRoles { get; set; }

    public string CreatedBy { get; set; }

    public DateTime? CreatedDate { get; set; }

    public string ModifiedBy { get; set; }

    public DateTime? ModifiedDate { get; set; }

    public string Hash { get; set; }

    public string Status { get; set; }
}
