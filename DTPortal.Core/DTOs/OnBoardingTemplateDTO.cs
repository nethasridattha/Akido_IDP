using System.Collections.Generic;

namespace DTPortal.Core.DTOs
{
    public class OnboardingTemplateDTO
    {
        public int TemplateId { get; set; }

        public string TemplateName { get; set; }

        public string TemplateMethod { get; set; }

        public string PublishedStatus { get; set; }

        public string CreatedBy { get; set; }

        public string UpdatedBy { get; set; }

        public string ApprovedBy { get; set; }

        public IEnumerable<OnboardingStepDTO> Steps { get; set; }
    }
}
