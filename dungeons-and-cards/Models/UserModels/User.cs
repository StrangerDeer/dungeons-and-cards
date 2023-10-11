using Microsoft.EntityFrameworkCore;

namespace dungeons_and_cards.Models.UserModels;
[PrimaryKey(nameof(EmailAddress))]
public class User
{
    public Guid UserId { get; }
    public string UserName { get; set; }
    public string EmailAddress { get; set; }
    public DateTime RegistrationDate { get; }

    public User(string userName, string emailAddress)
    {
        UserId = new Guid();
        UserName = userName;
        EmailAddress = emailAddress;
        RegistrationDate = DateTime.Now;
    }
}