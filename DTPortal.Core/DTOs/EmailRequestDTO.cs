using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTPortal.Core.DTOs
{
    public class EmailRequestDTO
    {
        public string EmailId { get; set; }
        public bool Org { get; set; }
        public string Link { get; set; }
        public bool Eseal { get; set; }
        public EsealCertificateDto EsealCertificateDto { get; set; }
        public TrustedStakeholder TrustedStakeholder { get; set; }
        public int Ttl { get; set; }
        public int EmailOtp { get; set; }
    }

    public class EsealCertificateDto
    {
        public string SpocFullName { get; set; }
        public string OrgName { get; set; }
        public string StartDate { get; set; }
        public string EndDate { get; set; }
    }

    public class TrustedStakeholder
    {
        public string ReferenceId { get; set; }
        public string Name { get; set; }
    }
}
