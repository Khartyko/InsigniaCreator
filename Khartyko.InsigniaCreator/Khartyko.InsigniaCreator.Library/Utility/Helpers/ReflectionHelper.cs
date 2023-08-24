using System.Diagnostics;
using System.Reflection;

namespace Khartyko.InsigniaCreator.Library.Utility.Helpers;

public static class ReflectionHelper
{
    public static ReflectionMetadata GetCallerMetadata(int frameOffset = 0)
    {
        var stackTrace = new StackTrace();
        StackFrame frame = stackTrace.GetFrame(frameOffset + 1)!;

        MethodBase methodBase = frame.GetMethod()!;
        Type ownerType = methodBase.DeclaringType!;
        var methodType = MethodTypes.RegularMethod;

        if (methodBase.IsSpecialName)
        {
            if (methodBase.Name.StartsWith("get_"))
            {
                // More than likely a 'Get' Property
                methodType = MethodTypes.PropertyGet;
            }
            else if (methodBase.Name.StartsWith("set_"))
            {
                // More than likely a 'Set' Property
                methodType = MethodTypes.PropertySet;
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
        string methodName = methodBase.Name;

        if (reflectionMetadata.MethodType == MethodTypes.RegularMethod)
        {
            ParameterInfo[] parameters = methodBase.GetParameters();
            string[] parameterNames = parameters.Select(
                    parameterInfo => string.Equals(parameterInfo.Name, parameterName)
                        ? $">{parameterInfo.Name}<"
                        : parameterInfo.Name!)
                .ToArray();

            parameterList = $"({string.Join(", ", parameterNames)})";
        }
        else
        {
            int startIndex = methodName.IndexOf('_') + 1;
            int usableLength = methodName.Length - 4;
            methodName = methodName.Substring(startIndex, usableLength);
        }
        
        return $"{type.Name}::{methodName}{parameterList}";
    }
}