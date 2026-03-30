using System;
using System.Collections.Generic;

namespace DTPortal.Core.Domain.Models.RegistrationAuthority;

public partial class ValidationRule
{
    public int Id { get; set; }

    public string FieldName { get; set; }

    public string Operator { get; set; }

    public string Value { get; set; }

    public int? ProgramId { get; set; }

    public string LogicalOperator { get; set; }

    public virtual Program Program { get; set; }
}
