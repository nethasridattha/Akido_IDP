using System;
using System.Collections.Generic;

namespace DTPortal.Core.Domain.Models.RegistrationAuthority;

public partial class SubscriberInfo
{
    public string SubscriberUid { get; set; }

    public string DisplayName { get; set; }

    public string MobileNumber { get; set; }

    public string SubscriberStatus { get; set; }

    public string Email { get; set; }

    public string FcmToken { get; set; }

    public string IdDocumentExpiryDate { get; set; }
}
