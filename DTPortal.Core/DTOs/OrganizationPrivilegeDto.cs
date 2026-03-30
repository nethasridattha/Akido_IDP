using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTPortal.Core.DTOs
{
    public class OrganizationPrivilegeDTO
    {
        public Boolean walletCertificateStatus { get; set; }

        public List<string> privileges { get; set; }
    }
}
