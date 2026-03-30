using System;
using System.Collections.Generic;

namespace DTPortal.Core.Domain.Models;

public partial class TransactionProfileStatus
{
    public int Id { get; set; }

    public int TransactionId { get; set; }

    public string TransactionStatus { get; set; }

    public string FailedReason { get; set; }

    public string PivotSignedConsent { get; set; }

    public DateTime? CreatedDate { get; set; }

    public DateTime? UpdatedDate { get; set; }

    public int? DatapivotId { get; set; }

    public virtual TransactionProfileRequest Transaction { get; set; }
}
