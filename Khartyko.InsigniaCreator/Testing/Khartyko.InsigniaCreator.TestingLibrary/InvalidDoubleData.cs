using System.Collections;

namespace Khartyko.InsigniaCreator.TestingLibrary;

public class InvalidDoubleData : IEnumerable<object[]>
{
    public IEnumerator<object[]> GetEnumerator()
    {
        yield return new object[] { double.NaN };
        yield return new object[] { double.PositiveInfinity };
        yield return new object[] { double.NegativeInfinity };
    }

    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
}