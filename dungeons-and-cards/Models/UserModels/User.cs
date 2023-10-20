using Microsoft.EntityFrameworkCore;

namespace dungeons_and_cards.Models.UserModels;
[PrimaryKey(nameof(UserName))]
public class User
{
    public Guid UserId { get; set; }
    public string UserName { get; set; }
    public string EmailAddress { get; set; }
    public DateTime RegistrationDate { get; set; }

    public User(string userName, string emailAddress)
    {
        UserId = Guid.NewGuid();
        UserName = userName;
        EmailAddress = emailAddress;
        RegistrationDate = DateTime.Now;
    }
}