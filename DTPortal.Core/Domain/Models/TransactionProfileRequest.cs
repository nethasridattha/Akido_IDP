using System;
using System.Collections.Generic;

namespace DTPortal.Core.Domain.Models;

public partial class TransactionProfileRequest
{
    public int Id { get; set; }

    public string TransactionId { get; set; }

    public int? ClientId { get; set; }

    public string Suid { get; set; }

    public string RequestDetails { get; set; }

    public string TransactionStatus { get; set; }

    public string FailedReason { get; set; }

    public DateTime CreatedDate { get; set; }

    public DateTime? UpdatedDate { get; set; }

    public virtual Client Client { get; set; }

    public virtual ICollection<TransactionProfileConsent> TransactionProfileConsents { get; set; } = new List<TransactionProfileConsent>();
}
