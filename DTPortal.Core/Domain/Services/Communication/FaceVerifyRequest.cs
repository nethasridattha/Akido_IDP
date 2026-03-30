using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTPortal.Core.Domain.Services.Communication
{
    public class FaceVerifyRequest
    {
        public double faceMatchScore { get; set; }
        public string OS { get; set; }
    }
}
