using System;
using System.Collections.Generic;

namespace DTPortal.Core.Domain.Models;

public partial class Client
{
    public int Id { get; set; }

    public string ClientId { get; set; }

    public string ClientSecret { get; set; }

    public string RedirectUri { get; set; }

    public string GrantTypes { get; set; }

    public string ResponseTypes { get; set; }

    public string ApplicationName { get; set; }

    public string ApplicationType { get; set; }

    public string ApplicationUrl { get; set; }

    public string LogoutUri { get; set; }

    public string Scopes { get; set; }

    public bool? WithPkce { get; set; }

    public string Hash { get; set; }

    public string Type { get; set; }

    public string PublicKeyCert { get; set; }

    public string EncryptionCert { get; set; }

    public DateTime CreatedDate { get; set; }

    public DateTime ModifiedDate { get; set; }

    public string CreatedBy { get; set; }

    public string UpdatedBy { get; set; }

    public string Status { get; set; }

    public string OrganizationUid { get; set; }

    public int? AuthScheme { get; set; }

    public bool? IsKycApplication { get; set; }

    public string ApplicationNameArabic { get; set; }

    public virtual ICollection<EConsentClient> EConsentClients { get; set; } = new List<EConsentClient>();

    public virtual ICollection<TransactionProfileRequest> TransactionProfileRequests { get; set; } = new List<TransactionProfileRequest>();
}
