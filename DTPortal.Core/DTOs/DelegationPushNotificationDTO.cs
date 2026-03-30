using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTPortal.Core.DTOs
{
    public class DelegationPushNotificationDTO
    {
        public string AccessToken { get; set; }

        public List<string> DelegateeList { get; set; }

        public string Title { get; set; }
        public string Body { get; set; }
        public string ConsentData { get; set; }
        public string Context { get; set; }
        public string Url { get; set; }
        public bool isDelegator { get; set; }

        public bool isIdle { get; set; }
    }
}
