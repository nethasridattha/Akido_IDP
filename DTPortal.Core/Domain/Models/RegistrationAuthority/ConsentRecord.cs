using System;
using System.Collections.Generic;

namespace DTPortal.Core.Domain.Models.RegistrationAuthority;

public partial class ConsentRecord
{
    public long Id { get; set; }

    public string CitizenName { get; set; }

    public string EmiratesId { get; set; }

    public string ServiceProvider { get; set; }

    public string ConsentType { get; set; }

    public string Purpose { get; set; }

    public DateOnly ValidityStart { get; set; }

    public DateOnly ValidityEnd { get; set; }

    public string Status { get; set; }

    public string ConsentMethod { get; set; }

    public DateTime CreatedDate { get; set; }

    public DateTime? UpdatedDate { get; set; }

    public string RevocationReason { get; set; }

    public DateTime? RevokedDate { get; set; }

    public string DigitalSignature { get; set; }

    public string IpAddress { get; set; }

    public string UserAgent { get; set; }

    public virtual ICollection<ConsentDataCategory> ConsentDataCategories { get; set; } = new List<ConsentDataCategory>();
}
