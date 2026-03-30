using System;
using System.Collections.Generic;

namespace DTPortal.Core.Domain.Models.RegistrationAuthority;

public partial class PaynovaReturnTransaction
{
    public long Id { get; set; }

    public string PaymentTransactionReference { get; set; }

    public string ClientReference { get; set; }

    public string PaymentReference { get; set; }

    public int? PaymentTransactionInstrument { get; set; }

    public string PaymentTransactionInstrumentName { get; set; }

    public int? Status { get; set; }

    public string StatusName { get; set; }

    public decimal? ServiceAmount { get; set; }

    public decimal? TotalCharges { get; set; }

    public decimal? TotalAmount { get; set; }

    public string CardNumber { get; set; }

    public string PayerCustomerCode { get; set; }

    public DateTime? TransactionDate { get; set; }

    public DateOnly? ReconciliationDate { get; set; }

    public int? ResponseCode { get; set; }

    public DateTime? ResponseDateTime { get; set; }

    public string ResponseDescription { get; set; }

    public string ClientExtra { get; set; }

    public string RawPayload { get; set; }

    public string Source { get; set; }

    public DateTime? CreatedOn { get; set; }

    public DateTime? UpdatedOn { get; set; }
}
