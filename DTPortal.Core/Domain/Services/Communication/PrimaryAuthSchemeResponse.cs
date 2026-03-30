using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DTPortal.Core.Domain.Models;

namespace DTPortal.Core.Domain.Services.Communication
{
    public class PrimaryAuthSchemeResponse : BaseResponse<PrimaryAuthScheme>
    {
        public PrimaryAuthSchemeResponse(PrimaryAuthScheme category) : base(category) { }

        public PrimaryAuthSchemeResponse(string message) : base(message) { }

        public PrimaryAuthSchemeResponse(PrimaryAuthScheme category, string message) :
           base(category, message)
        { }
    }
}
