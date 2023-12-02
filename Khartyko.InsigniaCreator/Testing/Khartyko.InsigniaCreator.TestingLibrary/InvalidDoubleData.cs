namespace Khartyko.InsigniaCreator.TestingLibrary;

public class InvalidDoubleData : TestDataItem
{
    public override IEnumerable<object[]> GetData()
    {
        yield return new object[] { double.NaN };
        yield return new object[] { double.PositiveInfinity };
        yield return new object[] { double.NegativeInfinity };
    }
}