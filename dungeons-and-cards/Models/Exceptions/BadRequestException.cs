namespace dungeons_and_cards.Services;

public class BadRequestException : Exception
{
    public BadRequestException(string message) : base(message)
    {
        
    }
}