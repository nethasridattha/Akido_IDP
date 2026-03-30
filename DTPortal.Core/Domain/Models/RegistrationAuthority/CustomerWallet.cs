using System;
using System.Collections.Generic;

namespace DTPortal.Core.Domain.Models.RegistrationAuthority;

public partial class CustomerWallet
{
    public long Id { get; set; }

    public string ClientIdentifier { get; set; }

    public string CustomerCode { get; set; }

    public string CustomerType { get; set; }

    public string ArabicName { get; set; }

    public string EnglishName { get; set; }

    public string IdentificationNumber { get; set; }

    public string StakeholderType { get; set; }

    public string Address { get; set; }

    public string Email { get; set; }

    public string Mobile { get; set; }

    public string Password { get; set; }

    public long? WalletNumber { get; set; }

    public string PaynovaIdentifier { get; set; }

    public DateTime CreatedOn { get; set; }

    public DateTime? UpdatedOn { get; set; }
}
