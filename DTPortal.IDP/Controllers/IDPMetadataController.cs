using DTPortal.Core.Domain.Services;
using DTPortal.Core.Domain.Services.Communication;
using DTPortal.IDP.DTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace DTPortal.IDP.Controllers
{
    public class IDPMetadataController: BaseController
    {
        private readonly ILogger<JwksController> _logger;
        private readonly IConfigurationService _configurationService;

        public IDPMetadataController(ILogger<JwksController> logger,
            IConfigurationService configurationService)
        {
            _logger = logger;
            _configurationService = configurationService;
        }

    }
}
