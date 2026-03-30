using DTPortal.Core.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTPortal.Core.Domain.Services.Communication
{
    public class ConfigurationResponse : BaseResponse<Configuration>
    {
        public ConfigurationResponse(Configuration category) : base(category) { }

        public ConfigurationResponse(string message) : base(message) { }

        public ConfigurationResponse(Configuration category, string message) :
            base(category, message) { }
    }

    public class configurationMCRequest
    {
        public string configName { get; set; }
        public object requestData { get; set; }
    }
}
