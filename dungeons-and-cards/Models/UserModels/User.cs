using Microsoft.EntityFrameworkCore;

namespace dungeons_and_cards.Models.UserModels;
public class User : BaseUser
{
    public User(string username, string hashPassword, string emailAddress) : base(Guid.NewGuid(), username, hashPassword, emailAddress, DateTime.Now)
    {
    }
    
    public bool CheckPassword(string password)
    {
        return BCrypt.Net.BCrypt.EnhancedVerify(password, HashPassword);
    }
    public void ChangePassword(string newPassword)
    {
        HashPassword = BCrypt.Net.BCrypt.EnhancedHashPassword(newPassword);
    }
}