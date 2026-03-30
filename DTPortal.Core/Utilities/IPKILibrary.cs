using DTPortal.Core.Domain.Services.Communication;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTPortal.Core.Utilities
{
    public interface IPKILibrary
    {
        // Generates Signature for given data
        public string GenerateSignature(string data);

        // Creates new certificate key pair and returns
        // public key details
        public string IssueCertificate(string certificateRequest);

        // Check for Active Certificate.
        // Create new certificate if it is expired or not exists.
        public int CheckAndCreateCertificate();

        // Create new certificate.
        public Task<int> CreateCertificateAsync(
            PKIIssueCertificateReq pkiIssueCertificate);

        public Task<ServiceResult> GenerateCertificateAsync();
    }
}
