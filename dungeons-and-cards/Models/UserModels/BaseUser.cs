using Microsoft.EntityFrameworkCore;

namespace dungeons_and_cards.Models.UserModels;

[PrimaryKey(nameof(UserId))]

public abstract class BaseUser
{
    public Guid UserId { get; set; }
    public string Username { get; set; }
    protected string HashPassword { get; set; }
    public string EmailAddress { get; set; }
    public Role UserRole { get; set; }
    public DateTime RegistrationDate { get; set; }
    
    protected BaseUser(Guid userId, string username, string hashPassword, string emailAddress, Role userRole, DateTime registrationDate)
    {
        UserId = userId;
        Username = username;
        HashPassword = hashPassword;
        EmailAddress = emailAddress;
        UserRole = userRole;
        RegistrationDate = registrationDate;
    }
    
}