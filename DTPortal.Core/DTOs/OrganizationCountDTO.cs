using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTPortal.Core.DTOs
{
    public  class OrganizationCountDTO
    {
        public int inactiveOrganizations { get; set; }
        public int activeOrganizations { get; set; }
        public int registeredOrganizations { get; set; }
        public int totalOrganizations { get; set; }
       
    }
}
