using Khartyko.InsigniaCreator.Library.Utility.Helpers;
#pragma warning disable CS8625

namespace Khartyko.InsigniaCreator.Library.Testing.Utility.Helpers;

public class ExceptionHelperTests
{
    #region GenerateArgumentNullException

    [Theory]
    [InlineData("Value cannot be null. (Parameter 'ExceptionHelperTests::ExceptionHelper_GenerateArgumentNullException_Succeeds(>expectedMessage<)')")]
    public void ExceptionHelper_GenerateArgumentNullException_Succeeds(string expectedMessage)
    {
        ArgumentNullException exception = ExceptionHelper.GenerateArgumentNullException(typeof(ExceptionHelperTests), nameof(expectedMessage));
        
        Assert.Equal(typeof(ArgumentNullException), exception.GetType());
        Assert.Equal(expectedMessage, exception.Message);
    }

    [Theory]
    [InlineData(null)]
    public void ExceptionHelper_GenerateArgumentNullException_NullType_Fails(Type targetType)
    {
        Assert.Throws<ArgumentNullException>(() => ExceptionHelper.GenerateArgumentNullException(targetType, nameof(targetType)));
    }

    [Theory]
    [InlineData(null, null)]
    [InlineData(null, "ExceptionHelper_GenerateArgumentNullException_NullInput_Fails")]
    [InlineData("parameterName", null)]
    public void ExceptionHelper_GenerateArgumentNullException_NullStrings_Fails(
        string parameterName,
        string callerName
    )
    {
        Assert.Throws<ArgumentException>(() => ExceptionHelper.GenerateArgumentNullException(GetType(), parameterName, callerName));
    }
    
    [Fact]
    public void ExceptionHelper_GenerateArgumentNullException_NoParameterOnCallerMethod_Fails()
    {
        Type type = GetType();
        
        Assert.Throws<ArgumentException>(() => ExceptionHelper.GenerateArgumentNullException(type, "NotAType"));
    }

    [Theory]
    [InlineData("This can be literally whatever")]
    [InlineData("As long as it isn't")]
    [InlineData("\"parameterName\"")]
    public void ExceptionHelper_GenerateArgumentNullException_InvalidParameter_Fails(string parameterName)
    {
        Type type = GetType();
        
        Assert.Throws<ArgumentException>(() => ExceptionHelper.GenerateArgumentNullException(type, parameterName));
    }

    [Theory]
    [InlineData("", "ExceptionHelper_GenerateArgumentNullException_BadStringInput_Fails")]
    [InlineData("parameterName", "")]
    [InlineData("", "")]
    public void ExceptionHelper_GenerateArgumentNullException_BadStringInput_Fails(string parameterName, string callerName)
    {
        Assert.Throws<ArgumentException>(() =>
            ExceptionHelper.GenerateArgumentNullException(GetType(), parameterName, callerName));
    }

    #endregion GenerateArgumentNullException
    
    #region GenerateArgumentException
    
    /*
     * Cases to handle:
     * - Passing case (all valid data)
     * - Invalid cases:
     *   - Null Values (4 parameters)
     *   - Invalid Strings (3 parameters)
     *   - Invalid Parameter
     *   - Method with no parameters
     */

    [Theory]
    [InlineData("")]
    public void ExceptionHelper_GenerateArgumentException_WithoutErrorMessageFragment_Succeeds(string expectedErrorMessage)
    {
        Type type = GetType();

        ArgumentException exception =
            ExceptionHelper.GenerateArgumentException(type, nameof(expectedErrorMessage));
        
        Assert.Equal(typeof(ArgumentException), exception.GetType());
        Assert.Equal(expectedErrorMessage, exception.Message);
    }
    
    [Theory]
    [InlineData("parameterName", "", "")]
    [InlineData("errorMessageFragment", "", "")]
    [InlineData("expectedErrorMessage", "", "")]
    [InlineData("parameterName", null, "")]
    public void ExceptionHelper_GenerateArgumentException_WithErrorMessageFragment_Succeeds(
        string parameterName,
        string errorMessageFragment,
        string expectedErrorMessage
    )
    {
        Type type = GetType();

        ArgumentException exception =
            ExceptionHelper.GenerateArgumentException(type, parameterName, errorMessageFragment);
        
        Assert.Equal(typeof(ArgumentException), exception.GetType());
        Assert.Equal(expectedErrorMessage, exception.Message);
    }
    
    [Fact]
    public void ExceptionHelper_GenerateArgumentException_Fails()
    {
        
    }
    
    #endregion GenerateArgumentException
    
}