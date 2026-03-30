using DTPortal.Core.Domain.Services.Communication;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTPortal.Core.Domain.Services
{
    public interface ICertificateIssuanceService
    {
        public Task<ServiceResult> IssueCertificateNew
            (CertificateIssueRequest certificateIssueRequest);
        public Task<ServiceResult> GenerateSignatureAsync
            (SignDataRequest signDataRequest);
    }
}
