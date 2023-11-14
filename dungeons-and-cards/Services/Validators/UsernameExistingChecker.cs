using dungeons_and_cards.Models.UserModels;

namespace dungeons_and_cards.Services.Validators;

public class UsernameExistingChecker : UserExistingChecker
{
    private static readonly string UsernameExistingMessage = "This username is already exist";
    private static readonly string UsernamePropertyName = "Username";
    private static readonly List<Type> UsernameValidTypes = new() {typeof(RegistrationUserModel), typeof(UserLogin)};
    private static List<string> Usernames;
    
    public UsernameExistingChecker(List<User> users) 
        : base(UsernameExistingMessage, UsernamePropertyName, UsernameValidTypes, Usernames)
    {
        Usernames = users.Select(u => u.Username).ToList();
    }
}