using DTPortal.Core.Domain.Services;
using DTPortal.Core.Domain.Services.Communication;
using DTPortal.Core.Utilities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace DTPortal.IDP.Controllers
{
    [Route("api/[controller]")]
    [AllowAnonymous]
    [ApiController]
    public class MultiPivotController : BaseController
    {
        private readonly ILogger<MultiPivotController> _logger;
        private readonly IConfiguration Configuration;
        private readonly OIDCConstants OIDCConstants;
        private readonly IGlobalConfiguration _globalConfiguration;
        private readonly IMessageLocalizer _messageLocalizer;
        public MultiPivotController(
            ILogger<MultiPivotController> logger,
            IConfiguration configuration,
            IGlobalConfiguration globalConfiguration,
            IMessageLocalizer messageLocalizer)
        {
            _logger = logger;
            Configuration = configuration;
            _globalConfiguration = globalConfiguration;
            var errorConfiguration = _globalConfiguration.
                GetErrorConfiguration();
            if (null == errorConfiguration)
            {
                _logger.LogError("Get Error Configuration failed");
                throw new NullReferenceException();
            }

            OIDCConstants = errorConfiguration.OIDCConstants;
            if (null == OIDCConstants)
            {
                _logger.LogError("Get Error Configuration failed");
                throw new NullReferenceException();
            }

            _messageLocalizer = messageLocalizer;
        }



    }
}
