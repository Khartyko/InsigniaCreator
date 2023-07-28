using Khartyko.InsigniaCreator.Library.Utility.Helpers;

#pragma warning disable CS8604
#pragma warning disable CS8600

namespace Khartyko.InsigniaCreator.Library.Testing.Utility.Helpers;

public class ObjectHelperTests
{
    #region NullCheck

    [Theory]
    [InlineData("A string")]
    [InlineData(42)]
    public void NullCheck_Succeeds(object target)
    {
        ObjectHelper.NullCheck(target, nameof(target));
    }

    [Fact]
    public void NullCheck_Fails()
    {
        object nullObj = null;
        
        Assert.Throws<ArgumentNullException>(() => ObjectHelper.NullCheck(nullObj, nameof(nullObj)));
    }

    #endregion NullCheck
}