using DTPortal.Core.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTPortal.Core.Domain.Services.Communication
{
    public class PurposeResponse : BaseResponse<Purpose>
    {
        public PurposeResponse(Purpose category) : base(category) { }

        public PurposeResponse(string message) : base(message) { }

        public PurposeResponse(Purpose category, string message) : base(category, message) { }
    }
}
