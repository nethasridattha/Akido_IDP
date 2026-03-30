using System;
using System.Collections.Generic;

namespace DTPortal.Core.Domain.Models.RegistrationAuthority;

public partial class Program
{
    public int Id { get; set; }

    public string ProgramName { get; set; }

    public decimal AmountPerCycle { get; set; }

    public string Recurrence { get; set; }

    public bool? OneTime { get; set; }

    public bool? MatchingRegistrants { get; set; }

    public string CreatedOn { get; set; }

    public string StartDate { get; set; }

    public string Status { get; set; }

    public string Currency { get; set; }

    public virtual ICollection<CycleMembership> CycleMemberships { get; set; } = new List<CycleMembership>();

    public virtual ICollection<Cycle> Cycles { get; set; } = new List<Cycle>();

    public virtual ICollection<Entitlement> Entitlements { get; set; } = new List<Entitlement>();

    public virtual ICollection<Fund> Funds { get; set; } = new List<Fund>();

    public virtual ICollection<ProgramMembership> ProgramMemberships { get; set; } = new List<ProgramMembership>();

    public virtual ICollection<Transaction> Transactions { get; set; } = new List<Transaction>();

    public virtual ICollection<ValidationRule> ValidationRules { get; set; } = new List<ValidationRule>();
}
