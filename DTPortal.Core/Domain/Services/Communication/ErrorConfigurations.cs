using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTPortal.Core.Domain.Services.Communication
{
    public class MessageConstants
    {
        public LocalizedMessage InternalError { get; set; }
        public LocalizedMessage UnAuthorized { get; set; }
        public LocalizedMessage AUTHENTICATION { get; set; }
        public LocalizedMessage REGISTRATION { get; set; }
        public LocalizedMessage SHARING { get; set; }
        public LocalizedMessage ProcessingError { get; set; }
        public LocalizedMessage FaceVerificationSuccess { get; set; }
        public LocalizedMessage VerifiedClientDetails { get; set; }
        public LocalizedMessage TransactionLogsFetchedSuccessfully { get; set; }
        public LocalizedMessage TransactionLogsCountFetchedSuccessfully { get; set; }
        public LocalizedMessage ConsentDetailsFetchedSuccessfully { get; set; }
        public LocalizedMessage FaceVerificationError { get; set; }
        public LocalizedMessage FaceVerificationCancelled { get; set; }
        public LocalizedMessage DocumentVerificationFailed { get; set; }
        public LocalizedMessage FaceVerificationFailed { get; set; }
        public LocalizedMessage FaceVerificationExpired { get; set; }
        public LocalizedMessage AuthNDone { get; set; }
        public LocalizedMessage JourneyDetailsFailed { get; set; }
        public LocalizedMessage InvalidArguments { get; set; }
        public LocalizedMessage DBConnectionError { get; set; }
        public LocalizedMessage UnAuthorizedError { get; set; }
        public LocalizedMessage WrongCredentials { get; set; }
        public LocalizedMessage SubscriberNotApproved { get; set; }
        public LocalizedMessage RandomCodeNotMatched { get; set; }
        public LocalizedMessage PinVerifyFailed { get; set; }
        public LocalizedMessage FaceVerifyFailed { get; set; }
        public LocalizedMessage UserDeniedAuthN { get; set; }
        public LocalizedMessage WrongCode { get; set; }
        public LocalizedMessage WrongPin { get; set; }
        public LocalizedMessage WrongFace { get; set; }
        public LocalizedMessage AuthNFailed { get; set; }
        public LocalizedMessage SubAlreadyAuthenticated { get; set; }
        public LocalizedMessage SubscriberNotFound { get; set; }
        public LocalizedMessage GlobalSessionNotFound { get; set; }
        public LocalizedMessage SubscriberFaceNotFound { get; set; }
        public LocalizedMessage TimeRestrictionApplied { get; set; }
        public LocalizedMessage SubAccountSuspended { get; set; }
        public LocalizedMessage NotificationSendFailed { get; set; }
        public LocalizedMessage SubNotProvisioned { get; set; }
        public LocalizedMessage SubNotActive { get; set; }
        public LocalizedMessage SubAccountStatus { get; set; }
        public LocalizedMessage TempSessionExpired { get; set; }
        public LocalizedMessage AuthnTokenExpired { get; set; }
        public LocalizedMessage AuthSchemeMisMatch { get; set; }
        public LocalizedMessage AuthSchemeAlreadyAuthenticated { get; set; }
        public LocalizedMessage UserSessionNotFound { get; set; }
        public LocalizedMessage SessionMismatch { get; set; }
        public LocalizedMessage SessionsNotFound { get; set; }
        public LocalizedMessage CredentialNotFound { get; set; }
        public LocalizedMessage CredentialListSuccess { get; set; }
        public LocalizedMessage GetLogSuccess { get; set; }
    }

    public class OIDCConstants
    {
        public LocalizedMessage InternalError { get; set; }
        public LocalizedMessage InvalidInput { get; set; }
        public LocalizedMessage InvalidScope { get; set; }
        public LocalizedMessage InvalidToken { get; set; }
        public LocalizedMessage InvalidClientAssertionOrType { get; set; }
        public LocalizedMessage insufficientScope { get; set; }
        public LocalizedMessage InvalidGrant { get; set; }
        public LocalizedMessage InvalidDocumentType { get; set; }
        public LocalizedMessage JourneyTokenCreated { get; set; }
        public LocalizedMessage InvalidGrantType { get; set; }
        public LocalizedMessage CodeNotFound { get; set; }
        public LocalizedMessage InvalidClient { get; set; }
        public LocalizedMessage InvalidAuthZHeader { get; set; }
        public LocalizedMessage UnsupportedAuthSchm { get; set; }
        public LocalizedMessage VerifyTokenScopeMissing { get; set; }
        public LocalizedMessage ClientSecretMismatch { get; set; }
        public LocalizedMessage InsufficientScope { get; set; }
        public LocalizedMessage InsufficientScopeDesc { get; set; }
        public LocalizedMessage ClientIdNotReceived { get; set; }
        public LocalizedMessage ClientIdMisMatch { get; set; }
        public LocalizedMessage ClientNotFound { get; set; }
        public LocalizedMessage ClientNotActive { get; set; }
        public LocalizedMessage InvalidCredentials { get; set; }
        public LocalizedMessage InvalidTokenDesc { get; set; }
        public LocalizedMessage CodeVerificationFailed { get; set; }
        public LocalizedMessage AssertionValidationFailed { get; set; }
        public LocalizedMessage ResponseTypeMisMatch { get; set; }
        public LocalizedMessage ClientScopesNotMatched { get; set; }
        public LocalizedMessage ScopesNotExists { get; set; }
        public LocalizedMessage DeniedConsent { get; set; }
        public LocalizedMessage RedirectUriMisMatch { get; set; }
        public LocalizedMessage GrantTypeMismatch { get; set; }
        public LocalizedMessage NonceNotReceived { get; set; }
        public LocalizedMessage AttributesNotFound { get; set; }
    }

    public class WebConstants
    {
        public LocalizedMessage RedirectUriMissing { get; set; }
        public LocalizedMessage LogoutUriMissing { get; set; }
        public LocalizedMessage InvalidLogoutUri { get; set; }
        public LocalizedMessage InvalidIdToken { get; set; }
        public LocalizedMessage NotAuthorized { get; set; }

        public LocalizedMessage GetOrganizationDetailsFailed { get; set; }
        public LocalizedMessage GetSubscriberDetailsFailed { get; set; }
        public LocalizedMessage NoCredentialVerifiersDataFound { get; set; }
        public LocalizedMessage CredentialVerifiersListSuccess { get; set; }
        public LocalizedMessage CredentialVerifierListFailed { get; set; }
        public LocalizedMessage LogoutError { get; set; }
        public LocalizedMessage InvalidClient { get; set; }
        public LocalizedMessage InvalidPostLogout { get; set; }
        public LocalizedMessage ClientIdNotFound { get; set; }
        public LocalizedMessage ClientIdRedirectUriMissing { get; set; }
        public LocalizedMessage InvalidJWT { get; set; }
        public LocalizedMessage DeniedConsent { get; set; }
        public LocalizedMessage ClientNotActive { get; set; }
        public LocalizedMessage DeniedPermission { get; set; }
        public LocalizedMessage ConsentRequired { get; set; }
        public LocalizedMessage SessionNotFound { get; set; }
        public LocalizedMessage InternalError { get; set; }
        public LocalizedMessage InternalServerError { get; set; }
        public LocalizedMessage ResponseTypeNotFound { get; set; }
        public LocalizedMessage ScopeNotFound { get; set; }
        public LocalizedMessage StateNotFound { get; set; }
        public LocalizedMessage NonceNotFound { get; set; }
        public LocalizedMessage SomethingWrong { get; set; }
        public LocalizedMessage InvalidParams { get; set; }
        public LocalizedMessage GetUserFailed { get; set; }
    }

    public class LocalizedMessage
    {
        public string En { get; set; }
        public string Ar { get; set; }
    }

    public class ErrorConfiguration
    {
        public MessageConstants Constants { get; set; }
        public OIDCConstants OIDCConstants { get; set; }
        public WebConstants WebConstants { get; set; }
    }
}
