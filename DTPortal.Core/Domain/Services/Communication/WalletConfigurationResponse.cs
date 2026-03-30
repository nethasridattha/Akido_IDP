using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTPortal.Core.Domain.Services.Communication
{
    public class WalletConfigurationResponse
    {
        public List<CredentialFormats> CredentialFormats { get; set; }
        public List<BindingMethod> BindingMethods { get; set; }
    }
    public class CredentialFormats
    {
        public string Name { get; set; }
        public string DisplayName { get; set; }
        public bool isSelected {  get; set; }   
    }
    public class BindingMethod
    {
        public string Name { get; set; }
        public string DisplayName { get; set; }
        public List<SupportedMethods> SupportedMethods { get; set; }
    }
    public class SupportedMethods
    {
        public string Name { get; set; }
        public string DisplayName { get; set; }
        public bool isSelected { get; set; }
    }
}
