using System;
using System.Collections.Generic;

namespace DTPortal.Core.Domain.Models;

public partial class WalletConsent
{
    public int Id { get; set; }

    public string Suid { get; set; }

    public string CredentialId { get; set; }

    public string ApplicationId { get; set; }

    public string ConsentData { get; set; }

    public string Status { get; set; }

    public DateTime CreatedDate { get; set; }

    public DateTime? UpdatedDate { get; set; }
}
