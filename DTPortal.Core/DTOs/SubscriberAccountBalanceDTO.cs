namespace DTPortal.Core.DTOs
{
    public class SubscriberAccountBalanceDTO
    {
        public int AccountBalanceId { get; set; }

        public string SubscriberUid { get; set; }

        public int TotalSigningCredits { get; set; }

        public int TotalSigningDebits { get; set; }

        public int TotalEsealCredits { get; set; }

        public int TotalEsealDebits { get; set; }

        public string LastTransactionTime { get; set; }

        public string CreationTime { get; set; }

        public string ModificationTime { get; set; }
    }
}
