using dungeons_and_cards.Models.UserModels;

namespace dungeons_and_cards.Services;

public interface IUserService
{
    Task<List<User>> GetAllUser();
    Task<Guid> AddNewUser(User newUser);
    
}