using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using System;
using DTPortal.Core.Domain.Services.Communication;
using DTPortal.Core.Domain.Services;
using System.Net.Http.Headers;
using Microsoft.Extensions.Configuration;
using DTPortal.Core.Utilities;
using Microsoft.Extensions.Logging;

namespace DTPortal.IDP.Controllers
{
    [Route("api/[controller]")]
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
