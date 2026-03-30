using System;
using System.Collections.Generic;

#nullable disable

namespace DTPortal.Core.Domain.Models.RegistrationAuthority
{
    public partial class OnoboardingStep
    {
        public int? OnoboardingStepId { get; set; }
        public string OnoboardingStep1 { get; set; }
        public string OnoboardingStepDisplayName { get; set; }
        public string IntegrationUr { get; set; }
        public int? OnboardingStepThreshold { get; set; }
    }
}
