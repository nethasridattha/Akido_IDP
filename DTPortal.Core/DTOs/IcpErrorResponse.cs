using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTPortal.Core.DTOs
{
    public class IcpErrorResponse
    {
        public int? statusCode { get; set; }
        public string message { get; set; }
        public string type { get; set; }
        public string code { get; set; }
        public string signature { get; set; }
    }

}
