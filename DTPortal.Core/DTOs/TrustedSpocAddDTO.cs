using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTPortal.Core.DTOs
{
    public class TrustedSpocAddDTO
    {
        public string SpocName { get; set; }
        public string SpocEmail { get; set; }
        public string MobileNo { get; set; }
        public string IdDocumentNo { get; set; }
        public string OrganizationTin { get; set; }
        public string OrganizationName { get; set; }
        public string CeoTin { get; set; }
    }
}
