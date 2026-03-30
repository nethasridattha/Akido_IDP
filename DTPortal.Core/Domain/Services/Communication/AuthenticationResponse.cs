using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTPortal.Core.Domain.Services.Communication
{

    public class verifyUserResult
    {
        public string userName { get; set; }
        public string AuthnToken { get; set; }
        public IList<string> AuthenticationSchemes { get; set; }
        public string RandomCode { get; set; }
        public string Fido2Options { get; set; }
        public string QrCode { get; set; }
        public string VerifierUrl { get; set; }
        public string VerifierCode { get; set; }
        public bool MobileUser { get; set; }
        public string userMail{ get; set; }
        public string JourneyToken { get; set; }
    }

    public class VerifyUserResponse : BaseResponse<verifyUserResult>
    {
        public VerifyUserResponse(verifyUserResult category) : base(category) { }

        public VerifyUserResponse(string message) : base(message) { }
    }

    public class IsUserVerifiedResponse
    {
        public bool Success { get; set; }
        public string Message { get; set; }
        public string Status { get; set; }
    }

    public class GetLoginSessionResponse
    {
        public bool Success { get; set; }
        public string Message { get; set; }
        public string Session { get; set; }
        public string Suid { get; set; }
    }
    public class SendMobileNotificationResponse
    {
        public bool Success { get; set; }
        public string Message { get; set; }
        public string RandomCode { get; set; }
    }
    public class GetAuthZCodeResponse
    {
        public bool Success { get; set; }
        public string Message { get; set; }
        public string AuthorizationCode { get; set; }
        public string State { get; set; }
        public string RedirectUri { get; set; }
    }

    public class GetAccessTokenErrResponse
    {
        public string error { get; set; }
        public string error_description { get; set; }
    }

    public class AccessTokenResponse
    {
        public string accessToken { get; set; }
        public string error { get; set; }
        public string error_description { get; set; }
    }

    public class GetAccessTokenResponse
    {
        public bool Success { get; set; }
        public string error { get; set; }
        public string error_description { get; set; }
        public string access_token { get; set; }
        // Type of access token. Always has the “Bearer” value.
        public string token_type { get; set; }
        // Lifetime (in seconds) of the access token.
        public long expires_in { get; set; }
        // Scopes granted to those to which the access token is associated,
        // separated by spaces.
        public string scopes { get; set; }
        public string id_token { get; set; }
        public string refresh_token { get; set; }
    }

    public class GetUserInfoResponse
    {
        public bool Success { get; set; }
        public string Error { get; set; }
        public string Message { get; set; }
        public string scope { get; set; }
        public string ClientId { get; set; }
        public string UserId { get; set; }
        public DateOnly? Dob { get; set; }
        public string Name { get; set; }
        public int Gender { get; set; }
        public string MobileNo { get; set; }
        public string MailId { get; set; }
        public string Sub { get; set; }
        public string Suid { get; set; }
        public string id_doc_type { get; set; }
        public string id_doc_number { get; set; }
        public string loa { get; set; }

        public string signedResponse { get; set; }
    }

    public class ErrorResponseDTO
    {
        public string error { get; set; }
        public string error_description { get; set; }
    }
    public class daesAdminBasic
    {
        public string uuid { get; set; }
        public string birthdate { get; set; }
        public string name { get; set; }
        public int gender { get; set; }
        public string sub { get; set; }
    }
    public class adminBasicFields
    {
        public string iss { get; set; }
        public string aud { get; set; }
        public string sub { get; set; }
        public daesAdminBasic daes_response { get; set; }

    }

    public class daesAdminProfile
    {
        public string uuid { get; set; }
        public string birthdate { get; set; }
        public string name { get; set; }
        public int gender { get; set; }
        public string phone_number { get; set; }
        public string email { get; set; }
        public string sub { get; set; }
    }
    public class adminProfileFields
    {
        public string iss { get; set; }
        public string aud { get; set; }
        public daesAdminProfile daes_response { get; set; }
        public string sub { get; set; }
    }

    public class daesSubBasic
    {
        public string suid { get; set; }
        public string birthdate { get; set; }
        public string name { get; set; }
        public string gender { get; set; }
        public string sub { get; set; }
    }
    public class subscriberBasicFields
    {
        public string iss { get; set; }
        public string aud { get; set; }
        public string sub { get; set; }
        public daesSubBasic daes_claims { get; set; }
    }

    public class daesSubProfile
    {
        public string suid { get; set; }
        public string birthdate { get; set; }
        public string name { get; set; }
        public string phone { get; set; }
        public string email { get; set; }
        public string gender { get; set; }
        public string id_document_type { get; set; }
        public string id_document_number { get; set; }
        public string loa { get; set; }
        public string country { get; set; }
        public string login_type { get; set; }
        public IList<LoginProfile> login_profile { get; set; }
    }
    public class subscriberProfileFields
    {
        public string iss { get; set; }
        public string aud { get; set; }
        public string sub { get; set; }
        public daesSubProfile daes_claims { get; set; }
    }
    public class VerifyUserAuthDataResponse
    {
        public bool Success { get; set; }
        public string Message { get; set; }
        public verifyUserAuthResult Result { get; set; } = new verifyUserAuthResult();
    }
    public class verifyUserAuthResult
    {
        public string RandomCode { get; set; }
        public string VerifierUrl { get; set; }
        public string QrCode { get; set; }
        public string Fido2Options { get; set; }
        public string AuthorizationCode { get; set; }
    }
}
