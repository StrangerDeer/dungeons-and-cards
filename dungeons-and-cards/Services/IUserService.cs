using dungeons_and_cards.Models.UserModels;

namespace dungeons_and_cards.Services;

public interface IUserService
{
    Task<List<User>> GetAllUser();
    Task<User> AddNewUser(RegistrationUserModel newUser);
    Task<User> DeleteUser(User deletedUser);
    Task<User> Login(UserLogin userLogin);
    Task<User> BannedUser(BannedUser bannedUser);
    Task<List<BannedUser>> GetAllBannedUser();
}