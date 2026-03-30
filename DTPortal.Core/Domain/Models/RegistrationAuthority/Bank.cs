using System;
using System.Collections.Generic;

namespace DTPortal.Core.Domain.Models.RegistrationAuthority;

public partial class Bank
{
    public int Id { get; set; }

    public string SubscriberUid { get; set; }

    public string ApplicantName { get; set; }

    public string CompanyName { get; set; }

    public string ApplicantPhoto { get; set; }

    public string PassportNumber { get; set; }

    public string Nationality { get; set; }

    public string ResidentIdNumber { get; set; }

    public string PassportDocument { get; set; }

    public string VisitorDocument { get; set; }

    public string TradeLicenseDocument { get; set; }

    public string EstablishmentCard { get; set; }

    public string ResidenceIdDocument { get; set; }

    public string BankAccountOpeningForm { get; set; }

    public string BankAccountOpeningJsonData { get; set; }

    public string ApplicationStatus { get; set; }

    public string BankAccountNumber { get; set; }

    public string BankAccountHolder { get; set; }

    public string BankAccountStatus { get; set; }

    public string CreatedOn { get; set; }

    public string UpdatedOn { get; set; }

    public bool? ExtendedEkyc { get; set; }

    public string KycPdfBase64 { get; set; }

    public string EkycPdfBase64 { get; set; }

    public string KycJson { get; set; }
}
