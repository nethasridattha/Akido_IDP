using System;

namespace DTPortal.Core.DTOs
{
    public class OrganizationPaymentHistoryDTO
    {
        public int Id { get; set; }

        public string OrganizationId { get; set; }

        public string OrganizationName { get; set; }

        public string PaymentInfo { get; set; } = "";

        public double TotalAmountPaid { get; set; }

        public string PaymentChannel { get; set; }

        public string TransactionReferenceId { get; set; }

        public string InvoiceNumber { get; set; }

        public DateTime CreatedOn { get; set; }

        public string CreatedBy { get; set; }

        public string UpdatedBy { get; set; }

        public string ApprovedBy { get; set; }
    }
}
