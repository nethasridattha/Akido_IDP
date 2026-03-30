using System;
using System.Collections.Generic;

namespace DTPortal.Core.Domain.Models.RegistrationAuthority;

public partial class PaymentTransaction
{
    public int Id { get; set; }

    public int? EntitlementId { get; set; }

    public string PaymentReference { get; set; }

    public decimal? Amount { get; set; }

    public string Currency { get; set; }

    public DateTime? TransactionDate { get; set; }

    public string Status { get; set; }

    public int? RegistrantId { get; set; }

    public virtual Entitlement Entitlement { get; set; }

    public virtual RegistrantDetail Registrant { get; set; }
}
