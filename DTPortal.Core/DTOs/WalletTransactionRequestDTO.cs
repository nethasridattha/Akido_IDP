using DTPortal.Core.Domain.Services.Communication;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace DTPortal.Core.DTOs
{
    [JsonUnmappedMemberHandling(JsonUnmappedMemberHandling.Disallow)]
    public class WalletTransactionRequestDTO
    {
        [Required]
        [StringLength(10,MinimumLength =1)]
        [RegularExpression(@"^[A-Z]+$",
            ErrorMessage = "Invalid Status.")]
        public string status { get; set; } = string.Empty;

        [Required]
        [StringLength(500,MinimumLength =1)]
        [RegularExpression(@"^(?!\s+$)[A-Za-z0-9 .,:_-]+$",
            ErrorMessage = "StatusMessage contains invalid characters or only whitespace.")]
        public string statusMessage { get; set; } = string.Empty;

        [Required]
        [StringLength(100,MinimumLength =1)]
        [RegularExpression(@"^[A-Za-z0-9._-]+$",
            ErrorMessage = "ClientID contains invalid characters.")]
        public string clientID { get; set; } = string.Empty;

        [Required]
        [StringLength(50, MinimumLength = 5,
            ErrorMessage = "The field suid must be a string with a minimum length of 5 and a maximum length of 50.")]
        [RegularExpression(@"^[A-Za-z0-9\-]+$",
            ErrorMessage = "Invalid SUID format.")]
        public string suid { get; set; } = string.Empty;

        [StringLength(20000)]
        [RegularExpression(@"^[A-Za-z0-9\-._~+/=]*$",
            ErrorMessage = "Invalid presentationToken format.")]
        public string presentationToken { get; set; }

        [Required]
        [StringLength(50,MinimumLength =1)]
        [RegularExpression(@"^[A-Za-z0-9._-]+$",
            ErrorMessage = "UserActivityType contains invalid characters.")]
        public string userActivityType { get; set; }

        [MaxLength(20)]
        public List<CredentialDetail> profiles { get; set; }
    }

    public class CallStackObject
    {
        public string ActivityType { get; set; }
        public string presentationToken { get; set; }
        public List<CredentialDetail> profiles { get; set; }
    }

}
