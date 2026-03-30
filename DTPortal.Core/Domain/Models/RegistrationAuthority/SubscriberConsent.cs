using System;
using System.Collections.Generic;

namespace DTPortal.Core.Domain.Models.RegistrationAuthority;

public partial class SubscriberConsent
{
    public int Id { get; set; }

    public string SubscriberUid { get; set; }

    public string ConsentData { get; set; }

    public string SignedConsentData { get; set; }

    public int? ConsentId { get; set; }

    public string CreatedOn { get; set; }
}
