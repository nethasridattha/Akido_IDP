using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DTPortal.IDP.ViewModel.Saml2
{
    // use for send idp to sp login response (post) data
    public class Saml2Response
    {
        public string entityEndpoint { get; set; }

        public string id { get; set; }

        public string context { get; set; }

        public string type { get; set; }

        public string relayState { get; set; }

        public bool ajaxSubmit { get; set; } = false;
    }
}
