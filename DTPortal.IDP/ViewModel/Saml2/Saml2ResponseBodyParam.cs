using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DTPortal.IDP.ViewModel.Saml2
{
    // use for get sp to idp logout response(post) data
    public class Saml2ResponseBodyParam
    {
        public string SAMLResponse { get; set; }

        public string RelayState { get; set; }
    }
}
