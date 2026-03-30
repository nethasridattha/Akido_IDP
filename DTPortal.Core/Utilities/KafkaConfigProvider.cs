using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTPortal.Core.Utilities
{
    public class KafkaConfigProvider : IKafkaConfigProvider
    {
        private readonly IConfiguration _configuration;

        public KafkaConfigProvider(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public KafkaConfig GetKafkaConfiguration()
        {
            return new KafkaConfig
            {
                BootstrapServers = _configuration.GetValue<string>("KafkaConfig:BootstrapServers"),
                CentralLogTopic = _configuration.GetValue<string>("KafkaConfig:CentralLogTopic"),
                ServiceLogTopic = _configuration.GetValue<string>("KafkaConfig:ServiceLogTopic"),
                AdminLogTopic = _configuration.GetValue<string>("KafkaConfig:AdminLogTopic")
            };
        }
    }
}
