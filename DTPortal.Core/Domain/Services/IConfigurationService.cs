using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DTPortal.Core.Domain.Models;
using DTPortal.Core.Domain.Models.RegistrationAuthority;
using DTPortal.Core.Domain.Services.Communication;

namespace DTPortal.Core.Domain.Services
{
    public interface IConfigurationService
    {
        Task<IList<string>> GetAllScopes();
        Task<IList<string>> GetAllGrantTypes();
         ConfigurationResponse SetConfiguration(
            string configName, object config);
        Task<ConfigurationResponse> SetConfigurationAsync(
                        string configName, object config, string updatedBy,
                        bool makerCheckerFlag = false);
        T GetPlainConfiguration<T>(string configName);
        T GetConfiguration<T>(string configName);
        Task<T> GetConfigurationAsync<T>(string configName);
        FaceThreshold GetThreshold();
        Task<string> GetActiveAuthenticationId();
        Task<ConfigurationResponse> UpdateDefaultAuthScheme(string Id);
    }
}
