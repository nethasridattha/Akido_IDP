using Fido2NetLib;
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
    public class GetUserProfileRequest
    {
        [Required]
        [StringLength(100, MinimumLength = 1)]
        [RegularExpression(@"^[A-Za-z0-9\-_]+$", ErrorMessage = "Invalid UserId format.")]
        public string UserId { get; set; } = string.Empty;

        [Required]
        [StringLength(50, MinimumLength = 1)]
        [RegularExpression(@"^[A-Za-z0-9\-_]+$", ErrorMessage = "Invalid UserIdType format.")]
        public string UserIdType { get; set; } = string.Empty;

        [Required]
        [StringLength(50, MinimumLength = 1)]
        [RegularExpression(@"^[A-Za-z0-9\-_]+$", ErrorMessage = "Invalid ProfileType format.")]
        public string ProfileType { get; set; } = string.Empty;

        [StringLength(200)]
        [RegularExpression(@"^[A-Za-z0-9\-_]+$", ErrorMessage = "Invalid Purpose format.")]
        public string Purpose { get; set; } = string.Empty;

        [StringLength(500)]
        [RegularExpression(@"^[A-Za-z0-9\-_]+$", ErrorMessage = "Invalid Scopes format.")]
        public string Scopes { get; set; } = string.Empty;

        [StringLength(2000)]
        [RegularExpression(@"^[A-Za-z0-9\-_]+$", ErrorMessage = "Invalid Token format.")]
        public string Token { get; set; } = string.Empty;
    }
}
