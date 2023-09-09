using Khartyko.InsigniaCreator.Library.Data;
using Khartyko.InsigniaCreator.Library.Entity;
using Khartyko.InsigniaCreator.Library.Utility.Helpers;

namespace Khartyko.InsigniaCreator.Domain.Utility;

public static class NodeHelper
{
	public static Node Create(Transform transform, Vector2 offset)
	{
		AssertionHelper.NullCheck(transform, nameof(transform));
		AssertionHelper.NullCheck(offset, nameof(offset));
		
		Vector2 position = offset * transform.Matrix;

		return new Node(
			new Vector2(
				MathHelper.Round(position.X),
				MathHelper.Round(position.Y)
			)
		);
	}
}