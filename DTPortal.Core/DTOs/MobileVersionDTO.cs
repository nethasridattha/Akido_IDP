namespace DTPortal.Core.DTOs
{
    public class MobileVersionDTO
    {
        public int Id { get; set; }

        public string OsVersion { get; set; }

        public string LatestVersion { get; set; }

        public string MinimumVersion { get; set; }

        public string UpdateLink { get; set; }
    }
}
