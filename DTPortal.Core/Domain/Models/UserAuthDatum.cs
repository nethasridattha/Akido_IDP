using System;
using System.Collections.Generic;

namespace DTPortal.Core.Domain.Models;

public partial class UserAuthDatum
{
    public int Id { get; set; }

    public string UserId { get; set; }

    public string AuthScheme { get; set; }

    public string AuthData { get; set; }

    public DateTime CreatedDate { get; set; }

    public DateTime ModifiedDate { get; set; }

    public string CreatedBy { get; set; }

    public string UpdatedBy { get; set; }

    public int? FailedLoginAttempts { get; set; }

    public string Status { get; set; }

    public DateTime? Expiry { get; set; }

    public bool? Istemporary { get; set; }
}
