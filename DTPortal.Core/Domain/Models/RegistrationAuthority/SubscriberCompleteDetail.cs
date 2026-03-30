using System;
using System.Collections.Generic;

namespace DTPortal.Core.Domain.Models.RegistrationAuthority;

public partial class SubscriberCompleteDetail
{
    public string SubscriberUid { get; set; }

    public string FullName { get; set; }

    public string SubscriberType { get; set; }

    public string DateOfBirth { get; set; }

    public string IdDocType { get; set; }

    public string IdDocNumber { get; set; }

    public string CreatedDate { get; set; }

    public string CertificateStatus { get; set; }

    public string DeviceStatus { get; set; }

    public string SubscriberStatus { get; set; }

    public string DeviceRegistrationTime { get; set; }

    public string CerificateExpiryDate { get; set; }

    public string CertificateIssueDate { get; set; }

    public DateTime? SignPinSetDate { get; set; }

    public DateTime? AuthPinSetDate { get; set; }

    public string SelfieUri { get; set; }

    public string SelfieThumbnailUri { get; set; }

    public string OnBoardingTime { get; set; }

    public string OnBoardingMethod { get; set; }

    public string LevelOfAssurance { get; set; }

    public string MobileNumber { get; set; }

    public string EmailId { get; set; }

    public string GeoLocation { get; set; }

    public string Gender { get; set; }

    public string RevocationDate { get; set; }

    public string RevocationReason { get; set; }

    public string CertificateSerialNumber { get; set; }

    public string VideoUrl { get; set; }

    public string VideoType { get; set; }
}
