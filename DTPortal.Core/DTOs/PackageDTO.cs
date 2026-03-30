namespace DTPortal.Core.DTOs
{
    public class PackageDTO
    {
        public int PackageId { get; set; }

        public string PackageCode { get; set; }

        public string PackageDescription { get; set; }

        public string ServiceFor { get; set; }

        public int TotalSigningTransactions { get; set; }

        public int TotalESealTransactions { get; set; }

        public int DiscountOnSigningTransactions { get; set; }

        public int DiscountOnESealTransactions { get; set; }

        public double TaxPercentage { get; set; }

        public string Status { get; set; }

        public string CreatedBy { get; set; }

        public string UpdatedBy { get; set; }

        public string ApprovedBy { get; set; }
    }
}
