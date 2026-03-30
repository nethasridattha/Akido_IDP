using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace DTPortal.Core.Domain.Services.Communication
{
    [JsonUnmappedMemberHandling(JsonUnmappedMemberHandling.Disallow)]
    public class AuthenticateUserRequest
    {
        [Required]
        [StringLength(100, MinimumLength = 5)]
        [RegularExpression(@"^[A-Za-z0-9\-]+$", ErrorMessage = "SessionId can contain only letters, numbers, and hyphen.")]
        public string SessionId { get; set; }

        [Required]
        [StringLength(100, MinimumLength = 2)]
        [RegularExpression(@"^[A-Za-z0-9_\-\.]+$", ErrorMessage = "AuthenticationScheme contains invalid characters.")]
        public string AuthenticationScheme { get; set; }

        [StringLength(500)]
        [RegularExpression(@"^$|^[A-Za-z0-9\-\._~\+\/=]*$", ErrorMessage = "AuthenticationData contains invalid characters.")]
        public string AuthenticationData { get; set; }

        public bool Approved { get; set; }

        [Required]
        [StringLength(100, MinimumLength = 1)]
        [RegularExpression(@"^[A-Za-z0-9\-]+$", ErrorMessage = "UserId can contain only letters, numbers, and hyphen.")]
        public string UserId { get; set; }

        [Range(-2, 10, ErrorMessage = "Invalid statusCode.")]
        public int statusCode { get; set; }

        public List<ProfileInfo> scopes { get; set; } = new();
    }
}
