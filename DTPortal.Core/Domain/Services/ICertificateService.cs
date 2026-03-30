using DTPortal.Core.Domain.Models;
using DTPortal.Core.Domain.Services.Communication;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static DTPortal.Common.CommonResponse;

namespace DTPortal.Core.Domain.Services
{
    public interface ICertificateService
    {
        public Task<CertificateResponse> DeactivateCertificateAsync(int id);
        public CertificateResponse DeactivateCertificate(int id);
        public  Task<Certificate> GetIdpActiveCertificateAsync();
    }
}
