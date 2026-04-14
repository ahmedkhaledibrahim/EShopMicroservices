namespace Basket.Api.Interfaces.ICaching
{
    public interface ICacheService
    {
        public Task<T?> GetAsync<T>(string key);
        public Task<bool> SetAsync<T>(string key, T value, TimeSpan? expiration = null);
        public Task DeleteAsync<T>(string key);
    }
}
