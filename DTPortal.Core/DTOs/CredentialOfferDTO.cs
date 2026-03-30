using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTPortal.Core.DTOs
{
    public class CredentialOfferDTO
    {
        public IssuerId IssuerId { get; set; }
    }

    public class IssuerId
    {
        public string IssuerName { get; set; }
        public Dictionary<string, SupportedCredential> SupportedCredentials { get; set; }
    }

    public class SupportedCredential
    {
        public string CredentialName { get; set; }
        public string CredentialType{get;set;}
        public List<string> format { get; set; }
        public string proofType { get; set; }
        public string revocation { get; set; }
        public string TrustUrl { get; set; }
        public List<AttributeData> data { get; set; }

    }
    public class AttributeData
    {
        public string AttributeName { get; set; }
        public string DisplayName { get; set; }
        public int DataType { get; set; }
    }
    public class QrIssuerId
    {
        public string IssuerName { get; set; }
        public Dictionary<string, QrSupportedCredential> SupportedCredentials { get; set; }
    }
    public class QrSupportedCredential
    {
        public string CredentialName { get; set; }
        public string CredentialType { get; set; }
        public List<string> format { get; set; }
        public string proofType { get; set; }
        public string revocation { get; set; }
        public bool faceValidation { get; set; }
        public QrAttributes data { get; set; }

    }
    public class QrAttributes
    {
        public List<AttributeData> publicData { get; set; }
        public List<AttributeData> privateData { get; set; }
    }
}
