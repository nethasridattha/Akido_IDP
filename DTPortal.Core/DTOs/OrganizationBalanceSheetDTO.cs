namespace DTPortal.Core.DTOs
{
    public class OrganizationBalanceSheetDTO
    {
        public OrganizationBalanceSheetDTO()
        {
            ServiceDefinitions = new ServiceDefinitionDTO();
        }

        public int Id { get; set; }

        public double TotalCredits { get; set; }

        public double TotalDebits { get; set; }

        public string OrganizationId { get; set; }

        public string CreatedBy { get; set; }

        public string UpdatedBy { get; set; }

        public string ApprovedBy { get; set; }

        public ServiceDefinitionDTO ServiceDefinitions { get; set; }
    }
}
