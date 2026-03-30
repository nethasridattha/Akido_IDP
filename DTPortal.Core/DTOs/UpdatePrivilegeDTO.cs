using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTPortal.Core.DTOs
{
    public class UpdatePrivilegeDTO
    {
        public int id { get; set; }
        public string orgId { get; set; }
        public string privilege { get; set; }
        public string status { get; set; }
        public string adminName { get; set; }
    }
}
