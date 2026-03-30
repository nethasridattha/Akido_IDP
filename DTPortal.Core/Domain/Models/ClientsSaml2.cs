using System;
using System.Collections.Generic;

namespace DTPortal.Core.Domain.Models;

public partial class ClientsSaml2
{
    public int Id { get; set; }

    public int? Clientid { get; set; }

    public string EntityId { get; set; }

    public string AssertionConsumerServiceBinding { get; set; }

    public string SingleLogoutServiceBinding { get; set; }

    public string Config { get; set; }

    public string Hash { get; set; }

    public string CreatedBy { get; set; }

    public string UpdatedBy { get; set; }

    public DateTime? CreatedDate { get; set; }

    public DateTime? ModifiedDate { get; set; }

    public virtual Client Client { get; set; }
}
