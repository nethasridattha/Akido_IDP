using DTPortal.Core.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTPortal.Core.Domain.Services.Communication
{
    public class TransactionProfileRequestResponse: BaseResponse<TransactionProfileRequest>
    {
        public TransactionProfileRequestResponse(TransactionProfileRequest category) : base(category) { }

        public TransactionProfileRequestResponse(string message) : base(message) { }

        public TransactionProfileRequestResponse(TransactionProfileRequest category, string message) : base(category, message) { }
    }
}
