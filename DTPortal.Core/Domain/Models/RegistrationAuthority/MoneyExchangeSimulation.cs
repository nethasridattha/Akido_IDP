using System;
using System.Collections.Generic;

namespace DTPortal.Core.Domain.Models.RegistrationAuthority;

public partial class MoneyExchangeSimulation
{
    public string ApplicantName { get; set; }

    public string PassportNumber { get; set; }

    public string AmountToBeExchanged { get; set; }

    public string ExchangedAmount { get; set; }

    public string Status { get; set; }

    public DateTime? CreatedDate { get; set; }

    public DateTime? UpdatedDate { get; set; }

    public string Country { get; set; }

    public string PassportDocument { get; set; }

    public string PidDocument { get; set; }

    public string JsonData { get; set; }

    public int Id { get; set; }

    public string Photo { get; set; }
}
