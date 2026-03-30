using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTPortal.Core.Domain.Services.Communication
{
    public class Attributes
    {
        public string name { get; set; }
        public DateTime created_time { get; set; }
        public DateTime expiry_time { get; set;}
        public int duration { get; set; }
    }
}
