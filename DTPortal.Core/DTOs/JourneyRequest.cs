using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTPortal.Core.DTOs
{
    public class JourneyRequest
    {
        public string journeyType { get; set; }
        public string emiratesIdNumber { get; set; }
        public string uaeKycId { get; set; }
        public PassportDetails passportDetails { get; set; }
        public string consent { get; set; }
    }

    public class PassportDetails
    {
        public string passportNumber { get; set; }
        public string passportType { get; set; }
        public string nationality { get; set; }
    }

}
