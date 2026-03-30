using System;
using System.Collections.Generic;

namespace DTPortal.Core.Domain.Models.RegistrationAuthority;

public partial class SystemRateCard
{
    public long Id { get; set; }

    public string ProjectId { get; set; }

    public string ServiceCode { get; set; }

    public string ServiceName { get; set; }

    public string ServiceType { get; set; }

    public decimal PricePerTransaction { get; set; }

    public decimal? PlatformFeePercent { get; set; }

    public decimal? GovernmentVatPercent { get; set; }

    public int? VatIncludedInPricing { get; set; }

    public int? IsActive { get; set; }

    public DateTime CreatedOn { get; set; }

    public DateTime UpdatedOn { get; set; }

    public DateTime? RateEffectiveFrom { get; set; }

    public DateTime? RateEffectiveUpto { get; set; }

    public virtual ICollection<SystemRateCardCustomPricing> SystemRateCardCustomPricings { get; set; } = new List<SystemRateCardCustomPricing>();

    public virtual ICollection<SystemRateCardVolume> SystemRateCardVolumes { get; set; } = new List<SystemRateCardVolume>();
}
