namespace DTPortal.Core.DTOs
{
    public class OrganizationUsageReportDTO
    {
        public int Id { get; set; }

        public string OrganizationId { get; set; }

        public string ReportMonth { get; set; }

        public string ReportYear { get; set; }

        public double TotalInvoiceAmount { get; set; }

        public string CreatedOn { get; set; }
    }
}
