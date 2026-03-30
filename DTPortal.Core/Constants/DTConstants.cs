using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DTPortal.Core.Constants
{
    // Common Constants across the project files.
    public static class DTInternalConstants
    {
        //public const string InternalError = "Internal error occurred";
        //public const string ProcessingError = "Processing error. Please try again.";
        //public const string InvalidArguments = "Invalid input arguments received.";
        //public const string DBConnectionError = "Could not connect to database. Please try again.";
        //public const string UnAuthorizedError = "You are not authorized to perform this operation..!";
        //public const string WrongCredentials = "Wrong credentials";
        //public const string SubscriberNotApproved = "Subscriber not approved";
        //public const string RandomCodeNotMatched = "Random code not matched";
        //public const string E_PIN_VERIFY_FAILED = "E_Failed to verify PIN";
        //public const string FAILED_TO_VERIFY_PIN = "Failed to verify PIN";
        //public const string AuthNSchmMisMatch = "Authentication scheme not matched";
        //public const string UserDeniedAuthN = "Subscriber Denied Authentication";
        //public const string WrongCode = "Wrong Code";
        //public const string WrongPin = "Wrong Pin";
        //public const string AuthNFailed = "Authentication Failed";
        //public const string SubAlreadyAuthenticated = "SUB_ALREADY_AUTHENTICATED";
        //public const string ResponseTypeMisMatch = "Client response not matched";
        //public const string RedirectUriMisMatch = "Client RedirectUri not matched";
        //public const string GrantTypeMismatch = "Grant type not matched";
        //public const string NonceNotReceived = "nonce not received";

        public const string SuccessMsg = "success";
        public const string E_False = "false";
        public const string S_True = "true";
        public const string deny = "deny";
        public const string FailedMsg = "Failed";
        public const string FailedStatus = "failed";
        public const int    InvalidParameter = -1;
        public const int    S_OK = 0;
        public const string NOT_AVAILABLE = "NOT_AVAILABLE";
        public const string pending = "pending";
        public const int    ENCDEC_KEY_ID = 24;
        public const string PasswordSalt = "appshield3.0";
        public const string DTPortalClientId = "ONBN53C1ydOLfvrJllgxuj9PwcyrpR5aOg5idWxnEXXwSqFe";
        public const string SigningPortalClientId = "0SifgPlF4tTiotSpW5B7Hc8Fl38CDhlsrSGXosT7nptNRQSz";
        public const string AuthNDone = "Authentication Successfull";
        public const string UserConsent = "{\"approved_scopes\": [{\"scope\": \"urn:idp:digitalid:profile\", \"permission\": true,\"created_date\": \"2021-08-01T19:42:49.6025041+05:30\"} ]}";
        public const string PasswordPolicyMismatch = "Password should contain: \n 1. Atleast {0} characters and maximum {1} characters\n 2. Atleast one upper case letter \n 3. Atleast one lower case letter \n 4. Atleast one number \n 5. Atleast one special character in [ @ # $ * & _ . + =]";
        public const string DeniedConsent = "Subscriber Denied Consent";
    }
}
