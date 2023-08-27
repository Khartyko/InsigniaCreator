/** \addtogroup Library
 * @{
 */
namespace Khartyko.InsigniaCreator.Library.Utility.Helpers;

public static class AssertionHelper
{
    public static void NullCheck(object? target, string name)
    {
        if (target is not null)
        {
            return;
        }

        ReflectionMetadata metadata = ReflectionHelper.GetCallerMetadata(1);

        string signature = ReflectionHelper.ConstructMethodSignature(metadata, name);

        throw new ArgumentNullException(name, $"{signature}:\n\t{name} is null");
    }

    public static void EqualCheck<T>(T? left, T? right, string leftDescriptor, string rightDescriptor)
    {
        NullCheck(left, leftDescriptor);
        NullCheck(right, rightDescriptor);

        if (!(left!.Equals(right)))
        {
            return;
        }

        ReflectionMetadata metadata = ReflectionHelper.GetCallerMetadata(1);

        string signature = ReflectionHelper.ConstructMethodSignature(metadata);
        var primaryMessage = $"\n\t'{leftDescriptor}' and '{rightDescriptor}' cannot be equal";
        var auxiliaryMessage = $"\n\t\t'{leftDescriptor}': {left}\n\t\t'{rightDescriptor}': {right}";

        throw new ArgumentException($"{signature}:{primaryMessage};{auxiliaryMessage}");
    }
    
    public static void EmptyOrWhitespaceCheck(string target, string name)
    {
        NullCheck(name, nameof(target));
        NullCheck(target, nameof(target));
        
        if (!string.IsNullOrWhiteSpace(target))
        {
            return;
        }

        ReflectionMetadata metadata = ReflectionHelper.GetCallerMetadata(1);

        string signature = ReflectionHelper.ConstructMethodSignature(metadata, name);
            
        throw new ArgumentException($"{signature}:\n\t{name} is null or whitespace");
    }

    public static void InvalidDoubleCheck(double value, string descriptor)
    {
        EmptyOrWhitespaceCheck(descriptor, nameof(descriptor));
        
        if (double.IsNaN(value))
        {
            ReflectionMetadata metadata = ReflectionHelper.GetCallerMetadata(1);

            string signature = ReflectionHelper.ConstructMethodSignature(metadata, descriptor);

            throw new ArgumentException($"{signature}:\n\t{descriptor} cannot be 'double.NaN'");
        }

        if (double.IsPositiveInfinity(value))
        {
            ReflectionMetadata metadata = ReflectionHelper.GetCallerMetadata(1);

            string signature = ReflectionHelper.ConstructMethodSignature(metadata, descriptor);

            throw new ArgumentException($"{signature}:\n\t{descriptor} cannot be 'double.PositiveInfinity'");
        }

        if (double.IsNegativeInfinity(value))
        {
            ReflectionMetadata metadata = ReflectionHelper.GetCallerMetadata(1);

            string signature = ReflectionHelper.ConstructMethodSignature(metadata, descriptor);

            throw new ArgumentException($"{signature}:\n\t{descriptor} cannot be 'double.NegativeInfinity'");
        }
    }

    public static void ZeroCheck(double value, string descriptor)
    {
        EmptyOrWhitespaceCheck(descriptor, nameof(descriptor));

        if (!MathHelper.Equals(value, 0.0))
        {
            return;
        }
        
        ReflectionMetadata metadata = ReflectionHelper.GetCallerMetadata(1);

        string signature = ReflectionHelper.ConstructMethodSignature(metadata, descriptor);

        throw new ArgumentException($"{signature}:\n\t{descriptor} cannot be 0.0");
    }

    public static void PositiveCheck(int value, string descriptor)
    {
        EmptyOrWhitespaceCheck(descriptor, nameof(descriptor));

        if (0 <= value)
        {
            return;
        }

        ReflectionMetadata metadata = ReflectionHelper.GetCallerMetadata(1);

        string signature = ReflectionHelper.ConstructMethodSignature(metadata, descriptor);

        throw new ArgumentException($"{signature}:\n\t'{descriptor}' cannot be equal to or less than zero; got '{value}'");
    }
    
    public static void PositiveCheck(long value, string descriptor)
    {
        EmptyOrWhitespaceCheck(descriptor, nameof(descriptor));

        if (0L <= value)
        {
            return;
        }

        ReflectionMetadata metadata = ReflectionHelper.GetCallerMetadata(1);

        string signature = ReflectionHelper.ConstructMethodSignature(metadata, descriptor);

        throw new ArgumentException($"{signature}:\n\t'{descriptor}' cannot be equal to or less than zero; got '{value}'");
    }
    
    public static void PositiveCheck(double value, string descriptor)
    {
        EmptyOrWhitespaceCheck(descriptor, nameof(descriptor));

        if (MathHelper.GreaterThan(value, 0.0))
        {
            return;
        }

        ReflectionMetadata metadata = ReflectionHelper.GetCallerMetadata(1);

        string signature = ReflectionHelper.ConstructMethodSignature(metadata, descriptor);

        throw new ArgumentOutOfRangeException($"{signature}:\n\t'{descriptor}' cannot be equal to or less than zero; got '{value}'");
    }

    public static void MinimumCheck(int value, int minimum, string descriptor)
    {
        EmptyOrWhitespaceCheck(descriptor, nameof(descriptor));

        if (value < minimum)
        {
            ReflectionMetadata metadata = ReflectionHelper.GetCallerMetadata(1);

            string signature = ReflectionHelper.ConstructMethodSignature(metadata, descriptor);

            throw new ArgumentOutOfRangeException(descriptor, $"{signature}:\n\tvalue cannot be less than or equal to the minimum; got: {value} < {minimum}");
        }
    }

    public static void RangeCheck(int value, int minimum, int maximum, string descriptor)
    {
        EmptyOrWhitespaceCheck(descriptor, nameof(descriptor));

        if (value < minimum)
        {
            ReflectionMetadata metadata = ReflectionHelper.GetCallerMetadata(1);

            string signature = ReflectionHelper.ConstructMethodSignature(metadata, descriptor);

            throw new ArgumentOutOfRangeException(descriptor, $"{signature}:\n\tvalue cannot be less than to the minimum; got: {value} < {minimum}");
        }

        if (value == minimum)
        {
            ReflectionMetadata metadata = ReflectionHelper.GetCallerMetadata(1);

            string signature = ReflectionHelper.ConstructMethodSignature(metadata, descriptor);

            throw new ArgumentOutOfRangeException(descriptor, $"{signature}:\n\tvalue cannot be equal to the minimum; got: {value} == {minimum}");
        }

        if (maximum < value)
        {
            ReflectionMetadata metadata = ReflectionHelper.GetCallerMetadata(1);

            string signature = ReflectionHelper.ConstructMethodSignature(metadata, descriptor);

            throw new ArgumentOutOfRangeException(descriptor, $"{signature}:\n\tmaximum cannot be less than the value; got: {maximum} < {value}");
        }
        
        if (value == maximum)
        {
            ReflectionMetadata metadata = ReflectionHelper.GetCallerMetadata(1);

            string signature = ReflectionHelper.ConstructMethodSignature(metadata, descriptor);

            throw new ArgumentOutOfRangeException(descriptor, $"{signature}:\n\tmaximum cannot be equal to the value; got: {maximum} == {value}");
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
            ReflectionMetadata metadata = ReflectionHelper.GetCallerMetadata(1);

            string signature = ReflectionHelper.ConstructMethodSignature(metadata, descriptor);

            throw new ArgumentOutOfRangeException(descriptor, $"{signature}:\n\tvalue cannot be less than to the minimum; got: {value} < {minimum}");
        }

        if (Equals(value, minimum))
        {
            ReflectionMetadata metadata = ReflectionHelper.GetCallerMetadata(1);

            string signature = ReflectionHelper.ConstructMethodSignature(metadata, descriptor);

            throw new ArgumentOutOfRangeException(descriptor, $"{signature}:\n\tvalue cannot be equal to the minimum; got: {value} == {minimum}");
        }

        if (maximum < value)
        {
            ReflectionMetadata metadata = ReflectionHelper.GetCallerMetadata(1);

            string signature = ReflectionHelper.ConstructMethodSignature(metadata, descriptor);

            throw new ArgumentOutOfRangeException(descriptor, $"{signature}:\n\tmaximum cannot be less than the value; got: {maximum} < {value}");
        }
        
        if (Equals(maximum, value))
        {
            ReflectionMetadata metadata = ReflectionHelper.GetCallerMetadata(1);

            string signature = ReflectionHelper.ConstructMethodSignature(metadata, descriptor);

            throw new ArgumentOutOfRangeException(descriptor, $"{signature}:\n\tmaximum cannot be equal to the value; got: {maximum} == {value}");
        }
    }

    public static void EmptyCheck<T>(IEnumerable<T> items, string descriptor)
    {
        NullCheck(items, nameof(items));
        EmptyOrWhitespaceCheck(descriptor, nameof(descriptor));
        
        if (items.Any())
        {
            return;
        }
        
        ReflectionMetadata metadata = ReflectionHelper.GetCallerMetadata(1);

        string signature = ReflectionHelper.ConstructMethodSignature(metadata, descriptor);

        throw new ArgumentException($"{signature}:\n\t'{descriptor}' cannot be empty");
    }
    
    public static void MinimumCountCheck<T>(IEnumerable<T> items, int minimumCount, string descriptor)
    {
        NullCheck(items, nameof(items));
        EmptyOrWhitespaceCheck(descriptor, nameof(descriptor));
        PositiveCheck(minimumCount, nameof(minimumCount));

        int itemCount = items.Count();

        if (minimumCount <= itemCount)
        {
            return;
        }
        
        ReflectionMetadata metadata = ReflectionHelper.GetCallerMetadata(1);

        string signature = ReflectionHelper.ConstructMethodSignature(metadata, descriptor);

        string message = itemCount == 0
            ? $"{signature}:\n\t'{descriptor}' cannot be empty"
            : $"{signature}:\n\t'{descriptor}' has to have more than {minimumCount} or more; got {itemCount} items instead";
        
        throw new ArgumentException(message);
    }

    public static void DuplicatesCheck<T>(IEnumerable<T> items, string descriptor)
    {
        NullCheck(items, nameof(items));
        EmptyOrWhitespaceCheck(descriptor, nameof(descriptor));

        List<T> itemsList = items.ToList();
        
        if (!itemsList.Any())
        {
            return;
        }

        List<T> duplicatesFound = itemsList
            .Where(outerItem => itemsList
                .Count(innerItem => Equals(outerItem, innerItem)) > 1)
            .ToList();

        if (!duplicatesFound.Any())
        {
            return;
        }
        
        ReflectionMetadata metadata = ReflectionHelper.GetCallerMetadata(1);

        string signature = ReflectionHelper.ConstructMethodSignature(metadata, descriptor);
        string duplicatesString = string.Join(", ", duplicatesFound);
        
        throw new ArgumentException($"{signature}:\n\t'' contains duplicates; got '{duplicatesString}'");
    }
}
/** @} */