using System;
using System.Collections.Generic;

namespace DTPortal.Core.Domain.Models;

public partial class CredentialVerifier
{
    public int Id { get; set; }

    public string CredentialId { get; set; }

    public string OrganizationId { get; set; }

    public string Attributes { get; set; }

    public string Configuration { get; set; }

    public DateTime? CreatedDate { get; set; }

    public DateTime? UpdatedDate { get; set; }

    public string Status { get; set; }

    public string Emails { get; set; }

    public string Remarks { get; set; }

    public int? Validity { get; set; }

    public string Domains { get; set; }

    public virtual Credential Credential { get; set; }
}
