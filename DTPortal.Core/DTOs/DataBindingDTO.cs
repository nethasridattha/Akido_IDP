using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTPortal.Core.DTOs
{
    public class DataBindingDTO
    {
        public string Name { get; set; }
        public List<string> SupportedMethods { get; set; }
    }

    public class WalletConfigurationDTO
    {
        public List<DataBindingDTO> dataBindings { get; set; } = new List<DataBindingDTO>();
        public List<string> credentialFormats { get; set; }
    }
}
