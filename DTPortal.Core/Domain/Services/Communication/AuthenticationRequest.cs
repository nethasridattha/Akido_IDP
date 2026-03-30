using DTPortal.Core.Domain.Models;
using Fido2NetLib;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using static DTPortal.Common.CommonResponse;

namespace DTPortal.Core.Domain.Services.Communication
{

    public class clientDetails
    {
        public string clientId { get; set; }
        public string scopes { get; set; }
        public string redirect_uri { get; set; }
        public string response_type { get; set; }
        public string grant_type { get; set; }
        public string nonce { get; set; }
        public string state { get; set; }
        public bool withPkce { get; set; }
    }

    public class GetAuthSessClientDetails
    {
        public string clientId { get; set; }
        public string scopes { get; set; }
        public string redirect_uri { get; set; }
        public string response_type { get; set; }
        public bool withPkce { get; set; }
    }
    public class Pkcedetails
    {
        public string codeChallenge { get; set; }
        public string codeChallengeMethod { get; set; }
    }

    public class ClientDetails
    {
        public string ClientId { get; set; }
        public string ExpiresAt { get; set; }
        public string Scopes { get; set; }
        public string GrantType { get; set; }
        public string RedirectUrl { get; set; }
        public string ResponseType { get; set; }
        public string AppName { get; set; }
        public string AppNameAr { get; set; }
    }

    public class TemporarySession
    {
        public string TemporarySessionId { get; set; }
        public string UserId { get; set; }
        public string DisplayName { get; set; }
        public string IpAddress { get; set; }
        public string MacAddress { get; set; }
        public IList<string> PrimaryAuthNSchemeList { get; set; }
        public IList<string> AuthNSuccessList { get; set; }
        public string LastAccessTime { get; set; }
        public string TypeOfDevice { get; set; }
        public string AdditionalValue { get; set; }
        public string UserAgentDetails { get; set; }
        public ClientDetails Clientdetails { get; set; }
        public string RandomCode { get; set; }
        public bool AllAuthNDone { get; set; }
        public bool withPkce { get; set; }
        public Pkcedetails PkceDetails { get; set; }
        public string AuthNStartTime { get; set; }
        public string CoRelationId { get; set; }
        public IList<LoginProfile> LoginProfile { get; set; }
        public List<ScopeAndClaimsInfo> allowedScopesAndClaims { get; set; }
        public string LoginType { get; set; }
        public string DocumentId { get; set; }
        public int TransactionId { get; set; }
        public string DeviceToken { get; set; }
        public string NotificationAdditionalValue { get; set; }
        public bool NotificationAuthNDone { get; set; }
        public string PreferredLanguage { get; set; }
        public string JourneyToken { get; set; }
    }

    public class OperationsDetails
    {
        public string OperationName { get; set; }
        public DateTime AuthenticatedTime { get; set; }
    }

    public class ScopeAndClaimsInfo
    {
        public string name { get; set; }
        public bool claimsPresent { get; set; }
        public List<string> claims { get; set; }
    }

    public class GlobalSession
    {
        public string GlobalSessionId { get; set; }
        public string UserId { get; set; }
        public string FullName { get; set; }
        public string IpAddress { get; set; }
        public string MacAddress { get; set; }
        public string AuthenticationScheme { get; set; }
        public string LoggedInTime { get; set; }
        public string LastAccessTime { get; set; }
        public string TypeOfDevice { get; set; }
        public string AdditionalValue { get; set; }
        public string UserAgentDetails { get; set; }
        public IList<string> ClientId { get; set; }
        public List<ClientAttributes> AcceptedAttributes { get; set; }
        public IList<OperationsDetails> OperationsDetails { get; set; }
        public List<string> LoggedClients { get; set; }
        public string CoRelationId { get; set; }
        public string LoginType { get; set; }
        public IList<LoginProfile> LoginProfile { get; set; }
    }

    public class ClientAttributes
    {
        public string ClientId { get; set; }
        public List<string> Attributes {  get; set; }
    }

    public class LoginProfile
    {
        public string Email { get; set; }
        public string OrgnizationId { get; set; }

    }

    public class UserSessions
    {
        public IList<string> GlobalSessionIds { get; set; }
    }

    public class ClientSessions
    {
        public IList<string> GlobalSessionIds { get; set; }
    }

    public class Authorizationcode
    {
        public string AuthZCode { get; set; }
        public string GlobalSessionId { get; set; }
        public string ClientId { get; set; }
        public string ExpiresAt { get; set; }
        public string Scopes { get; set; }
        public string RedirectUrl { get; set; }
        public string ResponseType { get; set; }
        public bool withPkce { get; set; }
        public string Nonce { get; set; }
        public string State { get; set; }
        public Pkcedetails PkceDetails { get; set; }
    }

    public class Accesstoken
    {
        public string GlobalSessionId { get; set; }
        public string UserId { get; set; }
        public string AccessToken { get; set; }
        public int ExpiresAt { get; set; }
        public string ClientId { get; set; }
        public string Scopes { get; set; }
        public string RefreshToken { get; set; }
        public string RefreshTokenExpiresAt { get; set; }
        public string GrantType { get; set; }
        public string RedirectUrl { get; set; }
        public List<string> AcceptedAttributes { get; set; }
    }

    public class Refreshtoken
    {
        public string RefreshToken { get; set; }
        public string AccessToken { get; set; }
        public string RefreshTokenExpiresAt { get; set; }
        public string ClientId { get; set; }
        public string GlobalSession { get; set; }
        public string RedirectUrl { get; set; }
        public string Scopes { get; set; }
        public string Nonce { get; set; }
    }

    public class ValidateClientRequest
    {
        public GetAuthSessClientDetails clientDetails { get; set; }
        public Pkcedetails PkceDetails { get; set; }
        public Client clientDetailsInDb { get; set; }
    }
    [JsonUnmappedMemberHandling(JsonUnmappedMemberHandling.Disallow)]
    public class VerifyUserRequest
    {
        [Required]
        [MinLength(6)]
        [MaxLength(254)]
        [RegularExpression(
        @"^(?:(?=.{1,64}@)[A-Za-z0-9_%+-]+(?:\.[A-Za-z0-9_%+-]+)*@" +
        @"(?:[A-Za-z0-9](?:[A-Za-z0-9-]{0,61}[A-Za-z0-9])?\.)+" +
        @"[A-Za-z]{2,63}" +
        @"|\+[1-9]\d{7,14}" +
        @"|784-?\d{4}-?\d{7}-?\d)$",
        ErrorMessage = "Invalid Emirates ID / Email / Mobile Number"
        )]
        public string userInput { get; set; }

        [Required]
        [Range(1, 6)]
        public int type { get; set; }

        [Required]
        [StringLength(100, MinimumLength = 1)]
        [RegularExpression(@"^[\x20-\x7E]+$", ErrorMessage = "Invalid Client ID")]
        public string clientId { get; set; }

        [Required]
        [Range(1, 10, ErrorMessage = "Invalid client type.")]
        public int clientType { get; set; }

        [StringLength(45)]
        [RegularExpression(
            @"^((25[0-5]|2[0-4]\d|[01]?\d\d?)\.){3}(25[0-5]|2[0-4]\d|[01]?\d\d?)$|" +
            @"^([0-9a-fA-F]{1,4}:){7}[0-9a-fA-F]{1,4}$",
    ErrorMessage = "Invalid IP address format.")]
        public string ip { get; set; }

        [Required]
        [StringLength(512)]
        [RegularExpression(@"^[\x20-\x7E]+$", ErrorMessage = "Invalid User Agent")]
        public string userAgent { get; set; }

        [Required]
        [StringLength(30)]
        [RegularExpression(@"^[A-Za-z]+$", ErrorMessage = "Invalid device type.")]
        public string typeOfDevice { get; set; }

        [Required]
        public bool rememberUser { get; set; }
    }

    [JsonUnmappedMemberHandling(JsonUnmappedMemberHandling.Disallow)]
    public class SDKVerifyUserRequest
    {
        [Required]
        [MinLength(6)]
        [MaxLength(254)]
        [RegularExpression(
            @"^(?:(?=.{1,64}@)[A-Za-z0-9_%+-]+(?:\.[A-Za-z0-9_%+-]+)*@" +
            @"(?:[A-Za-z0-9](?:[A-Za-z0-9-]{0,61}[A-Za-z0-9])?\.)+" +
            @"[A-Za-z]{2,63}" +
            @"|\+[1-9][0-9]{7,14}" +
            @"|784-?[0-9]{4}-?[0-9]{7}-?[0-9])$",
            ErrorMessage = "Invalid Emirates ID / Email / Mobile Number")]
        public string userInput { get; set; }

        [Required]
        [Range(1, 6)]
        public int type { get; set; }

        [Required]
        [StringLength(100, MinimumLength = 1)]
        [RegularExpression(@"^[A-Za-z0-9._\-]+$",
            ErrorMessage = "Invalid Client ID")]
        public string clientId { get; set; }

        [Required]
        [Range(1, 10, ErrorMessage = "Invalid client type.")]
        public int clientType { get; set; }

        [StringLength(45, MinimumLength = 7)]
        [RegularExpression(
            @"^((25[0-5]|2[0-4]\d|[01]?\d\d?)\.){3}(25[0-5]|2[0-4]\d|[01]?\d\d?)$|" +
            @"^([0-9a-fA-F]{1,4}:){7}[0-9a-fA-F]{1,4}$",
            ErrorMessage = "Invalid IP address format.")]
        public string ip { get; set; }

        [Required]
        [StringLength(512, MinimumLength = 1)]
        [RegularExpression(@"^[A-Za-z0-9 \./\(\);:,_\-\+]+$",
            ErrorMessage = "Invalid User Agent")]
        public string userAgent { get; set; }

        [Required]
        [StringLength(30, MinimumLength = 1)]
        [RegularExpression(@"^[A-Za-z]+$",
            ErrorMessage = "Invalid device type.")]
        public string typeOfDevice { get; set; }

        [Required]
        public bool rememberUser { get; set; }
    }

    [JsonUnmappedMemberHandling(JsonUnmappedMemberHandling.Disallow)]
    public class VerifyUserAuthDataRequest
    {
        [Required]
        [StringLength(50, MinimumLength = 30)]
        [RegularExpression(@"^[A-Za-z0-9\-_\.]+$", ErrorMessage = "AuthnToken can contain only letters, numbers, hyphen, underscore, and dot.")]
        public string AuthnToken { get; set; }

        [Required]
        [StringLength(50, MinimumLength = 1)]
        [RegularExpression(@"^[A-Za-z0-9_\-]+$",
            ErrorMessage = "AuthenticationScheme must not contain spaces or invalid characters.")]
        public string authenticationScheme { get; set; }

        [StringLength(50)]
        [RegularExpression(@"^[A-Za-z0-9\-/+=]*$", ErrorMessage = "AuthenticationData must be a valid Base64-like string or UUID.")]
        public string authenticationData { get; set; }

        public bool approved { get; set; }

        [StringLength(3, MinimumLength = 3)]
        [RegularExpression(@"^[0-9]+$", ErrorMessage = "RandomCode must contain only English numeric digits (0-9).")]
        public string randomCode { get; set; }

        [StringLength(20,MinimumLength =1)]
        [RegularExpression(@"^[A-Za-z0-9\-]+$", ErrorMessage = "DocumentNumber can contain only letters, numbers, and hyphen.")]
        public string documentNumber { get; set; }

        [Range(-2, 8)]
        public int statusCode { get; set; }
    }

    public class GetAccessTokenRequest
    {
        // Optional authorization code
        [StringLength(100)]
        [RegularExpression(@"^$|^[A-Za-z0-9\-\._~]*$", ErrorMessage = "Invalid authorization code format.")]
        public string code { get; set; }

        // Required grant_type
        [Required]
        [StringLength(100)]
        [RegularExpression(@"^[A-Za-z_:]+$", ErrorMessage = "Invalid grant_type.")]
        public string grant_type { get; set; }

        // Optional redirect URI
        [StringLength(1000)]
        [RegularExpression(@"^$|^[A-Za-z][A-Za-z0-9+\-.]*:\/\/[^\s]*$", ErrorMessage = "Invalid redirect_uri format.")]
        public string redirect_uri { get; set; }

        // Required client_id
        [Required]
        [StringLength(100)]
        [RegularExpression(@"^[A-Za-z0-9\-\._]+$", ErrorMessage = "Invalid client_id format.")]
        public string client_id { get; set; }

        // Optional client secret
        [StringLength(200)]
        [RegularExpression(@"^$|^[A-Za-z0-9\-\._~\+\/=]*$", ErrorMessage = "Invalid client_secret format.")]
        public string client_secret { get; set; }

        // Optional scope (allow empty, allow space-separated valid characters)
        [StringLength(500)]
        [RegularExpression(@"^$|^(?=.*[A-Za-z0-9])[A-Za-z0-9 ._:\-/]*$", ErrorMessage = "Invalid scope format.")]
        public string scope { get; set; }

        // Optional PKCE code_verifier
        [StringLength(200)]
        [RegularExpression(@"^$|^[A-Za-z0-9\-\._~]*$", ErrorMessage = "Invalid PKCE code_verifier.")]
        public string code_verifier { get; set; }

        // Optional client assertion type
        [StringLength(100)]
        [RegularExpression(@"^$|^urn:ietf:params:oauth:client-assertion-type:jwt-bearer$", ErrorMessage = "Invalid client_assertion_type.")]
        public string client_assertion_type { get; set; }

        // Optional JWTs (client_assertion)
        [StringLength(5000)]
        [RegularExpression(@"^$|^[A-Za-z0-9-_]+\.[A-Za-z0-9-_]+\.[A-Za-z0-9-_]+$", ErrorMessage = "Invalid JWT client_assertion.")]
        public string client_assertion { get; set; }

        // Optional JWTs (assertion)
        [StringLength(2000)]
        [RegularExpression(@"^$|^[A-Za-z0-9-_]+\.[A-Za-z0-9-_]+\.[A-Za-z0-9-_]+$", ErrorMessage = "Invalid JWT assertion.")]
        public string assertion { get; set; }

        // Optional refresh_token (allow URL-safe characters)
        [MaxLength(2000)]
        [RegularExpression(@"^$|^[A-Za-z0-9\-\._~\+\/=%]*$", ErrorMessage = "Invalid refresh token format.")]
        public string refresh_token { get; set; }
    }

    public class LogoutUserRequest
    {
        public string GlobalSession { get; set; }
    }
    public class GetTokenResponse
    {
        public string access_token { get; set; }
        // Type of access token. Always has the “Bearer” value.
        public string token_type { get; set; }
        // Lifetime (in seconds) of the access token.
        public long expires_in { get; set; }
        // Scopes granted to those to which the access token is associated,
        // separated by spaces.
        public string scopes { get; set; }

        public string id_token { get; set; }
    }
    public class GetAuthZCodeRequest
    {
        public string GlobalSessionId { get; set; }
        public clientDetails ClientDetails { get; set; }
        public Pkcedetails pkcedetails { get; set; }
    }
    public class GetAuthNSessReq
    {
        public GetAuthSessClientDetails clientDetails { get; set; }
        public Pkcedetails PkceDetails { get; set; }
        public string userId { get; set; }
        public string ip { get; set; }
        public string mac { get; set; }
    }

    public class GetInternalAuthNSessReq
    {
        public string clientId { get; set; }
        public string userId { get; set; }
        public string ip { get; set; }
        public string mac { get; set; }
    }

    public class Authentication_Scheme
    {
        public string name { get; set; }
        public string description { get; set; }
    }
    public class GetAuthNSessRes
    {
        public string success { get; set; }
        public string message { get; set; }
        public string temporarySession { get; set; }
        public IList<string> authenticationSchemes { get; set; }

    }

    public class SendMobileNotificationRes
    {
        public string success { get; set; }
        public string message { get; set; }
        public string randomCode { get; set; }
    }

    public class fido2Response
    {
        public AssertionOptions AssertionOptions { get; set; }
        public AuthenticatorAssertionRawResponse AuthenticatorAssertionRawResponse { get; set; }
    }

    public class LogoutRequest
    {
        public string id_token_hint { get; set; }
        public string post_logout_redirect_uri { get; set; }
        public string state { get; set; }
    }
    [JsonUnmappedMemberHandling(JsonUnmappedMemberHandling.Disallow)]
    public class VerifyUserAuthNDataRequest
    {
        [Required]
        [StringLength(50, MinimumLength = 30)]
        [RegularExpression(@"^[A-Za-z0-9\-_\.]+$",
            ErrorMessage = "authnToken can contain only letters, numbers, hyphen, underscore, and dot.")]
        public string authnToken { get; set; }

        [Required]
        [StringLength(50, MinimumLength = 1)]
        [RegularExpression(@"^[A-Za-z0-9_\- ]+$",
            ErrorMessage = "authenticationScheme contains invalid characters.")]
        public string authenticationScheme { get; set; }

        [StringLength(50)]
        [RegularExpression(@"^[A-Za-z0-9+/=]*$",
            ErrorMessage = "authenticationData must contain only base64 style characters.")]
        public string authenticationData { get; set; }

        [StringLength(10)]
        [RegularExpression(@"^(true|false|approved|rejected)$",
            ErrorMessage = "approved must be true, false, approved, or rejected.")]
        public string approved { get; set; }

        [MaxLength(20)]
        public List<ScopeAndClaimsInfo> allowedScopesAndClaims { get; set; }
    }
    public class VerifyUserAuthenticationDataRequest
    {
        public string authenticationScheme { get; set; }
        public string authenticationData { get; set; }
        public string suid { get; set; }
        public string token { get; set; }
    }
    public class VerifyQrCodeRequest
    {
        public string tempSession { get; set; }
        public string qrCode { get; set; }
    }
    [JsonUnmappedMemberHandling(JsonUnmappedMemberHandling.Disallow)]
    public class UserAuthDataReq
    {
        [Required]
        [StringLength(50, MinimumLength = 30)]
        public string suid { get; set; }
        [Required]
        [StringLength(50, MinimumLength = 1)]
        public string priauthscheme { get; set; }
    }

    public class UserAuthDataRes
    {
        public string AuthData { get; set; }
        public string priauthscheme { get; set; }
    }

    public class VerifyAgentConsentRequest
    {
        [StringLength(50, MinimumLength = 30)]
        public string authnToken { get; set; }
        [StringLength(50, MinimumLength = 1)]
        public string authenticationScheme { get; set; }
        [StringLength(50)]
        public string authenticationData { get; set; }
        [StringLength(10)]
        public string approved { get; set; }
    }
    public class VerifyUserAuthenticationRequest
    {
        public string authenticationScheme { get; set; }
        public string authenticationData { get; set; }
        public string Suid { get; set; }
    }
    public class  VerifierUrlResponse()
    {
        public string verifierUrl { get; set; }
        public string VerifierCode { get; set; }
    }
    [JsonUnmappedMemberHandling(JsonUnmappedMemberHandling.Disallow)]
    public class VerifyQrRequest
    {
        [Required]
        [StringLength(50, MinimumLength = 30)]
        [RegularExpression(@"^[A-Za-z0-9\-_\.]+$",
            ErrorMessage = "clientId can contain only letters, numbers, hyphen, underscore, and dot.")]
        public string clientId { get; set; }

        [Required]
        [StringLength(200, MinimumLength = 1)]
        [RegularExpression(@"^[A-Za-z0-9\-\._~:/\?\#\[\]@!\$&'\(\)\*\+,;=%]+$",
            ErrorMessage = "qrCode contains invalid characters.")]
        public string qrCode { get; set; }
    }

    public class MobileAuthTemporarySession
    {
        public string TemporarySessionId { get; set; }
        public ClientDetails ClientDetails { get; set; }
        public bool AllAuthNDone { get; set; }
        public string globalSessionId { get; set; }
        public bool withPkce { get; set; }
        public Pkcedetails PkceDetails { get; set; }
        public string State { get; set; }
        public string AcrValues { get; set; }
        public string nonce { get; set; }
        public string CoRelationId { get; set; }
        public List<string> AcceptedAttributes { get; set; }
        public string JourneyToken { get; set; }
    }

    [JsonUnmappedMemberHandling(JsonUnmappedMemberHandling.Disallow)]
    public class ICPAuthRequest
    {
        [Required]
        [StringLength(50,MinimumLength =1)]
        [RegularExpression(@"^[A-Za-z0-9\-]{3,50}$",
            ErrorMessage = "DocumentNumber can contain only letters, numbers, and hyphen.")]
        public string DocumentNumber { get; set; }

        [Required]
        [StringLength(20,MinimumLength =1)]
        [RegularExpression(@"^[A-Za-z]{2,20}$",
            ErrorMessage = "DocumentType must contain only letters.")]
        public string DocumentType { get; set; }

    }
    [JsonUnmappedMemberHandling(JsonUnmappedMemberHandling.Disallow)]
    public class ICPAuthSuidRequest
    {
        [Required(AllowEmptyStrings = false, ErrorMessage = "Suid is required")]
        [RegularExpression(@"^[A-Za-z0-9\-]{5,50}$",
            ErrorMessage = "Suid must be 5-50 characters and contain only letters, numbers, or hyphen.")]
        public string Suid { get; set; } = string.Empty;
    }
}
