namespace DTPortal.Core.DTOs
{
    public class ServiceDefinitionDTO
    {
        public int Id { get; set; }

        public string ServiceName { get; set; }

        public string ServiceDisplayName { get; set; }

        public string Status { get; set; }

        public bool PricingSlabApplicable { get; set; }
    }
}
