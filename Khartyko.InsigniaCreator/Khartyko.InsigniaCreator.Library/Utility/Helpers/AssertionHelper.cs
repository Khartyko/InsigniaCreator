/** \addtogroup Library
 * @{
 */

namespace Khartyko.InsigniaCreator.Library.Utility.Helpers;

/// <summary>
/// Contains static methods to assist with enforcing certain restrictions.
/// </summary>
public static class AssertionHelper
{
    /// <summary>
    /// Checks if the 'target' object is null or not.
    /// </summary>
    /// <param name="target">The object in question.</param>
    /// <param name="name">The name of the object in question.</param>
    /// <exception cref="ArgumentNullException">Can be thrown if 'target' is null.</exception>
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

    /// <summary>
    /// Checks if 'left' and 'right are equal.
    /// </summary>
    /// <param name="left">The first subject.</param>
    /// <param name="right">The second subject.</param>
    /// <param name="leftDescriptor">The name of the first subject.</param>
    /// <param name="rightDescriptor">The name of the second subject.</param>
    /// <typeparam name="T">The type of both objects.</typeparam>
    /// <exception cref="ArgumentNullException">Can be thrown if any argument is null.</exception>
    /// <exception cref="ArgumentException">Can be thrown if 'leftDescriptor' or 'rightDescriptor' is empty or whitespace.</exception>
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
    
    /// <summary>
    /// Checks if the target string is empty or whitespace.
    /// </summary>
    /// <param name="target">The string to check.</param>
    /// <param name="name">The name of the string to check.</param>
    /// <exception cref="ArgumentNullException">Can be thrown if 'target' is null.</exception>
    /// <exception cref="ArgumentException">Can be thrown if 'target' is empty or whitespace.</exception>
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

    /// <summary>
    /// Checks if a double is NaN, PositiveInfinity, or NegativeInfinity.
    /// </summary>
    /// <param name="value">The target double in question.</param>
    /// <param name="descriptor">The name of the target double in question.</param>
    /// <exception cref="ArgumentNullException">Can be thrown if 'descriptor' is null.</exception>
    /// <exception cref="ArgumentException">Can be thrown if 'descriptor' is empty or whitespace, or if 'value' is NaN, PositiveInfinity, or NegativeInfinity.</exception>
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

    /// <summary>
    /// Checks if a double is 0.0.
    /// </summary>
    /// <param name="value">The double in question.</param>
    /// <param name="descriptor">The name of the double in question.</param>
    /// <exception cref="ArgumentException">Can be thrown if 'descriptor' is empty or whitespace, or if 'value' is 0.0, NaN, PositiveInfinity, or NegativeInfinity.</exception>
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

    /// <summary>
    /// Checks if an integer value is 0 or greater.
    /// </summary>
    /// <param name="value">The integer in question.</param>
    /// <param name="descriptor">The name of the integer in question.</param>
    /// <exception cref="ArgumentNullException">Can be thrown if 'descriptor' is null.</exception>
    /// <exception cref="ArgumentOutOfRangeException">Can be thrown if 'value' is less than 0.</exception>
    public static void PositiveCheck(int value, string descriptor)
    {
        EmptyOrWhitespaceCheck(descriptor, nameof(descriptor));

        if (0 <= value)
        {
            return;
        }

        ReflectionMetadata metadata = ReflectionHelper.GetCallerMetadata(1);

        string signature = ReflectionHelper.ConstructMethodSignature(metadata, descriptor);

        throw new ArgumentOutOfRangeException($"{signature}:\n\t'{descriptor}' cannot be equal to or less than zero; got '{value}'");
    }
    
    /// <summary>
    /// Checks if a long is 0 or greater.
    /// </summary>
    /// <param name="value">The long value in question.</param>
    /// <param name="descriptor">The name of the long value in question.</param>
    /// <exception cref="ArgumentNullException">Can be thrown if 'descriptor' is null.</exception>
    /// <exception cref="ArgumentOutOfRangeException">Can be thrown if 'value' is less than 0.</exception>
    public static void PositiveCheck(long value, string descriptor)
    {
        EmptyOrWhitespaceCheck(descriptor, nameof(descriptor));

        if (0L <= value)
        {
            return;
        }

        ReflectionMetadata metadata = ReflectionHelper.GetCallerMetadata(1);

        string signature = ReflectionHelper.ConstructMethodSignature(metadata, descriptor);

        throw new ArgumentOutOfRangeException($"{signature}:\n\t'{descriptor}' cannot be equal to or less than zero; got '{value}'");
    }
    
    /// <summary>
    /// Checks if a double is 0 or greater.
    /// </summary>
    /// <param name="value">The double in question.</param>
    /// <param name="descriptor">The name of the double in question.</param>
    /// <exception cref="ArgumentNullException">Can be thrown if 'descriptor' is null.</exception>
    /// <exception cref="ArgumentOutOfRangeException">Can be thrown if 'value' is less than 0.0.</exception>
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

    /// <summary>
    /// Checks if a given value is at least a minimum specified.
    /// </summary>
    /// <param name="value">The int in question.</param>
    /// <param name="minimum">The minimum value to check for.</param>
    /// <param name="descriptor">The name of the int in question.</param>
    /// <exception cref="ArgumentNullException">Can be thrown if 'descriptor' is null.</exception>
    /// <exception cref="ArgumentOutOfRangeException">Can be thrown if 'value' is less than 'minimum'.</exception>
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

    /// <summary>
    /// Checks if a given value is at least a minimum specified.
    /// </summary>
    /// <param name="value">The ulong in question.</param>
    /// <param name="minimum">The minimum value to check for.</param>
    /// <param name="descriptor">The name of the ulong in question.</param>
    /// <exception cref="ArgumentNullException">Can be thrown if 'descriptor' is null.</exception>
    /// <exception cref="ArgumentOutOfRangeException">Can be thrown if 'value' is less than 'minimum'.</exception>
    public static void MinimumCheck(ulong value, ulong minimum, string descriptor)
    {
        EmptyOrWhitespaceCheck(descriptor, nameof(descriptor));

        if (value < minimum)
        {
            ReflectionMetadata metadata = ReflectionHelper.GetCallerMetadata(1);

            string signature = ReflectionHelper.ConstructMethodSignature(metadata, descriptor);

            throw new ArgumentOutOfRangeException(descriptor, $"{signature}:\n\tvalue cannot be less than or equal to the minimum; got: {value} < {minimum}");
        }
    }

    /// <summary>
    /// Checks if a given value is at least a minimum specified.
    /// </summary>
    /// <param name="value">The double in question.</param>
    /// <param name="minimum">The minimum value to check for.</param>
    /// <param name="descriptor">The name of the double in question.</param>
    /// <exception cref="ArgumentNullException">Can be thrown if 'descriptor' is null.</exception>
    /// <exception cref="ArgumentOutOfRangeException">Can be thrown if 'value' is less than 'minimum'.</exception>
    /// <exception cref="ArgumentException">Can be thrown if 'value' or 'minimum' are invalid doubles.</exception>
    public static void MinimumCheck(double value, double minimum, string descriptor)
    {
        InvalidDoubleCheck(value, nameof(value));
        InvalidDoubleCheck(minimum, nameof(minimum));
        EmptyOrWhitespaceCheck(descriptor, nameof(descriptor));

        if (MathHelper.LessThan(value, minimum))
        {
            ReflectionMetadata metadata = ReflectionHelper.GetCallerMetadata(1);

            string signature = ReflectionHelper.ConstructMethodSignature(metadata, descriptor);

            throw new ArgumentOutOfRangeException(descriptor, $"{signature}:\n\tvalue cannot be less than or equal to the minimum; got: {value} < {minimum}");
        }
    }

    /// <summary>
    /// Checks if an integer value falls within a given range.
    /// </summary>
    /// <param name="value">The integer value in question.</param>
    /// <param name="minimum">The minimum to check for.</param>
    /// <param name="maximum">The maximum to check for.</param>
    /// <param name="descriptor"></param>
    /// <exception cref="ArgumentNullException">Can be thrown if 'descriptor' is null.</exception>
    /// <exception cref="ArgumentOutOfRangeException">Can be thrown if 'value' doesn't fall within </exception>
    /// <exception cref="ArgumentException">Can be thrown if 'maximum' is less than or equal to 'minimum'.</exception>
    public static void RangeCheck(int value, int minimum, int maximum, string descriptor)
    {
        EmptyOrWhitespaceCheck(descriptor, nameof(descriptor));

        CheckMinimum(minimum, maximum, descriptor);

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
    
    /// <summary>
    /// Checks if a double is in within a specified range.
    /// </summary>
    /// <param name="value">The double in question.</param>
    /// <param name="minimum">The minimum to check for.</param>
    /// <param name="maximum">The maximum to check for.</param>
    /// <param name="descriptor">The name of the double in question.</param>
    /// <exception cref="ArgumentNullException">Can be thrown if 'descriptor' is null.</exception>
    /// <exception cref="ArgumentOutOfRangeException">Can be thrown if 'value' is out the specified range.</exception>
    /// <exception cref="ArgumentException">Can be thrown if 'maximum' is less than or equal to 'minimum'.</exception>
    public static void RangeCheck(double value, double minimum, double maximum, string descriptor)
    {
        InvalidDoubleCheck(value, nameof(value));
        InvalidDoubleCheck(minimum, nameof(minimum));
        InvalidDoubleCheck(maximum, nameof(maximum));
        
        EmptyOrWhitespaceCheck(descriptor, nameof(descriptor));

        CheckMinimum(minimum, maximum, descriptor);
        
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

    /// <summary>
    /// Checks if a given collection is empty.
    /// </summary>
    /// <param name="items">The collection in question.</param>
    /// <param name="descriptor">The name of the collection in question.</param>
    /// <typeparam name="T">The type of the collection.</typeparam>
    /// <exception cref="ArgumentNullException">Can be thrown if either 'items' or 'descriptor' is null.</exception>
    /// <exception cref="ArgumentException">Can be thrown if either 'items' or 'descriptor' is empty, or if 'descriptor' is whitespace.</exception>
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
    
    /// <summary>
    /// Checks if a collection has a minimum number of items.
    /// </summary>
    /// <param name="items">The collection in question.</param>
    /// <param name="minimumCount">The minimum number of items the collection is allowed to have.</param>
    /// <param name="descriptor">The name of the collection in question.</param>
    /// <typeparam name="T">The type of the collection.</typeparam>
    /// <exception cref="ArgumentNullException">Can be thrown if either 'items' or 'descriptor' is null.</exception>
    /// <exception cref="ArgumentException">Can be thrown if either 'items' or 'descriptor' is empty, if 'descriptor' is whitespace, or if 'items' has too few items.</exception>
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

    /// <summary>
    /// Checks if there are any duplicates in the given collection.
    /// </summary>
    /// <param name="items">The collection in question.</param>
    /// <param name="descriptor">The name of the collection in question.</param>
    /// <typeparam name="T">The type of the collection.</typeparam>
    /// <exception cref="ArgumentNullException">Can be thrown if either 'items' or 'descriptor' is null.</exception>
    /// <exception cref="ArgumentException">Can be thrown if 'descriptor' is empty or whitespace.</exception>
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

    private static void CheckMinimum(double minimum, double maximum, string descriptor)
    {
        if (maximum <= minimum)
        {
            ReflectionMetadata metadata = ReflectionHelper.GetCallerMetadata(1);

            string signature = ReflectionHelper.ConstructMethodSignature(metadata, descriptor);

            throw new ArgumentException($"{signature}:\n\t'maximum' cannot be less than or equal to 'minimum'; got: maximum: {maximum}, minimum: {minimum}");
        }
    }
}

/** @} */