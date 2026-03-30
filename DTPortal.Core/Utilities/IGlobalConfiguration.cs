using DTPortal.Core.Domain.Services.Communication;

namespace DTPortal.Core.Utilities
{
    public interface IGlobalConfiguration
    {
        public SSOConfig GetSSOConfiguration();
        public string GetPKIConfiguration();
        public string GetFCMConfiguration();

        public KafkaConfig GetKafkaConfiguration();
        public idp_configuration GetIDPConfiguration();
        public ErrorConfiguration GetErrorConfiguration();
        public WebConstants GetWebConstantsConfiguration();
        public string AdminPortalClientId();
        public string AdminPortalClientSecret();
        public string SigningPortalClientId();
        public ThresholdConfiguration GetThresholdConfiguration();
    }
}