using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DTPortal.IDP.ViewModel.Saml2
{
    // use for send idp to sp login request(post) data
    public class Saml2LoginRequest
    {
        public string SAMLResponse { get; set; }
    }
}
