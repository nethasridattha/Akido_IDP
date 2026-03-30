namespace DTPortal.Core.DTOs
{
    public class AdminLogReportDTO
    {
        public string _id { get; set; }
        public string ModuleName { get; set; }
        public string ServiceName { get; set; }
        public string ActivityName { get; set; }
        public string Timestamp { get; set; }
        public string LogMessage { get; set; }
        public string LogMessageType { get; set; }
        public string UserName { get; set; }
        public string DataTransformation { get; set; }
        public string Checksum { get; set; }
        public bool IsChecksumValid { get; set; }
        public int __v { get; set; }
    }
}
