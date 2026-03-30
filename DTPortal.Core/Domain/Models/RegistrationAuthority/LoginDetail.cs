using System;
using System.Collections.Generic;

namespace DTPortal.Core.Domain.Models.RegistrationAuthority;

public partial class LoginDetail
{
    public int Id { get; set; }

    public string Name { get; set; }

    public string EmailId { get; set; }

    public string Password { get; set; }

    public bool? IsAdmin { get; set; }

    public string CreatedOn { get; set; }

    public string UpdatedOn { get; set; }

    public string ConsentToken { get; set; }

    public string ConsentStatus { get; set; }
}
