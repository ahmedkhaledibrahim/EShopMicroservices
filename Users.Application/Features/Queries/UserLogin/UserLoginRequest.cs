
using MediatR;

namespace Users.Application.Features.Queries.UserLogin
{
    public class UserLoginRequest : IRequest<UserLoginResponse>
    {
        public string PhoneNumber { get; set; }
        public string Password { get; set; }
    }
}
