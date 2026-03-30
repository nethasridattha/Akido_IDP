using DTPortal.Core.Domain.Models;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace DTPortal.Core.Utilities
{
    public class LocalJWTManager: ILocalJWTManager
    {
        private readonly ILogger<LocalJWTManager> _logger;
        public JWTConfig _config { get; set; }

        public LocalJWTManager(JWTConfig config, ILogger<LocalJWTManager> logger)
        {
            _config = config;
            _logger = logger;
        }

        // Generate a JWToken
        public string GenerateSecKeyJWT(string userName, string nonce)
        {
            _logger.LogInformation("-->GenerateToken");

            // Generate a security key from secret key
            var securityKey = new SymmetricSecurityKey(
                Encoding.ASCII.GetBytes(_config.SecretKey));

            // Create Token Handler
            var tokenHandler = new JwtSecurityTokenHandler();

            // Create Token Parameters
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                // Create Claims
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(JwtRegisteredClaimNames.Sub, userName),
                    new Claim(ClaimTypes.Name, userName),
                    new Claim(JwtRegisteredClaimNames.Nonce, nonce),
                }),
                Expires = DateTime.UtcNow.AddMinutes(_config.ExpiryInMins),
                Issuer = _config.Issuer,
                Audience = _config.Audience,
                SigningCredentials = new SigningCredentials(securityKey,
                _config.Algorithm)
            };

            // Create Token
            var token = tokenHandler.CreateToken(tokenDescriptor);

            _logger.LogInformation("<-->GenerateToken");
            // Return Token as a string
            return tokenHandler.WriteToken(token);
        }

        // Validate JWToken
        public bool ValidateSecKeyJWT(string token)
        {
            _logger.LogInformation("-->ValidateToken");

            // Generate a security key from secret key
            var securityKey = new SymmetricSecurityKey(
                Encoding.ASCII.GetBytes(_config.SecretKey));

            // Create Token Handler
            var tokenHandler = new JwtSecurityTokenHandler();
            try
            {
                // Validate Token
                tokenHandler.ValidateToken(token, new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidIssuer = _config.Issuer,
                    ValidAudience = _config.Audience,
                    IssuerSigningKey = securityKey
                }, out SecurityToken validatedToken);
            }
            catch (SecurityTokenValidationException error)
            {
                _logger.LogError("ValidateToken Failed: {0}", error.Message);
                return false;
            }

            _logger.LogInformation("<--ValidateToken");
            return true;
        }
        public string GetJSONWebTokenClaims(string token)
        {
            _logger.LogDebug("-->GetJSONWebTokenClaims");

            // Local variable declaration
            string result = null;

            // Validate input parameters
            if (string.IsNullOrEmpty(token))
            {
                _logger.LogError("Invalid input parameter");
                return result;
            }

            // Create Token Handler
            var tokenHandler = new JwtSecurityTokenHandler();
            try
            {
                var str = tokenHandler.ReadJwtToken(token);
                if (null == str)
                {
                    _logger.LogError("Failed to read JWT Token");
                    return result;
                }

                result = str.Claims.FirstOrDefault(c => c.Type ==
                JwtRegisteredClaimNames.Sub).Value;
            }
            catch (Exception error)
            {
                _logger.LogError("GetJSONWebTokenClaims failed:{0}", error.Message);
                return null;
            }

            _logger.LogDebug("-->GetJSONWebTokenClaims");
            return result;
        }
        // ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
    }
}
