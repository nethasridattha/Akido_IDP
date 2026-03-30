using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTPortal.Core.DTOs
{
    public  class SoftwareNewListDTO
    {
        public int SoftwareId { get; set; }
        public string FileName { get; set; }
        public string SoftwareVersion { get; set; }
        public string DownloadLink { get; set; }
        public string InstallManual { get; set; }
        public string SoftwareName { get; set; }
        public string SizeOfSoftware { get; set; }
        public string Status { get; set; }

        public DateTime CreatedOn { get; set; }
        public DateTime? UpdatedOn { get; set; }
        public DateTime? PublishedOn { get; set; }
    }
}
