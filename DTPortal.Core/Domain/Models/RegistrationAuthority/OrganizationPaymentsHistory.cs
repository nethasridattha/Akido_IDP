using System;
using System.Collections.Generic;

namespace DTPortal.Core.Domain.Models.RegistrationAuthority;

public partial class OrganizationPaymentsHistory
{
    public int Id { get; set; }

    public string OrganizationId { get; set; }

    public string PaymentInfo { get; set; }

    public double TotalAmountPaid { get; set; }

    public string PaymentChannel { get; set; }

    public string TransactionReferenceId { get; set; }

    public string InvoiceNumber { get; set; }

    public string CreatedOn { get; set; }
}
