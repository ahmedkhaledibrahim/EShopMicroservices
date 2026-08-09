using Users.Domain.Entities.Common;
using Users.Domain.ValueObjects;

namespace Users.Domain.Entities
{
    public class UserEntity : BaseEntity
    {
        public PhoneNumber PhoneNumber { get; private set; }
        public string HashedPassword { get; private set; }
        private UserEntity() { }
        public static UserEntity Create(string phoneNumber, string hashedPassword)
        {
            return new UserEntity
            {
                PhoneNumber = PhoneNumber.Of(phoneNumber),
                HashedPassword = hashedPassword
            };
        }
    }
}
