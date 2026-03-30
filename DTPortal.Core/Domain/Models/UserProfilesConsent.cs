using System;
using System.Collections.Generic;

namespace DTPortal.Core.Domain.Models;

public partial class UserProfilesConsent
{
    public int Id { get; set; }

    public string Suid { get; set; }

    public string ClientId { get; set; }

    public string Profile { get; set; }

    public string Attributes { get; set; }

    public DateTime? CreatedDate { get; set; }

    public DateTime? ModifiedDate { get; set; }

    public string Status { get; set; }
}
