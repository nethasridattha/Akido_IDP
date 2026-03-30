using System;
using System.Collections.Generic;

namespace DTPortal.Core.Domain.Models.RegistrationAuthority;

public partial class WalletCertificateNative
{
    public string OrgName { get; set; }

    public string OrganizationUid { get; set; }

    public string CertificateSerialNumber { get; set; }

    public string PkiKeyId { get; set; }

    public string CertificateData { get; set; }

    public string WrappedData { get; set; }

    public DateTime? CertificateIssueDate { get; set; }

    public DateTime? CertificateExpiryDate { get; set; }
}
