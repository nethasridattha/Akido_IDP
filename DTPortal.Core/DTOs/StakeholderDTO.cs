using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTPortal.Core.DTOs
{
    public class Stakeholder
    {
        public IList<StakeholderDTO> trustedStakeholderDtosList { get; set; }
    }
    public class StakeholderDTO
    {
        public int id { get; set; }

        [Required(ErrorMessage = "Name is required")]
        public string name { get; set; } = string.Empty;

        [Required(ErrorMessage = "SPOC Email is required")]
        public string spocUgpassEmail { get; set; } = string.Empty;
        public string referenceId { get; set; } = string.Empty;
        public string organizationUid { get; set; } 
        public bool status { get; set; }
        public string onboardingTime { get; set; } = string.Empty;
        public string referredBy { get; set; } 
        public string creationTime { get; set; } = string.Empty;

        [Required(ErrorMessage = "Stakeholder Type is required")]
        public string stakeholderType { get; set; } = string.Empty;

    }
}
