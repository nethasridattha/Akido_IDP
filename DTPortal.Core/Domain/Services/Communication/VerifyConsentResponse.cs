using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTPortal.Core.Domain.Services.Communication
{
    public class VerifyConsentResponse
    {
        public bool Success { get; set; }
        public string Message { get; set; }
        public bool ConsentGiven { get; set; }
        public string Result { get; set; }

    }
}
