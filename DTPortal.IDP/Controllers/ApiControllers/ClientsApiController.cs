using DTPortal.Core.Domain.Models;
using DTPortal.Core.Domain.Services;
using DTPortal.Core.Domain.Services.Communication;
using DTPortal.Core.DTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using static DTPortal.Common.EncryptionLibrary;

namespace DTPortal.IDP.Controllers.ApiControllers
{
    [ApiExplorerSettings(IgnoreApi = true)]
    [Route("api/clients")]
    public class ClientsApiController : BaseController
    {
        private readonly IClientService _clientService;
        private readonly ISessionService _sessionService;
        private readonly IAuthSchemeSevice _authSchemeService;

        public ClientsApiController(IClientService clientService, 
            ISessionService sessionService,
            IAuthSchemeSevice authSchemeService)
        {
            _clientService = clientService;
            _sessionService = sessionService;
            _authSchemeService = authSchemeService;
        }

        [HttpGet]
        [Route("get-client-list")]
        public async Task<APIResponse> GetAllClientListAsync()
        {
            var clients = await _clientService.ListOAuth2ClientAsync();

            if (clients == null || !clients.Any())
            {
                return new APIResponse
                {
                    Success = false,
                    Message = "No clients found."
                };
            }

            var result = clients.Select(item => new ClientListDTO
            {
                Id = item.Id,
                ApplicationName = item.ApplicationName,
                ApplicationType = item.ApplicationType,
                ApplicationUri = item.ApplicationUrl,
                ClientId = item.ClientId,
                Status = item.Status,
                CreatedDate = item.CreatedDate
            }).ToList();

            return new APIResponse
            {
                Success = true,
                Message = "Client list retrieved successfully.",
                Result = result
            };
        }

        [HttpGet]
        [Route("get-client-list_by_orgId")]
        public async Task<APIResponse> GetClientListByOrgIdAsync(string orgId)
        {
            var clients = await _clientService.ListClientByOrgUidAsync(orgId);

            if (clients == null || !clients.Any())
            {
                return new APIResponse
                {
                    Success = false,
                    Message = "No clients found."
                };
            }

            var result = clients.Select(item => new ClientListDTO
            {
                Id = item.Id,
                ApplicationName = item.ApplicationName,
                ApplicationType = item.ApplicationType,
                ApplicationUri = item.ApplicationUrl,
                ClientId = item.ClientId,
                Status = item.Status,
                CreatedDate = item.CreatedDate
            }).ToList();

            return new APIResponse
            {
                Success = true,
                Message = "Client list retrieved successfully.",
                Result = result
            };
        }

        [HttpGet]
        [Route("get-kycClient-list_by_orgId")]
        public async Task<APIResponse> GetKycClientListByOrgIdAsync(string orgId)
        {
            var clients = await _clientService.ListKycClientByOrgUidAsync(orgId);

            if (clients == null || !clients.Any())
            {
                return new APIResponse
                {
                    Success = false,
                    Message = "No clients found."
                };
            }

            var result = clients.Select(item => new ClientListDTO
            {
                Id = item.Id,
                ApplicationName = item.ApplicationName,
                ApplicationType = item.ApplicationType,
                ApplicationUri = item.ApplicationUrl,
                ClientId = item.ClientId,
                Status = item.Status,
                CreatedDate = item.CreatedDate
            }).ToList();

            return new APIResponse
            {
                Success = true,
                Message = "Client list retrieved successfully.",
                Result = result
            };
        }

        [HttpGet]
        [Route("get-client-by-id/{id}")]
        public async Task<APIResponse> GetClientByIdAsync(int id)
        {
            var client = await _clientService.GetClientAsync(id);

            if (client == null)
            {
                return new APIResponse
                {
                    Success = false,
                    Message = "No client found."
                };
            }

            //var response = new ClientResponseDTO
            //{
            //    Id = client.Id,
            //    ClientId = client.ClientId,
            //    ClientSecret = client.ClientSecret,
            //    ApplicationName = client.ApplicationName,
            //    ApplicationType = client.ApplicationType,
            //    ApplicationUrl = client.ApplicationUrl,
            //    RedirectUri = client.RedirectUri,
            //    LogoutUri = client.LogoutUri,
            //    GrantTypes = client.GrantTypes,
            //    Scopes = client.Scopes,
            //    Status = client.Status,
            //    OrganizationUid = client.OrganizationUid,
            //    AuthScheme = client.AuthScheme,
            //    WithPkce = client.WithPkce,
            //    PublicKeyCert = client.PublicKeyCert
            //};

            return new APIResponse
            {
                Success = true,
                Message = "Successfully received client details.",
                Result = client
            };
        }

        [HttpGet]
        [Route("get-client-by-name")]
        public async Task<APIResponse> GetClientByNameAsync(string appName)
        {
            var client = await _clientService.GetClientByAppNameAsync(appName);

            if (client == null)
            {
                return new APIResponse
                {
                    Success = false,
                    Message = "No client found."
                };
            }

            var response = new ClientResponseDTO
            {
                Id = client.Id,
                ClientId = client.ClientId,
                ClientSecret = client.ClientSecret,
                ApplicationName = client.ApplicationName,
                ApplicationNameArabic = client.ApplicationNameArabic,
                ApplicationType = client.ApplicationType,
                ApplicationUrl = client.ApplicationUrl,
                RedirectUri = client.RedirectUri,
                LogoutUri = client.LogoutUri,
                GrantTypes = client.GrantTypes,
                Scopes = client.Scopes,
                Status = client.Status,
                OrganizationUid = client.OrganizationUid,
                AuthScheme = client.AuthScheme,
                WithPkce = client.WithPkce,
                PublicKeyCert = client.PublicKeyCert
            };

            return new APIResponse
            {
                Success = true,
                Message = "Successfully received client details.",
                Result = response
            };
        }

        [HttpGet]
        [Route("get-client-by-clientId")]
        public async Task<APIResponse> GetClientByClientIdAsync(string clientId)
        {
            var client = await _clientService.GetClientByClientIdAsync(clientId);

            if (client == null)
            {
                return new APIResponse
                {
                    Success = false,
                    Message = "No client found."
                };
            }

            var response = new ClientResponseDTO
            {
                Id = client.Id,
                ClientId = client.ClientId,
                ClientSecret = client.ClientSecret,
                ApplicationName = client.ApplicationName,
                ApplicationNameArabic = client.ApplicationNameArabic,
                ApplicationType = client.ApplicationType,
                ApplicationUrl = client.ApplicationUrl,
                RedirectUri = client.RedirectUri,
                LogoutUri = client.LogoutUri,
                GrantTypes = client.GrantTypes,
                Scopes = client.Scopes,
             
                Status = client.Status,
                OrganizationUid = client.OrganizationUid,
                AuthScheme = client.AuthScheme,
                WithPkce = client.WithPkce,
                PublicKeyCert = client.PublicKeyCert
            };

            return new APIResponse
            {
                Success = true,
                Message = "Successfully received client details.",
                Result = response
            };
        }

        [HttpGet]
        [Route("get-client-profilesAndPurposes")]
        public async Task<APIResponse> GetClientProfilesAndPurposesAsync(string clientId)
        {
            var client = await _clientService.GetClientProfilesAndPurposesAsync(clientId);
            if (client == null)
            {
                return new APIResponse
                {
                    Success = false,
                    Message = "No client found."
                };
            }
            var eConsentClient = client.EConsentClients.FirstOrDefault(s => s.Status == "ACTIVE");
            if (eConsentClient == null)
            {
                return new APIResponse
                {
                    Success = false,
                    Message = "No e-consent details found for the client."
                };
            }
            var response = new JObject();

            response.Add("Scopes", eConsentClient.Scopes);
            response.Add("Purposes", eConsentClient.Purposes);
            response.Add("Id", client.Id);
            response.Add("ClientId", client.Id);
            response.Add("ApplicationName", client.ApplicationName);
            
            return new APIResponse
            {
                Success = true,
                Message = "Successfully received client e-consent details.",
                Result = response
            };
        }

        [HttpGet]
        [Route("get-clientApp-name")]
        public async Task<APIResponse> GetClientAppNameAsync(string request)
        {
            var clientNameList = await _clientService.GetAllClientAppNames(request);
            if (clientNameList == null)
            {
                return new APIResponse
                {
                    Success = false,
                    Message = "No client found."
                };
            }

            return new APIResponse
            {
                Success = true,
                Message = "Successfully received client application name.",
                Result = clientNameList
            };
        }

        [HttpGet]
        [Route("get-all-clients-count")]
        public async Task<APIResponse> GetAllClientsCountAsync()
        {
            var count = await _clientService.GetAllClientsCount();
            return new APIResponse
            {
                Success = true,
                Message = "Successfully received clients count.",
                Result = count
            };
        }

        [HttpGet]
        [Route("get-enum-clientIds")]
        public async Task<APIResponse> GetEnumClientIdsAsync()
        {
            var clientIds = await _clientService.EnumClientIds();
            if (clientIds == null || !clientIds.Any())
            {
                return new APIResponse
                {
                    Success = false,
                    Message = "No clients found."
                };
            }
            return new APIResponse
            {
                Success = true,
                Message = "Successfully received client IDs.",
                Result = clientIds
            };
        }

        [HttpGet]
        [Route("get-enum-clients")]
        public async Task<APIResponse> GetEnumClientsAsync()
        {
            var clientIds = await _clientService.GetClientsByName(String.Empty);
            if (clientIds == null || !clientIds.Any())
            {
                return new APIResponse
                {
                    Success = false,
                    Message = "No clients found."
                };
            }
            return new APIResponse
            {
                Success = true,
                Message = "Successfully received client IDs.",
                Result = clientIds
            };
        }

        [HttpGet]
        [Route("get-applications-list")]
        public async Task<APIResponse> GetApplicationsListAsync()
        {
            var clientIds = await _clientService.GetApplicationsList();
            if (clientIds == null || !clientIds.Any())
            {
                return new APIResponse
                {
                    Success = false,
                    Message = "No clients found."
                };
            }
            return new APIResponse
            {
                Success = true,
                Message = "Successfully received client IDs.",
                Result = clientIds
            };
        }

        [HttpGet]
        [Route("get-applications-list-by-orgId")]
        public async Task<APIResponse> GetApplicationsListByOrgIdAsync(string orgid)
        {
            var clientIds = await _clientService.GetApplicationsListByOuid(orgid);
            if (clientIds == null || !clientIds.Any())
            {
                return new APIResponse
                {
                    Success = false,
                    Message = "No clients found."
                };
            }
            return new APIResponse
            {
                Success = true,
                Message = "Successfully received client IDs.",
                Result = clientIds
            };
        }

        [HttpGet]
        [Route("get-client-names")]
        public async Task<APIResponse> GetClientNamesAsync(string value)
        {
            var clientIds = await _clientService.GetClientNamesAsync(value);
            if (clientIds == null || !clientIds.Any())
            {
                return new APIResponse
                {
                    Success = false,
                    Message = "No clients found."
                };
            }
            return new APIResponse
            {
                Success = true,
                Message = "Successfully received client IDs.",
                Result = clientIds
            };
        }

        [HttpGet]
        [Route("get-application-dictionary")]
        public async Task<APIResponse> GetApplicationsDictionaryAsync()
        {
            var clientIds = await _clientService.GetApplicationsDictionary();
            if (clientIds == null || !clientIds.Any())
            {
                return new APIResponse
                {
                    Success = false,
                    Message = "No clients found."
                };
            }
            return new APIResponse
            {
                Success = true,
                Message = "Successfully received client IDs.",
                Result = clientIds
            };
        }

        [HttpPost]
        [Route("create-client")]
        public async Task<APIResponse> CreateClient([FromBody] ClientDTO request)
        {
            try
            {
                var responseType = "";

                if (request.GrantTypes.Contains("authorization_code") ||
                    request.GrantTypes.Contains("authorization_code_with_pkce"))
                {
                    responseType = "code";
                }

                if (request.GrantTypes.Contains("implicit"))
                {
                    responseType = responseType == "" ? "token" : "code token";
                }

                var client = new Client
                {
                    ClientId = GetUniqueString(48),
                    ClientSecret = GetUniqueString(64),
                    ApplicationName = request.ApplicationName,
                    ApplicationNameArabic=request.ApplicationNameArabic,
                    ApplicationType = request.ApplicationType,
                    ApplicationUrl = request.ApplicationUrl,
                    RedirectUri = request.RedirectUri,
                    GrantTypes = request.GrantTypes,
                    Scopes = request.Scopes,
                    LogoutUri = request.LogoutUri,
                    ResponseTypes = responseType,
                    OrganizationUid = request.OrganizationUid,
                    Type = "OAUTH2",
                    PublicKeyCert = request.PublicKeyCert,
                    EncryptionCert = string.Empty,
                    CreatedBy = request.UUID,
                    UpdatedBy = request.UUID,
                    AuthScheme = request.AuthScheme
                };

                if (!string.IsNullOrEmpty(request.Profiles) ||
                    !string.IsNullOrEmpty(request.Purposes))
                {
                    var eConsentClient = new EConsentClient
                    {
                        Scopes = request.Profiles,
                        Purposes = request.Purposes,
                        CreatedBy = "API",
                        CreatedDate = DateTime.SpecifyKind(DateTime.Now, DateTimeKind.Unspecified),
                        Status = "ACTIVE"
                    };

                    client.EConsentClients.Add(eConsentClient);
                }

                var result = await _clientService.CreateClientAsync(client);

                return new APIResponse
                {
                    Success = result.Success,
                    Message = result.Message,
                    Result = result.Result
                };
            }
            catch (Exception ex)
            {
                return new APIResponse
                {
                    Success = false,
                    Message = ex.Message
                };
            }
        }

        public static string getCertificate(IFormFile file)
        {
            var result = new StringBuilder();
            using (var reader = new StreamReader(file.OpenReadStream()))
            {
                while (reader.Peek() >= 0)
                    result.AppendLine(reader.ReadLine());
            }

            return result.ToString().Replace("\r", "");
        }

        public static string get_unique_string(int length)
        {
            const string src = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
            var result = new StringBuilder(length);
            byte[] buffer = new byte[sizeof(uint)];

            using (var rng = RandomNumberGenerator.Create())
            {
                while (result.Length < length)
                {
                    rng.GetBytes(buffer);
                    uint num = BitConverter.ToUInt32(buffer, 0);
                    result.Append(src[(int)(num % (uint)src.Length)]);
                }
            }

            return result.ToString();
        }

        [HttpPost]
        [Route("saveclient")]
        public async Task<IActionResult> SaveClient([FromBody] ClientsNewViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                return Ok(new APIResponse(ModelState.Values.SelectMany(v => v.Errors).ToList().ToString()));
            }

            if (viewModel.Cert != null && viewModel.Cert.ContentType != "application/x-x509-ca-cert")
            {
                return Ok(new APIResponse("invalid signing certificate"));
            }

            var responce = "";
            if (viewModel.GrantTypes.Contains("authorization_code") || viewModel.GrantTypes.Contains("authorization_code_with_pkce"))
            {
                responce = "code";
            }
            if (viewModel.GrantTypes.Contains("implicit"))
            {
                responce = (responce == "") ? responce + " token" : "token";
            }

            var client = new Client()
            {
                ClientId = get_unique_string(48),
                ClientSecret = get_unique_string(64),
                ApplicationName = viewModel.ApplicationName,
                ApplicationType = viewModel.ApplicationType,
                ApplicationUrl = viewModel.ApplicationUri,
                RedirectUri = viewModel.RedirectUri,
                GrantTypes = viewModel.GrantTypes,
                Scopes = viewModel.Scopes,
                LogoutUri = viewModel.LogoutUri,
                ResponseTypes = responce,
                OrganizationUid = (viewModel.OrganizationId != null ? viewModel.OrganizationId : ""),
                Type = "OAUTH2",
                PublicKeyCert = (viewModel.Cert != null ? getCertificate(viewModel.Cert) : ""),
                EncryptionCert = string.Empty,
                CreatedBy = "system",
                UpdatedBy = "system"
            };

            var response = await _clientService.CreateClientAsync(client);
            if (response == null || !response.Success)
            {
                var error = (response == null ? "Internal error please contact to admin" : response.Message);
                return Ok(new APIResponse(error));
            }
            else
            {
                return Ok(new APIResponse(true, "Data added success", client));
            }
        }

        [HttpPost]
        [Route("update-client")]
        public async Task<APIResponse> UpdateClientAsync([FromBody] ClientDTO request)
        {
            var clientInDb = await _clientService.GetClientAsync(request.Id);

            if (clientInDb == null)
            {
                return new APIResponse
                {
                    Success = false,
                    Message = "Client not found"
                };
            }

            var responseType = "";

            if (request.GrantTypes.Contains("authorization_code") ||
                request.GrantTypes.Contains("authorization_code_with_pkce"))
            {
                responseType = "code";
            }

            if (request.GrantTypes.Contains("implicit"))
            {
                responseType = responseType == "" ? "token" : "code token";
            }

            clientInDb.ClientId = request.ClientId;
            clientInDb.ClientSecret = request.ClientSecret;
            clientInDb.ApplicationName = request.ApplicationName;
            clientInDb.ApplicationType = request.ApplicationType;
            clientInDb.ApplicationNameArabic = request.ApplicationNameArabic;
            clientInDb.ApplicationUrl = request.ApplicationUrl;
            clientInDb.RedirectUri = request.RedirectUri;
            clientInDb.GrantTypes = request.GrantTypes;
            clientInDb.Scopes = request.Scopes;
            clientInDb.ResponseTypes = responseType;
            clientInDb.LogoutUri = request.LogoutUri;
            clientInDb.OrganizationUid = request.OrganizationUid;
            clientInDb.PublicKeyCert = request.PublicKeyCert;
            clientInDb.AuthScheme = request.AuthScheme;
            clientInDb.UpdatedBy = request.UUID;

            var result = await _clientService.UpdateClientAsync(clientInDb, null);

            if (!result.Success)
            {
                return new APIResponse
                {
                    Success = false,
                    Message = result.Message
                };
            }

            return new APIResponse
            {
                Success = true,
                Message = result.Message,
                Result = result.Result
            };
        }

        [HttpDelete]
        [Route("delete-client")]
        public async Task<APIResponse> DeleteClientAsync([FromQuery] int id, string updatedBy)
        {
            var result = await _clientService.DeleteClientAsync(id,updatedBy);
            if (!result.Success)
            {
                return new APIResponse
                {
                    Success = false,
                    Message = result.Message
                };
            }
            return new APIResponse
            {
                Success = true,
                Message = result.Message
            };
        }

        [HttpDelete]
        [Route("delete-client-by-clientId")]
        public async Task<APIResponse> DeleteClientAsync([FromQuery] string clientId)
        {
            var result = await _clientService.DeleteClientByClientId(clientId);
            if (!result.Success)
            {
                return new APIResponse
                {
                    Success = false,
                    Message = result.Message
                };
            }
            return new APIResponse
            {
                Success = true,
                Message = result.Message
            };
        }

        [HttpPut]
        [Route("activate-client")]
        public async Task<APIResponse> ActivateClientStatusAsync([FromQuery] int id)
        {
            var result = await _clientService.ActivateClientAsync(id);
            if (!result.Success)
            {
                return new APIResponse
                {
                    Success = false,
                    Message = result.Message
                };
            }
            return new APIResponse
            {
                Success = true,
                Message = result.Message
            };
        }

        [HttpPut]
        [Route("deactivate-client")]
        public async Task<APIResponse> DeactivateClientStatusAsync([FromQuery] int id)
        {
            var result = await _clientService.DeActivateClientAsync(id);
            if (!result.Success)
            {
                return new APIResponse
                {
                    Success = false,
                    Message = result.Message
                };
            }
            return new APIResponse
            {
                Success = true,
                Message = result.Message
            };
        }

        [HttpGet]
        [Route("get-client-session")]
        public async Task<APIResponse> GetClientSessionAsync([FromQuery] string clientId)
        {
            var result = await _sessionService.GetAllClientSessions(clientId);
            if (!result.Success)
            {
                return new APIResponse
                {
                    Success = false,
                    Message = result.Message
                };
            }
            return new APIResponse
            {
                Success = true,
                Message = result.Message,
                Result = result
            };
        }
        [HttpPost]
        [Route("logout-client-session")]
        public async Task<APIResponse> LogoutClientSessionAsync([FromQuery] string sessionID)
        {
            var data = new LogoutUserRequest
            {
                GlobalSession = sessionID
            };

            var result = await _sessionService.LogoutUser(data);
            if (!result.Success)
            {
                return new APIResponse
                {
                    Success = false,
                    Message = result.Message
                };
            }
            return new APIResponse
            {
                Success = true,
                Message = result.Message,
                Result = result.Result
            };
        }

        [HttpGet]
        [Route("get-auth-schemas-list")]
        public async Task<APIResponse> GetAuthSchemasListAsync()
        {
            var count = await _authSchemeService.ListAuthSchemesAsync();
            return new APIResponse
            {
                Success = true,
                Message = "Successfully received Auth Schemas.",
                Result = count
            };
        }
        [NonAction]
        string GetUniqueString(int length)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
            var result = new StringBuilder(length);

            for (int i = 0; i < length; i++)
            {
                int index = RandomNumberGenerator.GetInt32(chars.Length);
                result.Append(chars[index]);
            }

            return result.ToString();
        }
    }
}
