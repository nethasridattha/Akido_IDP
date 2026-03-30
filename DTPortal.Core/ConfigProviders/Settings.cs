using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DTPortal.API
{
    public class ConnectionStrings
    {
        public string IDPConnString { get; set; }
        public string RAConnString { get; set; }
        public string PKIConnString { get; set; }
    }
    public class Fido2
    {
        public string serverDomain { get; set; }
        public string serverName { get; set; }
        public string origin { get; set; }
        public int timestampDriftTolerance { get; set; }
    }


    public class Settings
    {
        public ConnectionStrings connectionStrings { get; set; }
        public Fido2 fido2 { get; set; }
        public string redisConnString { get; set; }
    }
}
