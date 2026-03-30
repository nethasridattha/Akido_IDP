using System;
using System.Collections.Generic;

namespace DTPortal.Core.Domain.Models.RegistrationAuthority;

public partial class CountryDetail
{
    public int CountryId { get; set; }

    public string CountryName { get; set; }

    public string CountryFlag { get; set; }

    public string CountryCode { get; set; }

    public string MaxMobileDigits { get; set; }

    public string SupportNationalityId { get; set; }
}
