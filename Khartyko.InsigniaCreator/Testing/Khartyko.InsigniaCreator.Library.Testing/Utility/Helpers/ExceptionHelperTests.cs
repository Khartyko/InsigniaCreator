using System.Diagnostics;
using Khartyko.InsigniaCreator.Library.Utility.Helpers;

#pragma warning disable CS8625

namespace Khartyko.InsigniaCreator.Library.Testing.Utility.Helpers;

public class ExceptionHelperTests
{
    #region GenerateArgumentNullException

    [Theory]
    [InlineData(
        "Value cannot be null. (Parameter 'ExceptionHelperTests::GenerateArgumentNullException_Succeeds(>expectedMessage<)')")]
    public void GenerateArgumentNullException_Succeeds(string expectedMessage)
    {
        ArgumentNullException exception =
            ExceptionHelper.GenerateArgumentNullException(typeof(ExceptionHelperTests), nameof(expectedMessage));

        Assert.Equal(typeof(ArgumentNullException), exception.GetType());
        Assert.Equal(expectedMessage, exception.Message);
    }

    [Theory]
    [InlineData(null)]
    public void GenerateArgumentNullException_NullType_Fails(Type targetType)
    {
        Assert.Throws<ArgumentNullException>(() =>
            ExceptionHelper.GenerateArgumentNullException(targetType, nameof(targetType)));
    }

    [Theory]
    [InlineData(null, null)]
    [InlineData(null, "GenerateArgumentNullException_NullInput_Fails")]
    [InlineData("parameterName", null)]
    public void GenerateArgumentNullException_NullStrings_Fails(
        string parameterName,
        string callerName
    )
    {
        Assert.Throws<ArgumentException>(() =>
            ExceptionHelper.GenerateArgumentNullException(GetType(), parameterName, callerName));
    }

    [Fact]
    public void GenerateArgumentNullException_NoParameterOnCallerMethod_Fails()
    {
        Type type = GetType();

        Assert.Throws<ArgumentException>(() => ExceptionHelper.GenerateArgumentNullException(type, "NotAType"));
    }

    [Theory]
    [InlineData("This can be literally whatever")]
    [InlineData("As long as it isn't")]
    [InlineData("\"parameterName\"")]
    public void GenerateArgumentNullException_InvalidParameter_Fails(string parameterName)
    {
        Type type = GetType();

        Assert.Throws<ArgumentException>(() => ExceptionHelper.GenerateArgumentNullException(type, parameterName));
    }

    [Theory]
    [InlineData("callerName", "")]
    [InlineData("parameterName", "")]
    public void GenerateArgumentNullException_BadStringInput_Fails(string parameterName,
        string callerName)
    {
        Assert.Throws<ArgumentException>(() =>
            ExceptionHelper.GenerateArgumentNullException(GetType(), parameterName, callerName));
    }

    #endregion GenerateArgumentNullException

    #region GenerateArgumentException

    /*
     * Cases to handle:
     * - Invalid cases:
     *   - Null Values (4 parameters)
     */

    [Theory]
    [InlineData(
        "There was an issue with the parameter 'expectedErrorMessage'. (Parameter 'ExceptionHelperTests::GenerateArgumentException_WithoutErrorMessageFragment_Succeeds(>expectedErrorMessage<)')")]
    public void GenerateArgumentException_WithoutErrorMessageFragment_Succeeds(
        string expectedErrorMessage)
    {
        Type type = GetType();

        ArgumentException exception =
            ExceptionHelper.GenerateArgumentException(type, nameof(expectedErrorMessage));

        Assert.Equal(typeof(ArgumentException), exception.GetType());
        Assert.Equal(expectedErrorMessage, exception.Message);
    }

    [Theory]
    [InlineData("parameterName", "",
        "There was an issue with the parameter 'parameterName'. (Parameter 'ExceptionHelperTests::GenerateArgumentException_WithErrorMessageFragment_Succeeds(>parameterName<, errorMessageFragment, expectedErrorMessage)')")]
    [InlineData("errorMessageFragment", "This is a message about 'errorMessageFragment'",
        "This is a message about 'errorMessageFragment'. (Parameter 'ExceptionHelperTests::GenerateArgumentException_WithErrorMessageFragment_Succeeds(parameterName, >errorMessageFragment<, expectedErrorMessage)')")]
    [InlineData("expectedErrorMessage", "This is another message about 'expectedErrorMessage'",
        "This is another message about 'expectedErrorMessage'. (Parameter 'ExceptionHelperTests::GenerateArgumentException_WithErrorMessageFragment_Succeeds(parameterName, errorMessageFragment, >expectedErrorMessage<)')")]
    [InlineData("parameterName", null,
        "There was an issue with the parameter 'parameterName'. (Parameter 'ExceptionHelperTests::GenerateArgumentException_WithErrorMessageFragment_Succeeds(>parameterName<, errorMessageFragment, expectedErrorMessage)')")]
    public void GenerateArgumentException_WithErrorMessageFragment_Succeeds(
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
    public void GenerateArgumentException_NullType_Fails()
    {
        Assert.Throws<ArgumentNullException>(() =>
            ExceptionHelper.GenerateArgumentException(null, nameof(ExceptionHelperTests)));
    }

    [Fact]
    public void GenerateArgumentException_NullParameterName_Fails()
    {
        const string bogusString = "This is a message that won't be seen";

        Assert.Throws<ArgumentException>(() =>
            ExceptionHelper.GenerateArgumentException(GetType(), null));
        Assert.Throws<ArgumentException>(() =>
            ExceptionHelper.GenerateArgumentException(GetType(), null, null));
        Assert.Throws<ArgumentException>(() =>
            ExceptionHelper.GenerateArgumentException(GetType(), null, null, null));
        Assert.Throws<ArgumentException>(() =>
            ExceptionHelper.GenerateArgumentException(GetType(), null, bogusString));
        Assert.Throws<ArgumentException>(() =>
            ExceptionHelper.GenerateArgumentException(GetType(), null, string.Empty, string.Empty));
        Assert.Throws<ArgumentException>(() =>
            ExceptionHelper.GenerateArgumentException(GetType(), null, string.Empty, bogusString));
        Assert.Throws<ArgumentException>(() =>
            ExceptionHelper.GenerateArgumentException(GetType(), null, bogusString, string.Empty));
        Assert.Throws<ArgumentException>(() =>
            ExceptionHelper.GenerateArgumentException(GetType(), null, bogusString, bogusString));
    }

    [Fact]
    public void GenerateArgumentException_NullCallerName_Fails()
    {
        const string bogusString = "This is a message that won't be seen";

        Assert.Throws<ArgumentException>(() =>
            ExceptionHelper.GenerateArgumentException(GetType(), bogusString, string.Empty, null));
        Assert.Throws<ArgumentException>(() =>
            ExceptionHelper.GenerateArgumentException(GetType(), bogusString, string.Empty, string.Empty));
        Assert.Throws<ArgumentException>(() =>
            ExceptionHelper.GenerateArgumentException(GetType(), bogusString, bogusString, null));
        Assert.Throws<ArgumentException>(() =>
            ExceptionHelper.GenerateArgumentException(GetType(), bogusString, bogusString, string.Empty));
    }

    [Theory]
    [InlineData("InvalidParameter")]
    [InlineData("")]
    [InlineData(null)]
    public void GenerateArgumentException_InvalidStringArgument_Fails(string parameterName)
    {
        const string bogusString = "This is some string that won't be seen because of the Exception thrown";

        Assert.Throws<ArgumentException>(() =>
            ExceptionHelper.GenerateArgumentException(GetType(), parameterName));
        Assert.Throws<ArgumentException>(() =>
            ExceptionHelper.GenerateArgumentException(GetType(), parameterName, bogusString));
    }

    [Theory]
    [InlineData("")]
    [InlineData("Some other bad input")]
    public void GenerateArgumentException_InvalidCallerName_Fails(string callerName)
    {
        Assert.Throws<ArgumentException>(() =>
            ExceptionHelper.GenerateArgumentException(GetType(), nameof(callerName), "", callerName));
    }
    
    [Fact]
    public void GenerateArgumentException_NoParameter_Fails()
    {
        const string bogusString = "This is some string that won't be seen because of the Exception thrown";
        Type type = GetType();
        
        Assert.Throws<ArgumentException>(() => ExceptionHelper.GenerateArgumentException(type, bogusString));
        Assert.Throws<ArgumentException>(() => ExceptionHelper.GenerateArgumentException(type, bogusString, bogusString));
    }

    #endregion GenerateArgumentException
}