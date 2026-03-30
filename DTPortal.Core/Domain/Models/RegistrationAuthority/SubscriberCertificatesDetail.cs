using System;
using System.Collections.Generic;

namespace DTPortal.Core.Domain.Models.RegistrationAuthority;

public partial class SubscriberCertificatesDetail
{
    public string SubscriberUid { get; set; }

    public string FullName { get; set; }

    public string IdDocType { get; set; }

    public string IdDocNumber { get; set; }

    public string CreatedDate { get; set; }

    public string OnBoardingMethod { get; set; }

    public string CerificateExpiryDate { get; set; }

    public string CertificateIssueDate { get; set; }

    public string CertificateSerialNumber { get; set; }

    public string CertificateStatus { get; set; }

    public string CertificateType { get; set; }
}
