using System.Reflection;
using System.Runtime.CompilerServices;

namespace Khartyko.InsigniaCreator.Library.Utility.Helpers;

public static class ExceptionHelper
{
    public static ArgumentNullException GenerateArgumentNullException(
        Type targetType,
        string parameterName,
        [CallerMemberName] string callerName = ""
    )
    {
        if (targetType is null)
        {
            throw GenerateArgumentNullException(
                typeof(ExceptionHelper),
                nameof(targetType)
            );
        }

        if (string.IsNullOrWhiteSpace(parameterName))
        {
            throw GenerateArgumentException(
                typeof(ExceptionHelper),
                nameof(parameterName),
                "'parameterName' is null or whitespace"
            );
        }

        if (string.IsNullOrWhiteSpace(callerName))
        {
            throw GenerateArgumentException(
                typeof(ExceptionHelper),
                nameof(callerName),
                "'callerName' is null or whitespace"
            );
        }
        
        var fullCallerName = $"{targetType.Name}::{callerName}";
        var parameterNames = GetMethodParameterNames(targetType, callerName);
        var constructedMessage = ConstructSignature(fullCallerName, parameterNames, parameterName);

        return new ArgumentNullException(constructedMessage);
    }

    public static ArgumentException GenerateArgumentException(
        Type targetType,
        string parameterName,
        string errorMessageFragment = "",
        [CallerMemberName] string callerName = ""
    )
    {
        if (targetType is null)
        {
            throw GenerateArgumentNullException(
                typeof(ExceptionHelper),
                nameof(targetType)
            );
        }

        if (string.IsNullOrWhiteSpace(parameterName))
        {
            throw GenerateArgumentException(
                typeof(ExceptionHelper),
                nameof(parameterName),
                "'parameterName' is null or whitespace"
            );
        }

        if (string.IsNullOrWhiteSpace(callerName))
        {
            throw GenerateArgumentException(
                typeof(ExceptionHelper),
                nameof(callerName),
                "'callerName' is null or whitespace"
            );
        }

        var errorMessage = string.IsNullOrWhiteSpace(errorMessageFragment)
            ? $"There was an issue with the parameter '{parameterName}'."
            : $"{errorMessageFragment}.";

        var fullCallerName = $"{targetType.Name}::{callerName}";
        var parameterNames = GetMethodParameterNames(targetType, callerName);
        var constructedMessage = ConstructSignature(fullCallerName, parameterNames, parameterName);

        throw new ArgumentException(errorMessage, constructedMessage);
    }
    
    private static IList<string> GetMethodParameterNames(Type type, string methodName)
    {
        IList<string> parameterNames;
        MethodInfo? method = type.GetMethod(methodName);

        if (method is null)
        {
            Type thisType = typeof(ExceptionHelper);
            var errorMessage = $"'{methodName}' is not a method found on the type '{type}'";

            var fullCallerName = $"{thisType.Name}::{methodName}";
            parameterNames = GetMethodParameterNames(thisType, nameof(GetMethodParameterNames));
            var constructedMessage = ConstructSignature(fullCallerName, parameterNames, nameof(methodName));

            throw new ArgumentException(errorMessage, constructedMessage);
        }

        parameterNames = new List<string>();
        IEnumerable<ParameterInfo> parameters = method.GetParameters();

        foreach (ParameterInfo parameterInfo in parameters)
        {
            if (parameterInfo.Name is null)
            {
                continue;
            }

            parameterNames.Add(parameterInfo.Name);
        }

        return parameterNames;
    }

    private static string ConstructSignature(
        string callerName,
        IList<string> parameterNames,
        string? parameterName = null
    )
    {
        const string fullCallerName = $"{nameof(ExceptionHelper)}::{nameof(ConstructSignature)}";

        if (string.IsNullOrEmpty(parameterName))
        {
            return $"{callerName}({string.Join(", ", parameterNames)})";
        }
        
        var targetIndex = parameterNames.IndexOf(parameterName);

        if (targetIndex == -1)
        {
            string errorMessage;
            string constructedSignature;
            
                
            if (parameterNames.Any())
            {
                constructedSignature = ConstructSignature(fullCallerName, parameterNames);
                var parameterNamesList = string.Join(", ", parameterNames);
                errorMessage = $"'{parameterName}' is not in the list of parameters for '{callerName}({parameterNamesList})'";
            }
            else
            {
                constructedSignature = $"{callerName}()";
                errorMessage = $"'{callerName}' has no parameters";
            }
            
            throw new ArgumentException(errorMessage, constructedSignature);
        }

        parameterNames[targetIndex] = $">{parameterName}<";

        return $"{callerName}({string.Join(", ", parameterNames)})";
    }
}