using System;
using System.Collections.Generic;

namespace DTPortal.Core.Domain.Models.RegistrationAuthority;

public partial class SubscriberSimDatum
{
    public int SubscriberSimId { get; set; }

    public string SubscriberUid { get; set; }

    public string Passport { get; set; }

    public string SimNo { get; set; }

    public string MobileNumber { get; set; }

    public string AlternateMobileNumber { get; set; }

    public string FullName { get; set; }

    public string Age { get; set; }

    public string Status { get; set; }

    public string Gender { get; set; }

    public string Email { get; set; }

    public string Nation { get; set; }

    public string SelfieImage { get; set; }

    public byte[] SimForm { get; set; }

    public string SimDataJson { get; set; }

    public string CreatedDate { get; set; }

    public string VisitorIdCardNumber { get; set; }
}
