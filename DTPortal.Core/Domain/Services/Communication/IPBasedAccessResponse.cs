using DTPortal.Core.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTPortal.Core.Domain.Services.Communication
{
    public class IPBasedAccessResponse : BaseResponse<IpBasedAccess>
    {
        public IPBasedAccessResponse(IpBasedAccess category) : base(category) { }

        public IPBasedAccessResponse(string message) : base(message) { }
    }
}
