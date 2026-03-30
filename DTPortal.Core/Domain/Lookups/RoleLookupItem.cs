using System;

namespace DTPortal.Core.Domain.Lookups
{
    public class RoleLookupItem : LookupItem
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string Status { get; set; }

        public DateTime CreatedDate { get; set; }
    }
}
