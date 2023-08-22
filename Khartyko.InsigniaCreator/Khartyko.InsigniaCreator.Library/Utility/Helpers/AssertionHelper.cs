namespace Khartyko.InsigniaCreator.Library.Utility.Helpers;

public static class AssertionHelper
{
    public static void NullCheck(object target, string name)
    {
        if (target is null)
        {
            throw new ArgumentNullException(name, $"{name} is null");
        }
    }

    public static void EmptyOrWhitespaceCheck(string target, string name)
    {
        if (name is null)
        {
            throw new ArgumentNullException(nameof(name), $"{nameof(name)} is null");
        }
        
        if (target is null)
        {
            throw new ArgumentNullException(name, $"{name} is null");
        }
        
        if (string.IsNullOrWhiteSpace(target))
        {
            throw new ArgumentException($"{name} is null or whitespace", name);
        }
    }

    public static void InvalidDoubleCheck(double value, string descriptor)
    {
        if (string.IsNullOrWhiteSpace(descriptor))
        {
            throw ExceptionHelper.GenerateArgumentException(
                typeof(AssertionHelper),
                nameof(descriptor),
                "'descriptor' cannot be null, empty, or whitespace."
            );
        }

        if (double.IsNaN(value))
        {
            throw ExceptionHelper.GenerateArgumentException(
                typeof(AssertionHelper),
                nameof(descriptor),
                $"{descriptor} is NaN"
            );
        }

        if (double.IsPositiveInfinity(value))
        {
            throw ExceptionHelper.GenerateArgumentException(
                typeof(AssertionHelper),
                nameof(descriptor),
                $"{descriptor} is Positive Infinity"
            );
        }

        if (double.IsNegativeInfinity(value))
        {
            throw ExceptionHelper.GenerateArgumentException(
                typeof(AssertionHelper),
                nameof(descriptor),
                $"{descriptor} is Negative Infinity"
            );
        }
    }

    public static void ZeroCheck(double value, string descriptor)
    {
        if (string.IsNullOrWhiteSpace(descriptor))
        {
            throw ExceptionHelper.GenerateArgumentException(
                typeof(AssertionHelper),
                nameof(descriptor),
                "'descriptor' cannot be null, empty, or whitespace"
            );
        }

        if (Equals(value, 0.0))
        {
            throw ExceptionHelper.GenerateArgumentException(
                typeof(AssertionHelper),
                nameof(descriptor),
                $"'{descriptor}' cannot be zero"
            );
        }
    }

    public static void PositiveCheck(double value, string descriptor)
    {
        if (string.IsNullOrWhiteSpace(descriptor))
        {
            throw ExceptionHelper.GenerateArgumentException(
                typeof(AssertionHelper),
                nameof(descriptor),
                "'descriptor' cannot be null, empty, or whitespace"
            );
        }

        if (MathHelper.LessThan(value, 0) || MathHelper.Equals(value, 0))
        {
            throw ExceptionHelper.GenerateArgumentException(
                typeof(AssertionHelper),
                nameof(descriptor),
                $"'{descriptor}' cannot be equal to or less than zero; (got '{value}')"
            );
        }
    }
    
    public static void RangeCheck(double value, double minimum, double maximum, string descriptor)
    {
        InvalidDoubleCheck(value, nameof(value));
        InvalidDoubleCheck(minimum, nameof(minimum));
        InvalidDoubleCheck(maximum, nameof(maximum));
        
        EmptyOrWhitespaceCheck(descriptor, nameof(descriptor));

        if (value < minimum)
        {
            throw ExceptionHelper.GenerateArgumentException(
                typeof(AssertionHelper),
                nameof(value),
                $"value cannot be less than to the minimum (Got: {value} < {minimum})"
            );
        }

        if (Equals(value, minimum))
        {
            throw ExceptionHelper.GenerateArgumentException(
                typeof(AssertionHelper),
                nameof(value),
                $"value cannot be equal to the minimum (Got: {value} == {minimum})"
            );
        }

        if (maximum < value)
        {
            throw ExceptionHelper.GenerateArgumentException(
                typeof(AssertionHelper),
                nameof(value),
                $"maximum cannot be less than the value (Got: {maximum} < {value})"
            );
        }
        
        if (Equals(maximum, value))
        {
            throw ExceptionHelper.GenerateArgumentException(
                typeof(AssertionHelper),
                nameof(value),
                $"maximum cannot be equal to the value (Got: {maximum} == {value})"
            );
        }
    }
}