using DTPortal.Core.Constants;
using DTPortal.Core.Domain.Models;
using DTPortal.Core.Domain.Repositories;
using DTPortal.Core.Domain.Services;
using DTPortal.Core.Domain.Services.Communication;
using DTPortal.Core.DTOs;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace DTPortal.Core.Utilities
{
    public class TokenManager : ITokenManager
    {
        // Initialize logger.
        private readonly ILogger<TokenManager> _logger;
        private readonly IPKIServiceClient _pkiServiceClient;
        private readonly IPKILibrary _pkiLibrary;
        private readonly IUnitOfWork _unitOfWork;
        private readonly SSOConfig ssoConfig;
        private readonly idp_configuration idpConfiguration;
        private readonly IGlobalConfiguration _globalConfiguration;
        private readonly ICertificateIssuanceService _certificateIssuanceService;
        public JWTConfig _config { get; set; }
        public TokenManager(JWTConfig config, ILogger<TokenManager> logger,
            IPKIServiceClient pkiServiceClient, IPKILibrary pkiLibrary,
            IUnitOfWork unitOfWork, IGlobalConfiguration globalConfiguration,
            ICertificateIssuanceService certificateIssuanceService)
        {
            _config = config;
            _pkiServiceClient = pkiServiceClient;
            _logger = logger;
            _pkiLibrary = pkiLibrary;
            _unitOfWork = unitOfWork;
            _globalConfiguration = globalConfiguration;
            _certificateIssuanceService = certificateIssuanceService;

            // Get SSO Configuration
            ssoConfig = _globalConfiguration.GetSSOConfiguration();
            if (null == ssoConfig)
            {
                _logger.LogError("Get SSO Configuration failed in token manager");
                throw new NullReferenceException();
            }

            idpConfiguration = _globalConfiguration.GetIDPConfiguration();
            if (null == idpConfiguration)
            {
                _logger.LogError("Get IDP Configuration failed");
                throw new NullReferenceException();
            }
        }
        // ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~

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
                _logger.LogError("ValidateToken Failed: {0}",
                    error.Message);
                return false;
            }

            _logger.LogInformation("<--ValidateToken");
            return true;
        }
        // ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~`
        public async Task<string> GenerateJWTToken(object payloadObj)
        {
            _logger.LogDebug("-->GenerateJWTToken");

            // Local variable declaration
            string jwtToken = null;
            string keyId = null;

            // Validate input parameters
            if (null == payloadObj)
            {
                _logger.LogError("Invalid input parameter");
                return jwtToken;
            }

            // Convert payload object to string
            var payload = JsonConvert.SerializeObject(payloadObj);
            if (payload == null)
            {
                _logger.LogError("Convert payload object to string Failed");
                return jwtToken;
            }

            // Convert to base64 url encoded string
            var base64UrlEncPayload = Base64UrlEncoder.Encode(payload);
            if (base64UrlEncPayload == null)
            {
                _logger.LogError("Convert payload string to base64 url" +
                    "encoding Failed");
                return jwtToken;
            }

            // Get Active IDP certificate
            var activeCertificate = await _unitOfWork.Certificates.
                GetActiveCertificateAsync();
            if (null == activeCertificate)
            {
                _logger.LogError("GetActiveCertificate Failed");
                return jwtToken;
            }

            keyId = activeCertificate.Kid;

            // Check certificate expiry
            bool certificateExpired = DateTime.Now >= activeCertificate.ExpiryDate;

            if (certificateExpired)
            {
                var certificateConfig = new PKIIssueCertificateReq();
                certificateConfig.commonName = "DAES";
                certificateConfig.keyID = Guid.NewGuid().ToString();
                certificateConfig.daesCertificate = true;
                certificateConfig.countryName = "UG";

                // Create new certificate.
                var result = await _pkiLibrary.GenerateCertificateAsync();
                if (!result.Success)
                {
                    _logger.LogError("CreateCertificateAsync Failed");
                    return jwtToken;
                }

                keyId = certificateConfig.keyID;

                _logger.LogWarning("Certificate Expired");

                activeCertificate.Status = "EXPIRED";

                // Update certificate status in Database
                _unitOfWork.Certificates.Update(activeCertificate);
                await _unitOfWork.SaveAsync();

                _logger.LogInformation("Old Certificate Status Updated to Expired");

                // Create Certificate request

            }

            // Create header Object
            var headerObj = new JWTHeader();
            headerObj.alg = "RS256";
            headerObj.typ = "JWT";
            headerObj.kid = keyId;

            // Convert header object to string
            var header = JsonConvert.SerializeObject(headerObj);
            if (header == null)
            {
                _logger.LogError("Convert header object to string Failed");
                return jwtToken;
            }

            // Convert to base64 url encoded string
            var base64UrlEncHeader = Base64UrlEncoder.Encode(header);
            if (base64UrlEncPayload == null)
            {
                _logger.LogError("Convert header string to base64 url" +
                    "encoding Failed");
                return jwtToken;
            }

            // Prepare data to sign
            var data = base64UrlEncHeader + "." + base64UrlEncPayload;

            // Convert to base64 String
            var plainTextBytes = System.Text.Encoding.UTF8.GetBytes(data);
            var base64Data = System.Convert.ToBase64String(plainTextBytes);

            // Create generate signature request object
            var generateSignatureReqObj = new PKIGenerateSignatureReq();
            generateSignatureReqObj.hash = base64Data;
            generateSignatureReqObj.tokenSign = true;
            generateSignatureReqObj.keyID = keyId;

            // Convert request object to string
            var generateSignatureReq = JsonConvert.SerializeObject(
                generateSignatureReqObj);
            if (generateSignatureReq == null)
            {
                _logger.LogError("Convert request object to string Failed");
                return null;
            }
            SignDataRequest request = new SignDataRequest()
            {
                Identifier = keyId,
                DataToSign = base64Data,
                HashData = true,
                TokenCert= true
            };
            // Generate signature for header and payload data
            var response =await _certificateIssuanceService.GenerateSignatureAsync(request);
            if (null == response || !response.Success)
            {
                _logger.LogError("GenerateTokenSignature failed");
                return jwtToken;
            }
            var signature = (string)response.Resource;

            // Prepare JWT Token
            jwtToken = data + "." + signature;
            _logger.LogDebug("<-->GenerateJWTToken");

            // Return JWT Token
            return jwtToken;
        }
        // ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
        public bool VerifyJWTToken(string jwtToken,
            string issuer,
            string audience,
            string certificate,
            bool validateIssuer = true,
            bool validateAud = true,
            bool expiry = true
            )
        {
            _logger.LogDebug("-->VerifyJWTToken");

            // Local variable declaration
            X509Certificate2 cert;

            // Validate input parameters
            if (string.IsNullOrEmpty(jwtToken) || string.IsNullOrEmpty(certificate))
            {
                _logger.LogError("Invalid input parameter");
                return false;
            }


            _logger.LogDebug("Issuer: {0}, Audience: {1}",
                issuer?.SanitizeForLogging(), audience?.SanitizeForLogging());

            try
            {
                cert = new X509Certificate2(Convert.FromBase64String
                    (@certificate));

                RsaSecurityKey rsaSecurityKey = new RsaSecurityKey(
                    cert.GetRSAPublicKey());

                var validationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = validateIssuer,
                    ValidateAudience = validateAud,
                    ValidateLifetime = expiry,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = issuer,
                    ValidAudience = audience,
                    IssuerSigningKey = rsaSecurityKey,
                    CryptoProviderFactory = new CryptoProviderFactory()
                    {
                        CacheSignatureProviders = false
                    }
                };

                var handler = new JwtSecurityTokenHandler();
                handler.ValidateToken(jwtToken, validationParameters,
                    out var validatedSecurityToken);
            }
            catch (Exception error)
            {
                _logger.LogError("VerifyJWTToken Failed: {0}", error.Message);
                return false;
            }

            _logger.LogDebug("<--VerifyJWTToken");
            return true;
        }
        // ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
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
        public async Task<bool> ValidateDeviceRegistrationToken(
            string token)
        {
            _logger.LogDebug("-->ValidateDeviceRegistrationToken");
            // Local variable declaration
            X509Certificate2 cert;

            // Validate input parameters
            if (string.IsNullOrEmpty(token))
            {
                _logger.LogError("Invalid input parameter");
                return false;
            }

            try
            {
                if (false == ssoConfig.sso_config.remoteSigning)
                {
                    // For Testing Only
                    cert = new X509Certificate2(@"publickey.crt");
                }
                else
                {
                    // Get Active IDP certificate
                    var certificate = await _unitOfWork.Certificates.
                        GetActiveCertificateAsync();
                    if (null == certificate)
                    {
                        _logger.LogError("GetActiveCertificate Failed");
                        return false;
                    }

                    cert = new X509Certificate2(
                        Convert.FromBase64String(@certificate.Data));
                }

                RsaSecurityKey rsaSecurityKey = new RsaSecurityKey(
                    cert.GetRSAPublicKey());

                var openidconnect = JsonConvert.DeserializeObject<OpenIdConnect>(
                    idpConfiguration.openidconnect.ToString());
                var validationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = DTInternalConstants.DTPortalClientId,
                    ValidAudience = openidconnect.issuer,
                    IssuerSigningKey = rsaSecurityKey,
                    CryptoProviderFactory = new CryptoProviderFactory()
                    {
                        CacheSignatureProviders = false
                    }
                };

                var handler = new JwtSecurityTokenHandler();
                handler.ValidateToken(token, validationParameters,
                    out var validatedSecurityToken);
            }
            catch (Exception error)
            {
                _logger.LogError("ValidateDeviceRegistrationToken Failed: {0}",
                    error.Message);
                return false;
            }

            _logger.LogDebug("<--ValidateDeviceRegistrationToken");
            return true;
        }
        // ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
        public clientDetails GetJWTClaims(string token)
        {
            _logger.LogDebug("-->GetJWTClaims");

            // Validate input parameters
            if (string.IsNullOrEmpty(token))
            {
                _logger.LogError("Invalid input parameter");
                return null;
            }

            clientDetails claims = new clientDetails();

            // Create Token Handler
            var tokenHandler = new JwtSecurityTokenHandler();
            try
            {
                var str = tokenHandler.ReadJwtToken(token);
                if (true == str.Claims.Any(c => c.Type == "client_id"))
                {
                    claims.clientId = str.Claims.FirstOrDefault(c => c.Type ==
                    "client_id").Value;
                }

                if (true == str.Claims.Any(c => c.Type ==
                    "scopes"))
                {
                    claims.scopes = str.Claims.FirstOrDefault(c => c.Type ==
                    "scopes").Value;
                }

                if (true == str.Claims.Any(c => c.Type ==
                    "redirect_uri"))
                {
                    claims.redirect_uri = str.Claims.FirstOrDefault(c => c.Type ==
                    "redirect_uri").Value;
                }

                if (true == str.Claims.Any(c => c.Type ==
                    "response_type"))
                {
                    claims.response_type = str.Claims.FirstOrDefault(c => c.Type ==
                    "response_type").Value;
                }

                if (true == str.Claims.Any(c => c.Type ==
                    "nonce"))
                {
                    claims.nonce = str.Claims.FirstOrDefault(c => c.Type ==
                    "nonce").Value;
                }

                if (true == str.Claims.Any(c => c.Type ==
                    "state"))
                {
                    claims.state = str.Claims.FirstOrDefault(c => c.Type ==
                    "state").Value;
                }
            }
            catch (Exception error)
            {
                _logger.LogError("GetJWTClaims Failed: {0}",
                    error.Message);
                return null;
            }

            _logger.LogDebug("<--GetJWTClaims");
            return claims;
        }
        // ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
        public async Task<string> CreateUserInfoToken(object claims, string scope,
            string clientId, string iss)
        {
            _logger.LogDebug("-->CreateUserInfoToken");

            // Local variable declaration
            string jwtToken = null;

            // Validate input parameters
            if (null == claims || string.IsNullOrEmpty(scope) ||
                string.IsNullOrEmpty(clientId) || string.IsNullOrEmpty(iss))
            {
                _logger.LogError("Invalid input parameter");
                return jwtToken;
            }

            try
            {
                var now = DateTime.Now;
                var unixTimeSeconds = new DateTimeOffset(now).ToUnixTimeSeconds();
                var userClaims = new List<Claim>();
                userClaims.Add(new Claim(JwtRegisteredClaimNames.Iss, iss));
                userClaims.Add(new Claim(JwtRegisteredClaimNames.Aud, clientId));

                if (scope == "openid")
                {
                    var userObject = (adminBasicFields)claims;
                    userObject.iss = iss;
                    userObject.aud = clientId;
                    jwtToken = await GenerateJWTToken(userObject);
                }
                if (scope == "profile")
                {
                    var userObject = (adminProfileFields)claims;
                    userObject.iss = iss;
                    userObject.aud = clientId;

                    jwtToken = await GenerateJWTToken(userObject);
                }
                if (scope == "sub_openid")
                {
                    var userObject = (subscriberBasicFields)claims;

                    userObject.iss = iss;
                    userObject.aud = clientId;
                    jwtToken = await GenerateJWTToken(userObject);
                }
                if (scope == "sub_profile")
                {
                    var userObject = (subscriberProfileFields)claims;

                    userObject.iss = iss;
                    userObject.aud = clientId;
                    jwtToken = await GenerateJWTToken(userObject);
                }
            }
            catch (Exception error)
            {
                _logger.LogError("CreateUserInfoToken Failed: {0}", error.Message);
                return null;
            }

            _logger.LogDebug("<--CreateUserInfoToken");
            return jwtToken;
        }

        public bool VerifyLogoutJWTToken(
        string jwtToken,
        string issuer,
        string audience,
        string certificate,
        bool validateIssuer = true,
        bool validateAud = true,
        bool expiry = true)
        {
            if (string.IsNullOrWhiteSpace(jwtToken) || string.IsNullOrWhiteSpace(certificate))
            {
                return false;
            }

            try
            {
                X509Certificate2 cert = LoadCertificate(certificate);

                if (cert == null)
                {
                    Console.WriteLine("Failed to load certificate.");
                    return false;
                }

                var rsaKey = new RsaSecurityKey(cert.GetRSAPublicKey());

                var validationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = validateIssuer,
                    ValidateAudience = validateAud,
                    ValidateLifetime = expiry,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = issuer,
                    ValidAudience = audience,
                    IssuerSigningKey = rsaKey,
                    CryptoProviderFactory = new CryptoProviderFactory
                    {
                        CacheSignatureProviders = false
                    }
                };

                var handler = new JwtSecurityTokenHandler();
                handler.ValidateToken(jwtToken, validationParameters, out SecurityToken validatedToken);

                _logger.LogInformation("Token validated successfully.");
                _logger.LogInformation($"Token ID: {((JwtSecurityToken)validatedToken).Id}");

                return true;
            }
            catch (SecurityTokenExpiredException ex)
            {
                _logger.LogError($"Token has expired: {ex.Message}");
                return false;
            }
            catch (SecurityTokenInvalidSignatureException ex)
            {
                _logger.LogError($"Invalid signature: {ex.Message}");
                return false;
            }
            catch (SecurityTokenInvalidIssuerException ex)
            {
                _logger.LogError($"Invalid issuer: {ex.Message}");
                return false;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Token validation failed: {ex.Message}");
                return false;
            }
        }

        private X509Certificate2 LoadCertificate(string certificate)
        {
            try
            {
                string originalCert = certificate.Trim();

                // Case 1: Already a raw PEM string
                if (originalCert.Contains("-----BEGIN CERTIFICATE-----"))
                {
                    return X509Certificate2.CreateFromPem(originalCert);
                }

                // Case 2: Base64 encoded — strip ALL whitespace
                string strippedCert = originalCert
                    .Replace("\r\n", "")
                    .Replace("\n", "")
                    .Replace("\r", "")
                    .Replace("\t", "")
                    .Replace(" ", "")
                    .Trim();

                int missing = (4 - strippedCert.Length % 4) % 4;
                strippedCert += new string('=', missing);

                byte[] decodedBytes = Convert.FromBase64String(strippedCert);
                string decodedString = Encoding.UTF8.GetString(decodedBytes);

                if (decodedString.Contains("-----BEGIN CERTIFICATE-----"))
                {
                    return X509Certificate2.CreateFromPem(decodedString);
                }

                return new X509Certificate2(decodedBytes);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Certificate loading failed: {ex.Message}");
                return null;
            }
        }
    }
}
