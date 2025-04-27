using static RedisCacheWithCore.Models.JsonPlaceHolderDTOs;

namespace RedisCacheWithCore.Service;

public interface IJsonPlaceholderService
{
    Task<IEnumerable<PostDTO>> GetPostsAsync();
    Task<IEnumerable<CommentDTO>> GetCommentsAsync();
    Task<IEnumerable<AlbumDTO>> GetAlbumsAsync();
    Task<IEnumerable<PhotoDTO>> GetPhotosAsync();
    Task<IEnumerable<TodoDTO>> GetTodosAsync();
    Task<IEnumerable<UserDTO>> GetUsersAsync();

    Task ClearCacheAsync();
    Task RefreshCacheAsync();

    // Redis List operations
    Task AddToListAsync(string key, string value);
    Task<IEnumerable<string>> GetListAsync(string key);

    // Redis Set operations
    Task AddToSetAsync(string key, string value);
    Task<IEnumerable<string>> GetSetAsync(string key);

    // Redis Pub/Sub operations
    Task PublishMessageAsync(string channel, string message);
    Task SubscribeToChannelAsync(string channel);
}
