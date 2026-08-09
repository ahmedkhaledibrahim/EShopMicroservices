using MediatR;
using Users.Application.Exceptions;
using Users.Application.Interfaces.IHashing;
using Users.Application.Interfaces.IRepositories;
using Users.Application.Interfaces.IToken;

namespace Users.Application.Features.Queries.UserLogin
{
    public class UserLoginHandler : IRequestHandler<UserLoginRequest, UserLoginResponse>
    {
        private readonly IUserRepository _userRepository;
        private readonly IPasswordHasher _passwordHasher;
        private readonly ITokenGenerator _tokenGenerator;
        public UserLoginHandler(IUserRepository userRepository, IPasswordHasher passwordHasher, ITokenGenerator tokenGenerator)
        {
            _userRepository = userRepository;
            _passwordHasher = passwordHasher;
            _tokenGenerator = tokenGenerator;
        }

        public async Task<UserLoginResponse> Handle(UserLoginRequest request, CancellationToken cancellationToken)
        {
            var user = await _userRepository.GetUser(request.PhoneNumber);
            if (user == null) throw new EntityNotFoundException("user not found");
            if (_passwordHasher.Verify(request.Password, user.HashedPassword))
            {
                return new UserLoginResponse
                {
                    AccessToken = _tokenGenerator.GenerateAccessToken(user)
                };
            }
            else {
                throw new UnauthorizedAccessException("Wrong phone number or password");
            }
        }
    }
}
