using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTPortal.Core.Domain.Services.Communication
{
    public class sso_config
    {
        public int session_timeout { get; set; }
        public int temporary_session_timeout { get; set; }
        public int access_token_timeout { get; set; }
        public int authorization_code_timeout { get; set; }
        public int active_sessions_per_user { get; set; }
        public int ideal_timeout { get; set; }
        public int wrong_pin { get; set; }
        public int wrong_code { get; set; }
        public int deny_count { get; set; }
        public int account_lock_time { get; set; }
        public int operation_authn_timeout { get; set; }
        public bool remoteSigning { get; set; }
        public IList<string> allowed_domain_users { get; set; }
    }
    public class adminportal_config
    {
        public int session_timeout { get; set; }
        public int temporary_session_timeout { get; set; }
        public int active_sessions_per_user { get; set; }
        public int ideal_timeout { get; set; }
        public int wrong_pin { get; set; }
        public int account_lock_time { get; set; }
        public bool remoteSigning { get; set; }
        public IList<string> allowed_domain_users { get; set; }
    }
    public class central_log_config
    {
        public string connection_string { get; set; }
        public string queue_name { get; set; }
    }

    public class service_log_config
    {
        public string connection_string { get; set; }
        public string queue_name { get; set; }
    }

    public class admin_log_config
    {
        public string connection_string { get; set; }
        public string queue_name { get; set; }
    }

    public class log_config
    {
        public central_log_config central_log_config { get; set; }
        public service_log_config service_log_config { get; set; }
        public admin_log_config admin_log_config { get; set; }
    }

    public class pki_service_config
    {
        public string base_address { get; set; }
        public string generate_signature_uri { get; set; }
        public string verify_signature_uri { get; set; }
    }
    public class ra_service_config
    {
        public string base_address { get; set; }
        public string status_update_uri { get; set; }
    }

    public class redis_server_config
    {
        public string connection_string { get; set; }
    }

    public class database_config
    {
        public string idp_connection_string { get; set; }
        public string ra_connection_string { get; set; }
    }

    public class authentication_schemes
    {
        public List<string> dtportal_auth_scheme { get; set; }
        public List<string> rasub_auth_scheme { get; set; }
        public List<string> critical_operation_auth_scheme { get; set; }
    }

    public class service_urls
    {
        public string idp { get; set; }
        public string pki { get; set;}
        public string ra { get; set; }
    }
    public class SSOConfig
    {
        public sso_config sso_config { get; set; }
        public log_config log_config { get; set; }
        public pki_service_config pki_service_config { get; set; }
        public ra_service_config ra_service_config { get; set; }
        public redis_server_config redis_server_config  { get; set; }
        public database_config database_config { get; set; }
        public authentication_schemes authentication_schemes { get; set; }
        public service_urls service_urls { get; set; }
    }

    public class AdminPortalSSOConfig
    {
        public sso_config sso_config { get; set; }
        public log_config log_config { get; set; }
        public pki_service_config pki_service_config { get; set; }
        public ra_service_config ra_service_config { get; set; }
        public redis_server_config redis_server_config { get; set; }
        public database_config database_config { get; set; }
        public authentication_schemes authentication_schemes { get; set; }
        public service_urls service_urls { get; set; }
    }
    public class idp_configuration
    {
        public object openidconnect { get; set; }
        public object saml2 { get; set; }
        public object common { get; set; }
    }

    public class ScopesDescription
    {
        public string Openid { get; set; }
        public string VerifyToken { get; set; }
        public string Profile { get; set; }
        public string Signin { get; set; }
    }

    public class ConsentConfiguration
    {
        public string ConsetMessage { get; set; }
        public ScopesDescription ScopesDescription { get; set; }
    }

    public class idpConfiguration
    {
        public idp_configuration idp_configuration { get; set; }
    }

    public class Key
    {
        public string kty { get; set; }
        public string kid { get; set; }
        public string use { get; set; }
        public string alg { get; set; }
        public string n { get; set; }
        public string e { get; set; }
    }

    public class JwksKey
    {
        public IList<Key> keys { get; set; }
    }

    public class IpWhiteList
    {
        public IList<string> ipWhiteList { get; set; }
    }

    public class OpenIdConnect
    {
        public string issuer { get; set; }
        public string authorization_endpoint { get; set; }
        public string token_endpoint { get; set; }
        public string userinfo_endpoint { get; set; }
        public string introspection_Endpoint { get; set; }
        public string jwks_uri { get; set; }
        public IList<string> response_types_supported { get; set; }
        public IList<string> scopes_supported { get; set; }
        public IList<string> grant_types_supported { get; set; }
        public IList<string> token_endpoint_auth_methods_supported { get; set; }
        public IList<string> claims_supported { get; set; }
        public bool request_parameter_supported { get; set; }
        public IList<string> id_token_signing_alg_values_supported { get; set; }
        public IList<string> userinfo_signing_alg_values_supported { get; set; }
        public IList<string> request_object_signing_alg_values_supported { get; set; }
        public IList<string> code_challenge_methods_supported { get; set; }
    }

    public class Common
    {
        public string signCertificate { get; set; }
        public string encryptionCertificate { get; set; }
        public object consent { get; set; }
        public bool OpenIdConnectMandatory { get; set; }
        public bool RequestSigningMandatory { get; set; }
        public bool TokenEndPointReqSigning { get; set; }
    }

}
