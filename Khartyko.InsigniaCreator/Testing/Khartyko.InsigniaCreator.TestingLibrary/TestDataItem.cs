using System.Collections;

namespace Khartyko.InsigniaCreator.TestingLibrary;

public abstract class TestDataItem : IEnumerable<object[]>
{
    public IEnumerator<object[]> GetEnumerator()
    {
        return GetData().GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }

    public abstract IEnumerable<object[]> GetData();
}