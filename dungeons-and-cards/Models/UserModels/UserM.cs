using Microsoft.EntityFrameworkCore;

namespace dungeons_and_cards.Models.UserModels;

[PrimaryKey(nameof(UserId))]
public abstract class UserM
{
    public Guid UserId { get; set; }
    public string Username { get; set; }
    protected string Password { get; set; }
    public string EmailAddress { get; set; }
    public DateTime RegistrationDate { get; set; }
    
    protected UserM(Guid userId, string username, string password, string emailAddress, DateTime registrationDate)
    {
        UserId = userId;
        Username = username;
        Password = password;
        EmailAddress = emailAddress;
        RegistrationDate = registrationDate;
    }
    
    
}