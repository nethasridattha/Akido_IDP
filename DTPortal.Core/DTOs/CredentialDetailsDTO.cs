using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTPortal.Core.DTOs
{
    public class CredentialDetailsDTO
    {
        public List<string> credentialFormats { get; set; }
        public List<BindingMethods> bindingMethods { get; set; }

    }
    public class BindingMethods
    {
        public string name {  get; set; }
        public List<string> supportedMethods { get; set; }
    }
}
