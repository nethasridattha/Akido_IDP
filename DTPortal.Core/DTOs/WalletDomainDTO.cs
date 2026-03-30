using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTPortal.Core.DTOs
{
    public class WalletDomainDTO
    {
        public string displayName { get; set; }
        public string id { get; set; }
        public Dictionary<string, string> purposes {get;set;}
    }
}
