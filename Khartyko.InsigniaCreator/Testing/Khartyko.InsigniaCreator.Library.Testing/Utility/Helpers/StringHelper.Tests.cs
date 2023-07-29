using Khartyko.InsigniaCreator.Library.Utility.Helpers;

#pragma warning disable CS8625

namespace Khartyko.InsigniaCreator.Library.Testing.Utility.Helpers;

public class StringHelperTests
{
    #region NullCheck

    [Theory]
    [InlineData("Not an empty string", "NotEmptyString")]
    [InlineData("123", "Numbers")]
    [InlineData("123 - has a value", "NumberAndLetters")]
    public void StringHelper_NullCheck_Succeeds(string target, string name)
    {
        StringHelper.NullCheck(target, name);
    }

    [Fact]
    public void StringHelper_NullCheck_Fails()
    {
        Assert.Throws<ArgumentNullException>(() => StringHelper.NullCheck(null, "Null"));
    }

    #endregion NullCheck

    #region EmptyOrWhitespaceCheck

    [Theory]
    [InlineData("Not an empty string", "NotEmptyString")]
    [InlineData("123", "Numbers")]
    [InlineData("123 - has a value", "NumberAndLetters")]
    public void StringHelper_EmptyOrWhitespaceCheck_Succeeds(string target, string name)
    {
        StringHelper.EmptyOrWhitespaceCheck(target, name);
    }

    [Theory]
    [InlineData(null, "Null")]
    [InlineData("", "Empty")]
    [InlineData("    ", "Spaces")]
    [InlineData("\t\r\n", "SpecialCharacters")]
    public void StringHelper_EmptyOrWhitespaceCheck_Fails(string target, string name)
    {
        Assert.Throws<ArgumentException>(() => StringHelper.EmptyOrWhitespaceCheck(target, name)); 
    }

    #endregion EmptyOrWhitespaceCheck
}