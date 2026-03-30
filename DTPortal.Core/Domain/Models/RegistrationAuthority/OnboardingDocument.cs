using System;
using System.Collections.Generic;

namespace DTPortal.Core.Domain.Models.RegistrationAuthority;

public partial class OnboardingDocument
{
    public int DocId { get; set; }

    public string CreatedDate { get; set; }

    public string Description { get; set; }

    public string ExpireDate { get; set; }

    public string Label { get; set; }

    public string MimeType { get; set; }

    public string Uuid { get; set; }

    public string Version { get; set; }

    public string ModifiedDate { get; set; }

    public string Status { get; set; }
}
