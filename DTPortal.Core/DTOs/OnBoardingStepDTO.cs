namespace DTPortal.Core.DTOs
{
    public class OnboardingStepDTO
    {
        public int OnboardingStepId { get; set; }

        public string OnboardingStep { get; set; }

        public string OnboardingStepDisplayName { get; set; }

        public string IntegrationUrl { get; set; }

        public string OnboardingStepThreshold { get; set; }

        public string AndriodTFliteThreshold { get; set; }

        public string AndriodDTTThreshold { get; set; }

        public string IosTFliteThreshold { get; set; }

        public string IosDTTThreshold { get; set; }
    }
}
