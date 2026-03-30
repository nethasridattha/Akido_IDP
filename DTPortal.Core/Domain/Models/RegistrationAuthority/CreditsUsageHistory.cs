using System;
using System.Collections.Generic;

namespace DTPortal.Core.Domain.Models.RegistrationAuthority;

public partial class CreditsUsageHistory
{
    public int Id { get; set; }

    public string SubscriberSuid { get; set; }

    public string OrganizationId { get; set; }

    public string TransactionForOrganization { get; set; }

    public string ServiceName { get; set; }

    public string CreatedOn { get; set; }
}
