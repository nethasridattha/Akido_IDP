using System;
using System.Collections.Generic;

namespace DTPortal.Core.Domain.Models;

public partial class PasswordPolicy
{
    public int Id { get; set; }

    public int PasswordHistory { get; set; }

    public int MinimumPwdAge { get; set; }

    public int MaximumPwdAge { get; set; }

    public int MinimumPwdLength { get; set; }

    public int MaximumPwdLength { get; set; }

    public bool IsReversibleEncryption { get; set; }

    public DateTime? CreatedDate { get; set; }

    public DateTime? ModifiedDate { get; set; }

    public int? CreatedBy { get; set; }

    public int? UpdatedBy { get; set; }

    public int? PwdContains { get; set; }

    public int? BadPwdCount { get; set; }

    public string Hash { get; set; }

    public virtual UserTable CreatedByNavigation { get; set; }

    public virtual UserTable UpdatedByNavigation { get; set; }
}
