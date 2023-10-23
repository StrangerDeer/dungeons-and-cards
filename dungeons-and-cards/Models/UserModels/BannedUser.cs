using Microsoft.EntityFrameworkCore;

namespace dungeons_and_cards.Models.UserModels;

public class BannedUser : UserM
{
    public DateTime BannedStart { get; set; }
    public DateTime BannedEnd { get; set; }

    public BannedUser(Guid userId, string userName, string emailAddress, DateTime registrationDate, DateTime bannedEnd)
        : base(userId, userName, emailAddress, registrationDate)
    {
        BannedStart = DateTime.Now;
        BannedEnd = bannedEnd;
    }
}