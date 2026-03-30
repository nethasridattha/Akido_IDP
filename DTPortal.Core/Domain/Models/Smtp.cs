using System;
using System.Collections.Generic;

namespace DTPortal.Core.Domain.Models;

public partial class Smtp
{
    public int Id { get; set; }

    public string SmtpHost { get; set; }

    public string FromName { get; set; }

    public string FromEmailAddr { get; set; }

    public bool RequireAuth { get; set; }

    public string SmtpUserName { get; set; }

    public string SmtpPwd { get; set; }

    public bool RequiresSsl { get; set; }

    public int SmtpPort { get; set; }

    public string MailSubject { get; set; }

    public string Template { get; set; }

    public string CreatedBy { get; set; }

    public string UpdatedBy { get; set; }

    public DateTime CreatedDate { get; set; }

    public DateTime ModifiedDate { get; set; }

    public string Hash { get; set; }
}
