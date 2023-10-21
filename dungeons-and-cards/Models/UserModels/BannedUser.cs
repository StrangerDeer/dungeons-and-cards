using Microsoft.EntityFrameworkCore;

namespace dungeons_and_cards.Models.UserModels;

[PrimaryKey(nameof(UserId))]
public class BannedUser : UserM
{
    public DateTime BannedStart { get; set; }
    public DateTime BannedEnd { get; set; }

    public BannedUser(Guid id, string username, string email, DateTime registrationDate, DateTime bannedEnd) : base(id, username, email, registrationDate)
    {
        BannedEnd = bannedEnd;
        BannedStart = DateTime.Now;
    }
}