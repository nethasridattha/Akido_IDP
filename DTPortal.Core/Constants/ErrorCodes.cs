using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DTPortal.Core.Constants
{
    public static class ErrorCodes
    {
        // RA/PKI Service Errors(1-20)
        public const uint PKI_SERVICE_ERROR = 100001;
        public const uint PKI_SERVICE_GEN_SIGNATURE_TIMEOUT = 100002;
        public const uint PKI_SERVICE_GEN_SIGNATURE_FAILED = 100003;
        public const uint PKI_SERVICE_VERIFY_SIGNATURE_TIMEOUT = 100004;
        public const uint PKI_SERVICE_VERIFY_SIGNATURE_FAILED = 100005;
        public const uint RA_USER_STATUS_UPDATE_FAILED = 100006;
        public const uint RA_USER_STATUS_UPDATE_TIMEOUT = 100007;
        public const uint PKI_SERVICE_GEN_SIGNATURE_UNAVAILABLE = 100008;
        public const uint PKI_SERVICE_VERIFY_SIGNATURE_UNAVAILABLE = 100009;
        public const uint RA_USER_STATUS_UPDATE_UNAVAILABLE = 100010;

        /* Redis Errors(21-100) */
        public const uint REDIS_COMMAND_EXCEPTION = 100021;
        public const uint REDIS_TIMEOUT_EXCEPTION = 100022;
        public const uint REDIS_CONNECTION_EXCEPTION = 100023;

        // Access Token Errors
        public const uint REDIS_ACCESS_TOKEN_ADD_FAILED = 100031;
        public const uint REDIS_ACCESS_TOKEN_GET_FAILED = 100032;
        public const uint REDIS_ACCESS_TOKEN_REMOVE_FAILED = 100033;
        public const uint REDIS_ACCESS_TOKEN_GET_TTL_FAILED = 100034;

        // Global Session Errors
        public const uint REDIS_GLOBAL_SESS_REMOVE_FAILED = 100035;
        public const uint REDIS_GLOBAL_SESS_ADD_FAILED = 100036;
        public const uint REDIS_GLOBAL_SESS_GET_FAILED = 100037;

        // User Sessions Errors
        public const uint REDIS_USER_SESS_REMOVE_FAILED = 100038;
        public const uint REDIS_USER_SESS_ADD_FAILED = 100039;
        public const uint REDIS_USER_SESS_GET_FAILED = 100040;

        // Authorization Code Errors
        public const uint REDIS_AUTHZ_CODE_REMOVE_FAILED = 100041;
        public const uint REDIS_AUTHZ_CODE_ADD_FAILED = 100042;
        public const uint REDIS_AUTHZ_CODE_GET_FAILED = 100043;

        // Temporary Session Errors
        public const uint REDIS_TEMP_SESS_ADD_FAILED = 100044;
        public const uint REDIS_TEMP_SESS_REMOVE_FAILED = 100045;
        public const uint REDIS_TEMP_SESS_GET_FAILED = 100046;

        /* Database Errors(101-400) */
        // User Login Table Errors
        public const uint DB_USER_ADD_LOGIN_DETAILS_FAILED = 100101;
        public const uint DB_USER_UPDATE_LOGIN_DETAILS_FAILED = 100102;
        public const uint DB_USER_LOGIN_DETAILS_NOT_FOUND = 100103;

        // User Table Errors
        public const uint DB_USER_UPDATE_STATUS_FAILED = 100104;
        public const uint DB_USER_GET_DETAILS_FAILED = 100105;

        // Client Table Errors
        public const uint DB_CLIENT_GET_DETAILS_FAILED = 100106;

        // Role Table Errors
        public const uint DB_ROLE_GET_FAILED = 100107;

        // Database General Error
        public const uint DB_ERROR = 100108;

        /* General Errors(401-700) */
        public const uint GENERATE_UNIQUE_KEY_FAILED = 100401;
        public const uint GENERATE_RANDOM_CODES_FAILED = 100402;
        public const uint GENERATE_ID_TOKEN_FAILED = 100403;
        public const uint GENERATE_USER_INFO_TOKEN_FAILED = 100404;

        // Errors(701-1000)
        //login controller
        public const uint LOGIN_METHOD_TARGET_NOT_FOUND = 100701;

        public const uint LOGIN_TEMP_SESS_NOT_FOUND_IN_COOCKIES = 100702;

        public const uint LOGIN_GET_GETLOGINSESSION_RES_NULL = 100703;

        public const uint LOGIN_GET_SSOCONFIG_RES_NULL = 100704;

        public const uint LOGIN_INDEX_POST_METHOD_EXCP = 100705;

        public const uint LOGIN_VERIFYUESR_METHOD_EXCP = 100706;

        public const uint LOGIN_AUTHENTICATUSER_METHOD_EXCP = 100707;

        public const uint LOGIN_ISUSERVERIFYCODE_METHOD_EXCP = 100708;

        public const uint LOGIN_SENDPUSHNOTIFICATION_METHOD_EXCP = 100709;

        public const uint LOGIN_LOGOUT_METHOD_EXCP = 100710;

        public const uint LOGIN_GETERRORCONSTANT_METHOD_EXCP = 100711;

        public const uint LOGIN_FORGOTPASSWORD_METHOD_EXCP = 100712;

        public const uint LOGIN_OIDCLOGOUT_METHOD_EXCP = 100713;

        public const uint LOGIN_OIDCLOGOUT_FAIL_TO_GET_IDP_CERT = 100714;

        public const uint SESSION_NOT_FOUND = 100715;


        //OAuth2controller
        public const uint OAUTH2_GET_IDPCONFIG_RES_NULL = 100716;

        public const uint OAUTH2_GET_VALIDATCLIENT_RES_NULL = 100717;

        public const uint OAUTH2_GET_METHOD_EXCP = 100718;

        public const uint OAUTH2_GETCODE_GETAUTHORIZATIONCODE_RES_NULL = 100719;

        public const uint OAUTH2_GETCODE_METHOD_EXCP = 100720;

        public const uint OAUTH2_ALLOW_MODIFYUSERCONCENT_RES_NULL = 100721;

        public const uint OAUTH2_ALLOW_METHOD_EXCP = 100722;

        //REGISTRATION CONTROLLER
        public const uint REG_GET_MISSING_REQ_CODE = 100723;

        public const uint REG_GET_VERIFYTOKEN_RES_NULL = 100724;

        public const uint REG_SAVECRED_REQ_VALUE_NULL = 100725;

        public const uint REG_SAVECRED_SAVEUSER_RES_NULL = 100726;

        public const uint REG_SAVECRED_REGTEMPDEVICE_RES_NULL = 100727;

        public const uint REG_SAVECRED_OPT_AND_REGTYPE_NOTFOUND_IN_COOCKIES = 100728;
    }
}
