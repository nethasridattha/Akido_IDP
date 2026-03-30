using System;
using System.Collections.Generic;

namespace DTPortal.Core.Domain.Models;

public partial class NorAuthScheme
{
    public int Id { get; set; }

    public int? AuthSchId { get; set; }

    public int? PriAuthSchId { get; set; }

    public DateTime? CreatedDate { get; set; }

    public DateTime? ModifiedDate { get; set; }

    public virtual AuthScheme AuthSch { get; set; }

    public virtual PrimaryAuthScheme PriAuthSch { get; set; }
}
