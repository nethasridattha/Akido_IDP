using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTPortal.Core.Domain.Services.Communication
{
    public class VerifyUserAuthenticationDataResult
    {
        public string ProvisionUrl;
    }
    public class VerifyUserAuthenticationDataResponse : BaseResponse<VerifyUserAuthenticationDataResult>
    {
        public VerifyUserAuthenticationDataResponse(VerifyUserAuthenticationDataResult category) : base(category) { }

        public VerifyUserAuthenticationDataResponse(string message) : base(message) { }
    }
}
