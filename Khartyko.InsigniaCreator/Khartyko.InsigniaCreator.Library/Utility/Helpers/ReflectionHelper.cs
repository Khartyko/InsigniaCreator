/** \addtogroup Library
 * @{
 */
using System.Diagnostics;
using System.Reflection;
using System.Text.RegularExpressions;

namespace Khartyko.InsigniaCreator.Library.Utility.Helpers;

/// <summary>
/// Static class that has methods that aid in getting a method's data or signature
/// </summary>
public static partial class ReflectionHelper
{
    /// <summary>
    /// Regex that is used to verify a method's 'Name' denotes a lambda method.
    /// </summary>
    /// <returns>A Regex object with the provided pattern.</returns>
    [GeneratedRegex("<[\\w_]+>b__[0-9]+")]
    private static partial Regex LambdaRegex();

    /// <summary>
    /// Gets the metadata of any methods that call this method, depending on how many frames upstream are specified.
    /// </summary>
    /// <param name="frameOffset">How many method calls to check upstream.</param>
    /// <exception cref="ArgumentOutOfRangeException">Can be thrown if 'frameOffset' is negative.</exception>
    /// <returns>Metadata in regards to a calling method.</returns>
    public static ReflectionMetadata GetCallerMetadata(int frameOffset = 0)
    {
        AssertionHelper.PositiveCheck(frameOffset, nameof(frameOffset));
        
        var stackTrace = new StackTrace();
        StackFrame frame = stackTrace.GetFrame(frameOffset + 1)!;

        MethodBase methodBase = frame.GetMethod()!;
        Type ownerType = methodBase.DeclaringType!;

        MethodTypes methodType = methodBase.MemberType == MemberTypes.Constructor
            ? MethodTypes.Constructor
            : MethodTypes.RegularMethod;
        
        Regex lambdaRegex = LambdaRegex();
        bool lambdaRegexMatches = lambdaRegex.IsMatch(methodBase.Name);
        
        if (lambdaRegexMatches)
        {
            methodType = MethodTypes.Lambda;
        }
        else if (methodBase.IsSpecialName)
        {
            ParameterInfo[] parameters = methodBase.GetParameters();

            if (methodBase.Name.StartsWith("get_"))
            {
                // More than likely a 'Get' Property
                // If it has a property, this is the indexer; otherwise it just gets the backing field
                methodType = parameters.Length == 1
                    ? MethodTypes.IndexerGet
                    : MethodTypes.PropertyGet;
            }
            else if (methodBase.Name.StartsWith("set_"))
            {
                // More than likely a 'Set' Property
                // If it has 2 properties, it means the first is the indexer
                methodType = parameters.Length == 2
                    ? MethodTypes.IndexerSet
                    : MethodTypes.PropertySet;
            }
        }

        return new ReflectionMetadata(ownerType, methodBase, methodType);
    }

    /// <summary>
    /// Creates the signature of a method, and denotes a particular parameter in its list, if found.
    /// </summary>
    /// <remarks>
    /// 'parameterName' can be null, empty, or anything else, as it is only used to look for a parameter's name.
    /// </remarks>
    /// <param name="reflectionMetadata">The metadata of the method.</param>
    /// <param name="parameterName">A parameter that may be found in the method's parameter list.</param>
    /// <exception cref="ArgumentNullException">Can be thrown if 'reflectionMetadata' is null.</exception>
    /// <returns>A string that holds the signature of the described method.</returns>
    public static string ConstructMethodSignature(ReflectionMetadata reflectionMetadata, string? parameterName = null)
    {
        AssertionHelper.NullCheck(reflectionMetadata, nameof(reflectionMetadata));

        Type type = reflectionMetadata.Type;
        MethodBase methodBase = reflectionMetadata.MethodBase;

        var parameterList = string.Empty;
        string typeName = type.Name;
        string methodName = methodBase.Name;

        switch (reflectionMetadata.MethodType)
        {
            case MethodTypes.RegularMethod:
            {
                ParameterInfo[] parameters = methodBase.GetParameters();
                string[] parameterNames = parameters.Select(
                        parameterInfo => string.Equals(parameterInfo.Name, parameterName)
                            ? $">{parameterInfo.Name}<"
                            : parameterInfo.Name!)
                    .ToArray();

                parameterList = $"({string.Join(", ", parameterNames)})";
                methodName = $"::{methodName}";

                break;
            }

            case MethodTypes.IndexerGet:
            {
                string contents = parameterName ?? string.Empty;

                string[] parameterNames = methodBase.GetParameters()
                    .Where(parameter => !string.IsNullOrWhiteSpace(parameter?.Name))
                    .Select(parameter => parameter.Name!)
                    .ToArray();

                string presentParameterName = parameterNames.First();

                contents = string.Equals(presentParameterName, contents)
                    ? $"[>{presentParameterName}<]"
                    : $"[{presentParameterName}]";

                parameterList = contents;
                methodName = string.Empty;

                break;
            }

            case MethodTypes.IndexerSet:
            {
                string contents = parameterName ?? string.Empty;

                string[] parameterNames = methodBase.GetParameters()
                    .Where(parameter => !string.IsNullOrWhiteSpace(parameter?.Name))
                    .Select(parameter => parameter.Name!)
                    .ToArray();

                string presentParameterName = parameterNames.First();
                string presentValueName = parameterNames.Last();

                if (string.Equals(presentParameterName, contents))
                {
                    contents = $"[>{presentParameterName}<] = value";
                }
                else if (string.Equals(presentValueName, contents))
                {
                    contents = $"[{presentParameterName}] = >{presentValueName}<";
                }
                else
                {
                    contents = $"[{presentParameterName}] = {presentValueName}";
                }

                parameterList = contents;
                methodName = string.Empty;

                break;
            }

            case MethodTypes.PropertySet:
            {
                int startIndex = methodName.IndexOf('_') + 1;
                int usableLength = methodName.Length - 4;
                string valueString = string.Equals(parameterName, "value")
                    ? ">value<"
                    : "value";
                methodName = $"::{methodName.Substring(startIndex, usableLength)} = {valueString}";

                break;
            }

            case MethodTypes.Constructor:
            {
                ParameterInfo[] parameters = methodBase.GetParameters();
                string[] parameterNames = parameters.Select(
                        parameterInfo => string.Equals(parameterInfo.Name, parameterName)
                            ? $">{parameterInfo.Name}<"
                            : parameterInfo.Name!)
                    .ToArray();

                parameterList = $"({string.Join(", ", parameterNames)})";
                methodName = $"::{type.Name}";

                break;
            }

            case MethodTypes.Lambda:
            {
                ParameterInfo[] parameters = methodBase.GetParameters();
                string[] parameterNames = parameters.Select(
                        parameterInfo => string.Equals(parameterInfo.Name, parameterName)
                            ? $">{parameterInfo.Name}<"
                            : parameterInfo.Name!)
                    .ToArray();

                parameterList = $"({string.Join(", ", parameterNames)})";
                methodName = $" -> λ";

                // This is just to get the method in which the lambda is called in
                ReflectionMetadata parentMethodMetadata = GetCallerMetadata(1);

                typeName = ConstructMethodSignature(parentMethodMetadata);
                
                break;
            }

            default:
            {
                int startIndex = methodName.IndexOf('_') + 1;
                int usableLength = methodName.Length - 4;
                methodName = $"::{methodName.Substring(startIndex, usableLength)}";

                break;
            }
        }

        return $"{typeName}{methodName}{parameterList}";
    }
}
/** @} */