using System.ComponentModel.DataAnnotations;
using System.Text.Json;
using dungeons_and_cards.Models.UserModels;
using dungeons_and_cards.Services;
using Microsoft.AspNetCore.Mvc;

namespace dungeons_and_cards.Controllers;

[ApiController]
[Route("api/users")]
[Produces("application/json")]
public class UserController : ControllerBase
{
    private readonly IUserService _userService;
    
    public UserController(IUserService userService)
    {
        _userService = userService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAllUser()
    {
        List<User> users = await _userService.GetAllUser();

        return Ok(users);
    }

    [HttpPost]
    public async Task<IActionResult> AddUser([FromBody] JsonElement body)
    {
        var jsonObj = body.Deserialize<User>();

        if (jsonObj == null)
        {
            return BadRequest("Body is empty");
        }

        var message = await _userService.AddNewUser(jsonObj);
        
        return Ok(message);
    }

    [HttpGet("ban-user")]
    public async Task<IActionResult> ListAllBannedUser()
    {
        List<BannedUser> users = await _userService.GetAllBannedUser();

        return Ok(users);
    }

    [HttpPost("ban-user")]
    public async Task<IActionResult> BanUser([FromBody] JsonElement body)
    {
        var jsonObj = body.Deserialize<BannedUser>();

        if (jsonObj == null)
        {
            return BadRequest("Body is empty");
        }

        var userId = await _userService.BannedUser(jsonObj);

        return Ok($"User with {userId} is banned");
    }


}