using DTPortal.Core.Constants;
using DTPortal.Core.Domain.Models;
using DTPortal.Core.Domain.Services;
using DTPortal.Core.Domain.Services.Communication;
using DTPortal.Core.DTOs;
using Google.Api.Gax.ResourceNames;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace DTPortal.Core.Services
{
    public class OrganizationService : IOrganizationService
    {
        private readonly HttpClient _client;
        private readonly ILogger<OrganizationService> _logger;
        private readonly IConfiguration _configuration;

        public OrganizationService(HttpClient httpClient,
            IConfiguration configuration,
            ILogger<OrganizationService> logger)
        {
            _configuration = configuration;
            httpClient.BaseAddress = new Uri(configuration["APIServiceLocations:OrganizationOnboardingServiceBaseAddress"]);
            httpClient.DefaultRequestHeaders.Add("Accept", "application/json");
           
            _client = httpClient;
            _client.Timeout = TimeSpan.FromMinutes(10);
            _logger = logger;
        }
         public async Task<OrganizationCountDTO> GetOrganizationStatusCount()
        {
            try
            {
                _client.BaseAddress = new Uri(
                    _configuration["APIServiceLocations:OrganizationServiceBaseAddress"]);

                HttpResponseMessage response =
                    await _client.GetAsync("api/get/status");

                if (response.IsSuccessStatusCode)
                {
                    var apiResponse = JsonConvert.DeserializeObject<APIResponse>(
                        await response.Content.ReadAsStringAsync());

                    if (apiResponse.Success)
                    {
                        return JsonConvert.DeserializeObject<OrganizationCountDTO>(
                            apiResponse.Result.ToString());
                    }

                    _logger.LogError(apiResponse.Message);
                }
                else
                {
                    _logger.LogError($"Request failed: {response.StatusCode}");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
            }

            return null;
        }


        public async Task<string[]> GetOrganizationNamesAysnc(string value)
        {
            try
            {
                HttpResponseMessage response = await _client.GetAsync($"api/get/organization/searchType?searchType={value}");
                if (response.StatusCode == HttpStatusCode.OK)
                {
                    APIResponse apiResponse = JsonConvert.DeserializeObject<APIResponse>(await response.Content.ReadAsStringAsync());
                    if (apiResponse.Success)
                    {
                        return JsonConvert.DeserializeObject<string[]>(apiResponse.Result.ToString());
                    }
                    else
                    {
                        _logger.LogError(apiResponse.Message);
                    }
                }
                else
                {
                    _logger.LogError($"The request with uri={response.RequestMessage.RequestUri} failed " +
                       $"with status code={response.StatusCode}");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
            }

            return null;
        }

        public async Task<string[]> GetOrganizationNamesAndIdAysnc()
        {
            try
            {
                HttpResponseMessage response = await _client.GetAsync($"api/get/organiztion");
                if (response.StatusCode == HttpStatusCode.OK)
                {
                    APIResponse apiResponse = JsonConvert.DeserializeObject<APIResponse>(await response.Content.ReadAsStringAsync());
                    if (apiResponse.Success)
                    {
                        return JsonConvert.DeserializeObject<string[]>(apiResponse.Result.ToString());
                    }
                    else
                    {
                        _logger.LogError(apiResponse.Message);
                    }
                }
                else
                {
                    _logger.LogError($"The request with uri={response.RequestMessage.RequestUri} failed " +
                       $"with status code={response.StatusCode}");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
            }

            return null;
        }

        public async Task<string[]> GetActiveSubscribersEmailListAsync(string value)
        {
            try
            {
                HttpResponseMessage response = await _client.GetAsync($"api/get/subscriber/email-By-searchType?searchType={value}");
                if (response.StatusCode == HttpStatusCode.OK)
                {
                    APIResponse apiResponse = JsonConvert.DeserializeObject<APIResponse>(await response.Content.ReadAsStringAsync());
                    if (apiResponse.Success)
                    {
                        return JsonConvert.DeserializeObject<string[]>(apiResponse.Result.ToString());
                    }
                    else
                    {
                        _logger.LogError(apiResponse.Message);
                    }
                }
                else
                {
                    _logger.LogError($"The request with uri={response.RequestMessage.RequestUri} failed " +
                       $"with status code={response.StatusCode}");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
            }

            return null;
        }

        public async Task<IList<SignatureTemplatesDTO>> GetSignatureTemplateListAsyn()
        {
            try
            {
                HttpResponseMessage response = await _client.GetAsync($"api/get/all/templates");
                if (response.StatusCode == HttpStatusCode.OK)
                {
                    APIResponse apiResponse = JsonConvert.DeserializeObject<APIResponse>(await response.Content.ReadAsStringAsync());
                    if (apiResponse.Success)
                    {
                        return JsonConvert.DeserializeObject<IList<SignatureTemplatesDTO>>(apiResponse.Result.ToString());
                    }
                    else
                    {
                        _logger.LogError(apiResponse.Message);
                    }
                }
                else
                {
                    _logger.LogError($"The request with uri={response.RequestMessage.RequestUri} failed " +
                       $"with status code={response.StatusCode}");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
            }

            return null;
        }

        public async Task<OrganizationDTO> GetOrganizationDetailsAsync(string organizationName)
        {
            try
            {
                _logger.LogInformation("Get organization details by organization name api call start");
                HttpResponseMessage response = await _client.GetAsync($"api/get/organization/detailsBy/organizationName?organizationName={organizationName}");
                _logger.LogInformation("Get organization details by organization name api call end");
                if (response.StatusCode == HttpStatusCode.OK)
                {
                    APIResponse apiResponse = JsonConvert.DeserializeObject<APIResponse>(await response.Content.ReadAsStringAsync());
                    if (apiResponse.Success)
                    {
                        OrganizationDTO organization = JsonConvert.DeserializeObject<OrganizationDTO>(apiResponse.Result.ToString());
                        organization.IsDetailsAvailable = true;

                        return organization;
                    }
                    else
                    {
                        _logger.LogError(apiResponse.Message);
                        return new OrganizationDTO();
                    }
                }
                else
                {
                    _logger.LogError($"The request with URI={response.RequestMessage.RequestUri} failed " +
                           $"with status code={response.StatusCode}");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
            }

            return null;
        }

        public async Task<ServiceResult> GetOrganizationDetailsByUIdAsync(string organizationUid)
        {
            try
            {
                _logger.LogInformation("Get organization details by id api call start");
                HttpResponseMessage response = await _client.GetAsync($"api/get/organization/detailsById/{organizationUid}");
                _logger.LogInformation("Get organization details by id api call end");
                if (response.StatusCode == HttpStatusCode.OK)
                {
                    APIResponse apiResponse = JsonConvert.DeserializeObject<APIResponse>(await response.Content.ReadAsStringAsync());
                    if (apiResponse.Success)
                    {
                        OrganizationDTO organization = JsonConvert.DeserializeObject<OrganizationDTO>(apiResponse.Result.ToString());
                        if (organization != null)
                        {
                            organization.IsDetailsAvailable = true;

                            return new ServiceResult(true, apiResponse.Message, organization);
                        }
                        else
                        {
                            return new ServiceResult(false, apiResponse.Message, organization);
                        }
                    }
                    else
                    {
                        _logger.LogError(apiResponse.Message);
                    }
                }
                else
                {
                    _logger.LogError($"The request with URI={response.RequestMessage.RequestUri} failed " +
                           $"with status code={response.StatusCode}");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
            }

            return new ServiceResult(false, "An error occurred while getting organization details. Please try later.");
        }

        public async Task<IEnumerable<RevokeReasonDTO>> GetAllRevokeReasonsAsync()
        {
            try
            {
                HttpResponseMessage response = await _client.GetAsync("api/get/service/revoke-reasons");
                if (response.StatusCode == HttpStatusCode.OK)
                {
                    APIResponse apiResponse = JsonConvert.DeserializeObject<APIResponse>(await response.Content.ReadAsStringAsync());
                    if (apiResponse.Success)
                    {
                        return JsonConvert.DeserializeObject<IEnumerable<RevokeReasonDTO>>(apiResponse.Result.ToString());
                    }
                    else
                    {
                        _logger.LogError(apiResponse.Message);
                    }
                }
                else
                {
                    _logger.LogError($"The request with uri={response.RequestMessage.RequestUri} failed " +
                       $"with status code={response.StatusCode}");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
            }

            return null;
        }

        public async Task<ServiceResult> AddOrganizationAsync(OrganizationDTO organization, bool makerCheckerFlag = false)
        {
            try
            {
                _logger.LogInformation("AddOrganizationAsync start");

                //var isExists = await IsOrganizationExists(organization.OrganizationName);
                //if (isExists == true)
                //{
                //    _logger.LogError($"Organization with Organization Name ={organization.OrganizationName} already exists");
                //    return new ServiceResult(false, "Organization already exists");
                //}

                // Trim the organization name to remove leading and trailing spaces
                var organizationName = organization.OrganizationName.Trim(); 
                var isExists = await IsOrganizationExists(organizationName);

                if (isExists)
                {
                    _logger.LogError($"Organization with Organization Name = {organizationName} already exists");
                    return new ServiceResult(false, "Organization already exists");
                }

                string json = JsonConvert.SerializeObject(organization,
                    new JsonSerializerSettings { ContractResolver = new CamelCasePropertyNamesContractResolver() });
                StringContent content = new StringContent(json, Encoding.UTF8, "application/json");

                _logger.LogInformation("Add Organization api call start");
                HttpResponseMessage response = await _client.PostAsync("api/post/service/register/organization", content);
                _logger.LogInformation("Add Organization api call end");
                if (response.StatusCode == HttpStatusCode.OK)
                {
                    APIResponse apiResponse = JsonConvert.DeserializeObject<APIResponse>(await response.Content.ReadAsStringAsync());
                    if (apiResponse.Success)
                    {
                        return new ServiceResult(true, apiResponse.Message);
                    }
                    else
                    {
                        _logger.LogError(apiResponse.Message);
                        return new ServiceResult(false, apiResponse.Message);
                    }
                }
                else
                {
                    _logger.LogError($"The request with URI={response.RequestMessage.RequestUri} failed " +
                               $"with status code={response.StatusCode}");
                }

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
            }

            _logger.LogInformation("AddOrganizationAsync end");
            return new ServiceResult(false, "An error occurred while creating the organization. Please try later.");
        }

        public async Task<ServiceResult> UpdateOrganizationAsync(OrganizationDTO Organization, bool makerCheckerFlag = false)
        {
            try
            {
                _logger.LogInformation("UpdateOrganizationAsync start");
                var organization = await GetOrganizationDetailsByUIdAsync(Organization.OrganizationUid);
                if (!organization.Success)
                {
                    return new ServiceResult(false, organization.Message);
                    //return new ServiceResult(false, "Organization doesn't exists");
                }

                var org = (OrganizationDTO)organization.Resource;

                Organization.SignedPdf = org.SignedPdf;

                string json = JsonConvert.SerializeObject(Organization,
                    new JsonSerializerSettings { ContractResolver = new CamelCasePropertyNamesContractResolver() });
                StringContent content = new StringContent(json, Encoding.UTF8, "application/json");
                _logger.LogInformation("Update Organization api call start");
                HttpResponseMessage response = await _client.PostAsync("api/post/update-organization", content);
                _logger.LogInformation("Update Organization api call end");
                if (response.StatusCode == HttpStatusCode.OK)
                {
                    APIResponse apiResponse = JsonConvert.DeserializeObject<APIResponse>(await response.Content.ReadAsStringAsync());
                    if (apiResponse.Success)
                    {
                        return new ServiceResult(true, apiResponse.Message);
                    }
                    else
                    {
                        _logger.LogError(apiResponse.Message);
                        return new ServiceResult(false, apiResponse.Message);
                    }
                }
                else
                {
                    _logger.LogError($"The request with URI={response.RequestMessage.RequestUri} failed " +
                               $"with status code={response.StatusCode}");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
            }

            _logger.LogInformation("UpdateOrganizationAsync end");
            return new ServiceResult(false, "An error occurred while updating the organization. Please try later.");
        }

        public async Task<ServiceResult> DelinkOrganizationEmployeeAsync(DelinkOrganizationEmployeeDTO delinkOrganizationEmployee)
        {
            try
            {
                string json = JsonConvert.SerializeObject(delinkOrganizationEmployee,
                    new JsonSerializerSettings { ContractResolver = new CamelCasePropertyNamesContractResolver() });
                StringContent content = new StringContent(json, Encoding.UTF8, "application/json");

                HttpResponseMessage response = await _client.PostAsync("api/post/deactive", content);
                if (response.StatusCode == HttpStatusCode.OK)
                {
                    APIResponse apiResponse = JsonConvert.DeserializeObject<APIResponse>(await response.Content.ReadAsStringAsync());
                    if (apiResponse.Success)
                    {
                        return new ServiceResult(true, apiResponse.Message);
                    }
                    else
                    {
                        _logger.LogError(apiResponse.Message);
                        return new ServiceResult(false, apiResponse.Message);
                    }
                }
                else
                {
                    _logger.LogError($"The request with URI={response.RequestMessage.RequestUri} failed " +
                               $"with status code={response.StatusCode}");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
            }

            return new ServiceResult(false, "An error occurred while delinking an organization employee email. Please try later.");
        }

        public async Task<ServiceResult> ValidateEmailListAsync(List<string> value)
        {
            try
            {
                var json = JsonConvert.SerializeObject(new
                {
                    EmailList = value
                }, new JsonSerializerSettings { ContractResolver = new CamelCasePropertyNamesContractResolver() });
                StringContent content = new StringContent(json, Encoding.UTF8, "application/json");

                HttpResponseMessage response = await _client.PostAsync("api/post/add/authorizeduser", content);
                if (response.StatusCode == HttpStatusCode.OK)
                {
                    APIResponse apiResponse = JsonConvert.DeserializeObject<APIResponse>(await response.Content.ReadAsStringAsync());
                    if (apiResponse.Success)
                    {
                        return new ServiceResult(true, apiResponse.Message);
                    }
                    else
                    {
                        _logger.LogError(apiResponse.Message);
                        return new ServiceResult(false, apiResponse.Message, JsonConvert.DeserializeObject<string[]>(apiResponse.Result.ToString()));
                    }
                }
                else
                {
                    _logger.LogError($"The request with URI={response.RequestMessage.RequestUri} failed " +
                               $"with status code={response.StatusCode}");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
            }

            return new ServiceResult(false, "An error occurred while validating emails. Please try later.");
        }

        public async Task<bool> IsOrganizationExists(string orgName)
        {
            try
            {
                HttpResponseMessage response = await _client.GetAsync($"api/get/organization-exist?organizationName={orgName}");
                if (response.StatusCode == HttpStatusCode.OK)
                {
                    APIResponse apiResponse = JsonConvert.DeserializeObject<APIResponse>(await response.Content.ReadAsStringAsync());
                    return apiResponse.Success;
                }
                else
                {
                    _logger.LogError($"The request with URI={response.RequestMessage.RequestUri} failed " +
                           $"with status code={response.StatusCode}");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
            }

            return false;
        }

        public async Task<ServiceResult> IssueCertificateAsync(string organizationUid, string uuid, string transactionReferenceId, bool makerCheckerFlag = false)
        {
            try
            {
                _logger.LogInformation("IssueCertificateAsync start");
                var organization = await GetOrganizationDetailsByUIdAsync(organizationUid);
                if (!organization.Success)
                {
                    return new ServiceResult(false, organization.Message);
                }

                var org = (OrganizationDTO)organization.Resource;
                org.TransactionReferenceId = transactionReferenceId;

                string issueCertificate = JsonConvert.SerializeObject(
                   new
                   {
                       OrganizationUid = organizationUid,
                       IsPostPaid = org.EnablePostPaidOption,
                       TransactionReferenceId = transactionReferenceId,

                   }, new JsonSerializerSettings { ContractResolver = new CamelCasePropertyNamesContractResolver() });
                StringContent content = new StringContent(issueCertificate, Encoding.UTF8, "application/json");
                _logger.LogInformation("Issue Certificate request body: "+ issueCertificate.ToString());
                _logger.LogInformation("Issue CertificateAsync api call start");
                // HttpResponseMessage response = await _client.PostAsync($"post/service/issue/certificates/{organizationUid}/{org.EnablePostPaidOption}", null);
                HttpResponseMessage response = await _client.PostAsync($"post/service/issue/certificates", content);
                _logger.LogInformation("Issue CertificateAsync api call end");
                if (response.StatusCode == HttpStatusCode.OK)
                {
                    APIResponse apiResponse = JsonConvert.DeserializeObject<APIResponse>(await response.Content.ReadAsStringAsync());
                    _logger.LogInformation($"Issue Certificate API response: {apiResponse.Result.ToString()}");
                    if (apiResponse.Success)
                    {
                        return new ServiceResult(true, apiResponse.Message);
                    }
                    else
                    {
                        _logger.LogError(apiResponse.Message);
                        return new ServiceResult(false, apiResponse.Message);
                    }
                }
                else
                {
                    _logger.LogError($"The request with URI={response.RequestMessage.RequestUri} failed " +
                               $"with status code={response.StatusCode}");
                }

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
            }

            _logger.LogInformation("IssueCertificateAsync end");
            return new ServiceResult(false, "An error occurred while issuing certificate. Please try later.");
        }

        public async Task<ServiceResult> IssueWalletCertificateAsync(string organizationUid, string uuid, string transactionReferenceId, bool makerCheckerFlag = false)
        {
            try
            {
                _logger.LogInformation("IssueCertificateAsync start");
                var organization = await GetOrganizationDetailsByUIdAsync(organizationUid);
                if (!organization.Success)
                {
                    return new ServiceResult(false, organization.Message);
                }

                var org = (OrganizationDTO)organization.Resource;
                org.TransactionReferenceId = transactionReferenceId;

                string issueCertificate = JsonConvert.SerializeObject(
                   new
                   {
                       OrganizationUid = organizationUid,
                       IsPostPaid = org.EnablePostPaidOption,
                       TransactionReferenceId = transactionReferenceId,

                   }, new JsonSerializerSettings { ContractResolver = new CamelCasePropertyNamesContractResolver() });
                StringContent content = new StringContent(issueCertificate, Encoding.UTF8, "application/json");

                _logger.LogInformation("Issue CertificateAsync api call start");
                // HttpResponseMessage response = await _client.PostAsync($"post/service/issue/certificates/{organizationUid}/{org.EnablePostPaidOption}", null);
                HttpResponseMessage response = await _client.PostAsync($"issue-wallet-certificate", content);
                _logger.LogInformation("Issue CertificateAsync api call end");
                if (response.StatusCode == HttpStatusCode.OK)
                {
                    APIResponse apiResponse = JsonConvert.DeserializeObject<APIResponse>(await response.Content.ReadAsStringAsync());
                    if (apiResponse.Success)
                    {
                        return new ServiceResult(true, apiResponse.Message);
                    }
                    else
                    {
                        _logger.LogError(apiResponse.Message);
                        return new ServiceResult(false, apiResponse.Message);
                    }
                }
                else
                {
                    _logger.LogError($"The request with URI={response.RequestMessage.RequestUri} failed " +
                               $"with status code={response.StatusCode}");
                }

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
            }

            _logger.LogInformation("IssueCertificateAsync end");
            return new ServiceResult(false, "An error occurred while issuing certificate. Please try later.");
        }

        public async Task<ServiceResult> RevokeCertificateAsync(string organizationUid, int reasonId, string remarks, string uuid, bool makerCheckerFlag = false)
        {
            try
            {
                var organization = await GetOrganizationDetailsByUIdAsync(organizationUid);
                if (!organization.Success)
                {
                    return new ServiceResult(false, organization.Message);
                }

                var org = (OrganizationDTO)organization.Resource;

                string json = JsonConvert.SerializeObject(
                    new
                    {
                        OrganizationUid = organizationUid,
                        ReasonId = reasonId,
                        Remarks = remarks
                    }, new JsonSerializerSettings { ContractResolver = new CamelCasePropertyNamesContractResolver() });
                StringContent content = new StringContent(json, Encoding.UTF8, "application/json");

                HttpResponseMessage response = await _client.PostAsync($"post/service/certificate/revoke", content);
                if (response.StatusCode == HttpStatusCode.OK)
                {
                    APIResponse apiResponse = JsonConvert.DeserializeObject<APIResponse>(await response.Content.ReadAsStringAsync());
                    if (apiResponse.Success)
                    {
                        return new ServiceResult(true, apiResponse.Message);
                    }
                    else
                    {
                        _logger.LogError(apiResponse.Message);
                        return new ServiceResult(false, apiResponse.Message);
                    }
                }
                else
                {
                    _logger.LogError($"The request with URI={response.RequestMessage.RequestUri} failed " +
                               $"with status code={response.StatusCode}");
                }

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
            }

            return new ServiceResult(false, "An error occurred while revoking certificate. Please try later.");
        }

        public async Task<ServiceResult> VerifyDocumentSignatureAsync(string organizationUid, string uuid, string docType, string signedDoc, IList<string> signatories)
        {
            try
            {
                _logger.LogInformation("VerifyDocumentSignatureAsync start");

                var organization = await GetOrganizationDetailsByUIdAsync(organizationUid);
                if (!organization.Success)
                {
                    return new ServiceResult(false, organization.Message);
                }

                string json = JsonConvert.SerializeObject(
                    new
                    {
                        DocumentType = docType,
                        DocData = signedDoc,
                        SubscriberUid = uuid,
                        OrganizationUid = organizationUid,
                        Signatories = signatories
                    }, new JsonSerializerSettings { ContractResolver = new CamelCasePropertyNamesContractResolver() });

                //_logger.LogInformation($"Verify Document Signature Request Payload: {json}");
                StringContent content = new StringContent(json, Encoding.UTF8, "application/json");

                _logger.LogInformation("verify api call start");
                HttpResponseMessage response = await _client.PostAsync($"api/verify", content);
                _logger.LogInformation("verify api call end");
                _logger.LogInformation($"Response Status Code: {response.StatusCode}");
                _logger.LogInformation($"Response Request URI: {response.Content}");
                if (response.StatusCode == HttpStatusCode.OK)
                {
                    APIResponse apiResponse = JsonConvert.DeserializeObject<APIResponse>(await response.Content.ReadAsStringAsync());
                    if (apiResponse.Success)
                    {
                        var signatureVerificationdetails = new[]
                        {
                         new
                         {
                            SignedBy = "",
                            SignedTime = "",
                            ValidationTime = "",
                            SignatureValid = false
                        }};

                        JObject result = (JObject)JToken.FromObject(apiResponse.Result);
                        _logger.LogInformation($"Verify Document Result: {result.ToString()}");
                        var verificationdetails = JsonConvert.DeserializeAnonymousType(result["signatureVerificationDetails"].ToString(), signatureVerificationdetails);
                        return new ServiceResult(verificationdetails[0].SignatureValid, apiResponse.Message);
                    }
                    else
                    {
                        _logger.LogError(apiResponse.Message);
                        return new ServiceResult(false, apiResponse.Message);
                    }
                }
                else
                {
                    _logger.LogError($"The request with URI={response.RequestMessage.RequestUri} failed " +
                               $"with status code={response.StatusCode}");
                }

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
            }

            _logger.LogInformation("VerifyDocumentSignatureAsync end");
            return new ServiceResult(false, "An error occurred while verifying the document signature. Please try later.");
        }

        public async Task<ServiceResult> GetEsealCertificateStatus(string organizationUid)
        {

            try
            {
                HttpResponseMessage response = await _client.GetAsync($"api/get/orgStatus?orgUid={organizationUid}");
                if (response.StatusCode == HttpStatusCode.OK)
                {
                    APIResponse apiResponse = JsonConvert.DeserializeObject<APIResponse>(await response.Content.ReadAsStringAsync());
                    if (apiResponse.Success)
                    {
                        
                        return new ServiceResult(true, apiResponse.Message, apiResponse.Result);
                    }
                    else
                    {
                        _logger.LogError(apiResponse.Message);
                        return new ServiceResult(false, apiResponse.Message);

                    }
                }
                else
                {
                    _logger.LogError($"The request with URI={response.RequestMessage.RequestUri} failed " +
                           $"with status code={response.StatusCode}");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
            }

            return new ServiceResult(false, "An error occurred while getting Eseal-Certificate Status. Please try later.");
        }

        public async Task<ServiceResult> GetStakeholdersAsync(string organizationUid)
        {

            try
            {
                HttpResponseMessage response = await _client.GetAsync($"api/get/allstakeholder?referredBy={organizationUid}&stakeholderType=");
                if (response.StatusCode == HttpStatusCode.OK)
                {
                    APIResponse apiResponse = JsonConvert.DeserializeObject<APIResponse>(await response.Content.ReadAsStringAsync());
                    if (apiResponse.Success)
                    {

                        return new ServiceResult(true, apiResponse.Message, apiResponse.Result);
                    }
                    else
                    {
                        _logger.LogError(apiResponse.Message);
                        return new ServiceResult(false, apiResponse.Message);

                    }
                }
                else
                {
                    _logger.LogError($"The request with URI={response.RequestMessage.RequestUri} failed " +
                           $"with status code={response.StatusCode}");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
            }

            return new ServiceResult(false, "An error occurred while getting Stakeholder list. Please try later.");
        }


        public async Task<ServiceResult> AddStakeHolder(IList<StakeholderDTO> stakeholderDTO)
        {

            try
            {
                //List<StakeholderDTO> list = new List<StakeholderDTO>();
                //list.Add(stakeholderDTO);
                Stakeholder stakeholder = new Stakeholder()
                {
                    trustedStakeholderDtosList = stakeholderDTO
                };

                string json = JsonConvert.SerializeObject(stakeholder,
                   new JsonSerializerSettings { ContractResolver = new CamelCasePropertyNamesContractResolver() });
                StringContent content = new StringContent(json, Encoding.UTF8, "application/json");

                HttpResponseMessage response = await _client.PostAsync($"api/post/addstakeholderlist", content);
                if (response.StatusCode == HttpStatusCode.OK)
                {
                    APIResponse apiResponse = JsonConvert.DeserializeObject<APIResponse>(await response.Content.ReadAsStringAsync());
                    if (apiResponse.Success)
                    {

                        return new ServiceResult(true, apiResponse.Message);
                    }
                    else
                    {
                        _logger.LogError(apiResponse.Message);
                        return new ServiceResult(false, apiResponse.Message);

                    }
                }
                else
                {
                    _logger.LogError($"The request with URI={response.RequestMessage.RequestUri} failed " +
                           $"with status code={response.StatusCode}");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
            }

            return new ServiceResult(false, "An error occurred while Adding the StakeHolder. Please try later.");
        }

        public async Task<OrganizationDTO> GetOrganizationDetailsByUId(string organizationUid)
        {
            try
            {
                _logger.LogInformation("Get organization details by organization name api call start");
                HttpResponseMessage response = await _client.GetAsync($"api/get/organization/detailsById/{organizationUid}");
                _logger.LogInformation("Get organization details by organization name api call end");
                if (response.StatusCode == HttpStatusCode.OK)
                {
                    APIResponse apiResponse = JsonConvert.DeserializeObject<APIResponse>(await response.Content.ReadAsStringAsync());
                    if (apiResponse.Success)
                    {
                        OrganizationDTO organization = JsonConvert.DeserializeObject<OrganizationDTO>(apiResponse.Result.ToString());
                        organization.IsDetailsAvailable = true;

                        return organization;
                    }
                    else
                    {
                        _logger.LogError(apiResponse.Message);
                        return new OrganizationDTO();
                    }
                }
                else
                {
                    _logger.LogError($"The request with URI={response.RequestMessage.RequestUri} failed " +
                           $"with status code={response.StatusCode}");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
            }

            return null;
        }

        public async Task<ServiceResult> GetVendorsAsync(string organizationUid)
        {

            try
            {
                HttpResponseMessage response = await _client.GetAsync($"api/get/all/beneficiaries/by/sponsor-id/{organizationUid}");
                if (response.StatusCode == HttpStatusCode.OK)
                {
                    APIResponse apiResponse = JsonConvert.DeserializeObject<APIResponse>(await response.Content.ReadAsStringAsync());
                    if (apiResponse.Success)
                    {
                        JArray vendorArray = JArray.Parse(apiResponse.Result.ToString());
                        var vendorList = JsonConvert.DeserializeObject<List<VendorListDTO>>(vendorArray.ToString());
                        return new ServiceResult(true, apiResponse.Message, vendorList);
                    }
                    else
                    {
                        _logger.LogError(apiResponse.Message);
                        return new ServiceResult(false, apiResponse.Message);

                    }
                }
                else
                {
                    _logger.LogError($"The request with URI={response.RequestMessage.RequestUri} failed " +
                           $"with status code={response.StatusCode}");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
            }

            return new ServiceResult(false, "An error occurred while getting Stakeholder list. Please try later.");
        }

        public async Task<ServiceResult> AddVendor(AddVendorDTO addVendorDTO)
        {

            try
            {

                string json = JsonConvert.SerializeObject(addVendorDTO,
                   new JsonSerializerSettings { ContractResolver = new CamelCasePropertyNamesContractResolver() });
                StringContent content = new StringContent(json, Encoding.UTF8, "application/json");

                HttpResponseMessage response = await _client.PostAsync($"api/add/beneficiaries", content);
                if (response.StatusCode == HttpStatusCode.OK)
                {
                    APIResponse apiResponse = JsonConvert.DeserializeObject<APIResponse>(await response.Content.ReadAsStringAsync());
                    if (apiResponse.Success)
                    {

                        return new ServiceResult(true, apiResponse.Message);
                    }
                    else
                    {
                        _logger.LogError(apiResponse.Message);
                        return new ServiceResult(false, apiResponse.Message);

                    }
                }
                else
                {
                    _logger.LogError($"The request with URI={response.RequestMessage.RequestUri} failed " +
                           $"with status code={response.StatusCode}");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
            }

            return new ServiceResult(false, "An error occurred while Adding the StakeHolder. Please try later.");
        }

        public async Task<ServiceResult> VerifyVendor(string orgId, string vendorId)
        {
            try
            {
                HttpResponseMessage response = await _client.GetAsync($"api/verify-vendor-id?orgid={orgId}&vendorId={vendorId}");
                if (response.StatusCode == HttpStatusCode.OK)
                {
                    APIResponse apiResponse = JsonConvert.DeserializeObject<APIResponse>(await response.Content.ReadAsStringAsync());
                    if (apiResponse.Success)
                    {
                        return new ServiceResult(true, apiResponse.Message, apiResponse.Result);
                    }
                    else
                    {
                        _logger.LogError(apiResponse.Message);
                        return new ServiceResult(false, apiResponse.Message);

                    }
                }
                else
                {
                    _logger.LogError($"The request with URI={response.RequestMessage.RequestUri} failed " +
                           $"with status code={response.StatusCode}");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
            }

            return new ServiceResult(false, "An error occurred while verifying vendor. Please try later.");
        }

        public async Task<Dictionary<string,string>> GetOrganizationsDictionary()
        {
            var organizationsString = await GetOrganizationNamesAndIdAysnc();
            if(organizationsString == null || organizationsString.Length == 0)
            {
                return new Dictionary<string, string>();
            }
            var dictionary = organizationsString.Select(s => s.Split(new[] { ',' }, 2))
                                .Where(parts => parts.Length == 2)
                                .ToDictionary(parts => parts[1].Trim(), parts => parts[0].Trim());
            return dictionary;
        }

        public async Task<List<OrganizationListResponse>> GetOrganizationsListDictionary()
        {
            try
            {
                HttpResponseMessage response = await _client.GetAsync($"api/get/org/list/by/ouid");
                if (response.StatusCode == HttpStatusCode.OK)
                {
                    APIResponse apiResponse = JsonConvert.DeserializeObject<APIResponse>(await response.Content.ReadAsStringAsync());
                    if (apiResponse.Success)
                    {
                        return JsonConvert.DeserializeObject<List<OrganizationListResponse>>(apiResponse.Result.ToString());
                    }
                    else
                    {
                        _logger.LogError(apiResponse.Message);
                    }
                }
                else
                {
                    _logger.LogError($"The request with uri={response.RequestMessage.RequestUri} failed " +
                       $"with status code={response.StatusCode}");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
            }
            return null;
        }
    }
}
            

