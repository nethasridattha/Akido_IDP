using System;
using System.Collections.Generic;

namespace DTPortal.Core.Domain.Models;

public partial class Certificate
{
    public int Id { get; set; }

    public string Kid { get; set; }

    public string SerialNumber { get; set; }

    public DateTime? ExpiryDate { get; set; }

    public DateTime? IssueDate { get; set; }

    public string Status { get; set; }

    public string Data { get; set; }

    public DateTime? CreatedDate { get; set; }

    public DateTime? ModifiedDate { get; set; }
}
