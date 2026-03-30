using System;
using System.Collections.Generic;

namespace DTPortal.Core.Domain.Models;

public partial class EncDecKey
{
    public int Id { get; set; }

    public DateTime? CreatedDate { get; set; }

    public DateTime? ModifiedDate { get; set; }

    public string CreatedBy { get; set; }

    public string UpdatedBy { get; set; }

    public string Purpose { get; set; }

    public string Description { get; set; }

    public int? Key1Length { get; set; }

    public string AlgId { get; set; }

    public string UniqueId { get; set; }

    public byte[] Key2 { get; set; }

    public byte[] Key1 { get; set; }

    public int? Key2Length { get; set; }
}
