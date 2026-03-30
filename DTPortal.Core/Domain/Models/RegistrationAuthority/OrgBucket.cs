using System;
using System.Collections.Generic;

namespace DTPortal.Core.Domain.Models.RegistrationAuthority;

public partial class OrgBucket
{
    public int Id { get; set; }

    public string BucketId { get; set; }

    public int BucketConfigurationId { get; set; }

    public int? TotalDigitalSignatures { get; set; }

    public int? TotalEseal { get; set; }

    public string CreatedOn { get; set; }

    public string UpdatedOn { get; set; }

    public string Status { get; set; }

    public string LastSignatoryId { get; set; }

    public string SponsorId { get; set; }

    public string PaymentRecievedOn { get; set; }

    public short PaymentRecieved { get; set; }

    public int? Additionaldsremaining { get; set; }

    public int? Additionaledsremaining { get; set; }

    public virtual OrgBucketsConfig BucketConfiguration { get; set; }
}
