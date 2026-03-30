using System;
using System.Collections.Generic;

namespace DTPortal.Core.Domain.Models.RegistrationAuthority;

public partial class OrgWallet
{
    public int Id { get; set; }

    public int OrgOnboardingFormId { get; set; }

    public string WalletTransactionReferenceId { get; set; }

    public string WalletConsent { get; set; }

    public DateTime WalletIssuesOn { get; set; }

    public DateTime WalletValidUpto { get; set; }

    public DateTime CreatedOn { get; set; }

    public DateTime UpdatedOn { get; set; }
}
