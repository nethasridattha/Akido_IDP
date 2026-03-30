using System;

namespace DTPortal.Core.DTOs
{
    public class SubscriberPaymentHistoryDTO
    {
        public int Id { get; set; }

        public string SubscriberSuid { get; set; }

        public string EncryptedEmail { get; set; }

        public string EncryptedMobileNumber { get; set; }

        //
        //public int OrganizationId { get; set; }

        public bool PaymentForOrganization { get; set; }

        //
        public string PaymentInfo { get; set; }

        public double TotalAmount { get; set; }

        public string PaymentChannel { get; set; }

        public string TransactionReferenceId { get; set; }

        public string AggregatorAcknowledgementId { get; set; }

        public string PaymentStatus { get; set; }

        public DateTime CreatedOn { get; set; }







        public int PurchaseId { get; set; }

        public string SubscriberUid { get; set; }

        public string PaymentTime { get; set; }

        public int PackageId { get; set; }

        public string PackageCode { get; set; }


        public double TotalAmountPaid { get; set; }

        public double TotalDiscountApplied { get; set; }

        public string PaymentRefNumber { get; set; }

        public string PaymentMode { get; set; }

        public string CreationTime { get; set; }
    }
}
