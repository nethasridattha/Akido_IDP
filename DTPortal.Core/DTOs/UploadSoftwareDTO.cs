using Microsoft.AspNetCore.Http;

namespace DTPortal.Core.DTOs
{
    public class UploadSoftwareDTO
    {
        public IFormFile SoftwareZip { get; set; }

        public IFormFile Mannual { get; set; }

        public string SoftwareVersion { get; set; }

        public string SoftwareName { get; set; }
    }

    public class ModelData
    {
        public string softwareVersion { get; set; }

        public string softwareName { get; set; }
    }
}
