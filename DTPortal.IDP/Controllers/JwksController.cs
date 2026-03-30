using DTPortal.Core.Domain.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Net.Http.Json;
using Newtonsoft.Json;
using DTPortal.Core.Domain.Services.Communication;

namespace DTPortal.IDP.Controllers
{
    [ApiExplorerSettings(IgnoreApi = true)]
    [Route("api/[controller]")]
    [ApiController]
    public class JwksController : Controller
    {
        private readonly ILogger<JwksController> _logger;
        private readonly IConfigurationService _configurationService;

        public JwksController(ILogger<JwksController> logger, IConfigurationService configurationService)
        {
            _logger = logger;
            _configurationService = configurationService;
        }


        [Route("Jwksuri")]
        [HttpGet]
        public async Task<IActionResult> Jwksuri()
        {
            var jwks = await _configurationService.
                GetConfigurationAsync<JwksKey>("Jwks_Config");
            if(null == jwks)
            {
                _logger.LogError("Unable to Get Jwks_Config");
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
            var jwksString = JsonConvert.SerializeObject(jwks);
            return Ok(jwks);
        }
    }
}
