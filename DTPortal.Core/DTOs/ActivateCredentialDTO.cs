using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTPortal.Core.DTOs
{
    public class ActivateCredentialDTO
    {
        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "Invalid credential Id.")]
        public int Id { get; set; }

        [StringLength(1000, ErrorMessage = "Remarks cannot exceed 1000 characters.")]
        public string Remarks { get; set; } = string.Empty;
    }
}
