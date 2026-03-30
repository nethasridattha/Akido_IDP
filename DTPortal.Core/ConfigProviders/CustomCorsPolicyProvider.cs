using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DTPortal.Core.Domain.Services;
using DTPortal.Core.Utilities;
using Microsoft.AspNetCore.Cors.Infrastructure;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace DTPortal.Core.ConfigProviders
{
    public class CustomCorsPolicyProvider : ICorsPolicyProvider
    {
        private readonly IClientService _clientService;
        private readonly ILogger<CustomCorsPolicyProvider> _logger;
        public CustomCorsPolicyProvider(ILogger<CustomCorsPolicyProvider> logger,
            IClientService clientService)
        {
            _logger = logger;
            _clientService = clientService;
        }

        public async Task<CorsPolicy> GetPolicyAsync(HttpContext context,
            string policyName)
        {
            if(null == policyName || "AllowedOrigins" != policyName)
            {
                _logger.LogDebug("Cors Policy Invalid/Not Supported");
                return null;
            }

            // Check the value of origin header
            var originHeader = context.Request.Headers["origin"];
            _logger.LogInformation("Origin Header: {0}",
      originHeader.ToString().SanitizeForLogging());
            if (string.IsNullOrEmpty(originHeader))
            {
                _logger.LogError("Origin header is invalid");
                return null;
            }

            // Get allowed Origins
            var allowedOrigins = await _clientService.GetAllowedOrigins(originHeader);
            if(null == allowedOrigins || 0 == allowedOrigins.Length)
            {
                _logger.LogInformation("Allowed Origin List is empty");
                return null;
            }

            // Allowed Methods
            string[] allowedMethods = {"POST", "GET" };

            // Build Cors Policy
            return new CorsPolicyBuilder()
                .AllowAnyHeader()
                .WithMethods(allowedMethods)
                .WithOrigins(allowedOrigins)
                .Build();
        }
    }
}
