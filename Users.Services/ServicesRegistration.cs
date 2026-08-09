using Microsoft.Extensions.DependencyInjection;
using Users.Application.Interfaces.IHashing;
using Users.Application.Interfaces.IRepositories;
using Users.Application.Interfaces.IToken;
using Users.Services.Hashing;
using Users.Services.Repositories;
using Users.Services.Token;

namespace Users.Services
{
    public static class ServicesRegistration
    {
        public static IServiceCollection AddUsersServices(this IServiceCollection services) {
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IPasswordHasher, Argon2Hasher>();
            services.AddScoped<ITokenGenerator, TokenGenerator>();
            return services;
        }
    }
}
