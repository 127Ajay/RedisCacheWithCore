using RedisCacheWithCore.Service;
using Microsoft.AspNetCore.Mvc;

namespace RedisCacheWithCore.Controllers;

[ApiController]
[Route("/api")]
public class JsonPlaceHolderController : Controller
{
    private readonly IJsonPlaceholderService _jsonPlaceholderService;
    public JsonPlaceHolderController(IJsonPlaceholderService jsonPlaceholderService)
    {
        _jsonPlaceholderService = jsonPlaceholderService;
    }

    [HttpGet("posts")]
    public async Task<IActionResult> GetPosts()
    {
        var posts = await _jsonPlaceholderService.GetPostsAsync();
        return Ok(posts);
    }

    [HttpGet("comments")]
    public async Task<IActionResult> GetComments()
    {
        var comments = await _jsonPlaceholderService.GetCommentsAsync();
        return Ok(comments);
    }
    [HttpGet("albums")]
    public async Task<IActionResult> GetAlbums()
    {
        var albums = await _jsonPlaceholderService.GetAlbumsAsync();
        return Ok(albums);
    }

    [HttpGet("photos")]
    public async Task<IActionResult> GetPhotos()
    {
        var photos = await _jsonPlaceholderService.GetPhotosAsync();
        return Ok(photos);
    }

    [HttpGet("todos")]
    public async Task<IActionResult> GetTodos()
    {
        var todos = await _jsonPlaceholderService.GetTodosAsync();
        return Ok(todos);
    }

    [HttpGet("users")]
    public async Task<IActionResult> GetUsers()
    {
        var users = await _jsonPlaceholderService.GetUsersAsync();
        return Ok(users);
    }

    [HttpGet("all-data")]
    public async Task<IActionResult> GetAllData()
    {
        var posts = await _jsonPlaceholderService.GetPostsAsync();
        var comments = await _jsonPlaceholderService.GetCommentsAsync();
        var albums = await _jsonPlaceholderService.GetAlbumsAsync();
        var photos = await _jsonPlaceholderService.GetPhotosAsync();
        var todos = await _jsonPlaceholderService.GetTodosAsync();
        var users = await _jsonPlaceholderService.GetUsersAsync();
        return Ok(new
        {
            Posts = posts,
            Comments = comments,
            Albums = albums,
            Photos = photos,
            Todos = todos,
            Users = users
        });
    }

    [HttpDelete("clear-cache")]
    public async Task ClearDataAsync()
    {
        await _jsonPlaceholderService.ClearCacheAsync();
    }

    [HttpGet("refresh-cache")]
    public async Task RefreshCacheAsync()
    {
        await _jsonPlaceholderService.RefreshCacheAsync();
    }

    [HttpPost("list/{key}")]
    public async Task<IActionResult> AddToList(string key, [FromBody] string value)
    {
        await _jsonPlaceholderService.AddToListAsync(key, value);
        return Ok($"Added {value} to list {key}");
    }

    [HttpGet("list/{key}")]
    public async Task<IActionResult> GetList(string key)
    {
        var list = await _jsonPlaceholderService.GetListAsync(key);
        return Ok(list);
    }

    [HttpPost("hashset/{key}")]
    public async Task<IActionResult> AddToSet(string key, [FromBody] string value)
    {
        await _jsonPlaceholderService.AddToSetAsync(key, value);
        return Ok($"Added {value} to set {key}");
    }

    [HttpGet("hashset/{key}")]
    public async Task<IActionResult> GetSet(string key)
    {
        var set = await _jsonPlaceholderService.GetSetAsync(key);
        return Ok(set);
    }

    [HttpPost("publish/{channel}")]
    public async Task<IActionResult> PublishMessage(string channel, [FromBody] string message)
    {
        await _jsonPlaceholderService.PublishMessageAsync(channel, message);
        return Ok($"Published message to channel {channel}: {message}");
    }

    [HttpGet("subscribe/{channel}")]
    public async Task<IActionResult> SubscribeToChannel(string channel)
    {
        await _jsonPlaceholderService.SubscribeToChannelAsync(channel);
        return Ok($"Subscribed to channel {channel}");
    }
}
