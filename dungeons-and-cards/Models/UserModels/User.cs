using Microsoft.EntityFrameworkCore;

namespace dungeons_and_cards.Models.UserModels;
public class User : UserM
{
    private static Guid Id { get; set; }
    private static DateTime StartDate { get; set; }
    private static string UserPassword { get; set; }
    public User(string username, string password, string emailAddress) : base(Id, username, UserPassword, emailAddress, StartDate)
    {
        Id = Guid.NewGuid();
        StartDate = DateTime.Now;
        UserPassword = HashPassword(password);
    }

    private string HashPassword(string password)
    {
        return BCrypt.Net.BCrypt.EnhancedHashPassword(password);
    }
    public bool CheckPassword(string password)
    {
        return BCrypt.Net.BCrypt.EnhancedVerify(password, Password);
    }

    public void ChangePassword(string newPassword)
    {
        Password = BCrypt.Net.BCrypt.EnhancedHashPassword(newPassword);
    }
}