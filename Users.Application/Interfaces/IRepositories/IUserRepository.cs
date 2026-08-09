using Users.Domain.Entities;

namespace Users.Application.Interfaces.IRepositories
{
    public interface IUserRepository
    {
        public Task<UserEntity> GetUser(string phoneNumber);
        public Task<Guid> AddUser(UserEntity user);
        public Task<UserEntity> UpdateUser(UserEntity user);
    }
}
