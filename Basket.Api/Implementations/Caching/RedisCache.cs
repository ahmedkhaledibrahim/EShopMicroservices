using Basket.Api.Interfaces.ICaching;
using StackExchange.Redis;
using System.Text.Json;

namespace Basket.Api.Implementations.Caching
{
    public class RedisCache : ICacheService
    {
        private readonly IDatabase _database;

        public RedisCache(IConnectionMultiplexer redis)
        {
            _database = redis.GetDatabase();
        }

        public async Task DeleteAsync<T>(string key)
        {
            await _database.KeyDeleteAsync(key);
        }

        public async Task<T?> GetAsync<T>(string key)
        {
            var value = await _database.StringGetAsync(key);
            if (value.IsNullOrEmpty)
            {
                return default;
            }
            return JsonSerializer.Deserialize<T>(value.ToString());
        }

        public async Task<bool> SetAsync<T>(string key, T value, TimeSpan? expiration)
        {
            await _database.StringSetAsync(key, JsonSerializer.Serialize(value), new Expiration(DateTime.UtcNow.Add(expiration ?? TimeSpan.FromMinutes(5))));
            return true;

        }
    }
}
