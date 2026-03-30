using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTPortal.Core.DTOs
{
    public class IcpJourneyDetailsResponse
    {
        public IcpJourneyResult result { get; set; }
    }

    public class IcpJourneyResult
    {
        public IcpJourneyData data { get; set; }
    }

    public class IcpJourneyData
    {
        public SelfieAnalysis selfieAnalysis { get; set; }
    }

    public class SelfieAnalysis
    {
        public PassiveLiveness passiveLiveness { get; set; }
        public FaceMatch faceMatch { get; set; }
    }

    public class PassiveLiveness
    {
        public bool success { get; set; }
        public bool livenessConfirmed { get; set; }
    }

    public class FaceMatch
    {
        public bool success { get; set; }
        public bool matched { get; set; }
        public string message { get; set; }
    }

}
