using Khartyko.InsigniaCreator.Library.Utility.Helpers;

namespace Khartyko.InsigniaCreator.Library.Data;

public partial class Vector3
{
	public static Vector3 operator -(Vector3 vector)
	{
		AssertionHelper.NullCheck(vector, nameof(vector));

		// Note: This might be changed in the future if the 'Z' value isn't used
		return new Vector3(-vector.X, -vector.Y, -vector.Z);
	}

	#region Addition

	public static Vector3 operator +(Vector3 left, Vector3 right)
	{
		AssertionHelper.NullCheck(left, nameof(left));
		AssertionHelper.NullCheck(right, nameof(right));

		return new Vector3(left.X + right.X, left.Y + right.Y, left.Z + right.Z);
	}

	public static Vector2 operator +(Vector2 left, Vector3 right)
	{
		AssertionHelper.NullCheck(left, nameof(left));
		AssertionHelper.NullCheck(right, nameof(right));

		return new Vector2(left.X + right.X, left.Y + right.Y);
	}

	public static Vector3 operator +(Vector3 left, Vector2 right)
	{
		AssertionHelper.NullCheck(left, nameof(left));
		AssertionHelper.NullCheck(right, nameof(right));

		return new Vector3(left.X + right.X, left.Y + right.Y, left.Z);
	}

	public static Vector3 operator +(Vector3 left, double right)
	{
		AssertionHelper.NullCheck(left, nameof(left));
		AssertionHelper.InvalidDoubleCheck(right, nameof(right));

		return new Vector3(left.X + right, left.Y + right, left.Z + right);
	}

	public static Vector3 operator +(double left, Vector3 right)
	{
		AssertionHelper.InvalidDoubleCheck(left, nameof(left));
		AssertionHelper.NullCheck(right, nameof(right));

		return new Vector3(left + right.X, left + right.Y, left + right.Z);
	}

	#endregion Addition

	#region Subtraction

	public static Vector3 operator -(Vector3 left, Vector3 right)
	{
		AssertionHelper.NullCheck(left, nameof(left));
		AssertionHelper.NullCheck(right, nameof(right));

		return new Vector3(left.X - right.X, left.Y - right.Y, left.Z - right.Z);
	}

	public static Vector2 operator -(Vector2 left, Vector3 right)
	{
		AssertionHelper.NullCheck(left, nameof(left));
		AssertionHelper.NullCheck(right, nameof(right));

		return new Vector2(left.X - right.X, left.Y - right.Y);
	}

	public static Vector3 operator -(Vector3 left, Vector2 right)
	{
		AssertionHelper.NullCheck(left, nameof(left));
		AssertionHelper.NullCheck(right, nameof(right));

		return new Vector3(left.X - right.X, left.Y - right.Y, left.Z);
	}

	public static Vector3 operator -(Vector3 left, double right)
	{
		AssertionHelper.NullCheck(left, nameof(left));
		AssertionHelper.InvalidDoubleCheck(right, nameof(right));

		return new Vector3(left.X - right, left.Y - right, left.Z - right);
	}

	public static Vector3 operator -(double left, Vector3 right)
	{
		AssertionHelper.InvalidDoubleCheck(left, nameof(left));
		AssertionHelper.NullCheck(right, nameof(right));

		return new Vector3(left - right.X, left - right.Y, left - right.Z);
	}

	#endregion Subtraction

	#region Multiplication

	public static Vector3 operator *(Vector3 left, Vector3 right)
	{
		AssertionHelper.NullCheck(left, nameof(left));
		AssertionHelper.NullCheck(right, nameof(right));

		return new Vector3(left.X * right.X, left.Y * right.Y, left.Z * right.Z);
	}

	public static Vector2 operator *(Vector2 left, Vector3 right)
	{
		AssertionHelper.NullCheck(left, nameof(left));
		AssertionHelper.NullCheck(right, nameof(right));

		return new Vector2(left.X * right.X, left.Y * right.Y);
	}

	public static Vector3 operator *(Vector3 left, Vector2 right)
	{
		AssertionHelper.NullCheck(left, nameof(left));
		AssertionHelper.NullCheck(right, nameof(right));

		return new Vector3(left.X * right.X, left.Y * right.Y, left.Z);
	}

	public static Vector3 operator *(Vector3 left, double right)
	{
		AssertionHelper.NullCheck(left, nameof(left));
		AssertionHelper.InvalidDoubleCheck(right, nameof(right));

		return new Vector3(left.X * right, left.Y * right, left.Z * right);
	}

	public static Vector3 operator *(double left, Vector3 right)
	{
		AssertionHelper.InvalidDoubleCheck(left, nameof(left));
		AssertionHelper.NullCheck(right, nameof(right));

		return new Vector3(left * right.X, left * right.Y, left * right.Z);
	}

	#endregion Multiplication

	#region Division

	public static Vector3 operator /(Vector3 left, Vector3 right)
	{
		AssertionHelper.NullCheck(left, nameof(left));
		AssertionHelper.NullCheck(right, nameof(right));

		return new Vector3(left.X / right.X, left.Y / right.Y, left.Z / right.Z);
	}

	public static Vector2 operator /(Vector2 left, Vector3 right)
	{
		AssertionHelper.NullCheck(left, nameof(left));
		AssertionHelper.NullCheck(right, nameof(right));

		return new Vector2(left.X / right.X, left.Y / right.Y);
	}

	public static Vector3 operator /(Vector3 left, Vector2 right)
	{
		AssertionHelper.NullCheck(left, nameof(left));
		AssertionHelper.NullCheck(right, nameof(right));

		return new Vector3(left.X / right.X, left.Y / right.Y, left.Z);
	}

	public static Vector3 operator /(Vector3 left, double right)
	{
		AssertionHelper.NullCheck(left, nameof(left));
		AssertionHelper.InvalidDoubleCheck(right, nameof(right));
		AssertionHelper.ZeroCheck(right, nameof(right));

		return new Vector3(left.X / right, left.Y / right, left.Z / right);
	}

	public static Vector3 operator /(double left, Vector3 right)
	{
		AssertionHelper.InvalidDoubleCheck(left, nameof(left));
		AssertionHelper.NullCheck(right, nameof(right));

		return MathHelper.Equals(left, 0.0)
			? Zero
			: new Vector3(left / right.X, left / right.Y, left / right.Z);
	}

	#endregion Division
}