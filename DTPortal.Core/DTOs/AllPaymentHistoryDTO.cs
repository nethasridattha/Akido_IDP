using System;

namespace DTPortal.Core.DTOs
{
    public class AllPaymentHistoryDTO
    {

        public int Id { get; set; }

        public string OrganizationId { get; set; }

        public string SubscriberSuid { get; set; }

        public string TransactionReferenceId { get; set; } = string.Empty;

        public string AggregatorAcknowledgementId { get; set; } = string.Empty;

        public string PaymentStatus { get; set; } = string.Empty;

        public double TotalAmount { get; set; }

        public string EncryptedMobileNumber { get; set; }

        public string EncryptedEmail { get; set; }


        public bool PaymentForOrganization { get; set; }
        public string PaymentInfo { get; set; }
        public string PaymentChannel { get; set; }
        public DateTime CreatedOn { get; set; }
        public string PaymentCategory { get; set; }



        public string Timestamp { get; set; }

        public string Status { get; set; }



    }
}
