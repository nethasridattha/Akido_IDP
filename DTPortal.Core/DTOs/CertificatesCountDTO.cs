namespace DTPortal.Core.DTOs
{
    public class CertificatesCountDTO
    {
        public int ActiveCertificates { get; set; }

        public int RevokedCertificates { get; set; }

        public int ExpiredCertificates { get; set; }

        public int TotalCertificates { get; set; }
    }
}
