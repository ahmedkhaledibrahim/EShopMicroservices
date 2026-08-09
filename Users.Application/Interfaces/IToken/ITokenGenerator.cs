using Users.Domain.Entities;

namespace Users.Application.Interfaces.IToken
{
    public interface ITokenGenerator
    {
        public string GenerateAccessToken(UserEntity user);
    }
}
