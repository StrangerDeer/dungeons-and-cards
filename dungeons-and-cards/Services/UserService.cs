using dungeons_and_cards.Models.Contexts;
using dungeons_and_cards.Models.UserModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace dungeons_and_cards.Services;

public class UserService : IUserService
{
    private Context _context;
    
    public UserService(Context context)
    {
        _context = context;
    }
    
    public async Task<List<User>> GetAllUser()
    {
        return await _context.Users.ToListAsync();
    }

    public async Task<User> AddNewUser(RegistrationUserModel newUser)
    {
        if (newUser.Username.IsNullOrEmpty() ||
            newUser.Password.IsNullOrEmpty() ||
            newUser.EmailAddress.IsNullOrEmpty())
        {
            throw new BadRequestException("One of the field is empty");
        }

        await CheckUserIsBannedWithEmail(newUser.EmailAddress);
        await CheckUserIsExist(newUser.EmailAddress, newUser.Username);
        
        User user = new User(newUser.Username, HashPassword(newUser.Password), newUser.EmailAddress);
        _context.Users.Add(user);
        await _context.SaveChangesAsync();
            
        return user;
    }
    public async Task<User> DeleteUser(User deletedUser)
    {
        User? user = await _context.Users.FirstOrDefaultAsync(user => user.Username.Equals(deletedUser.Username));

        if (user == null)
        {
            throw new BadRequestException($"User with {deletedUser.Username} username is not found");
        }

        _context.Users.Remove(user);
        await _context.SaveChangesAsync();

        return user;
    }

    public async Task<User> Login(UserLogin userLogin)
    {
        if (userLogin.Username.IsNullOrEmpty() ||
            userLogin.Password.IsNullOrEmpty())
        {
            throw new BadRequestException("One of the field is empty");
        }

        User? user = await _context.Users.FirstOrDefaultAsync(u => u.Username.Equals(userLogin.Username));

        if (user == null)
        {
            throw new BadRequestException("User is not found with this username");
        }

        await CheckUserIsBannedWithUsername(user.Username);

        if (!user.CheckPassword(userLogin.Password))
        {
            throw new BadRequestException("Wrong Password");
        }

        return user;
    }

    public async Task<User> BannedUser(BannedUser bannedUser)
    {
        User? user = await _context.Users.FirstOrDefaultAsync(u => u.Username.Equals(bannedUser.Username));

        if (user == null)
        {
            throw new BadRequestException($"User with {bannedUser.Username} is not found");
        }
        
        _context.Users.Remove(user);
        _context.BannedUsers.Add(bannedUser);
        await _context.SaveChangesAsync().ConfigureAwait(true);

        return user;

    }

    public async Task<List<BannedUser>> GetAllBannedUser()
    {
        return await _context.BannedUsers.ToListAsync();
    }

    private static string HashPassword(string password)
    {
        return BCrypt.Net.BCrypt.EnhancedHashPassword(password);
    }
    
    private async Task CheckUserIsBannedWithEmail(string email)
    {
        BannedUser? user = await _context.BannedUsers.FirstOrDefaultAsync(user => user.EmailAddress.Equals(email));

        if (user != null)
        {
            throw new BadRequestException($"User is banned for {user.BannedEnd}");
        }
    }

    private async Task CheckUserIsBannedWithUsername(string username)
    {
        BannedUser? user = await _context.BannedUsers.FirstOrDefaultAsync(u => u.Username.Equals(username));

        if (user != null)
        {
            throw new BadRequestException($"User is banned  for {user.BannedEnd}");
        }
    }

    private async Task CheckUserIsExist(string email, string username)
    {
        var user = await _context.Users.FirstOrDefaultAsync(u => u.Username.Equals(username));

        if (user != null)
        {
            throw new BadRequestException("Username is already exist!");
        }
        
        user =  await _context.Users.FirstOrDefaultAsync(u => u.EmailAddress.Equals(email));

        if (user != null)
        {
            throw new BadRequestException($"This e-mail address is already taken");
        }
    }
}