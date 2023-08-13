using Khartyko.InsigniaCreator.Library.Utility.Helpers;

#pragma warning disable CS8600, CS8604, CS8625

namespace Khartyko.InsigniaCreator.Library.Testing.Utility.Helpers;

public class StringHelperTests
{
    #region EmptyOrWhitespaceCheck

    [Theory]
    [InlineData("Not an empty string", "NotEmptyString")]
    [InlineData("123", "Numbers")]
    [InlineData("123 - has a value", "NumberAndLetters")]
    public void EmptyOrWhitespaceCheck_Succeeds(string target, string name)
    {
        StringHelper.EmptyOrWhitespaceCheck(target, name);
    }

    [Fact]
    public void EmptyOrWhitespaceCheck_NullTarget_Fails()
    {
        string nullString = null;
        
        Assert.Throws<ArgumentNullException>(() => StringHelper.EmptyOrWhitespaceCheck(nullString, "NullString"));
    }

    [Theory]
    [InlineData("", "Empty")]
    [InlineData("    ", "Spaces")]
    [InlineData("\t\r\n", "SpecialCharacters")]
    public void EmptyOrWhitespaceCheck_InvalidTarget_Fails(string target, string name)
    {
        Assert.Throws<ArgumentException>(() => StringHelper.EmptyOrWhitespaceCheck(target, name)); 
    }

    [Fact]
    public void EmptyOrWhitespaceCheck_NullName_Fails()
    {
        string nullString = null;

        Assert.Throws<ArgumentNullException>(() => StringHelper.EmptyOrWhitespaceCheck("ValidString", nullString));
    }

    #endregion EmptyOrWhitespaceCheck
}