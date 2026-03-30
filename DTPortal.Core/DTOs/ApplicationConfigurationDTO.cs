using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DTPortal.Core.Domain.Services.Communication;

namespace DTPortal.Core.DTOs
{
    public class ApplicationConfigurationDTO
    {
        public SSOConfig SSOConfiguration { get; set; } = new SSOConfig();

        public adminportal_config AdminPortalConfiguration { get; set; } = new adminportal_config();

        public idp_configuration IDPConfiguration { get; set; } = new idp_configuration();

        public string UUID { get; set; }
    }
}
