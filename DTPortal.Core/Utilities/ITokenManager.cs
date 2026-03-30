using DTPortal.Core.Domain.Services.Communication;
using DTPortal.Core.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DTPortal.Core.Utilities
{
    public interface ITokenManager
    {
        public string GenerateSecKeyJWT(string userName, string nonce);
        public bool ValidateSecKeyJWT(string token);
        public string GetJSONWebTokenClaims(string token);
        Task<string> GenerateJWTToken(object payloadObj);
        bool VerifyJWTToken(string jwtToken,
            string issuer,
            string audience,
            string certificate,
            bool validateIssuer = true,
            bool validateAud = true,
            bool expiry = true
            );

        public clientDetails GetJWTClaims(string token);
        //Task<bool> ValidateRequestJWToken(string token, string issuer);
        //string CreateUserInfoToken(GetUserInfoResponse claims);
        Task <string> CreateUserInfoToken(object claims,
            string scope,
            string clientId,
            string iss);
        Task<bool> ValidateDeviceRegistrationToken(string token);

        bool VerifyLogoutJWTToken(
            string jwtToken,
            string issuer,
            string audience,
            string certificate,
            bool validateIssuer = true,
            bool validateAud = true,
            bool expiry = true);
    }
}
