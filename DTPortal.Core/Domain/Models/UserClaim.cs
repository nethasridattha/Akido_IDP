using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace DTPortal.Core.Domain.Models;
[JsonUnmappedMemberHandling(JsonUnmappedMemberHandling.Disallow)]
public partial class UserClaim
{
    public int Id { get; set; }

    [Required]
    [StringLength(100, MinimumLength = 2)]
    [RegularExpression(@"^[A-Za-z0-9_.-]{2,100}$", ErrorMessage = "Name can contain letters, numbers, underscore, dot, and hyphen.")]
    public string Name { get; set; }

    [Required]
    [StringLength(150, MinimumLength = 2)]
    [RegularExpression(@"^[A-Za-z0-9\s_.-]{2,150}$", ErrorMessage = "DisplayName contains invalid characters.")]
    public string DisplayName { get; set; }

    [StringLength(500)]
    [RegularExpression(@"^[A-Za-z0-9\s.,!@#%&()_\-:;']{0,500}$", ErrorMessage = "Description contains invalid characters.")]
    public string Description { get; set; }

    public bool UserConsent { get; set; }

    public bool DefaultClaim { get; set; }

    public bool MetadataPublish { get; set; }

    public DateTime? CreatedDate { get; set; }

    public DateTime? ModifiedDate { get; set; }

    [StringLength(100)]
    public string CreatedBy { get; set; }

    [StringLength(100)]
    public string UpdatedBy { get; set; }

    [StringLength(20)]
    public string Status { get; set; }

    [StringLength(150)]
    public string DisplayNameArabic { get; set; }
}
