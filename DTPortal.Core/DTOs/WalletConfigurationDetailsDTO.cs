using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTPortal.Core.DTOs
{
    public class WalletConfigurationDetailsDTO
    {
        public string format { get; set; }
        public string bindingMethod {  get; set; }
        public string supportedMethod { get; set; }
    }
    public class WalletConfigurationsDTO
    {
        public string Name { get; set; }
        public string DisplayName { get; set; }
        public List<BindingMethodsDTO> bindingMethods { get; set; }
    }
    public class BindingMethodsDTO
    {
        public string Name { get; set; }
        public string DisplayName { get; set; }
        public List<SupportedMethodsDTO> supportedMethods { get; set; }
    }
    public class SupportedMethodsDTO
    {
        public string Name { get; set; }
        public string DisplayName { get; set; }
    }
}
