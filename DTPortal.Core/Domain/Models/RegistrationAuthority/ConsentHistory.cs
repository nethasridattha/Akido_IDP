using System;
using System.Collections.Generic;

namespace DTPortal.Core.Domain.Models.RegistrationAuthority;

public partial class ConsentHistory
{
    public int Id { get; set; }

    public int ConsentId { get; set; }

    public string Consent { get; set; }

    public string CreatedOn { get; set; }

    public string OptionalTermsAndConditions { get; set; }

    public string OptionalDataAndPrivacy { get; set; }

    public string ConsentType { get; set; }

    public bool ConsentRequired { get; set; }

    public string PrivacyConsent { get; set; }
}
