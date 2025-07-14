namespace DinarInvestments.Domain.Shared;

public static class Guard
{
    public static void AssertArgumentNotNull(object value, string argumentName)
    {
        if (value == null)
            throw new ArgumentNullException(argumentName);
    }

    public static void AssertArgumentNotNullOrEmptyOrWhitespace(string value, string argumentName)
    {
        AssertArgumentNotNull(value, argumentName);
        if (string.IsNullOrWhiteSpace(value))
            throw new ArgumentException("Value cannot be an empty string.", argumentName);
    }

    public static void AssertArgumentNotLessThanOrEqualToZero<T>(T? value, string argumentName, string message = null)
        where T : struct, IComparable
    {
        AssertArgumentNotNull(value, argumentName);
        if (value.Value.CompareTo(default(T)) <= 0)
        {
            message ??= $"{argumentName} should be greater than zero.";
            throw new ArgumentException(message);
        }
    }
    
    public static void AssertArgumentNotLessThanZero<T>(T? value, string argumentName, string message = null)
        where T : struct, IComparable
    {
        AssertArgumentNotNull(value, argumentName);
        if (value.Value.CompareTo(default(T)) < 0)
        {
            message ??= $"{argumentName} should be greater than zero.";
            throw new ArgumentException(message);
        }
    }

    public static void AssertArgumentEquals<T>(T object1, T object2, string message = null)
    {
        if (!Equals(object1, object2))
        {
            message ??= $"{object1} should equal to {object2}";
            throw new InvalidOperationException(message);
        }
    }

    public static void AssertArgumentNotEquals<T>(T object1, T object2, string message = null)
    {
        if (Equals(object1, object2))
        {
            message ??= $"{object1} shouldn't equal to {object2}";
            throw new InvalidOperationException(message);
        }
    }

    public static void AssertEnumValue<TEnum>(TEnum value, string argumentName) where TEnum : struct, Enum
    {
        if (!Enum.IsDefined(typeof(TEnum), value))
        {
            throw new ArgumentException($"Invalid enum value for {argumentName}: {value}", argumentName);
        }
    }
}