using System;
using System.Collections.Generic;

namespace DTPortal.Core.Domain.Models.RegistrationAuthority;

public partial class GenericRateCardDefinition
{
    public int Id { get; set; }

    public int ServiceId { get; set; }

    public double Rate { get; set; }

    public string Stakeholder { get; set; }

    public DateTime? RateEffectiveFrom { get; set; }

    public string Status { get; set; }

    public string RateEffectiveUpto { get; set; }

    public string CreatedBy { get; set; }

    public string UpdatedBy { get; set; }

    public string ApprovedBy { get; set; }

    public virtual ICollection<GenericRateCardAdditionalDefinition> GenericRateCardAdditionalDefinitions { get; set; } = new List<GenericRateCardAdditionalDefinition>();

    public virtual ServicesDefinition Service { get; set; }
}
