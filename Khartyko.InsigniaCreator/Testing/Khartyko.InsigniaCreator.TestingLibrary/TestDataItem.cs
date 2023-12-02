using System.Collections;

namespace Khartyko.InsigniaCreator.TestingLibrary;

public abstract class TestDataItem : IEnumerable<object[]>
{
	public IEnumerator<object[]> GetEnumerator() => GetData().GetEnumerator();

	IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

	public abstract IEnumerable<object[]> GetData();
}