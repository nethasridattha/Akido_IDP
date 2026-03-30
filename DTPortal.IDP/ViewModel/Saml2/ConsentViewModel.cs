using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DTPortal.IDP.ViewModel.Saml2
{
    public class ConsentViewModel  
    {
        public string client_ID { get; set; }

        public string Application_Name { get; set; }
        public string SAMLResponse { get; set; }
        public string username { get; set; }

        public string suid { get; set; }
        public List<string> scopes { get; set; }

        public List<ScopeDetails> scopesList { get; set; }
    }
}
