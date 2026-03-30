using System;
using System.Collections.Generic;

namespace DTPortal.Core.Domain.Models.RegistrationAuthority;

public partial class PricingSlabDefinition
{
    public int Id { get; set; }

    public int ServiceId { get; set; }

    public double Discount { get; set; }

    public double VolumeRangeFrom { get; set; }

    public double VolumeRangeTo { get; set; }

    public string Stakeholder { get; set; }

    public virtual ServicesDefinition Service { get; set; }
}
