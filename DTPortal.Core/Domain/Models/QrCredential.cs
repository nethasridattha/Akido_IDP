using System;
using System.Collections.Generic;

namespace DTPortal.Core.Domain.Models;

public partial class QrCredential
{
    public int Id { get; set; }

    public string CredentialUid { get; set; }

    public string CredentialName { get; set; }

    public string DataAttributes { get; set; }

    public string OrganizationId { get; set; }

    public string CredentialOffer { get; set; }

    public bool? PortraitVerificationRequired { get; set; }

    public string TestVcData { get; set; }

    public string Status { get; set; }

    public string Remarks { get; set; }

    public DateTime? CreatedDate { get; set; }

    public DateTime? UpdatedDate { get; set; }

    public string DisplayName { get; set; }

    public virtual ICollection<QrCredentialVerifier> QrCredentialVerifiers { get; set; } = new List<QrCredentialVerifier>();
}
