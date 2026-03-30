namespace DTPortal.Core.DTOs
{
    public class SelfServiceOrganizationDTO
    {
        public int OrgOnboardingFormsId { get; set; }
        public string OrgRegIdNumber { get; set; }
        public string OrgName { get; set; }
        public string OrgOfficialContactNumber { get; set; }
        public string OrgCorporateAddress { get; set; }
        public string OrgWebUrl { get; set; }
        public string OrgTanTaxNumber { get; set; }
        public string UrsbCertificate { get; set; }
        public string ApprovalLetter { get; set; }
        public string ObFormStatus { get; set; }
        public string OrgApprovalStatus { get; set; }
        public string OrgObRejectedReason { get; set; }
        public string SignApprByBrmStaff { get; set; }
        public string OtpVerification { get; set; }
        public string OrgUid { get; set; }
        public string OrgCategory { get; set; }
        public string SpocSuid { get; set; }
        public string CreatedOn { get; set; }
        public string UpdatedOn { get; set; }
        public string AdminUgpassEmail { get; set; }
        public string CreatedBy { get; set; }
        public FinancialAuditorDetailsDTO OrgFinancialAuditorDetailsDTO { get; set; }
        public SpocDetailsDTO OrganisationSpocDetailsDTO { get; set; }
        public CeoDetailsDTO OrgCeoDetailsDTO { get; set; }
        public UraReportsDTO UraReportsDTO { get; set; }
    }

    public class FinancialAuditorDetailsDTO
    {
        public int OrgOnboardingFormId { get; set; }
        public string FinancialAuditorName { get; set; }
        //public string FinancialAuditorNin { get; set; }
        public string FinancialAuditorIdDocumentNumber { get; set; }
        public string FinancialAuditorUgPassEmail { get; set; }
        public string FinancialAuditorLicenseNum { get; set; }
        public string CreatedOn { get; set; }
        public string UpdatedOn { get; set; }
        public int OrgFinancialAuditorDetailsId { get; set; }
        public string financialAuditorTinNumber { get; set; }
    }

    public class SpocDetailsDTO
    {
        public int OrgSpocDetailsId { get; set; }
        public int OrgOnboardingFormId { get; set; }
        public string SpocOfficeEmail { get; set; }
        public string SpocName { get; set; }
        public string SpocUgpassEmail { get; set; }
        public string SpocUgpassMobNum { get; set; }
        public string SpocIdDocumentNumber { get; set; }
        //public string SpocPassport { get; set; }
        public string SpocSocialSecurityNum { get; set; }
        public string SpocTaxNum { get; set; }
        public string SpocOtpVerfyStatus { get; set; }
        public bool SpocFaceMatchStatus { get; set; }
        public string OrgUid { get; set; }
        public string SpocSuid { get; set; }
        public string Spoc_face_captured { get; set; }
        public string SpocFaceFromUgpass { get; set; }
        public string CreatedOn { get; set; }
        public string UpdatedOn { get; set; }
    }

    public class CeoDetailsDTO
    {
        public int OrgOnboardingFormId { get; set; }
        public int OrgCeoDetailsiId { get; set; }
        public string OrgUid { get; set; }
        public string CeoPanTaxNum { get; set; }
        public string CeoName { get; set; }
        public string CeoEmail { get; set; }
        public string ceoIdDocumentNumber { get; set; }
    }
    public class UraReportsDTO
    {
        public string auditorUraPdf { get; set; }
        public string spocUraPdf { get; set; }
        public string ceoUraPdf { get; set; }
        public string orgUraPdf { get; set; }
    }
}
