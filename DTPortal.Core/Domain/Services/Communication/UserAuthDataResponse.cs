using DTPortal.Core.Domain.Models;

namespace DTPortal.Core.Domain.Services.Communication
{
    public class UserAuthDataResponse : BaseResponse<UserAuthDatum>
    {
        public UserAuthDataResponse(UserAuthDatum category) : base(category) { }

        public UserAuthDataResponse(string message) : base(message) { }
    }
}
