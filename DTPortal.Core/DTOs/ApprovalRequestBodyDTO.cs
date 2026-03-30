namespace DTPortal.Core.DTOs
{
    public class ApprovalRequestBodyDTO
    {
        public int Id { get; set; }

        public bool Approve { get; set; }

        public string ApprovedBy { get; set; }

        public string Reason { get; set; }
    }
}
