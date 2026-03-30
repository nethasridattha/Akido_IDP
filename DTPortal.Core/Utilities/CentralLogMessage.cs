using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTPortal.Core.Utilities
{
    public class LogMessage
    {
        public string identifier { get; set; }
        public string correlationID { get; set; }
        public string transactionID { get; set; }
        public string subTransactionID { get; set; }
        public string startTime { get; set; }
        public string endTime { get; set; }
        public string geoLocation { get; set; }
        public string callStack { get; set; }
        public string serviceName { get; set; }
        public string transactionType { get; set; }
        public string transactionSubType { get; set; }
        public string logMessageType { get; set; }
        public string logMessage { get; set; }
        public string serviceProviderName { get; set; }
        public string serviceProviderAppName { get; set; }
        public string signatureType { get; set; }
        public bool eSealUsed { get; set; }
        public string checksum { get; set; }
        public string userActivityType { get; set; }
        public string authenticationType { get; set; }
        public string activityType { get; set; }
    }
}
