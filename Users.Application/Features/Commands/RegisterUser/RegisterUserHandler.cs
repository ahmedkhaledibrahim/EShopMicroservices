
using MediatR;
using Users.Application.Interfaces.IHashing;
using Users.Application.Interfaces.IRepositories;
using Users.Domain.Entities;

namespace Users.Application.Features.Commands.RegisterUser
{
    public class RegisterUserHandler : IRequestHandler<RegisterUserRequest, Unit>
    {
        private readonly IUserRepository _userRepository;
        private readonly IPasswordHasher _passwordHasher;

        public RegisterUserHandler(IUserRepository userRepository, IPasswordHasher passwordHasher)
        {
            _userRepository = userRepository;
            _passwordHasher = passwordHasher;
        }

        public async Task<Unit> Handle(RegisterUserRequest request, CancellationToken cancellationToken)
        {
            var hashedPassword = _passwordHasher.Hash(request.Password);
            var newUser = UserEntity.Create(request.PhoneNumber, hashedPassword);
            await _userRepository.AddUser(newUser);
            return Unit.Value;
        }
    }
}
