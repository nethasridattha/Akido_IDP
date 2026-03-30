using System;
using System.Collections.Generic;

namespace DTPortal.Core.Domain.Models.RegistrationAuthority;

public partial class SystemRateCardVolume
{
    public long Id { get; set; }

    public long SystemServicePkFk { get; set; }

    public int MinVolume { get; set; }

    public int MaxVolume { get; set; }

    public decimal PricePerTransaction { get; set; }

    public DateTime? CreatedOn { get; set; }

    public DateTime? UpdatedOn { get; set; }

    public DateTime RateEffectiveFrom { get; set; }

    public DateTime? RateEffectiveUpto { get; set; }

    public virtual SystemRateCard SystemServicePkFkNavigation { get; set; }
}
