using Fido2NetLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTPortal.Core.Domain.Services.Communication
{
    public class UserBasicProfile
    {
        public string SubscriberUid { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public string IdDocType { get; set; }
        public string IdDocNumber { get; set; }
        public string DisplayName { get; set; }
        public string CertificateStatus { get; set; }
        public string MobileNumber { get; set; }
        public string SubscriberStatus { get; set; }
        public string Email { get; set; }
        public string FcmToken { get; set; }
        public string Loa { get; set; }
        public string Gender { get; set; }
        public string Country { get; set; }
        public string Photo { get; set; }
        public string pid_document { get; set; }
        public string cardStatus { get; set; }
        public string issuedOn { get; set; }
        public string idDocument { get; set; }
        public string idNumber { get; set; }
        public string NonResidentIdDocument { get; set; }
        public string ResidentIdDocument { get; set; }
        public string VisaNumber { get; set; }
        public string VisaPdf { get; set; }
        public string VisaStatus { get; set; }
        public string NonResidentCardNumber { get; set; }
        public string Nationality { get; set; }
        public string FatherName { get; set; }
        public string MotherName { get; set; }
        public string Occupation { get; set; }
        public string PlaceOfBirth { get; set; }
        public string BloodGroup { get; set; }
        public string Address { get; set; }
        public string VisitorCardNumber { get; set; }
        public string VisitorCardPdf { get; set; }
        public string VisitorStatus { get; set; }
        public string IssueDate { get; set; }
        public string ExpiryDate { get; set; }
        public string PassPortImage { get; set; }
        public string SignedVisaDocument { get; set; }
        public string FamilyBook { get; set; }
        public string SubscriberCard { get; set; }
        public string KycDocument { get; set; }
        public string EKycDocument { get; set;}

    }
    public class UserHealthProfile
    {
        public string blood_group { get; set; }
        public int height { get; set; }
        public int weight { get; set; }
        public double bmi { get; set; }
    }

    public class UserMDLProfile
    {
        public string issue_date { get; set; }
        public string expiry_date { get; set; }
        public string issuing_country { get; set; }
        public string issuing_authority { get; set; }
        public string document_number { get; set; }
        public string mdl_document { get; set; }
        public string international_permit { get; set; }
    }
    public class UserSocialProfile
    {
        public string address { get; set; }
        public string age { get; set; }
        public string birthdate { get; set; }
        public string email { get; set; }
        public string income { get; set; }
        public string x_cst_indv_Doc_type { get; set; }
        public string x_cst_indv_Doc_value { get; set; }
        public string x_cst_indv_children { get; set; }
        public string x_cst_indv_suid { get; set; }
        public string name { get; set; }
        public string phone_sanitized { get; set; }
        public string x_cst_indv_sex { get; set; }
        public string occupation { get; set; }
        public string x_cst_indv_marital_status { get; set; }
        public List<Program> Programs { get; set; }
    }
    public class Program
    {
        public decimal Amount { get; set; }
        public string Description { get; set; }
        public string Name { get; set; }
    }
    public class VisaDetailsResponse
    {
        public bool Success { get; set; }
        public string Message { get; set; }
        public List<VisaProfile> Result { get; set; }
    }

    public class VisaProfile
    {
        public int VisaAuthorityId { get; set; }
        public string FullName { get; set; }
        public string Nationality { get; set; }
        public string CountryCode { get; set; }
        public string VisaType { get; set; }
        public string DateOfBirth { get; set; }
        public string Gender { get; set; }
        public string MobileNumber { get; set; }
        public string Email { get; set; }
        public string PassportNumber { get; set; }
        public string PassportExpiryDate { get; set; }
        public string PassportIssueDate { get; set; }
        public string PaymentReferenceNumber { get; set; }
        public string VisaIssuedAt { get; set; }
        public string VisaIssuedOn { get; set; }
        public string VisaValidUntil { get; set; }
        public string EnterOnOrBefore { get; set; }
        public string NumberOfEntries { get; set; }
        public string VisaStatus { get; set; }
        public string VisaSubType { get; set; }
        public string VisaFormJson { get; set; }
        public string PassportDocumentImage { get; set; }
        public string FileData { get; set; }
        public string CreatedOn { get; set; }
        public string UpdatedOn { get; set; }
        public string SelfieImage { get; set; }
        public string Uid { get; set; }
        public string SignedVisaDocument { get; set; }

    }
    public class BusinessEstablishmentProfile
    {
        public int Id { get; set; }
        public string ApplicantName { get; set; }
        public string SubscriberUId { get; set; }
        public string ApplicantPhoto { get; set; }
        public string ApplicantPassport { get; set; }
        public string ApplicantVisa { get; set; }
        public string TradelicenseNumber { get; set; }
        public string TradelicenseDocument { get; set; }
        public string PaymentReferenceNumber { get; set; }
        public string CompanyName { get; set; }
        public string FormJsonData { get; set; }
        public string PassportNumber { get; set; }
        public string VisaNumber { get; set; }
        public string EstablishmentCard { get; set; }
        public string EstablishmentCardNumber { get; set; }
        public string IssueDate { get; set; }
        public string ApplicationStatus { get; set; }
        public string CreateOn { get; set; }
        public string UpdateOn { get; set; }
    }
    public class TradeLicenceProfile
    {
        public int Id { get; set; }
        public string ApplicantPhoto { get; set; }
        public string SubscriberUId { get; set; }
        public string ApplicantPassport { get; set; }
        public string ApplicantVisa { get; set; }
        public string TradeLicenseApplication { get; set; }
        public string TermsConditions { get; set; }
        public string KycForm { get; set; }
        public string TradeLicenseFormJsonData { get; set; }
        public string TermsConditionsJsonData { get; set; }
        public string KycJsonData { get; set; }
        public string ApplicantName { get; set; }
        public string PassportNumber { get; set; }
        public string VisaNumber { get; set; }
        public string PaymentReferenceNumber { get; set; }
        public string CompanyName { get; set; }
        public string TradelicenseAppliedOn { get; set; }
        public string ApplicationStatus { get; set; }
        public string TradelicenseDocument { get; set; }
        public string TradelicenseNumber { get; set; }
        public string IssueDate { get; set; }
        public string ExpiryDate { get; set; }
        public string CreateOn { get; set; }
        public string UpdateOn { get; set; }
    }
}
