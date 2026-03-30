using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTPortal.Core.DTOs
{
    public class BeneficiaryPrivilegeDTO
    {
        public int PrivilegeId { get; set; }
        public string PrivilegeServiceName { get; set; }
        public string PrivilegeServiceDisplayName { get; set; }
        public string Status { get; set; }
        public int isChargeable { get; set; }
    }
}
