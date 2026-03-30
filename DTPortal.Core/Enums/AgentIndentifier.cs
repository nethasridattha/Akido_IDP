using System.ComponentModel.DataAnnotations;

namespace DTPortal.Web.Enums
{
    public enum AgentIndentifier
    {
        [Display(Name = "Mobile Number")]
        MOBILE = 3,

        [Display(Name = "Email")]
        EMAIL = 4
    }
}
