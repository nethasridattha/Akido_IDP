using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DTPortal.IDP.ViewModel.Saml2
{
    //use for get sp to idp login request / idp to sp logout request (get) data
    public class Saml2RequestQueryParam
    {
        public string SAMLRequest { get; set; }

        public string SigAlg { get; set; }

        public string Signature { get; set; }
    }
}
