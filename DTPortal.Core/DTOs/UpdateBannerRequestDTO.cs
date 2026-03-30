using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTPortal.Core.DTOs
{
    public class UpdateBannerRequestDTO { 
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public List<ImageData> Banners { get; set; } = new();
        public string UpdatedBy { get; set; } = null!; }
}
