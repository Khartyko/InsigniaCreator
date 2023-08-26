using System.Diagnostics;
using System.Reflection;
using System.Text.RegularExpressions;
namespace Khartyko.InsigniaCreator.Library.Utility.Helpers;

public static partial class ReflectionHelper
{
    [GeneratedRegex("<[\\w_]+>b__[0-9]+")]
    private static partial Regex LambdaRegex();

    public static ReflectionMetadata GetCallerMetadata(int frameOffset = 0)
    {
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