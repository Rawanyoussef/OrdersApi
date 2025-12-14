namespace Orders.Services
{
    public interface ICacheService
    {
        Task SetAsync(string key, object value, TimeSpan ttl);
        Task<T?> GetAsync<T>(string key);
        Task RemoveAsync(string key);
    }
}