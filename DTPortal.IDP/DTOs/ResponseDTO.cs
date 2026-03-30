using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DTPortal.IDP.DTOs
{
    public class ResponseDTO
    {
        public bool Success { get; set; }
        public string Message { get; set; }
        public object Result { get; set; }
    }
    public class ErrorResponseDTO
    {
        public string error { get; set; }
        public string error_description { get; set; }
    }

    public class AccessTokenOpenIdResponseDTO
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

    public class AccessTokenOpenIdRefreshTokenDTO
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
        public string refresh_token { get; set; }
    }

    public class AccessTokenOAuthResponseDTO
    {
        public string access_token { get; set; }
        // Type of access token. Always has the “Bearer” value.
        public string token_type { get; set; }
        // Lifetime (in seconds) of the access token.
        public long expires_in { get; set; }
        // Scopes granted to those to which the access token is associated,
        // separated by spaces.
        public string scopes { get; set; }
    }
    public class AccessTokenOAuthRefreshTokenDTO
    {
        public string access_token { get; set; }
        // Type of access token. Always has the “Bearer” value.
        public string token_type { get; set; }
        // Lifetime (in seconds) of the access token.
        public long expires_in { get; set; }
        // Scopes granted to those to which the access token is associated,
        // separated by spaces.
        public string scopes { get; set; }
        public string refresh_token { get; set; }
    }
    public class UserInfoResponseDTO
    {
        public string user_id { get; set; }
        public string birthdate { get; set; }
        public string name { get; set; }
        public int gender { get; set; }
        public string phone_number { get; set; }
        public string email { get; set; }
        public string sub { get; set; }
    }

    public class SubscriberInfoResponseDTO
    {
        public string suid { get; set; }
        public string birthdate { get; set; }
        public string name { get; set; }
        public string phone_number { get; set; }
        public string email { get; set; }
        public string sub { get; set; }
        public int gender { get; set; }
        public string daes_id_document_type { get; set; }
        public string daes_id_documemnt_number { get; set; }
        public string daes_loa { get; set; }
    }
}
