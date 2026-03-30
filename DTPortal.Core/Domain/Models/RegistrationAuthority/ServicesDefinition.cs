using System;
using System.Collections.Generic;

namespace DTPortal.Core.Domain.Models.RegistrationAuthority;

public partial class ServicesDefinition
{
    public int Id { get; set; }

    public string ServiceName { get; set; }

    public string ServiceDisplayName { get; set; }

    public string Status { get; set; }

    public short PricingSlabApplicable { get; set; }

    public virtual ICollection<BalanceSheetOrganization> BalanceSheetOrganizations { get; set; } = new List<BalanceSheetOrganization>();

    public virtual ICollection<BalanceSheetSubscriber> BalanceSheetSubscribers { get; set; } = new List<BalanceSheetSubscriber>();

    public virtual ICollection<GenericRateCardDefinition> GenericRateCardDefinitions { get; set; } = new List<GenericRateCardDefinition>();

    public virtual ICollection<OrganizationPricingSlabDefinition> OrganizationPricingSlabDefinitions { get; set; } = new List<OrganizationPricingSlabDefinition>();

    public virtual ICollection<PricingSlabDefinition> PricingSlabDefinitions { get; set; } = new List<PricingSlabDefinition>();
}
