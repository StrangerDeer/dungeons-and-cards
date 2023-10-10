using dungeons_and_cards.Models.Contexts;
using dungeons_and_cards.Models.UserModels;
using Microsoft.EntityFrameworkCore;

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

    public async Task<Guid> AddNewUser(User newUser)
    {
        try
        {
            _context.Users.Add(newUser);
            await _context.SaveChangesAsync();

            return newUser.UserId;
        }
        catch (Exception e)
        {
            throw e;
        }
       
    }
}