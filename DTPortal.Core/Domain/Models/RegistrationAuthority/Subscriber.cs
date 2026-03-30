using System;
using System.Collections.Generic;

namespace DTPortal.Core.Domain.Models.RegistrationAuthority;

public partial class Subscriber
{
    public int SubscriberId { get; set; }

    public string SubscriberUid { get; set; }

    public string FullName { get; set; }

    public string MobileNumber { get; set; }

    public string EmailId { get; set; }

    public string DateOfBirth { get; set; }

    public string IdDocType { get; set; }

    public string IdDocNumber { get; set; }

    public string NationalId { get; set; }

    public string CreatedDate { get; set; }

    public string UpdatedDate { get; set; }

    public string OsName { get; set; }

    public string OsVersion { get; set; }

    public string AppVersion { get; set; }

    public string DeviceInfo { get; set; }

    public short IsSmartphoneUser { get; set; }

    public string Title { get; set; }

    public virtual SubscriberDevice SubscriberDevice { get; set; }

    public virtual ICollection<SubscriberDevicesHistory> SubscriberDevicesHistories { get; set; } = new List<SubscriberDevicesHistory>();

    public virtual SubscriberFcmToken SubscriberFcmToken { get; set; }

    public virtual ICollection<SubscriberOnboardingDatum> SubscriberOnboardingData { get; set; } = new List<SubscriberOnboardingDatum>();

    public virtual SubscriberStatus SubscriberStatus { get; set; }
}
