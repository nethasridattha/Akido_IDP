using DTPortal.Core.Domain.Services.Communication;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTPortal.Core.Domain.Services
{
    public interface ISDKAuthenticationService
    {
        public Task<VerifyUserResponse> VerifyUser(SDKVerifyUserRequest request);
        public Task<VerifyUserAuthDataResponse> VerifyUserAuthData(
            VerifyUserAuthDataRequest request);
        public Task<ServiceResult> GetVerificationUrl();
        public Task<VerifyUserAuthDataResponse> IsUserVerifiedQrCode
            (VerifyQrRequest verifyQrCodeRequest);
    }
}
