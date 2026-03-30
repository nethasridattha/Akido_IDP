using System;
using System.Collections.Generic;

namespace DTPortal.Core.Domain.Models.RegistrationAuthority;

public partial class Consent
{
    public int ConsentId { get; set; }

    public string Consent1 { get; set; }

    public string CreatedOn { get; set; }

    public string UpdatedOn { get; set; }

    public string ConsentType { get; set; }

    public string Status { get; set; }

    public string PrivacyConsent { get; set; }
}
