using DTPortal.Core.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTPortal.Core.Domain.Services.Communication
{
    public class WalletPurposeResponse : BaseResponse<WalletPurpose>
    {
        public WalletPurposeResponse(WalletPurpose category) : base(category) { }

        public WalletPurposeResponse(string message) : base(message) { }

        public WalletPurposeResponse(WalletPurpose category, string message) : base(category, message) { }
    }
}
