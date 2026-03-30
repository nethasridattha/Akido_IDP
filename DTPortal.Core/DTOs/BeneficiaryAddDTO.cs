using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTPortal.Core.DTOs
{
    public class BeneficiaryAddDTO
    {
        public string sponsorDigitalId { get; set; }
        public string sponsorName { get; set; }
        public string sponsorType { get; set; }
        public string sponsorExternalId { get; set; }
        public string beneficiaryDigitalId { get; set; }
        public string beneficiaryType { get; set; }
        public string beneficiaryName { get; set; }
        public string beneficiaryNin { get; set; }
        public string beneficiaryPassport { get; set; }
        public string beneficiaryMobileNumber { get; set; }
        public string beneficiaryOfficeEmail { get; set; }
        public string beneficiaryUgpassEmail { get; set; }
        public string signaturePhoto { get; set; }
        public string designation { get; set; }
        public List<BeneficiaryValidityDto> beneficiaryValidities { get; set; }
    }
    public class BeneficiaryValidityDto
    {
        //public int BeneficiaryId { get; set; }
        public int privilegeServiceId { get; set; }
        public bool validityApplicable { get; set; }
        public DateTime? validFrom { get; set; }
        public DateTime? validUpTo { get; set; }
        public string status { get; set; }
    }

}
