using RedisCacheWithCore.Models;
using RedisCacheWithCore.Service.Redis;
using System;
using static RedisCacheWithCore.Models.JsonPlaceHolderDTOs;

namespace RedisCacheWithCore.Service;

public class JsonPlaceholderService : IJsonPlaceholderService
{
    private readonly HttpClient _httpClient;
    private readonly IRedisService _redisService;
    private ILogger<JsonPlaceholderService> _logger;
    private const string BaseUrl = "https://jsonplaceholder.typicode.com/";
    private const int DefaultCacheDurationMinutues = 60; // in Minutues

    public JsonPlaceholderService(HttpClient httpClient, IRedisService redisService, ILogger<JsonPlaceholderService> logger)
    {
        _httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));
        _redisService = redisService ?? throw new ArgumentNullException(nameof(redisService));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        _httpClient.BaseAddress = new Uri(BaseUrl);
    }

    private async Task<IEnumerable<T>> GetCachedDataAsync<T>(string endpoint, string cacheKey, string entityName)
    {

        try { 
            var cachedData = await _redisService.GetDataAsync<IEnumerable<T>>(cacheKey);
            if (cachedData != null)
            {
                _logger.LogInformation($"{entityName} is retreived from cache");
                return cachedData;
            }

            var data = await _httpClient.GetFromJsonAsync<IEnumerable<T>>(endpoint);
            if (data != null)
            {
                DateTime expiration = DateTime.UtcNow.AddMinutes(DefaultCacheDurationMinutues);
                await _redisService.SetDataAsync(cacheKey, data, expiration);
                _logger.LogInformation($"{entityName} is cached on {expiration}");
                return data;
            }

            _logger.LogWarning($"No data found for {entityName} at {endpoint}");
            return Enumerable.Empty<T>();

        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"Error occurred while getting data from {endpoint} for {entityName}: {ex.Message}");
            throw;
        }
    }

    public async Task<IEnumerable<AlbumDTO>> GetAlbumsAsync()
    {
        return await GetCachedDataAsync<AlbumDTO>("/posts", "post_all", "posts");
    }

    public async Task<IEnumerable<CommentDTO>> GetCommentsAsync()
    {
        return await GetCachedDataAsync<CommentDTO>("/comments", "comments_all", "comments");
    }

    public async Task<IEnumerable<PhotoDTO>> GetPhotosAsync()
    {
        return await GetCachedDataAsync<PhotoDTO>("/photos", "photos_all", "photos");
    }

    public async Task<IEnumerable<PostDTO>> GetPostsAsync()
    {
        return await GetCachedDataAsync<PostDTO>("/posts", "posts_all", "posts");
    }

    public async Task<IEnumerable<TodoDTO>> GetTodosAsync()
    {
        return await GetCachedDataAsync<TodoDTO>("/todos", "todos_all", "todos");
    }

    public async Task<IEnumerable<UserDTO>> GetUsersAsync()
    {
        return await GetCachedDataAsync<UserDTO>("/users", "users_all", "users");
    }

    public async Task ClearCacheAsync()
    {
        try
        {
            await _redisService.RemoveDataAsync("posts_all");
            await _redisService.RemoveDataAsync("comments_all");
            await _redisService.RemoveDataAsync("albums_all");
            await _redisService.RemoveDataAsync("photos_all");
            await _redisService.RemoveDataAsync("todos_all");
            await _redisService.RemoveDataAsync("users_all");
            _logger.LogInformation("Cache cleared successfully.");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error occurred while clearing cache: {Message}", ex.Message);
        }
    }

    public async Task RefreshCacheAsync()
    {
        try
        {
            await _redisService.RefreshCacheAsync<IEnumerable<PostDTO>>("posts_all", DateTime.UtcNow.AddMinutes(DefaultCacheDurationMinutues));
            await _redisService.RefreshCacheAsync<IEnumerable<CommentDTO>>("comments_all", DateTime.UtcNow.AddMinutes(DefaultCacheDurationMinutues));
            await _redisService.RefreshCacheAsync<IEnumerable<AlbumDTO>>("albums_all", DateTime.UtcNow.AddMinutes(DefaultCacheDurationMinutues));
            await _redisService.RefreshCacheAsync<IEnumerable<PhotoDTO>>("photos_all", DateTime.UtcNow.AddMinutes(DefaultCacheDurationMinutues));
            await _redisService.RefreshCacheAsync<IEnumerable<TodoDTO>>("todos_all", DateTime.UtcNow.AddMinutes(DefaultCacheDurationMinutues));
            await _redisService.RefreshCacheAsync<IEnumerable<UserDTO>>("users_all", DateTime.UtcNow.AddMinutes(DefaultCacheDurationMinutues));
            _logger.LogInformation("Cache refreshed successfully.");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error occurred while refreshing cache: {Message}", ex.Message);
        }
    }
}
