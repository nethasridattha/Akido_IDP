using System;
using System.Collections.Generic;

namespace DTPortal.Core.Domain.Models.RegistrationAuthority;

public partial class OrganizationCertificate
{
    public string CertificateSerialNumber { get; set; }

    public string OrganizationUid { get; set; }

    public string PkiKeyId { get; set; }

    public string CertificateData { get; set; }

    public string WrappedKey { get; set; }

    public DateTime CertificateIssueDate { get; set; }

    public DateTime CerificateExpiryDate { get; set; }

    public string CertificateStatus { get; set; }

    public string Remarks { get; set; }

    public DateTime CreatedDate { get; set; }

    public DateTime? UpdatedDate { get; set; }

    public string CertificateType { get; set; }

    public string TransactionReferenceId { get; set; }

    public virtual OrganizationDetail OrganizationU { get; set; }
}
