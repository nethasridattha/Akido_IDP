using System;
using System.Collections.Generic;

namespace DTPortal.Core.Domain.Models.RegistrationAuthority;

public partial class SubscriberView
{
    public string SubscriberUid { get; set; }

    public string DateOfBirth { get; set; }

    public string IdDocType { get; set; }

    public string IdDocNumber { get; set; }

    public string DisplayName { get; set; }

    public string MobileNumber { get; set; }

    public string Email { get; set; }

    public short? IsSmartphoneUser { get; set; }

    public string OrgEmailsList { get; set; }

    public string CertificateStatus { get; set; }

    public string SubscriberType { get; set; }

    public string SubscriberStatus { get; set; }

    public string FcmToken { get; set; }

    public string Loa { get; set; }

    public string Gender { get; set; }

    public string DateOfExpiry { get; set; }

    public string CreatedDate { get; set; }

    public string SelfieUri { get; set; }

    public string Country { get; set; }

    public string NationalId { get; set; }
    public string UaeKycId { get; set; }

    public string Selfie { get; set; }

    public string LanguagePreferred { get; set; }
}
