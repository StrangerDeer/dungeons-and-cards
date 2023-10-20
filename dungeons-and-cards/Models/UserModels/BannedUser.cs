using Microsoft.EntityFrameworkCore;

namespace dungeons_and_cards.Models.UserModels;

[PrimaryKey(nameof(UserName))]
public class BannedUser
{
    public Guid UserId { get; set; }
    public string UserName { get; set; }
    public string EmailAddress { get; set; }
    public DateTime BannedStart { get; set; }
    public DateTime BannedEnd { get; set; }

    public BannedUser(Guid userId, string userName, string emailAddress, DateTime bannedEnd)
    {
        UserId = userId;
        UserName = userName;
        EmailAddress = emailAddress;
        BannedEnd = bannedEnd;
        BannedStart = DateTime.Now;
    }
}