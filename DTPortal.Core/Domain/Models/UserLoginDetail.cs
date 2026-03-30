using System;
using System.Collections.Generic;

namespace DTPortal.Core.Domain.Models;

public partial class UserLoginDetail
{
    public int Id { get; set; }

    public string UserId { get; set; }

    public bool IsReversibleEncryption { get; set; }

    public string LastAuthData { get; set; }

    public bool? IsScrambled { get; set; }

    public DateTime? BadLoginTime { get; set; }

    public int PriAuthSchId { get; set; }

    public int? WrongPinCount { get; set; }

    public int? WrongCodeCount { get; set; }

    public int? DeniedCount { get; set; }
}
