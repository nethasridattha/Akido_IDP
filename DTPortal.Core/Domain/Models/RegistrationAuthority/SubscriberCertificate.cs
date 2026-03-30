using System;
using System.Collections.Generic;

namespace DTPortal.Core.Domain.Models.RegistrationAuthority;

public partial class SubscriberCertificate
{
    public string CertificateSerialNumber { get; set; }

    public string CertificateData { get; set; }

    public string CerificateExpiryDate { get; set; }

    public string CertificateIssueDate { get; set; }

    public string CertificateStatus { get; set; }

    public string CertificateType { get; set; }

    public string CreatedDate { get; set; }

    public string PkiKeyId { get; set; }

    public string Remarks { get; set; }

    public string SubscriberUid { get; set; }

    public string UpdatedDate { get; set; }
}
