using DTPortal.Core.Domain.Services.Communication;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTPortal.Core.Utilities
{
    public class PKIIssueCertificateReq
    {
        public string keyID { get; set; }
        public string commonName { get; set; }
        public bool daesCertificate { get; set; }
        public string countryName { get; set; }
    }

    public class PKIIssueCertificateRes
    {
        public string status { get; set; }
        public string certificate { get; set; }
        public string certificate_serial_number { get; set; }
        public string issueDate { get; set; }
        public string expiryDate { get; set; }
        public Key kidData { get; set; }
        public string error_message { get; set; }
        public string error_code { get; set; }
    }

    public class PKIGenerateSignatureReq
    {
        public string keyID { get; set; }
        public bool tokenSign { get; set; }
        public string hash { get; set; }
    }

    public class PKIGenerateSignatureRes
    {
        public string status { get; set; }
        public string signature { get; set; }
        public string error_message { get; set; }
        public string error_code { get; set; }
    }
    public class JWTHeader
    {
        public string alg { get; set; }
        public string kid { get; set; }
        public string typ { get; set; }
    }
}
