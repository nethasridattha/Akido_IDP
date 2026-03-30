using System;
using System.Collections.Generic;

namespace DTPortal.Core.Domain.Models.RegistrationAuthority;

public partial class GenericRateCardAdditionalDefinition
{
    public int Id { get; set; }

    public int GenericRateCardId { get; set; }

    public double Tax { get; set; }

    public virtual GenericRateCardDefinition GenericRateCard { get; set; }
}
