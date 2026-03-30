using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTPortal.Core.DTOs
{
    public class VerificationMethodsStatsDTO
    {
        public int TotalMethods { get; set; }
        public int ActiveMethods { get; set; }
        public decimal AveragePrice { get; set; }
        public int TotalBiometricMethods { get; set; }
    }
}
