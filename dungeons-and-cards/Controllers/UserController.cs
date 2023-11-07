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
    
    [HttpGet]
    public async Task<IActionResult> GetAllUser()
    {
        List<User> users = await _userService.GetAllUser();

        return Ok(users);
    }

    [HttpPost]
    public async Task<IActionResult> AddUser([FromBody] JsonElement body)
    {
        var jsonObj = body.Deserialize<RegistrationUserModel>();

        if (jsonObj == null)
        {
            return BadRequest("Body is empty");
        }

        try
        {
            var user = await _userService.AddNewUser(jsonObj);
        
            return Ok($"User is registered with {user.UserId} ID.");
        }
        catch (Exception e)
        {
            return BadRequest(new {message = e.Message});
        }
    }
    
    [HttpDelete]
    public async Task<IActionResult> DeleteUser([FromBody] JsonElement body)
    {
        string objEmailKey = "EmailAddress";
        string emailAddress = "";
        
        foreach (var element in body.EnumerateObject())
        {
            if (element.NameEquals(objEmailKey))
            {
                emailAddress = element.Value.GetString();
            }
        }

        await _userService.DeleteUser(emailAddress);
        
        return Ok();
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

            return Ok($"Hello {user.Username}");
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
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