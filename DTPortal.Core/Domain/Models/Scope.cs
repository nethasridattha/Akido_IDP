using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
namespace DTPortal.Core.Domain.Models;

public partial class Scope
{
    public int Id { get; set; }

    [Required]
    [StringLength(100, MinimumLength = 2)]
    [RegularExpression(@"^[A-Za-z0-9._:-]{2,100}$", ErrorMessage = "Name contains invalid characters.")]
    public string Name { get; set; }

    [Required]
    [StringLength(150, MinimumLength = 2)]
    [RegularExpression(@"^[A-Za-z0-9\s_.-]{2,150}$", ErrorMessage = "DisplayName contains invalid characters.")]
    public string DisplayName { get; set; }

    [StringLength(500)]
    [RegularExpression(@"^[A-Za-z0-9\s.,!@#%&()_\-:;']{0,500}$", ErrorMessage = "Description contains invalid characters.")]
    public string Description { get; set; }

    public bool UserConsent { get; set; }

    public bool DefaultScope { get; set; }

    public bool MetadataPublish { get; set; }

    public DateTime? CreatedDate { get; set; }

    public DateTime? ModifiedDate { get; set; }

    [StringLength(100)]
    public string CreatedBy { get; set; }

    [StringLength(100)]
    public string UpdatedBy { get; set; }

    [StringLength(20)]
    public string Status { get; set; }

    [StringLength(500)]
    [RegularExpression(@"^[A-Za-z0-9_,.\s-]{0,500}$", ErrorMessage = "ClaimsList contains invalid characters.")]
    public string ClaimsList { get; set; }

    public bool IsClaimsPresent { get; set; }

    public bool SaveConsent { get; set; }

    [StringLength(36)]
    public string Version { get; set; }

    [StringLength(150)]
    [RegularExpression(@"^[\u0600-\u06FF\sA-Za-z0-9_.-]{0,150}$", ErrorMessage = "DisplayNameArabic contains invalid characters.")]
    public string DisplayNameArabic { get; set; }
}
