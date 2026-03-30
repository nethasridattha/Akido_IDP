using System;
using System.Collections.Generic;

namespace DTPortal.Core.Domain.Models.RegistrationAuthority;

public partial class BlacklistedVisitor
{
    public int Id { get; set; }

    public string SubscriberName { get; set; }

    public string PassportNumber { get; set; }

    public string CreatedOn { get; set; }

    public string Status { get; set; }
}
