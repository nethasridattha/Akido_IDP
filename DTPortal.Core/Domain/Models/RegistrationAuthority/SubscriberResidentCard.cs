using System;
using System.Collections.Generic;

namespace DTPortal.Core.Domain.Models.RegistrationAuthority;

public partial class SubscriberResidentCard
{
    public int Id { get; set; }

    public string ApplicantName { get; set; }

    public string ApplicantPhoto { get; set; }

    public string PassportNumber { get; set; }

    public string PassportDocument { get; set; }

    public string Nationality { get; set; }

    public string VisitorCardNumber { get; set; }

    public string VisitorCard { get; set; }

    public string TradeLicenseDocument { get; set; }

    public string EstablishmentCard { get; set; }

    public string SponsorName { get; set; }

    public string SponsorIdNumber { get; set; }

    public string PaymentReferenceNumber { get; set; }

    public byte[] RegisrationIdCardForm { get; set; }

    public string RegistrationIdCardFormData { get; set; }

    public string SignatureOfApplicant { get; set; }

    public string ApplicationStatus { get; set; }

    public string ResidenceIdNumber { get; set; }

    public string ResidenceIdDocument { get; set; }

    public DateOnly? IssuedOn { get; set; }

    public DateOnly? ValidUpto { get; set; }

    public DateOnly? CreatedOn { get; set; }

    public DateOnly? UpdatedOn { get; set; }

    public string IdDocNumber { get; set; }
}
