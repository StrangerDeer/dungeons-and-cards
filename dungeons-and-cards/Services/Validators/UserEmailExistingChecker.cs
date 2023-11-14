using dungeons_and_cards.Models.UserModels;

namespace dungeons_and_cards.Services.Validators;

public class UserEmailExistingChecker : UserExistingChecker
{
    private static readonly string UserEmailUsernameExistingMessage = "This e-mail is already taken";
    private static readonly string UserEmailPropertyName = "EmailAddress";
    private static readonly List<Type> UserEmailValidTypes = new() {typeof(RegistrationUserModel)};
    private static List<string> UserEmails;
    
    public UserEmailExistingChecker(List<User> users) 
        : base(UserEmailUsernameExistingMessage, UserEmailPropertyName, UserEmailValidTypes, UserEmails)
    {
        UserEmails = users.Select(u => u.EmailAddress).ToList();
    }
}