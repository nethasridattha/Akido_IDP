using System;
using System.Collections.Generic;

namespace DTPortal.Core.Domain.Models.RegistrationAuthority;

public partial class SubscriberPaymentsHistory
{
    public int Id { get; set; }

    public string SubscriberSuid { get; set; }

    public string EncryptedEmail { get; set; }

    public string EncryptedMobileNumber { get; set; }

    public string OrganizationId { get; set; }

    public bool PaymentForOrganization { get; set; }

    public string PaymentInfo { get; set; }

    public double TotalAmount { get; set; }

    public string PaymentChannel { get; set; }

    public string TransactionReferenceId { get; set; }

    public string AggregatorAcknowledgementId { get; set; }

    public string PaymentStatus { get; set; }

    public string CreatedOn { get; set; }

    public string PaymentCategory { get; set; }
}
