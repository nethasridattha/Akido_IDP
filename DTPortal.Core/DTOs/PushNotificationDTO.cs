using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTPortal.Core.DTOs
{
    public class PushNotificationDTO
    {
        public string AccessToken { get; set; }
        public string Suid { get; set; }
        public string Email { get; set; }
        public string Title { get; set; }
        public string Body { get; set; }
        public string Text { get; set; }
        public string Context { get; set; }
        public string Url { get; set; }
    }
}
