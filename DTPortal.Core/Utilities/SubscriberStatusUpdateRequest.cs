using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTPortal.Core.Utilities
{
    public class SubscriberStatusUpdateResponse
    {
        public bool success { get; set; }
        public string message { get; set; }
        public string result { get; set; }
    }
    public class SubscriberStatusUpdateRequest
    {
        public string description { get; set; }
        public string subscriberStatus { get; set; }
        public string subscriberUniqueId { get; set; }
    }


}
