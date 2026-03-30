namespace DTPortal.Core.DTOs
{
    public class SoftwareListDTO
    {
        public int SoftwareId { get; set; }
        public string SoftwareName { get; set; }
        public string InstallManual { get; set; }
        public string DownloadLink { get; set; }
        public string FileName { get; set; }
        public string SoftwareVersion { get; set; }
        public string PdfFile { get; set; }
        public string ZipFile { get; set; }
        public string Status { get; set; }
        public string SizeOfSoftware { get; set; }
        public string SizeOfManual { get; set; }
        public string PublishedOn { get; set; }
        public string UpdatedOn { get; set; }
        public string CreatedOn { get; set; }
    }
}
