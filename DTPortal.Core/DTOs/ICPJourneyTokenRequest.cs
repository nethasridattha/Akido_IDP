using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTPortal.Core.DTOs
{
    public class ICPJourneyTokenRequest
    {
        public string suid { get; set; }
        public string journeyType { get; set; }
        public string emiratesIdNumber { get; set; }
        public string uaeKycId { get; set; }
    }
}
