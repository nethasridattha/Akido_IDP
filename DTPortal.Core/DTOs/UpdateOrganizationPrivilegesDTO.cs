using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTPortal.Core.DTOs
{
    public class UpdateOrganizationPrivilegesDTO
    {
        public string orgId { get; set; }
        public List<string> privileges { get; set; }
        public string modifiedBy { get; set; }
    }
}
