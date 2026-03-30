using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DTPortal.IDP.ViewModel.Oauth2
{
    public class ConsentViewModel  
    {
        public AuthorizationViewModel clientDetails { get; set; }
        public string username { get; set; }
        public string usermail { get; set; }

        public string suid { get; set; }
        public List<string> scopes { get; set; }
        public string SelectedAttributesJson { get; set; }

        public List<ScopeDetails> scopesList { get; set; }
    }
}
