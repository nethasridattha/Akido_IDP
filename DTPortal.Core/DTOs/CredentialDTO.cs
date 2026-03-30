using DTPortal.Core.Domain.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTPortal.Core.DTOs
{
    public class CredentialDTO
    {
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string credentialName { get; set; } = string.Empty;

        [Required]
        [StringLength(200)]
        public string displayName { get; set; } = string.Empty;

        [Required]
        [StringLength(100)]
        public string credentialId { get; set; } = string.Empty;

        [Required]
        [StringLength(100)]
        public string credentialUId { get; set; } = string.Empty;

        [StringLength(500)]
        public string remarks { get; set; } = string.Empty;

        public List<int> categories { get; set; } = new();

        [StringLength(50)]
        public string verificationDocType { get; set; } = string.Empty;

        public List<DataAttributesDTO> dataAttributes { get; set; } = new();

        [Required]
        [StringLength(50)]
        public string authenticationScheme { get; set; } = string.Empty;

        [Required]
        [StringLength(50)]
        public string categoryId { get; set; } = string.Empty;

        [Range(1, 3650, ErrorMessage = "Validity must be between 1 and 3650 days.")]
        public int validity { get; set; }

        [Required]
        [StringLength(100)]
        public string organizationId { get; set; } = string.Empty;

        [Url(ErrorMessage = "Invalid trust URL format.")]
        [StringLength(500)]
        public string trustUrl { get; set; } = string.Empty;

        [StringLength(2000)]
        public string credentialOffer { get; set; } = string.Empty;

        public DateTime createdDate { get; set; }

        [StringLength(10000)]
        public string logo { get; set; } = string.Empty;

        [Required]
        [StringLength(20)]
        public string status { get; set; } = string.Empty;
    }
    public class DataAttributesDTO
    {
        [Required]
        [StringLength(200)]
        public string displayName { get; set; } = string.Empty;

        [Required]
        [StringLength(100)]
        public string attribute { get; set; } = string.Empty;

        [Range(1, 10, ErrorMessage = "Invalid data type.")]
        public int dataType { get; set; }
    }
}
