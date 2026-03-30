using DTPortal.Core.Domain.Models;


namespace DTPortal.Core.Domain.Services.Communication
{
    public class SMTPResponse : BaseResponse<Smtp>
    {
        public SMTPResponse(Smtp smtp) : base(smtp) { }

        public SMTPResponse(string message) : base(message) { }
    }
}
