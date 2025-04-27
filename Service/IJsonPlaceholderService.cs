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
}
