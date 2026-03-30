namespace DTPortal.Core.DTOs
{
    public class SubscribersCountDTO
    {
        public int TotalSubscribers { get; set; }

        public int ActiveSubscribers { get; set; }

        public int InActiveSubscribers { get; set; }

        public int DisabledSubscribers { get; set; }

        public int CertificateRevokedSubscribers { get; set; }

        public int CertificateExpiredSubscribers { get; set; }

        public int OnboardedSubscribers { get; set; }
    }
}
