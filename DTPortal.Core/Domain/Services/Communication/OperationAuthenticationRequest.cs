using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTPortal.Core.Domain.Services.Communication
{
    public class ValidateOperationAuthNRequest
    {
        public string userName { get; set; }
        public string OperationName { get; set; }
    }

    public class VerifyOperationAuthDataRequest
    {
        public string tempSession { get; set; }
        public string authNSchemeName { get; set; }
        public string authData { get; set; }
    }
}
