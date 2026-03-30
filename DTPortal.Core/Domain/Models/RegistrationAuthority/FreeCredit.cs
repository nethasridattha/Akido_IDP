using System;
using System.Collections.Generic;

namespace DTPortal.Core.Domain.Models.RegistrationAuthority;

public partial class FreeCredit
{
    public int Id { get; set; }

    public double FreeCredits { get; set; }

    public string CreditsType { get; set; }

    public string NotificationMessage { get; set; }

    public string Stakeholder { get; set; }

    public string MaximumLimitMessage { get; set; }
}
