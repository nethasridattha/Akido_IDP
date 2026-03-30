using DTPortal.Core.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTPortal.Core.Domain.Services.Communication
{
    public class CertificateResponse : BaseResponse<Certificate>
    {
        public CertificateResponse(Certificate category) : base(category) { }

        public CertificateResponse(string message) : base(message) { }
    }
}
