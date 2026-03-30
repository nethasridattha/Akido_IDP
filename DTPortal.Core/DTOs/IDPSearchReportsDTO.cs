namespace DTPortal.Core.DTOs
{
    public class IDPSearchReportsDTO
    {
        public string Identifier { get; set; }

        public string ServiceName { get; set; }

        public string TransactionType { get; set; }

        public string StartDate { get; set; }

        public string EndDate { get; set; }

        public int PerPage { get; set; }
    }
}
