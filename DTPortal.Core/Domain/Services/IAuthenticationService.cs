using DTPortal.Core.Domain.Models;
using DTPortal.Core.Domain.Services.Communication;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static DTPortal.Common.CommonResponse;

namespace DTPortal.Core.Domain.Services
{
    public interface IAuthenticationService
    {
        Task<Response> GetVerifierUrl();
        Response ValidateClient(ValidateClientRequest requestObj);
        Task<Response> ValidateSession(string globalSessionId);
        Task<Response> CustomValidateSession(string globalSessionId);
        Task<VerifyUserResponse> ICPVerifyUser(VerifyUserRequest request);
        Task<VerifyUserResponse> VerifyUser(VerifyUserRequest request);
        Task<VerifyUserAuthDataResponse> VerifyUserAuthData(VerifyUserAuthDataRequest request);
        Task<IsUserVerifiedResponse> IsUserVerified(string authNToken);
        Task<GetLoginSessionResponse> GetLoginSession(string authNToken);
        Task<GetAuthZCodeResponse> GetAuthorizationCode(GetAuthZCodeRequest request);
        Task<GetAccessTokenResponse> GetAccessToken(GetAccessTokenRequest request, string authHeader, string type);
        Task<Response> LogoutUser(LogoutUserRequest request);
        Task<SendMobileNotificationResponse> SendMobileNotification(string authnToken);
        Task<Response> VerifyUserAuthNData(
                   VerifyUserAuthNDataRequest request);
        Task<VerifyUserAuthenticationDataResponse> VerifyUserAuthenticationData(
            VerifyUserAuthenticationDataRequest request);

        Task<VerifyUserAuthDataResponse> IsUserVerifiedQrCode(VerifyQrCodeRequest verifyQrCodeRequest);

        Task<VerifyUserResponse> ChangeAuthScheme(string authScheme, string temporarySession);

        Task<Response> VerifyAgentConsent(
            VerifyAgentConsentRequest request);
        Task<Response> VerifyUserAuthentication(
            VerifyUserAuthenticationRequest request);

        Task<ServiceResult> ICPLoginVerify(ICPAuthRequest request);
        Task<Response> LogAttributes
            (string GlobalSessionId, LogAttributesRequest request);

        Task<VerifyConsentResponse> IsUserGivenConsent
            (string GlobalSessionId,string ClientId);

        Task<VerifyConsentResponse> SendConsentDeniedLogMessage
            (string GlobalSessionId, string ClientId);

        Task<Response> SendAuthenticationLogMessage
            (string GlobalSessionId, string ClientId);

        Task<ServiceResult> ICPLoginSuidVerify(ICPAuthSuidRequest request);

        string GetLocalizedDeviceName(string userAgent, string language);
    }
}
