using System;
using System.Collections.Generic;

namespace DTPortal.Core.Domain.Models;

public partial class OperationsAuthscheme
{
    public int Id { get; set; }

    public string OperationName { get; set; }

    public string AuthenticationSchemeName { get; set; }

    public string Description { get; set; }

    public bool? AuthenticationRequired { get; set; }

    public string DisplayName { get; set; }

    public string CreatedBy { get; set; }

    public string ModifiedBy { get; set; }

    public DateTime? CreatedDate { get; set; }

    public DateTime? ModifiedDate { get; set; }

    public string Hash { get; set; }
}
