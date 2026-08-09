using Microsoft.Extensions.Options;
using MongoDB.Driver;
using Users.Domain.Entities;
using Users.Persistence.Settings;

namespace Users.Persistence.Context
{
    public class ApplicationDbContext
    {
        private readonly IMongoDatabase _database;
        public ApplicationDbContext(IOptions<MongoDbSettings> settings)
        {
            var client = new MongoClient(settings.Value.ConnectionString);
            _database = client.GetDatabase(settings.Value.DatabaseName);
        }
        public IMongoCollection<UserEntity> Users => _database.GetCollection<UserEntity>("Users");
    }
}
