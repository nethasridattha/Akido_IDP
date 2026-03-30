using DTPortal.IDP.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DTPortal.Core.Domain.Services.Communication;

namespace DTPortal.Core.Domain.Services.Communication
{
    public class TokenResponse
    {
        public AccessTokenOpenIdResponseDTO OpenIdToken { get; set; }

        public AccessTokenOpenIdRefreshTokenDTO OpenIdRefreshToken { get; set; }

        public AccessTokenOAuthResponseDTO OAuthToken { get; set; }

        public AccessTokenOAuthRefreshTokenDTO OAuthRefreshToken { get; set; }

        public DTPortal.IDP.DTOs.ErrorResponseDTO Error { get; set; }
    }
}
