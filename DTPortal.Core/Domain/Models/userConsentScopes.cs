using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTPortal.Core.Domain.Models
{
    public class approved_scopes
    {
        public string scope { get; set; }
        public bool permission { get; set;}
        public string version { get; set; }
        public string created_date { get; set; }
        public List<string> attributes { get; set; }
    }
    public class Scopes
    {
        public IList<approved_scopes> approved_scopes { get; set; }
    }
}
