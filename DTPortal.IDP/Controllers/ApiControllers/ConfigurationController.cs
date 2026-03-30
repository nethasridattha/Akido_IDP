using DTPortal.Core.Constants;
using DTPortal.Core.Domain.Models;
using DTPortal.Core.Domain.Services;
using DTPortal.Core.Domain.Services.Communication;
using DTPortal.Core.DTOs;
using DTPortal.Core.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DTPortal.IDP.Controllers.ApiControllers
{
    [ApiExplorerSettings(IgnoreApi = true)]
    [Route("api/[controller]")]
    public class ConfigurationController : BaseController
    {
        private readonly IConfigurationService _configurationService;
        private readonly IAuthSchemeSevice _authSchemeSevice;
        public ConfigurationController(IConfigurationService configurationService,
            IAuthSchemeSevice authSchemeSevice)
        {
                _configurationService = configurationService;
                _authSchemeSevice = authSchemeSevice;
        }

        [HttpGet]
        [Route("get-application-configuration")]
        public async Task<APIResponse> GetApplicationConfigurationAsync()
        {
            try
            {
                var configInDB = await _configurationService.GetConfigurationAsync<SSOConfig>(ConfigKeysConstants.SSO);
                if (configInDB == null)
                {
                    return new APIResponse("Failed to get SSO Application configuration");
                }

                var AdminPortalConfigInDB =
                    await _configurationService.GetConfigurationAsync<adminportal_config>(ConfigKeysConstants.AdminPortal);
                if (AdminPortalConfigInDB == null)
                {
                    return new APIResponse("Failed to get Admin Portal configuration");
                }

                var IDPConfigInDB = await _configurationService.GetConfigurationAsync<idp_configuration>(ConfigKeysConstants.IDP);
                if (IDPConfigInDB == null)
                {
                    return new APIResponse("Failed to get IDP configuration");
                }

                return new APIResponse(true, "Successfully received application configuration", new
                {
                    SSOConfig = configInDB,
                    AdminPortalConfig = AdminPortalConfigInDB,
                    IDPConfig = IDPConfigInDB
                });
            }
            catch (Exception ex)
            {
                return new APIResponse($"An error occurred while fetching application configuration: {ex.Message}");
            }
        }

        [HttpPost]
        [Route("update-application-configuration")]
        public async Task<APIResponse> UpdateApplicationConfigurationAsync([FromBody] ApplicationConfigurationDTO config)
        {
            try
            {
                if (config.SSOConfiguration == null)
                    return new APIResponse("SSO configuration cannot be null.");

                if (config.AdminPortalConfiguration == null)
                    return new APIResponse("Admin portal configuration cannot be null.");

                if (config.IDPConfiguration == null)
                    return new APIResponse("IDP configuration cannot be null.");

                var updateSSOConfigResult = await _configurationService.SetConfigurationAsync(ConfigKeysConstants.SSO, config.SSOConfiguration, config.UUID);
                if (!updateSSOConfigResult.Success)
                {
                    return new APIResponse("Failed to update SSO Application configuration");
                }
                var updateAdminPortalConfigResult = await _configurationService.SetConfigurationAsync(ConfigKeysConstants.AdminPortal, config.AdminPortalConfiguration, config.UUID);
                if (!updateAdminPortalConfigResult.Success)
                {
                    return new APIResponse("Failed to update Admin Portal configuration");
                }
                var updateIDPConfigResult = await _configurationService.SetConfigurationAsync(ConfigKeysConstants.IDP, config.IDPConfiguration, config.UUID);
                if (!updateIDPConfigResult.Success)
                {
                    return new APIResponse("Failed to update IDP configuration");
                }
                return new APIResponse(true, "Successfully updated application configuration");
            }
            catch (Exception ex)
            {
                return new APIResponse($"An error occurred while updating application configuration: {ex.Message}");
            }
        }

        [HttpGet]
        [Route("get-default-authentication-scheme")]
        public async  Task<APIResponse> GetDefaultAuthenticationSchemeAsync()
        {
            try
            {
                var activeAuthSchemaId = await _configurationService.GetActiveAuthenticationId();
                if (activeAuthSchemaId == null)
                {
                    return new APIResponse("Failed to get active authentication");
                }

                var authSchemeList = await _authSchemeSevice.ListAuthSchemesAsync();

                var list = new List<SelectListItem>();
                foreach (var authScheme in authSchemeList)
                {
                    list.Add(new SelectListItem { Text = authScheme.DisplayName, Value = authScheme.Id.ToString() });
                }

                var defaultAuthScheme = new DefaultAuthenticationSchemeDTO
                {
                    AuthenticationSchemesList = list,
                    AuthSchemeId = activeAuthSchemaId
                };
                return new APIResponse(true, "Successfully received default authentication scheme",
                    defaultAuthScheme);
            }
            catch (Exception ex)
            {
                return new APIResponse($"An error occurred while fetching default authentication scheme: {ex.Message}");
            }
        }

        [HttpPut]
        [Route("update-default-authentication-scheme/{authSchemeId}")]
        public async Task<APIResponse> UpdateDefaultAuthenticationSchemeAsync(string authSchemeId)
        {
            try
            {
                if (string.IsNullOrEmpty(authSchemeId))
                {
                    return new APIResponse("AuthSchemeId cannot be null or empty.");
                }

                var result = await _configurationService.UpdateDefaultAuthScheme(authSchemeId);

                return new APIResponse(true, "Successfully updated default authentication scheme", result.Result);
            }
            catch (Exception ex)
            {
                return new APIResponse($"An error occurred while updating default authentication scheme: {ex.Message}");
            }
        }
    }
}
