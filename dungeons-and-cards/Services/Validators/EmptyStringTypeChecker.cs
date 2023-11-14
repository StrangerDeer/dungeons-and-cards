using System.Reflection;
using Microsoft.IdentityModel.Tokens;

namespace dungeons_and_cards.Services.Validators;

public class EmptyStringTypeChecker : BaseValidator
{
    private static readonly string EmptyStringFieldMessage = "One of the fields is empty";
        
    public EmptyStringTypeChecker() : base(EmptyStringFieldMessage)
    {
    }

    public override bool Checker<T>(T parameter)
    {
        PropertyInfo[] properties = parameter!.GetType().GetProperties();
        
        foreach (PropertyInfo property in properties)
        {
            if (property.GetValue(parameter)!.ToString().IsNullOrEmpty())
            {
                return true;
            }
        }
        
        return false;
    }
}