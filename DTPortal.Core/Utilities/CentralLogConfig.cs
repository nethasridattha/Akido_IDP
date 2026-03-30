using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTPortal.Core.Utilities
{
    public class CentralLogConfig
    {
        public string ConnectionString { get; set; }
        public string QueueName { get; set; }
    }
    public class ServiceLogConfig
    {
        public string ConnectionString { get; set; }
        public string QueueName { get; set; }
    }

    public class AdminLogConfig
    {
        public string ConnectionString { get; set; }
        public string QueueName { get; set; }
    }

    public class LogConfig
    {
        public CentralLogConfig CentralLogConfig { get; set; }
        public ServiceLogConfig ServiceLogConfig { get; set; }
        public AdminLogConfig AdminLogConfig { get; set; }
    }
}
