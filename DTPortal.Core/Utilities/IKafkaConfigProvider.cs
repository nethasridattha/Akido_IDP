namespace DTPortal.Core.Utilities
{
    public interface IKafkaConfigProvider
    {
        KafkaConfig GetKafkaConfiguration();
    }
}
