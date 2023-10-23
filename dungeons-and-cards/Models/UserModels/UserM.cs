
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace dungeons_and_cards.Models.UserModels;
[PrimaryKey(nameof(UserId))]
public abstract class UserM
{
    public Guid UserId { get; set; }
    public string UserName { get; set; }
    public string EmailAddress { get; set; }
    public DateTime RegistrationDate { get; set; }
    
    protected UserM(Guid userId, string userName, string emailAddress, DateTime registrationDate)
    {
        UserId = userId;
        UserName = userName;
        EmailAddress = emailAddress;
        RegistrationDate = registrationDate;
    }
    
    
}