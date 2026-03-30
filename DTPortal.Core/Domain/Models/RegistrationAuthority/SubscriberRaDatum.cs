using System;
using System.Collections.Generic;

namespace DTPortal.Core.Domain.Models.RegistrationAuthority;

public partial class SubscriberRaDatum
{
    public string SubscriberUid { get; set; }

    public string CommonName { get; set; }

    public string CountryName { get; set; }

    public string CreatedDate { get; set; }

    public string CertificateType { get; set; }

    public string PkiPassword { get; set; }

    public string PkiPasswordHash { get; set; }

    public string PkiUserName { get; set; }

    public string PkiUserNameHash { get; set; }

    public string UpdatedDate { get; set; }
}
