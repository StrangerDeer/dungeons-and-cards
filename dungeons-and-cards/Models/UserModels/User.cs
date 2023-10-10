using Microsoft.EntityFrameworkCore;

namespace dungeons_and_cards.Models.UserModels;
[PrimaryKey(nameof(UserId))]
public class User
{
    public Guid UserId { get; }
    public string UserName { get; set; }

    public User(string userName)
    {
        UserName = userName;
        UserId = new Guid();
    }
}