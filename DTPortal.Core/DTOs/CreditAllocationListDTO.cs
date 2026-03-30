using System;

namespace DTPortal.Core.DTOs
{
    public class CreditAllocationListDTO
    {
        public int Id { get; set; }
        public string OrgName { get; set; }
        public string OrgId { get; set; }
        public double AmountReceived { get; set; }
        public string TransactionRefId { get; set; }
        public string InvoiceNo { get; set; }
        public string PaymentChannel { get; set; }
        public double? TotalSigningCredits { get; set; }
        public double? TotalEsealCredits { get; set; }
        public double? OnboardingCredits { get; set; }
        public DateTime CreatedOn { get; set; }
        public string AllocationStatus { get; set; }
        public string OnlinePaymentGateway { get; set; }
        public string OnlinePaymentGatewayReferenceNo { get; set; }
        public string CreatedBy { get; set; }
        public string UpdatedBy { get; set; }
    }
}
