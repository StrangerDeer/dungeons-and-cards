namespace dungeons_and_cards.Models.UserModels;

public class RegistrationUserModel
{
    public string Username { get; set; }
    public string Password { get; set; }
    public string EmailAddress { get; set; }

    public RegistrationUserModel(string username, string password, string emailAddress)
    {
        Username = username;
        Password = password;
        EmailAddress = emailAddress;
    }
}