using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTPortal.Core.DTOs
{
    public class MdlProfileDTO
    {
        public string name { get; set; }
        public string birthdate { get; set; }
        public string gender { get; set; }
        public string email { get; set; }
        public string phoneNumber { get; set; }
        public string documentType { get; set; }
        public string documentNumber { get; set; }
        public string photo { get; set; }
        public string issueDate { get; set; }
        public string expiryDate { get; set; }
        public string issuingCountry { get; set; }
        public string issuingAuthority { get; set; }
        public string drivingLicenseNumber { get; set; }
        public string mdlDocument { get; set; }
        public string internationalPermit { get; set; }

    }
}
