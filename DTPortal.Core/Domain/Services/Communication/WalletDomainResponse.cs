using DTPortal.Core.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTPortal.Core.Domain.Services.Communication
{
    public class WalletDomainResponse : BaseResponse<WalletDomain>
    {
        public WalletDomainResponse(WalletDomain category) : base(category) { }

        public WalletDomainResponse(string message) : base(message) { }

        public WalletDomainResponse(WalletDomain category, string message) : base(category, message) { }
    }
}
