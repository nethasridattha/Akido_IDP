using System.Threading.Tasks;
using System.Collections.Generic;

using DTPortal.Core.DTOs;
using DTPortal.Core.Domain.Services.Communication;

namespace DTPortal.Core.Domain.Services
{
    public interface IOrganizationService
    {
        Task<string[]> GetOrganizationNamesAysnc(string value);
        Task<OrganizationCountDTO> GetOrganizationStatusCount();
        Task<string[]> GetOrganizationNamesAndIdAysnc();

        Task<string[]> GetActiveSubscribersEmailListAsync(string value);

        Task<IList<SignatureTemplatesDTO>> GetSignatureTemplateListAsyn();

        Task<OrganizationDTO> GetOrganizationDetailsAsync(string organizationName);
        Task<ServiceResult> GetOrganizationDetailsByUIdAsync(string organizationUid);

        Task<ServiceResult> AddOrganizationAsync(OrganizationDTO onboardingOrganization, bool makerCheckerFlag = false);

        Task<ServiceResult> UpdateOrganizationAsync(OrganizationDTO updateOrganization, bool makerCheckerFlag = false);

        Task<ServiceResult> DelinkOrganizationEmployeeAsync(DelinkOrganizationEmployeeDTO delinkOrganizationEmployee);

        Task<ServiceResult> ValidateEmailListAsync(List<string> value);

        Task<ServiceResult> IssueCertificateAsync(string organizationUid, string uuid,  string transactionReferenceId, bool makerCheckerFlag = false);

        Task<ServiceResult> IssueWalletCertificateAsync(string organizationUid, string uuid, string transactionReferenceId, bool makerCheckerFlag = false);

        Task<ServiceResult> RevokeCertificateAsync(string organizationUid, int reasonId, string remarks, string uuid, bool makerCheckerFlag = false);

        Task<bool> IsOrganizationExists(string orgName);

        Task<ServiceResult> VerifyDocumentSignatureAsync(string organizationUid, string uuid, string docType, string signedDoc, IList<string> signatories);

        Task<ServiceResult> GetEsealCertificateStatus(string organizationUid);

        Task<ServiceResult> GetStakeholdersAsync(string organizationUid);

        Task<ServiceResult> AddStakeHolder(IList<StakeholderDTO> stakeholderDTO);

        Task<OrganizationDTO> GetOrganizationDetailsByUId(string organizationUid);

        Task<ServiceResult> GetVendorsAsync(string organizationUid);

        Task<ServiceResult> AddVendor(AddVendorDTO stakeholderDTO);

        Task<ServiceResult> VerifyVendor(string orgId, string vendorId);

        Task<Dictionary<string, string>> GetOrganizationsDictionary();

        Task<List<OrganizationListResponse>> GetOrganizationsListDictionary();
    }
}
