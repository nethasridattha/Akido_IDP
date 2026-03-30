using System;
using System.Collections.Generic;

namespace DTPortal.Core.Domain.Models.RegistrationAuthority;

public partial class Transaction
{
    public int Id { get; set; }

    public int? FundId { get; set; }

    public int? ProgramId { get; set; }

    public decimal? DeductedAmount { get; set; }

    public decimal? RemainingFund { get; set; }

    public DateTime? TransactionDate { get; set; }

    public string Currency { get; set; }

    public string Description { get; set; }

    public string TransactionType { get; set; }

    public virtual Fund Fund { get; set; }

    public virtual Program Program { get; set; }
}
