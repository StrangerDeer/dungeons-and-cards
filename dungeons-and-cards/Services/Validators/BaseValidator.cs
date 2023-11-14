using dungeons_and_cards.Services.Exceptions;

namespace dungeons_and_cards.Services.Validators;

public abstract class BaseValidator
{
    private string Message { get; set; }

    protected BaseValidator(string message)
    {
        Message = message;
    }
    
    public abstract bool Checker<T>(T parameter) where T : class;

    public virtual void ValidatorMessage()
    {
        throw new BadRequestException(Message);
    }
}