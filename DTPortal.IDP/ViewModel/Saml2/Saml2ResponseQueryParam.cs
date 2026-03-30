using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DTPortal.IDP.ViewModel.Saml2
{
    //use for get sp to idp logout response (get) data
    public class Saml2ResponseQueryParam
    {
        public string SAMLResponse { get; set; }

        public string SigAlg { get; set; }

        public string Signature { get; set; }

        public string RelayState { get; set; }
    }
}
