using System;
using System.Collections.Generic;

namespace DTPortal.Core.Domain.Models;

public partial class ProvisionStatus
{
    public int Id { get; set; }

    public string Suid { get; set; }

    public string CredentialId { get; set; }

    public string DocumentId { get; set; }

    public string Status { get; set; }

    public DateTime? CreatedDate { get; set; }

    public DateTime? UpdatedDate { get; set; }
}
