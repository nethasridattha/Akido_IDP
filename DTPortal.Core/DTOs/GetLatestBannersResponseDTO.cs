using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTPortal.Core.DTOs
{
    public class GetLatestBannersResponseDTO
    {
        public string LatestBannerId { get; set; } = string.Empty;
        public List<ImageData> Banners { get; set; }
    }
    public class GetLatestBannerTextsResponseDTO
    {
        public string LatestBannerTextId { get; set; } = string.Empty;
        public List<BannerTextData> BannerTexts { get; set; }
    }

    public class UpdateBannerTextRequestDTO
    {
        public int Id { get; set; }

        public string Name { get; set; } = string.Empty;

        public List<BannerTextData> BannerTexts { get; set; } = new();

        public string UpdatedBy { get; set; } = string.Empty;
    }

    public class ImageData
    {
        public string Name { get; set; }
        public string Data { get; set; }
    }

    public class BannerTextData
    {
        public string Name { get; set; } = string.Empty;
        public string BannerEn { get; set; } = string.Empty;
        public string BannerAr { get; set; } = string.Empty;
        public string BannerIcon { get; set; }
        public string BannerPosition { get; set; } = "CENTRED";

    }
}
