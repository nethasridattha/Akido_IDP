using System;
using System.Collections.Generic;

namespace DTPortal.Core.Domain.Models.RegistrationAuthority;

public partial class PaymentHistory
{
    public long Id { get; set; }

    public string Email { get; set; }

    public string Mobile { get; set; }

    public string FullName { get; set; }

    public string CustomerType { get; set; }

    public string OrganizationId { get; set; }

    public string TransactionType { get; set; }

    public string Description { get; set; }

    public string TransactionId { get; set; }

    public string PaymentReferenceTransactionId { get; set; }

    public decimal Amount { get; set; }

    public string Status { get; set; }

    public string PaymentMethod { get; set; }

    public DateTime CreatedOn { get; set; }

    public string PaymentCategory { get; set; }

    public string PaymentInfo { get; set; }

    public string PaymentResponse { get; set; }

    public decimal? Vat { get; set; }

    public decimal? TransactionFee { get; set; }

    public decimal? PlatformFee { get; set; }
}
