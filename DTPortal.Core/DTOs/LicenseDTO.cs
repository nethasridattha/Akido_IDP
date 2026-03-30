using System;

namespace DTPortal.Core.DTOs
{
    public class LicenseDTO
    {
        public int Id { get; set; }
        public string Ouid { get; set; }
        public string Appid { get; set; }
        public string CreatedDateTime { get; set; }
        public string UpdatedDateTime { get; set; }
        public string LicenseInfo { get; set; }
        public string IssuedOn { get; set; }
        public string ValidUpTo { get; set; }
        public string LicenseType { get; set; }
        public string LastActivated { get; set; }
        public string FirstActivated { get; set; }
        public string LicenceStatus { get; set; }
        public string ApplicationName { get; set; }
        public string OrganizationName { get; set; }
    }
}
