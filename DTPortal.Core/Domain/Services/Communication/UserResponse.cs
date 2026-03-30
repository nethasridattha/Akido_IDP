using DTPortal.Core.Domain.Models;


namespace DTPortal.Core.Domain.Services.Communication
{
    public class UserResponse : BaseResponse<UserTable>
    {
        public UserResponse(UserTable user) : base(user) { }

        public UserResponse(string message) : base(message) { }
        public UserResponse(UserTable category, string message) :
    base(category, message)
        { }
    }

    public class UserRequest
    {
        public UserTable user { get; set; }
        public string password { get; set; }
    }
}
