using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTPortal.Core.Domain.Services.Communication
{
    public class CertificateResult
    {
        public Key KidData { get; set; }
        public CertData CertData { get; set; }
    }
    public class CertData
    {
        public string Certificate { get; set; }
        public string SerialNumber { get; set; }
        public DateTime IssueDate { get; set; }
        public DateTime ExpiryDate { get; set; }
        public string WrappedKey { get; set; }
        public string KeyLabel { get; set; }
    }

    public class CertificateIssueRequest
    {
        public string identifier { get; set; }
        public string certSubject { get; set; }
        public string country { get; set; }
        public string certProcedure { get; set; }
        public bool tokenCert { get; set; }

    }

    public class SignDataRequest
    {
        public string Identifier { get; set; }
        public string DataToSign { get; set; }
        public bool HashData { get; set; }
        public bool TokenCert { get; set; }
    }

    public class SignDataResponse
    {

    }
}
