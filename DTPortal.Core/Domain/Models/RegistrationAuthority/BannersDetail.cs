using System;
using System.Collections.Generic;

namespace DTPortal.Core.Domain.Models.RegistrationAuthority;

public partial class BannersDetail
{
    public int BannerId { get; set; }

    public string BannerText { get; set; }

    public string BannerSmallImage { get; set; }

    public string BannnerBackgroundImage { get; set; }

    public string BannnerFullImage { get; set; }
}
