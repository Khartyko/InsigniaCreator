using Khartyko.InsigniaCreator.Domain.Utility;
using Khartyko.InsigniaCreator.Library.Data;
using Khartyko.InsigniaCreator.Library.Entity;

#pragma warning disable CS8600, CS8604

namespace Khartyko.InsigniaCreator.Domain.Testing.Utility;

public class NodeHelperTests
{
	[Theory, ClassData(typeof(NodeHelperTestData))]
	public void Create_Succeeds(Transform transform, Vector2 offset, Vector2 expectedPosition)
	{
		Node node = NodeHelper.Create(transform, offset);
		
		Assert.Equal(expectedPosition, node.Position);
	}

	[Fact]
	public void Create_NullTransform_Fails()
	{
		Transform nullTransform = null;
		Vector2 offset = Vector2.Zero;

		Assert.Throws<ArgumentNullException>(() => NodeHelper.Create(nullTransform, offset));
	}

	[Fact]
	public void Create_NullOffset_Fails()
	{
		var transform = new Transform();
		Vector2 nullVector = null;

		Assert.Throws<ArgumentNullException>(() => NodeHelper.Create(transform, nullVector));
	}
}