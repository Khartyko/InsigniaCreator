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

        return new ReflectionMetadata(ownerType, methodBase);
    }

    public static string ConstructMethodSignature(ReflectionMetadata reflectionMetadata, string? parameterName = null)
    {
        AssertionHelper.NullCheck(reflectionMetadata, nameof(reflectionMetadata));

        Type type = reflectionMetadata.Type;
        MethodBase methodBase = reflectionMetadata.MethodBase;
        
        ParameterInfo[] parameters = methodBase.GetParameters();
        string[] parameterNames = parameters.Select(
                parameterInfo => string.Equals(parameterInfo.Name, parameterName)
                    ? $">{parameterInfo.Name}<"
                    : parameterInfo.Name!)
            .ToArray();
        
        return $"{type.Name}::{methodBase.Name}({string.Join(", ", parameterNames)})";
    }
}