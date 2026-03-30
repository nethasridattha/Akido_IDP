using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTPortal.Core.DTOs
{
    public class CredentialOfferResponseDTO
    {
        public Dictionary<string, CredentialDetails> Organizations { get; set; }
    }

    public class CredentialDetails
    {
        public string Id { get; set; }
        public string IssuerName { get; set; }
        public string IssuerKey { get; set; }
        public string IssuerCertificateChain { get; set; }
        public List<SupportedCredentialDetails> SupportedCredentials { get; set; }
    }

    public class SupportedCredentialDetails
    {
        public string CredentialId { get; set; }
        public string CredentialType { get; set; }
        public List<string> Type{ get; set; }
        public string Schema { get; set; }
        public List<string> Format { get; set; }
        public string ProofType { get; set; }
        public Revocation Revocation { get; set; }
    }

    public class Revocation
    {
        public string Type { get; set; }
        public string RevocationListURL { get; set; }
    }

    public class Attribute
    {
        public string AttributeName { get; set; }
        public string DisplayName { get; set; }
        public int DataType { get; set; }
    }

}
