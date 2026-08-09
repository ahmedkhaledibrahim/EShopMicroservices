using MongoDB.Driver;
using Users.Application.Interfaces.IRepositories;
using Users.Domain.Entities;
using Users.Persistence.Context;

namespace Users.Services.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly ApplicationDbContext _context;

        public UserRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Guid> AddUser(UserEntity user)
        {
            await _context.Users.InsertOneAsync(user);
            return user.ID;
        }

        public async Task<UserEntity> GetUser(string phoneNumber)
        {
            var user = await _context.Users
                .Find(x => x.PhoneNumber.Value == phoneNumber)
                .FirstOrDefaultAsync();
            return user;
        }

        public async Task<UserEntity> UpdateUser(UserEntity user)
        {
            await _context.Users.ReplaceOneAsync(x => x.ID == user.ID, user);
            return user;
        }
    }
}
