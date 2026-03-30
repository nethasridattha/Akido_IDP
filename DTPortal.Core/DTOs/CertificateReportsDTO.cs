using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTPortal.Core.DTOs
{
    public class CertificateReportsDTO
    {
        public string FullName { get; set; }

        public string OnboardingMethod { get; set; }

        public string IdDocNumber { get; set; }

        public string CertificateSerialNumber { get; set; }

        public string CertificateIssueDate { get; set; }

        public string CerificateExpiryDate { get; set; }
    }
}
