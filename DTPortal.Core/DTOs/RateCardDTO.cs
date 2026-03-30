namespace DTPortal.Core.DTOs
{
    public class RateCardDTO
    {
        public RateCardDTO()
        {
            ServiceDefinitions = new ServiceDefinitionDTO();
        }

        public int Id { get; set; }

        public string StakeHolder { get; set; }

        public double Rate { get; set; }

        public string RateEffectiveFrom { get; set; }

        public string RateEffectiveTo { get; set; }

        public double Tax { get; set; }

        public string Status { get; set; }

        public string CreatedBy { get; set; }

        public string UpdatedBy { get; set; }

        public string ApprovedBy { get; set; }

        public ServiceDefinitionDTO ServiceDefinitions { get; set; }
    }
}
