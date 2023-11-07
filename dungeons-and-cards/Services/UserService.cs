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
            throw new Exception("Please fill all field");
        }
        
        if (await CheckUserIsBanned(newUser.EmailAddress))
        {
            throw new Exception("User is banned!");
        }

        if (await CheckUserIsExist(newUser.EmailAddress, newUser.Username))
        {
            throw new Exception("User is exist");
        }

        User user = new User(newUser.Username, newUser.Password, newUser.EmailAddress);
        _context.Users.Add(user);
        await _context.SaveChangesAsync();
            
        return user;
    }

    public async Task<string> DeleteUser(string email)
    {
        string result;
        User? user = await _context.Users.FirstOrDefaultAsync(user => user.EmailAddress.Equals(email));

        if (user == null)
        {
            result = "User not found";
            return result;
        }

        _context.Users.Remove(user);
        await _context.SaveChangesAsync();

        result = $"{user.Username} is deleted";
        return result;
    }

    public async Task<User> Login(UserLogin userLogin)
    {
        if (userLogin.Username.IsNullOrEmpty() ||
            userLogin.Password.IsNullOrEmpty())
        {
            throw new Exception("One of the field is empty");
        }

        User? user = await _context.Users.FirstOrDefaultAsync(u => u.Username.Equals(userLogin.Username));

        if (user == null)
        {
            throw new Exception("User is not found with this username");
        }

        if (!user.CheckPassword(userLogin.Password))
        {
            throw new Exception("Wrong Password");
        }

        return user;
    }

    public async Task<Guid> BannedUser(BannedUser bannedUser)
    {
        try
        {
            User userForBanned = await _context.Users.FirstOrDefaultAsync(user => user.EmailAddress.Equals(bannedUser.EmailAddress));
            _context.Users.Remove(userForBanned);
            _context.BannedUsers.Add(bannedUser);
            await _context.SaveChangesAsync().ConfigureAwait(true);

            return bannedUser.UserId;

        }
        catch (Exception e)
        {
            throw e;
        }
    }

    public async Task<List<BannedUser>> GetAllBannedUser()
    {
        return await _context.BannedUsers.ToListAsync().ConfigureAwait(true);
    }

    private async Task<bool> CheckUserIsBanned(string email)
    {
        BannedUser? user = await _context.BannedUsers.FirstOrDefaultAsync(user => user.EmailAddress.Equals(email));

        if (user == null)
        {
            return false;
        }

        return true;
    }

    private async Task<bool> CheckUserIsExist(string email, string username)
    {
        var user = await _context.Users.FirstOrDefaultAsync(user => user.Username.Equals(username));

        if (user != null)
        {
            return true;
        }
        
        user =  await _context.Users.FirstOrDefaultAsync(user => user.EmailAddress.Equals(email));

        if (user != null)
        {
            return true;
        }

        return false;
    }
}