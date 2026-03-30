using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DTPortal.IDP.ViewModel.Login
{
    public class LoginViewModel
    {
        public string userName { get; set; }

        public string error { get; set; }

        public string Target { get; set; }

        public string Method { get; set; }

        public string RememberedUser { get; set; }

        public Dictionary<string, AuthSchema> AuthSchemas { get; set; } = new Dictionary<string, AuthSchema>();

    }

    public class AuthSchema
    {
        public string Code { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Icon { get; set; }
    }

}
