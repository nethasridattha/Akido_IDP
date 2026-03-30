using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTPortal.Core.DTOs
{
    public class TinVerificationDTO
    {
        public GetClientRegistrationResponse GetClientRegistrationResponse { get; set; }
    }

    public class GetClientRegistrationResponse
    {
        public GetClientRegistrationResult GetClientRegistrationResult { get; set; }
    }

    public class GetClientRegistrationResult
    {
        public string Country { get; set; }
        public string ErrorCode { get; set; }
        public string ErrorDesc { get; set; }
        public string MobileNumber { get; set; }
        public string PostalAddress { get; set; }
        public string RegistrationStatus { get; set; }
        public string TIN { get; set; }
        public string TaxPayerEmail { get; set; }
        public string TaxPayerName { get; set; }
        public string TypeOfUser { get; set; }
        public string IsCustomsAgent { get; set; }
    }

}
