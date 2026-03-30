using System;
using System.Collections.Generic;

namespace DTPortal.Core.Domain.Models;

public partial class TransactionProfileConsent
{
    public int Id { get; set; }

    public int? TransactionId { get; set; }

    public string ConsentStatus { get; set; }

    public string ConsentDataSignature { get; set; }

    public string ApprovedProfileAttributes { get; set; }

    public DateTime? CreatedDate { get; set; }

    public DateTime? UpdatedDate { get; set; }

    public string RequestedProfileAttributes { get; set; }

    public virtual TransactionProfileRequest Transaction { get; set; }
}
