using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTPortal.Core.Domain.Services.Communication
{
    public class VerificationMethodResponse
    {
        public int Id { get; set; }
        public string MethodUid { get; set; }
        public string MethodCode { get; set; }
        public string MethodType { get; set; }
        public string MethodName { get; set; }
        public decimal Pricing { get; set; }
        public string ProcessingTime { get; set; }
        public string ConfidenceThreshold { get; set; }
        public string TargetSegments { get; set; }
        public string MandatoryAttributes { get; set; }
        public string OptionalAttributes { get; set; }
        public List<PricingSlabDefinition> PriceSlabs { get; set; }
        public string Description { get; set; }
        public string Status { get; set; }
    }
    public class PricingSlabDefinition
    {
        public decimal From { get; set; }
        public decimal To { get; set; }
        public decimal Discount { get; set; }
    }
}
