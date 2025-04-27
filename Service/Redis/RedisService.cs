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

    // String operations
    public async Task<string?> GetStringAsync(string key)
    {
        return await _database.StringGetAsync(key);
    }

    public async Task SetStringAsync(string key, string value, TimeSpan? expiration = null)
    {
        await _database.StringSetAsync(key, value, expiration);
    }

    public async Task DeleteStringAsync(string key)
    {
        await _database.KeyDeleteAsync(key);
    }

    // Hash operations
    public async Task<HashEntry[]> GetHashAsync(string key)
    {
        return await _database.HashGetAllAsync(key);
    }

    public async Task SetHashAsync(string key, HashEntry[] hashEntries)
    {
        await _database.HashSetAsync(key, hashEntries);
    }

    public async Task DeleteHashFieldAsync(string key, string field)
    {
        await _database.HashDeleteAsync(key, field);
    }

    // List operations
    public async Task<long> PushToListAsync(string key, string value)
    {
        return await _database.ListRightPushAsync(key, value);
    }

    public async Task<string?> PopFromListAsync(string key)
    {
        return await _database.ListLeftPopAsync(key);
    }

    public async Task<string[]> GetListAsync(string key, long start = 0, long stop = -1)
    {
        return (await _database.ListRangeAsync(key, start, stop)).ToStringArray();
    }

    // Set operations
    public async Task<bool> AddToSetAsync(string key, string value)
    {
        return await _database.SetAddAsync(key, value);
    }

    public async Task<bool> RemoveFromSetAsync(string key, string value)
    {
        return await _database.SetRemoveAsync(key, value);
    }

    public async Task<string[]> GetSetMembersAsync(string key)
    {
        return (await _database.SetMembersAsync(key)).ToStringArray();
    }

    // Pub/Sub operations
    public async Task PublishAsync(string channel, string message)
    {
        var subscriber = _database.Multiplexer.GetSubscriber();
        await subscriber.PublishAsync(channel, message);
    }

    public async Task SubscribeAsync(string channel, Action<string> messageHandler)
    {
        var subscriber = _database.Multiplexer.GetSubscriber();
        await subscriber.SubscribeAsync(channel, (ch, msg) => messageHandler(msg));
    }
}
