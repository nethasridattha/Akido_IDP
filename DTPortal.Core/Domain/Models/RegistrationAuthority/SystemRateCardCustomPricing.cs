using System;
using System.Collections.Generic;

namespace DTPortal.Core.Domain.Models.RegistrationAuthority;

public partial class SystemRateCardCustomPricing
{
    public long Id { get; set; }

    public long SystemServicePkFk { get; set; }

    public string StakeholderType { get; set; }

    public string StakeholderName { get; set; }

    public string StakeholderId { get; set; }

    public decimal DiscountPercentage { get; set; }

    public bool? IsActive { get; set; }

    public DateTime CreatedOn { get; set; }

    public DateTime UpdatedOn { get; set; }

    public virtual SystemRateCard SystemServicePkFkNavigation { get; set; }
}
