namespace DTPortal.Core.DTOs
{
    public class RevokeCertificateRequestDTO
    {
        public int ReasonId { get; set; }

        public string SubscriberUniqueId { get; set; }

        public string Description { get; set; }
    }
}
