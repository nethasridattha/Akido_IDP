using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DTPortal.Core.Domain.Models;

namespace DTPortal.Core.Domain.Services.Communication
{
    public class AuthSchemeResponse : BaseResponse<AuthScheme>
    {
        public AuthSchemeResponse(AuthScheme category) : base(category) { }

        public AuthSchemeResponse(string message) : base(message) { }

        public AuthSchemeResponse(AuthScheme category, string message) :
           base(category, message)
        { }
    }
}
