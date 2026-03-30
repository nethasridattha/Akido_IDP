using Google.Apis.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTPortal.Core.Domain.Services.Communication
{
    public class LogAttributesRequest
    {
        public string clientId { get; set; }
        public List<LogProfileInfo> ScopeDetail { get; set; }
    }
}
