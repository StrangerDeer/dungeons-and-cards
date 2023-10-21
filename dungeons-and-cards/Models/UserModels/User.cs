using Microsoft.EntityFrameworkCore;

namespace dungeons_and_cards.Models.UserModels;
[PrimaryKey(nameof(UserId))]
public abstract class User : UserM
{
    private static Guid Id { get; set; }
    private static DateTime StartDate { get; set; }
    public User(string userName, string emailAddress) : base(Id, userName, emailAddress, StartDate)
    {
        Id = Guid.NewGuid();
        StartDate = DateTime.Now;
    }
    
}