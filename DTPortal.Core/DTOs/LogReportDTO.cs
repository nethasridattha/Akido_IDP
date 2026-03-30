namespace DTPortal.Core.DTOs
{
    public class LogReportDTO
    {
        public string _id { get; set; }
        public string Identifier { get; set; }
        public string CorrelationID { get; set; }
        public string TransactionID { get; set; }
        public string SubTransactionID { get; set; }
        public string Timestamp { get; set; }
        public string StartTime { get; set; }
        public string EndTime { get; set; }
        public string GeoLocation { get; set; }
        public string DeviceId { get; set; }
        public string CallStack { get; set; }
        public string ServiceName { get; set; }
        public string TransactionType { get; set; }
        public string TransactionSubType { get; set; }
        public string LogMessageType { get; set; }
        public string AuthenticationType { get; set; }
        public string LogMessage { get; set; }
        public string ServiceProviderName { get; set; }
        public string ServiceProviderAppName { get; set; }
        public string SignatureType { get; set; }
        public bool ESealUsed { get; set; }
        public string Checksum { get; set; }
        public bool IsChecksumValid { get; set; }

        public string userActivityType { get; set; }
        public int __v { get; set; }
    }
}
