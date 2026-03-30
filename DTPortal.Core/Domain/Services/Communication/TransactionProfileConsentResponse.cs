using DTPortal.Core.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTPortal.Core.Domain.Services.Communication
{
    public class TransactionProfileConsentResponse: BaseResponse<TransactionProfileConsent>
    {
        public TransactionProfileConsentResponse(TransactionProfileConsent category) : base(category) { }

        public TransactionProfileConsentResponse(string message) : base(message) { }

        public TransactionProfileConsentResponse(TransactionProfileConsent category, string message) : base(category, message) { }
    }
}
