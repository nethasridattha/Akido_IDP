using System;
using System.Collections.Generic;

namespace DTPortal.Core.Domain.Models;

public partial class QrCredentialVerifier
{
    public int Id { get; set; }

    public string CredentialId { get; set; }

    public string OrganizationId { get; set; }

    public string Attributes { get; set; }

    public string Emails { get; set; }

    public string Status { get; set; }

    public string Remarks { get; set; }

    public DateTime? CreatedDate { get; set; }

    public DateTime? UpdatedDate { get; set; }

    public virtual QrCredential Credential { get; set; }
}
