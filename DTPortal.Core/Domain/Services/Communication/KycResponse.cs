using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTPortal.Core.Domain.Services.Communication
{
    public class KycResponse
    {
        public bool success { get; set; }
        public string message { get; set; }
        public string result { get; set; }
    }
}
