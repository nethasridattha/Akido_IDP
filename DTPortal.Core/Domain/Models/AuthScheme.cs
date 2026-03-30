using System;
using System.Collections.Generic;

namespace DTPortal.Core.Domain.Models;

public partial class AuthScheme
{
    public int Id { get; set; }

    public string Name { get; set; }

    public string Guid { get; set; }

    public string Description { get; set; }

    public string Status { get; set; }

    public DateTime? CreatedDate { get; set; }

    public DateTime? ModifiedDate { get; set; }

    public int? PriAuthSchCnt { get; set; }

    public string CreatedBy { get; set; }

    public string UpdatedBy { get; set; }

    public bool? IsPrimaryAuthscheme { get; set; }

    public string DisplayName { get; set; }

    public string Hash { get; set; }

    public int? SupportsProvisioning { get; set; }

    public virtual ICollection<NorAuthScheme> NorAuthSchemes { get; set; } = new List<NorAuthScheme>();
}
