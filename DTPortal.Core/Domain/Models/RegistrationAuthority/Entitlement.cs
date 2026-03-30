using System;
using System.Collections.Generic;

namespace DTPortal.Core.Domain.Models.RegistrationAuthority;

public partial class Entitlement
{
    public int Id { get; set; }

    public int? ProgramId { get; set; }

    public int? RegistrantId { get; set; }

    public int? CycleId { get; set; }

    public DateOnly? ValidFrom { get; set; }

    public DateOnly? ValidUntil { get; set; }

    public string Ern { get; set; }

    public string Code { get; set; }

    public string Status { get; set; }

    public string Currency { get; set; }

    public decimal? Amount { get; set; }

    public string VideoFile { get; set; }

    public string VideoUploadedOn { get; set; }

    public virtual Cycle Cycle { get; set; }

    public virtual ICollection<PaymentTransaction> PaymentTransactions { get; set; } = new List<PaymentTransaction>();

    public virtual Program Program { get; set; }

    public virtual RegistrantDetail Registrant { get; set; }
}
