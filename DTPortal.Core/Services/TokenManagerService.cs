using DTPortal.Core.Domain.Services;
using DTPortal.Core.Domain.Services.Communication;
using DTPortal.Core.Utilities;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTPortal.Core.Services
{
    public class TokenManagerService : ITokenManagerService
    {
        private readonly ILogger<TokenManagerService> _logger;
        private readonly ITokenManager _tokenManager;
        private readonly idp_configuration idpConfiguration;
        private readonly IGlobalConfiguration _globalConfiguration;

        public TokenManagerService(ILogger<TokenManagerService> logger,
            ITokenManager tokenManager,IGlobalConfiguration globalConfiguration)
        {
            _logger = logger;
            _tokenManager = tokenManager;
            _globalConfiguration = globalConfiguration;

            idpConfiguration = _globalConfiguration.GetIDPConfiguration();
            if (null == idpConfiguration)
            {
                _logger.LogError("Get IDP Configuration failed");
                throw new NullReferenceException();
            }
        }

        public bool ValidateJWToken(string jwtOken, string issuer,
            string audience)
        {
            return _tokenManager.VerifyJWTToken(jwtOken,issuer,audience,null);
        }

        public clientDetails GetClientDetailsfromJwt(string jwtOken)
        {
            return _tokenManager.GetJWTClaims(jwtOken);
        }

        public bool ValidateRequestJWToken(string jwtoken, string issuer,
            string certificate)
        {
            var openidconnect = JsonConvert.DeserializeObject<OpenIdConnect>(
                idpConfiguration.openidconnect.ToString());
            return _tokenManager.VerifyJWTToken(jwtoken, issuer,
                openidconnect.issuer, certificate);
        }

        public bool ValidateLogoutJWToken(string jwtoken,
            string certificate)
        {
            _logger.LogInformation("Validating logout token");
            _logger.LogInformation("Logout token: {0}", jwtoken?.SanitizeForLogging());
            _logger.LogInformation("Certificate: {0}", certificate?.SanitizeForLogging());
            var openidconnect = JsonConvert.DeserializeObject<OpenIdConnect>(
                                 idpConfiguration.openidconnect.ToString());
            return _tokenManager.VerifyLogoutJWTToken(jwtoken, openidconnect.issuer, "",
                certificate, true, false, false);
        }

        public async Task<bool> ValidateDeviceRegistrationToken(string token)
        {
            return await _tokenManager.ValidateDeviceRegistrationToken(token);
        }
    }
}
