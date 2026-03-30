using System;
using System.Collections.Generic;

namespace DTPortal.Core.Domain.Models;

public partial class Credential
{
    public int Id { get; set; }

    public string CredentialUid { get; set; }

    public string CredentialName { get; set; }

    public string DisplayName { get; set; }

    public string VerificationDocType { get; set; }

    public string DataAttributes { get; set; }

    public string AuthenticationScheme { get; set; }

    public string CategoryId { get; set; }

    public string OrganizationId { get; set; }

    public string Logo { get; set; }

    public string ServiceDetails { get; set; }

    public string CredentialOffer { get; set; }

    public string TestVcData { get; set; }

    public string Status { get; set; }

    public string Remarks { get; set; }

    public DateTime? CreatedDate { get; set; }

    public DateTime? UpdatedDate { get; set; }

    public string SignedDocument { get; set; }

    public string TrustUrl { get; set; }

    public int? Validity { get; set; }

    public string Categories { get; set; }

    public string SharingAuthenticationScheme { get; set; }

    public string ViewingAuthenticationScheme { get; set; }

    public virtual ICollection<CredentialVerifier> CredentialVerifiers { get; set; } = new List<CredentialVerifier>();
}
