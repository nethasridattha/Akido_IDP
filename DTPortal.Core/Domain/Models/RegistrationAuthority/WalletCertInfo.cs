using System;
using System.Collections.Generic;

namespace DTPortal.Core.Domain.Models.RegistrationAuthority;

public partial class WalletCertInfo
{
    public int? OrganizationDetailsId { get; set; }

    public string OrganizationUid { get; set; }

    public string OrgName { get; set; }

    public string SpocUgpassEmail { get; set; }

    public DateTime? UpdatedDate { get; set; }

    public DateTime? CertificateIssueDate { get; set; }

    public DateTime? CertificateExpiryDate { get; set; }

    public string CertificateStatus { get; set; }
}
