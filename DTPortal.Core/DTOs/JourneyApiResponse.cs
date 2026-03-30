using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTPortal.Core.DTOs
{
    public class JourneyApiResponse
    {
        public JourneyResult result { get; set; }
        public string signature { get; set; }
    }

    public class JourneyResult
    {
        public string journeyToken { get; set; }
        public string expiresIn { get; set; }
        public long expiresAt { get; set; }
    }

}
