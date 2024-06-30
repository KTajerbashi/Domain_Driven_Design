namespace MiniBlog.Core.Domain.Resources;

public class MessagePatterns
{
    public static string EmptyStringValidationMessage = "The value For {0} could not be null";
    public static string IdInvalidationMessage = "The Value for Id could not be less than 1";
    public static string StringLengthValidationMessage = "The length For {0} should be between {1} and {2}";
    public static string FirstName = nameof(FirstName);
    public static string LastName = nameof(LastName);
}
