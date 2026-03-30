using System;
using System.Collections.Generic;

namespace DTPortal.Core.Domain.Models.RegistrationAuthority;

public partial class SubscriberPaymentHistory
{
    public string SubscriberSuid { get; set; }

    public string PaymentInfo { get; set; }

    public double? TotalAmount { get; set; }

    public string PaymentStatus { get; set; }

    public string PaymentCategory { get; set; }

    public string SubscriberStatus { get; set; }

    public string CreatedOn { get; set; }

    public string OrganizationId { get; set; }

    public string TransactionReferenceId { get; set; }
}
