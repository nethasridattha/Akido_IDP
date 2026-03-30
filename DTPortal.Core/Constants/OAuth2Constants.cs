using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTPortal.Core.Constants
{
    public static class OAuth2Constants
    {
        public const string authorization_code_with_pkce = "authorization_code_with_pkce";
        public const string GlobalSession = "GlobalSession";
        public const string AuthorizationCode = "authorization_code";
        public const string AuthorizationCodeWithPkce = "authorization_code_with_pkce";
        public const string AccessToken = "access_token";
        public const string PrivateKeyJwt = "private_key_jwt";
        public const string openid = "openid";
        public const string ClientSecretBasic = "client_secret_basic";
        public const string Saml2GrantType = "urn:ietf:params:oauth:grant-type:saml2-bearer";
        public const string ClientCredentials = "client_credentials";
        public const string Bearer = "Bearer";
        public const string RefreshToken = "refresh_token";
        public const string Profile = "urn:idp:digitalid:profile";
        public const string VerifyToken = "urn:idp:digitalid:verifytoken";
    }
}
