using System;
using System.Collections.Generic;

namespace DTPortal.Core.Domain.Models.RegistrationAuthority;

public partial class DaesCertificate
{
    public string CertificateSerialNumber { get; set; }

    public string CertificateData { get; set; }

    public DateTime CerificateExpiryDate { get; set; }

    public DateTime CertificateIssueDate { get; set; }

    public string CertificateStatus { get; set; }

    public string CertificateType { get; set; }

    public DateTime CreatedDate { get; set; }

    public DateTime? UpdatedDate { get; set; }
}
