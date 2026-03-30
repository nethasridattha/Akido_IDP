using MimeKit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTPortal.Core.Domain.Services.Communication
{
    public class SMTPRequest
    {
    }

    public class Message
    {
        public List<MailboxAddress> To { get; set; }
        public string Subject { get; set; }
        public string Content { get; set; }

        public Message(IEnumerable<string> to, string subject, string content)
        {
            To = new List<MailboxAddress>();
            //To.AddRange(to.Select(x => new MailboxAddress(x)));
            To.AddRange(to.Select(x => MailboxAddress.Parse(x)));
            Subject = subject;
            Content = content;
        }
    }
    public class EmailConfiguration
    {
        public string From { get; set; }
        public string SmtpServer { get; set; }
        public int Port { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
    }

    public class SmtpSettings
    {
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
        public int Id { get; set; }
    }

    public class EmailDto
    {
        public List<string> To { get; set; }
        public string Subject { get; set; }
        public string Content { get; set; }
    }

}
