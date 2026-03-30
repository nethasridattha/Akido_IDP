using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTPortal.Core.Services
{

    public class VerifyPinRequest
    {
        public string subscriberDigitalID { get; set; }
        public string authenticationPin { get; set; }
        public string correlationId { get; set; }
    }

    public class VerifyPinResponse
    {
        public bool success { get; set; }
        public string message { get; set; }
        public string result { get; set; }
    }

    public class GenerateSignatureRequest
    {
        public string transactionID { get; set; }
        public string serviceName { get; set; }
        public string startTime { get; set; }
        public string logMessage { get; set; }
        public string logMessageType { get; set; }
        public string transactionType { get; set; }
        public int certType { get; set; }
        public string signingPassword { get; set; }
        public string hash { get; set; }
        public bool hashData { get; set; }
        public string subscriberUniqueId{get;set;}
        public string correlationId { get; set; }

    }

    public class VerifySignatureRequest
    {
        public string transactionID { get; set; }
        public string serviceName { get; set; }
        public string startTime { get; set; }
        public string logMessage { get; set; }
        public string logMessageType { get; set; }
        public string transactionType { get; set; }
        public int certType { get; set; }
        public string subscriberUniqueId { get; set; }
        public bool hashData { get; set; }
        public string random { get; set; }
        public string signature { get; set; }
        public string correlationId { get; set; }
    }
    public class verifyfaceCallstack
    {
        public VerifyFaceRequest callStack { get; set; }
    }
    public class VerifyFaceRequest
    {
        public string storedImage { get; set; }
        public string capturedImage { get; set; }
    }
}
