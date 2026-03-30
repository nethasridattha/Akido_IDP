using DTPortal.Core.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTPortal.Core.Domain.Services.Communication
{
    public class ScopeResponse : BaseResponse<Scope>
    {
        public ScopeResponse(Scope category) : base(category) { }

        public ScopeResponse(string message) : base(message) { }

        public ScopeResponse(Scope category, string message) : base(category, message){ }
    }
}
