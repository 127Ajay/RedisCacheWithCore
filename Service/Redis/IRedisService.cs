using StackExchange.Redis;

namespace RedisCacheWithCore.Service.Redis;

public interface IRedisService
{
    Task<T?> GetDataAsync<T>(string key);
    Task SetDataAsync<T>(string key, T value, DateTime expiration);
    Task RemoveDataAsync(string key);

    Task RefreshCacheAsync<T>(string key, DateTime expiration);

    // String operations
    Task<string?> GetStringAsync(string key);
    Task SetStringAsync(string key, string value, TimeSpan? expiration = null);
    Task DeleteStringAsync(string key);

    // Hash operations
    Task<HashEntry[]> GetHashAsync(string key);
    Task SetHashAsync(string key, HashEntry[] hashEntries);
    Task DeleteHashFieldAsync(string key, string field);

    // List operations
    Task<long> PushToListAsync(string key, string value);
    Task<string?> PopFromListAsync(string key);
    Task<string[]> GetListAsync(string key, long start = 0, long stop = -1);

    // Set operations
    Task<bool> AddToSetAsync(string key, string value);
    Task<bool> RemoveFromSetAsync(string key, string value);
    Task<string[]> GetSetMembersAsync(string key);

    // Pub/Sub operations
    Task PublishAsync(string channel, string message);
    Task SubscribeAsync(string channel, Action<string> messageHandler);
}
