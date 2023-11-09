using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Text.Json;
using dungeons_and_cards.Models.UserModels;
using dungeons_and_cards.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace dungeons_and_cards.Controllers;

[ApiController]
[EnableCors("AllowAngularOrigins")]  
[Authorize]
[Route("api/user")]
[Produces("application/json")]

public class UserController : ControllerBase
{
    private readonly IConfiguration _configuration;
    private readonly IUserService _userService;
    
    public UserController(IConfiguration configuration, IUserService userService)
    {
        _configuration = configuration;
        _userService = userService;
    }

    [AllowAnonymous]
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

    [AllowAnonymous]
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
            string token = GenerateToken(user);
            
            return Ok(token);
        }
        catch (BadRequestException e)
        {
            return BadRequest(e.Message);
        }
    }
    
    [Authorize]
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

    [NonAction]
    private string GenerateToken(User user)
    {
        var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JwtSetting:Key"]!));
        var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

        var claims = new[]
        {
            new Claim(JwtRegisteredClaimNames.Sub, user.UserId.ToString()),
            new Claim(JwtRegisteredClaimNames.UniqueName, user.Username),
            new Claim(JwtRegisteredClaimNames.Email, user.EmailAddress),
            new Claim(ClaimTypes.Role, user.UserRole.ToString()),
            new Claim("RegistrationDate", user.RegistrationDate.ToString("s"))
        };

        var token = new JwtSecurityToken(_configuration["JwtSetting:Issuer"],
            _configuration["JwtSetting:Audience"],
            claims,
            expires: DateTime.UtcNow.AddMinutes(1),
            signingCredentials: credentials);

        return new JwtSecurityTokenHandler().WriteToken(token);
        
    }
}