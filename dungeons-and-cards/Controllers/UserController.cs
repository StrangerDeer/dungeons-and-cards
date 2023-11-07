using System.IdentityModel.Tokens.Jwt;
using System.Text.Json;
using dungeons_and_cards.Models.UserModels;
using dungeons_and_cards.Services;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace dungeons_and_cards.Controllers;

[ApiController]
[EnableCors("AllowAngularOrigins")]  
[Route("api/user")]
[Produces("application/json")]

public class UserController : ControllerBase
{
    private readonly IUserService _userService;
    
    public UserController(IUserService userService)
    {
        _userService = userService;
    }

    [HttpPost]
    public async Task<IActionResult> RegisterUser([FromBody] JsonElement body)
    {
        var jsonObj = body.Deserialize<RegistrationUserModel>();

        if (jsonObj == null)
        {
            return BadRequest("Body is empty");
        }
        
        try
        {
            var user = await _userService.AddNewUser(jsonObj);
            string message = $"User is registered with {user.UserId} ID.";
        
            return Ok(message);
        }
        catch (BadRequestException e)
        {
            return BadRequest(new {message = e.Message});
        }
    }
    
    [HttpDelete]
    public async Task<IActionResult> DeleteUser([FromBody] JsonElement body)
    {
        var jsonObj = body.Deserialize<User>();

        if (jsonObj == null)
        {
            return BadRequest("Body is empty");
        }

        try
        {
            User user = await _userService.DeleteUser(jsonObj);
            string message = $"We sorry to you left the community! {user.Username} is deleted.";
            return Ok(message);
        }
        catch (BadRequestException e)
        {
            return BadRequest(e.Message);
        }
    }

    [HttpPost("login")]
    public async Task<IActionResult> LoginUser([FromBody] JsonElement body)
    {
        var loginModel = body.Deserialize<UserLogin>();

        if (loginModel == null)
        {
            return BadRequest("Body is empty");
        }

        try
        {
            User user = await _userService.Login(loginModel);
            string message = $"Hello {user.Username}";

            return Ok(message);
        }
        catch (BadRequestException e)
        {
            return BadRequest(e.Message);
        }
    }
    
    [HttpGet("all-user")]
    public async Task<IActionResult> GetAllUser()
    {
        List<User> users = await _userService.GetAllUser();

        return Ok(users);
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

        try
        {
            User user = await _userService.BannedUser(jsonObj);
            string message = $"{user.Username} with {user.UserId} ID is banned";
            
            return Ok(message);
        }
        catch (BadRequestException e)
        {
            return BadRequest(e.Message);
        }
    }
}