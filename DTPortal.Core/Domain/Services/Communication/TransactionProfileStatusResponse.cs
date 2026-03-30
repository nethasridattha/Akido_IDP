using DTPortal.Core.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTPortal.Core.Domain.Services.Communication
{
    public class TransactionProfileStatusResponse : BaseResponse<TransactionProfileRequest>
    {
        public TransactionProfileStatusResponse(TransactionProfileRequest category) : base(category) { }

        public TransactionProfileStatusResponse(string message) : base(message) { }

        public TransactionProfileStatusResponse(TransactionProfileRequest category, string message) : base(category, message) { }
    }
}
