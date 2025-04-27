using RedisCacheWithCore.Service;
using Microsoft.AspNetCore.Mvc;

namespace RedisCacheWithCore.Controllers;

[ApiController]
[Route("/api")]
public class JsonPlaceHolderController :Controller
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
}
