using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace DTPortal.Core.DTOs
{
    public class DefaultAuthenticationSchemeDTO
    {
        public List<SelectListItem> AuthenticationSchemesList { get; set; }


        public string AuthSchemeId { get; set; }
    }
}
