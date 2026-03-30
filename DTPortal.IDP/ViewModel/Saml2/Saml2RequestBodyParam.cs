using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DTPortal.IDP.ViewModel.Saml2
{
    //use for get sp to idp login request / idp to sp logout request (post) data
    public class Saml2RequestBodyParam
    {
        public string SAMLRequest { get; set; }
        public string RelayState { get; set; }
    }
}
