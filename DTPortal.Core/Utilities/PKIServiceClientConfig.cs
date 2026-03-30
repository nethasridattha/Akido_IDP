using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTPortal.Core.Utilities
{
    public class PKIServiceClientConfig
    {
        public string BaseAddress { get; set; }
        public string GenerateSignatureUri { get; set; }
        public string VerifySignatureUri { get; set; }
    }
}
