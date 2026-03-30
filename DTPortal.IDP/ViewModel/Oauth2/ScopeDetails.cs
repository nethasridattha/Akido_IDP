using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DTPortal.IDP.ViewModel.Oauth2
{
    public class ScopeDetails
    {
        public string name { get; set; }

        public string displayName { get; set; }

        public List<AttributeDetails> attributes { get; set; }

        public string version { get; set; }

        public string description { get; set; }
    }

    public class AttributeDetails { 
        public string name { get; set; }
        public string displayName { get; set; }
        public bool mandatory { get; set; } 
    }
}
