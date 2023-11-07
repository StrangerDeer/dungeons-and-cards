using dungeons_and_cards.Models.UserModels;

namespace dungeons_and_cards.Services;

public interface IUserService
{
    Task<List<User>> GetAllUser();
    Task<User> AddNewUser(RegistrationUserModel newUser);
    Task<string> DeleteUser(string email);
    Task<User> Login(UserLogin userLogin);
    Task<Guid> BannedUser(BannedUser bannedUser);
    Task<List<BannedUser>> GetAllBannedUser();
}