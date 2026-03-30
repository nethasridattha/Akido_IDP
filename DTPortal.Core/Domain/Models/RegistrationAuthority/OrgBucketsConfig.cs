using System;
using System.Collections.Generic;

namespace DTPortal.Core.Domain.Models.RegistrationAuthority;

public partial class OrgBucketsConfig
{
    public int Id { get; set; }

    public string OrgId { get; set; }

    public string OrgName { get; set; }

    public string AppId { get; set; }

    public string Label { get; set; }

    public string BucketClosingMessage { get; set; }

    public string CreatedOn { get; set; }

    public string UpdatedOn { get; set; }

    public string Status { get; set; }

    public int? AdditionalDs { get; set; }

    public int? AdditionalEds { get; set; }

    public virtual ICollection<OrgBucket> OrgBuckets { get; set; } = new List<OrgBucket>();
}
