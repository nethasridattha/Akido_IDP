using System;
using System.Collections.Generic;

namespace DTPortal.Core.Domain.Models;

public partial class UserTable
{
    public int Id { get; set; }

    public string Uuid { get; set; }

    public DateOnly? Dob { get; set; }

    public string MailId { get; set; }

    public string Status { get; set; }

    public DateTime CreatedDate { get; set; }

    public DateTime ModifiedDate { get; set; }

    public string FullName { get; set; }

    public string CreatedBy { get; set; }

    public string UpdatedBy { get; set; }

    public int Gender { get; set; }

    public string MobileNo { get; set; }

    public DateTime? LastLoginTime { get; set; }

    public DateTime? CurrentLoginTime { get; set; }

    public DateTime? LockedTime { get; set; }

    public string Hash { get; set; }

    public int? RoleId { get; set; }

    public string AuthScheme { get; set; }

    public string AuthData { get; set; }

    public string OldStatus { get; set; }

    public virtual Role Role { get; set; }
}
