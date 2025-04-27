
using StackExchange.Redis;
using System.Text.Json;

namespace RedisCacheWithCore.Service.Redis;

public class RedisService : IRedisService
{
    private readonly IDatabase _database;

    public RedisService()
    {
        var redis = ConnectionMultiplexer.Connect("localhost:6379");
        _database = redis.GetDatabase();
    }

    public async Task<T?> GetDataAsync<T>(string key)
    {
        var value = await _database.StringGetAsync(key);
        return value.IsNullOrEmpty ? default : JsonSerializer.Deserialize<T>(value);
    }


    public Task SetDataAsync<T>(string key, T value, DateTime expiration)
    {
        var jsonValue = JsonSerializer.Serialize(value);
        var ttl = expiration - DateTime.UtcNow;
        if (ttl <= TimeSpan.Zero)
        {
            ttl = TimeSpan.FromSeconds(1); // or a small default positive value
        }
        return _database.StringSetAsync(key, jsonValue, ttl);
    }

    public Task RemoveDataAsync(string key)
    {
        return _database.KeyDeleteAsync(key);
    }

    public async Task RefreshCacheAsync<T>(string key, DateTime expiration)
    {
        var value = await GetDataAsync<object>(key);
        if (value != null)
        {
            await SetDataAsync(key, value, expiration);
        }
    }
}
