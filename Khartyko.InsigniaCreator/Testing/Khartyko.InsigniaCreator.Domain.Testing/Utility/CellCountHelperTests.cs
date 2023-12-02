

using Khartyko.InsigniaCreator.Domain.Utility;
/** \addtogroup Domain.Testing
* @{
*/
namespace Khartyko.InsigniaCreator.Domain.Testing.Utility;

public class CellCountHelperTests
{
    [Theory]
    [InlineData(true, 3, 3)]
    [InlineData(false, 3, 4)]
    [InlineData(true, 4, 3)]
    [InlineData(false, 1, 2)]
    [InlineData(true, 2, 1)]
    [InlineData(false, 2, 2)]
    public void ConstrainCountByCentering_Succeeds(bool centerAlongAxis, int count, int expectedCount)
    {
        int actualCount = CellCounterHelper.ConstrainCountByCentering(centerAlongAxis, count);

        Assert.Equal(expectedCount, actualCount);
    }

    [Theory]
    [InlineData(0)]
    [InlineData(-1)]
    public void ConstrainCountByCentering_InvalidCount_Fails(int invalidCount)
    {
        Assert.Throws<ArgumentOutOfRangeException>(() => CellCounterHelper.ConstrainCountByCentering(true, invalidCount));
        Assert.Throws<ArgumentOutOfRangeException>(() => CellCounterHelper.ConstrainCountByCentering(false, invalidCount));
    }
}

/** @} */