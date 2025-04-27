namespace RedisCacheWithCore.Service.Redis;

public interface IRedisService
{
    Task<T?> GetDataAsync<T>(string key);
    Task SetDataAsync<T>(string key, T value, DateTime expiration);
    Task RemoveDataAsync(string key);

    Task RefreshCacheAsync<T>(string key, DateTime expiration);
}
