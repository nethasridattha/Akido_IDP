namespace DTPortal.Core.Domain.Lookups
{
    public class ActivityLookupItem : LookupItem
    {
        public bool McEnabled { get; set; }
        public bool McSupported { get; set; }
        public bool IsCritical { get; set; }
        public int ParentId { get; set; }
    }

}
