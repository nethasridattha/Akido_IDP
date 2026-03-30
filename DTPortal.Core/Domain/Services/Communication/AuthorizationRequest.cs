using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace DTPortal.Core.Domain.Services.Communication
{
    public class AuthorizationRequest
    {
        [Required]
        [StringLength(50,MinimumLength =1)]
        [RegularExpression(@"^[A-Za-z0-9\-_\.]+$",
            ErrorMessage = "Invalid client_id format.")]
        public string client_id { get; set; }

        [StringLength(500, MinimumLength = 1)]
        [RegularExpression(@"^[A-Za-z][A-Za-z0-9+\-.]*:\/\/[^\s]+$",
    ErrorMessage = "Invalid redirect_uri format.")]
        public string redirect_uri { get; set; }

        [Required]
        [StringLength(50, MinimumLength = 1)]
        [RegularExpression(@"^(code|token)$",
            ErrorMessage = "Invalid response_type.")]
        public string response_type { get; set; }

        [Required]
        [StringLength(100, MinimumLength = 1)]
        [RegularExpression(@"^[A-Za-z0-9 ._:\-/]+$",
        ErrorMessage = "The scope field is required.")]
        public string scope { get; set; }

        [StringLength(100, MinimumLength = 1)]
        [RegularExpression(@"^[A-Za-z0-9\-_\.]*$",
            ErrorMessage = "Invalid state format.")]
        public string state { get; set; }

        [StringLength(100, MinimumLength = 1)]
        [RegularExpression(@"^[A-Za-z0-9\-_\.]*$",
            ErrorMessage = "Invalid nonce format.")]
        public string nonce { get; set; }

        [StringLength(5000, MinimumLength = 1)]
        [RegularExpression(@"^[A-Za-z0-9_-]+\.[A-Za-z0-9_-]+\.[A-Za-z0-9_-]+$",
            ErrorMessage = "Invalid JWT format.")]
        public string request { get; set; }

        [StringLength(128, MinimumLength = 43)]
        [RegularExpression(@"^[A-Za-z0-9_-]+$")]
        public string code_challenge { get; set; }

        [StringLength(100, MinimumLength = 1)]
        [RegularExpression(@"^(plain|S256)$",
            ErrorMessage = "Invalid code_challenge_method.")]
        public string code_challenge_method { get; set; }
    }
}
