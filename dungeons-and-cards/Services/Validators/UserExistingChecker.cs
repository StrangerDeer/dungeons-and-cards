using dungeons_and_cards.Services.Exceptions;

namespace dungeons_and_cards.Services.Validators;

public abstract class UserExistingChecker : BaseValidator
{
    private readonly string _propertyName;
    private readonly List<string> _users;
    private readonly List<Type> _validTypes;
    
    protected UserExistingChecker(string message, string propertyName, List<Type> validTypes, List<string> users) : base(message)
    {
        _users = users;
        _propertyName = propertyName;
        _validTypes = validTypes;
    }

    public override bool Checker<T>(T parameter)
        where T : class
    {
        string userPropertyForCheck = UsernameFromParameter(parameter);

        foreach (string username in _users)
        {
            if (username.Equals(userPropertyForCheck))
            {
                return true;
            }
        }
        
        return false;
    }

    private string UsernameFromParameter<T>(T parameter) where T : class
    {
        Type parameterType = parameter.GetType();
        
        if (_validTypes.Contains(parameterType))
        {
            return (string)parameterType.GetProperty(_propertyName)?.GetValue(parameter)!;
        }

        string allowedTypes = string.Join(", ", _validTypes.Select(t => t.Name));
        string message = $"Not valid type for {GetType().Name}. Allowed types: {allowedTypes}.";
        
        throw new BadTypeException(message);
    }
}