using System;
using System.Collections.Generic;

namespace DTPortal.Core.Domain.Models.RegistrationAuthority;

public partial class ManualCreditsAllocation
{
    public int Id { get; set; }

    public string OrgName { get; set; }

    public string OrganisationId { get; set; }

    public double? AmountReceived { get; set; }

    public string TransactionReferenceId { get; set; }

    public string InvoiceNumber { get; set; }

    public string PaymentChannel { get; set; }

    public string OnlinePaymentGateway { get; set; }

    public string OnlinePaymentGatewayReferenceNum { get; set; }

    public double? NoOfOnboardingCredits { get; set; }

    public double? TotalSigningCredits { get; set; }

    public double? TotalEsealCredits { get; set; }

    public string CreatedOn { get; set; }

    public string AllocationStatus { get; set; }
}
