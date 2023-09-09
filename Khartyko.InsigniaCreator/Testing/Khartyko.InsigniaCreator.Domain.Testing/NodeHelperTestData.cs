using Khartyko.InsigniaCreator.Library.Data;
using Khartyko.InsigniaCreator.TestingLibrary;
namespace Khartyko.InsigniaCreator.Domain.Testing;

public class NodeHelperTestData : TestDataItem
{
	public override IEnumerable<object[]> GetData()
	{
		yield return new object[]
		{
			new Transform(),
			Vector2.Zero,
			Vector2.Zero
		};
		
		yield return new object[]
		{
			new Transform(),
			Vector2.One,
			Vector2.One
		};
		
		yield return new object[]
		{
			new Transform(
				new Vector2(2),
				0,
				Vector2.Zero
			),
			Vector2.One,
			new Vector2(2)
		};
		
		yield return new object[]
		{
			new Transform(
				Vector2.One,
				0,
				Vector2.One
			),
			Vector2.One,
			new Vector2(2)
		};
		
		yield return new object[]
		{
			new Transform(
				new Vector2(2, 1),
				0,
				 new Vector2(3, 4)
			),
			new Vector2(5, 6),
			new Vector2(13, 10)
		};
	}
}