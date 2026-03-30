using System;
using System.Collections.Generic;

namespace DTPortal.Core.Domain.Models.RegistrationAuthority;

public partial class PoaCredentialRequest
{
    public int Id { get; set; }

    public string PrincipleEmail { get; set; }

    public string PrincipleName { get; set; }

    public string AgentEmail { get; set; }

    public string AgentName { get; set; }

    public string AdditionalFields { get; set; }

    public DateOnly? ValidUpto { get; set; }

    public string Status { get; set; }

    public string PoaRequestForm { get; set; }

    public string PrincipleIdDocNumber { get; set; }

    public string PrincipleSuid { get; set; }

    public string AgentIdDocNumber { get; set; }

    public string AgentSuid { get; set; }

    public string NotaryName { get; set; }

    public string NotaryEmail { get; set; }

    public string NotaryIdDocNumber { get; set; }

    public string NotarySuid { get; set; }

    public string Scope { get; set; }

    public string PoaDocSigned { get; set; }

    public string CreatedOn { get; set; }

    public string UpdatedOn { get; set; }

    public string PrinciplePhoto { get; set; }
}
