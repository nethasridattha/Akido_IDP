using System;
using System.Collections.Generic;

namespace DTPortal.Core.Domain.Models.RegistrationAuthority;

public partial class Fund
{
    public int Id { get; set; }

    public int? ProgramId { get; set; }

    public string ProgramName { get; set; }

    public decimal? TotalDebitFund { get; set; }

    public decimal? TotalFund { get; set; }

    public string Currency { get; set; }

    public decimal? AvailableFund { get; set; }

    public decimal? ReservedFund { get; set; }

    public virtual Program Program { get; set; }

    public virtual ICollection<Transaction> Transactions { get; set; } = new List<Transaction>();
}
