using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DTPortal.Core.Domain.Services.Communication
{
    public class ConsentRequest
    {
        public string SessionId { get; set; }
    }

    public class ConsentResponse
    {
        public string clientId { get; set; }
        public string clientName { get; set; }
        public bool consentRequired { get; set; }
        public List<ScopeDetail> scopes { get; set; }
    }

    public class ScopeDetail
    {
        public string Name { get; set; }
        public string DisplayName { get; set; }
        public List<AttributeInfo> Attributes { get; set; }

    }

    public class AttributeInfo
    {
        public string Name { get; set; }
        public bool Mandatory { get; set; }
        public string DisplayName { get; set; }
    }

    public class ConsentApprovalRequest
    {
        [Required]
        [StringLength(100)]
        public string clientId { get; set; } = string.Empty;

        [Required]
        [StringLength(100)]
        public string suid { get; set; } = string.Empty;

        [Required]
        public List<ScopeObject> scopes { get; set; } = new();
    }

    public class ScopeObject
    {
        [Required]
        [StringLength(100)]
        public string Name { get; set; } = string.Empty;

        public List<string> Attributes { get; set; } = new();
    }

    public class CredentialDetail
    {
        [Required(ErrorMessage = "CredentialId is required.")]
        [StringLength(100, MinimumLength = 1,
            ErrorMessage = "CredentialId must be between 1 and 100 characters.")]
        [RegularExpression(@"^[A-Za-z0-9._-]+$",
            ErrorMessage = "CredentialId contains invalid characters.")]
        public string CredentialId { get; set; } = string.Empty;

        [Required(ErrorMessage = "DisplayName is required.")]
        [StringLength(200, MinimumLength = 1,
            ErrorMessage = "DisplayName must be between 1 and 200 characters.")]
        [RegularExpression(@"^(?!\s+$)[A-Za-z0-9][A-Za-z0-9 ._-]*$",
            ErrorMessage = "DisplayName contains invalid characters.")]
        public string DisplayName { get; set; } = string.Empty;

        public List<ClaimsDetail> Attributes { get; set; } = new();
    }

    public class ProfileInfo
    {
        [StringLength(100)]
        [RegularExpression(@"^[A-Za-z0-9 ._:\-()]*$", ErrorMessage = "Name contains invalid characters.")]
        public string Name { get; set; } = string.Empty;

        [StringLength(200)]
        [RegularExpression(@"^(?!\s+$)[A-Za-z0-9 ._:\-()]*$", ErrorMessage = "DisplayName contains invalid characters.")]
        public string DisplayName { get; set; } = string.Empty;

        public List<ClaimsDetail> Attributes { get; set; } = new();
    }

    public class ClaimsDetail
    {
        [StringLength(100)]
        [RegularExpression(@"^(?!\s+$)[A-Za-z0-9 ._:\-()]*$", ErrorMessage = "Name contains invalid characters.")]
        public string Name { get; set; } = string.Empty;

        [StringLength(200)]
        [RegularExpression(@"^(?!\s+$)[A-Za-z0-9 ._:\-()]*$", ErrorMessage = "DisplayName contains invalid characters.")]
        public string DisplayName { get; set; } = string.Empty;
    }

    public class LogProfileInfo
    {
        [Required]
        [StringLength(100)]
        public string Name { get; set; }
        public List<string> Attributes { get; set; }
    }

    public class LogDetailsObject
    {
        public string ActivityType { get; set; }
        public List<ProfileInfo> Profiles { get; set; }
    }

}
