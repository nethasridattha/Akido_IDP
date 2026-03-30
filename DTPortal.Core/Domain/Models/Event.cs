using System;
using System.Collections.Generic;

#nullable disable

namespace DTPortal.Core.Domain.Models
{
    public partial class Event
    {
        public int Id { get; set; }
        public string Subject { get; set; }
        public string Action { get; set; }
        public string Description { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string IpAddress { get; set; }
        public string MacAddress { get; set; }
        public string TimeZone { get; set; }
        public string Result { get; set; }
    }
}
