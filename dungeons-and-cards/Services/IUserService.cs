using dungeons_and_cards.Models.UserModels;

namespace dungeons_and_cards.Services;

public interface IUserService
{
    Task<List<User>> GetAllUser();
    Task<string> AddNewUser(User newUser);
    Task<Guid> BannedUser(BannedUser bannedUser);
    Task<List<BannedUser>> GetAllBannedUser();
}