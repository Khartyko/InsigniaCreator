using Khartyko.InsigniaCreator.Library.Utility.Helpers;

namespace Khartyko.InsigniaCreator.Library.Data;

public partial class Vector3
{
	/// <summary>
	/// Vector3 instance with 'X' and 'Y' set to 0.0 and 'Z' set to 1.0.
	/// </summary>
	public static readonly Vector3 Zero = new(0);
	
	/// <summary>
	/// Vector3 instance with all 3 values set to 1.0.
	/// </summary>
	public static readonly Vector3 One = new(1);

	/// <summary>
	/// Operator that negates a Vector2, making all 3 values change their sign.
	/// </summary>
	/// <param name="vector">The Vector2 to negate</param>
	/// <exception cref="ArgumentNullException">Can be thrown if 'vector' is null.</exception>
	/// <returns>A Vector3 that has negated values from the operand.</returns>
	public static Vector3 operator -(Vector3 vector)
	{
		AssertionHelper.NullCheck(vector, nameof(vector));

		// Note: This might be changed in the future if the 'Z' value isn't used
		return new Vector3(-vector.X, -vector.Y, -vector.Z);
	}

	#region Addition

	/// <summary>
	/// Operator that adds both values of a Vector3 to a double and returns the results as a Vector3.
	/// </summary>
	/// <remarks>
	/// This is functionally the same as Vector3 + double.
	/// </remarks>
	/// <param name="left">The first double operand.</param>
	/// <param name="right">The second Vector3 operand.</param>
	/// <exception cref="ArgumentException">If 'left' is either NaN, PositiveInfinity, or NegativeInfinity.</exception>
	/// <exception cref="ArgumentNullException">If 'right' is null.</exception>
	/// <returns>A Vector3 with the addition results from both operands.</returns>
	public static Vector3 operator +(double left, Vector3 right)
	{
		AssertionHelper.InvalidDoubleCheck(left, nameof(left));
		AssertionHelper.NullCheck(right, nameof(right));

		return new Vector3(left + right.X, left + right.Y, left + right.Z);
	}

	/// <summary>
	/// Operator that adds a double to both values of a Vector3 and returns the results as a Vector3.
	/// </summary>
	/// <remarks>
	/// This is functionally the same as double + Vector3.
	/// </remarks>
	/// <param name="left">The first Vector3 operand.</param>
	/// <param name="right">The second double operand.</param>
	/// <exception cref="ArgumentNullException">If 'left' is null</exception>
	/// <exception cref="ArgumentException">If 'right' is either NaN, PositiveInfinity, or NegativeInfinity.</exception>
	/// <returns>A Vector3 with the addition results from both operands.</returns>
	public static Vector3 operator +(Vector3 left, double right)
	{
		AssertionHelper.NullCheck(left, nameof(left));
		AssertionHelper.InvalidDoubleCheck(right, nameof(right));

		return new Vector3(left.X + right, left.Y + right, left.Z + right);
	}

	/// <summary>
	/// Operator that adds 2 Vector3 instances and returns the results as a Vector3.
	/// </summary>
	/// <param name="left">The first Vector3 operand.</param>
	/// <param name="right">The second Vector3 operand.</param>
	/// <exception cref="ArgumentNullException">Can be thrown if 'left' or 'right' is null.</exception>
	/// <returns>A Vector3 with the addition results from both operands.</returns>
	public static Vector3 operator +(Vector3 left, Vector3 right)
	{
		AssertionHelper.NullCheck(left, nameof(left));
		AssertionHelper.NullCheck(right, nameof(right));

		return new Vector3(left.X + right.X, left.Y + right.Y, left.Z + right.Z);
	}

	/// <summary>
	/// Operator that adds a Vector2 and a Vector3 and returns the results as a Vector2.
	/// </summary>
	/// <remarks>
	/// The 'Z'-component of 'right is ignored.
	/// </remarks>
	/// <param name="left">The first Vector2 operand.</param>
	/// <param name="right">The second Vector3 operand.</param>
	/// <exception cref="ArgumentNullException">Can be thrown if 'left' or 'right' is null.</exception>
	/// <returns>A Vector2 with the addition results from both operands.</returns>
	public static Vector2 operator +(Vector2 left, Vector3 right)
	{
		AssertionHelper.NullCheck(left, nameof(left));
		AssertionHelper.NullCheck(right, nameof(right));

		return new Vector2(left.X + right.X, left.Y + right.Y);
	}

	/// <summary>
	/// Operator that adds a Vector3 and Vector2 and returns the results as a Vector3.
	/// </summary>
	/// <param name="left">The first Vector3 operand.</param>
	/// <param name="right">The second Vector2 operand.</param>
	/// <exception cref="ArgumentNullException">Can be thrown if 'left' or 'right' is null.</exception>
	/// <returns>A Vector2 with the addition results from both operands.</returns>
	public static Vector3 operator +(Vector3 left, Vector2 right)
	{
		AssertionHelper.NullCheck(left, nameof(left));
		AssertionHelper.NullCheck(right, nameof(right));

		return new Vector3(left.X + right.X, left.Y + right.Y, left.Z);
	}

	#endregion Addition

	#region Subtraction

	/// <summary>
	/// Operator that subtracts both values of a Vector3 from a double and returns the results as a Vector3.
	/// </summary>
	/// <remarks>
	/// This will throw the following exceptions:
	/// - ArgumentException: If 'left' is either NaN, PositiveInfinity, or NegativeInfinity.
	/// - ArgumentNullException: If 'right' is null.
	/// </remarks>
	/// <param name="left">The first double operand.</param>
	/// <param name="right">The second Vector3 operand.</param>
	/// <exception cref="ArgumentException">If 'left' is either NaN, PositiveInfinity, or NegativeInfinity.</exception>
	/// <exception cref="ArgumentNullException">If 'right' is null</exception>
	/// <returns>A Vector3 with the subtraction results from both operands.</returns>
	public static Vector3 operator -(double left, Vector3 right)
	{
		AssertionHelper.InvalidDoubleCheck(left, nameof(left));
		AssertionHelper.NullCheck(right, nameof(right));

		return new Vector3(left - right.X, left - right.Y, left - right.Z);
	}

	/// <summary>
	/// Operator that subtracts a double from both values of a Vector3 and returns the results as a Vector3.
	/// </summary>
	/// <remarks>
	/// This will throw the following exceptions:
	/// - ArgumentNullException: If 'left' is null.
	/// - ArgumentException: If 'right' is either NaN, PositiveInfinity, or NegativeInfinity.
	/// </remarks>
	/// <param name="left">The first Vector3 operand.</param>
	/// <param name="right">The second double operand.</param>
	/// <exception cref="ArgumentNullException">If 'left' is null</exception>
	/// <exception cref="ArgumentException">If 'right' is either NaN, PositiveInfinity, or NegativeInfinity.</exception>
	/// <returns>A Vector3 with the subtraction results from both operands.</returns>
	public static Vector3 operator -(Vector3 left, double right)
	{
		AssertionHelper.NullCheck(left, nameof(left));
		AssertionHelper.InvalidDoubleCheck(right, nameof(right));

		return new Vector3(left.X - right, left.Y - right, left.Z - right);
	}

	/// <summary>
	/// Operator that subtracts one Vector3 instance from another and returns the results as a Vector3.
	/// </summary>
	/// <param name="left">The first Vector3 operand.</param>
	/// <param name="right">The second Vector3 operand.</param>
	/// <exception cref="ArgumentNullException">Can be thrown if 'left' or 'right' is null.</exception>
	/// <returns>A Vector3 with the subtraction results from both operands.</returns>
	public static Vector3 operator -(Vector3 left, Vector3 right)
	{
		AssertionHelper.NullCheck(left, nameof(left));
		AssertionHelper.NullCheck(right, nameof(right));

		return new Vector3(left.X - right.X, left.Y - right.Y, left.Z - right.Z);
	}

	/// <summary>
	/// Operator that subtracts one Vector3 instance from a Vector2 instance and returns the results as a Vector2.
	/// </summary>
	/// <remarks>
	/// The 'Z' component of 'right' is ignored.
	/// </remarks>
	/// <param name="left">The first Vector2 operand.</param>
	/// <param name="right">The second Vector3 operand.</param>
	/// <exception cref="ArgumentNullException">Can be thrown if 'left' or 'right' is null.</exception>
	/// <returns>A Vector2 with the subtraction results from both operands.</returns>
	public static Vector2 operator -(Vector2 left, Vector3 right)
	{
		AssertionHelper.NullCheck(left, nameof(left));
		AssertionHelper.NullCheck(right, nameof(right));

		return new Vector2(left.X - right.X, left.Y - right.Y);
	}

	/// <summary>
	/// Operator that subtracts one Vector2 instance from a Vector3 instance and returns the results as a Vector3.
	/// </summary>
	/// <param name="left">The first Vector3 operand.</param>
	/// <param name="right">The second Vector2 operand.</param>
	/// <exception cref="ArgumentNullException">Can be thrown if 'left' or 'right' is null.</exception>
	/// <returns>A Vector3 with the subtraction results from both operands.</returns>
	public static Vector3 operator -(Vector3 left, Vector2 right)
	{
		AssertionHelper.NullCheck(left, nameof(left));
		AssertionHelper.NullCheck(right, nameof(right));

		return new Vector3(left.X - right.X, left.Y - right.Y, left.Z);
	}

	#endregion Subtraction

	#region Multiplication

	/// <summary>
	/// Operator that multiplies both values of a Vector3 to a double and returns the results as a Vector3.
	/// </summary>
	/// <remarks>
	/// This is functionally the same as Vector3 * double.
	/// </remarks>
	/// <param name="left">The first double operand.</param>
	/// <param name="right">The second Vector3 operand.</param>
	/// <exception cref="ArgumentException">If 'left' is either NaN, PositiveInfinity, or NegativeInfinity.</exception>
	/// <exception cref="ArgumentNullException">If 'right' is null</exception>
	/// <returns>A Vector3 with the multiplication results from both operands.</returns>
	public static Vector3 operator *(double left, Vector3 right)
	{
		AssertionHelper.InvalidDoubleCheck(left, nameof(left));
		AssertionHelper.NullCheck(right, nameof(right));

		return new Vector3(left * right.X, left * right.Y, left * right.Z);
	}

	/// <summary>
	/// Operator that multiplies a double to both values of a Vector3 and returns the results as a Vector3.
	/// </summary>
	/// <remarks>
	/// This is functionally the same as Vector3 * double.
	/// </remarks>
	/// <param name="left">The first Vector3 operand.</param>
	/// <param name="right">The second double operand.</param>
	/// <exception cref="ArgumentNullException">If 'left' is null</exception>
	/// <exception cref="ArgumentException">If 'right' is either NaN, PositiveInfinity, or NegativeInfinity.</exception>
	/// <returns>A Vector3 with the multiplication results from both operands.</returns>
	public static Vector3 operator *(Vector3 left, double right)
	{
		AssertionHelper.NullCheck(left, nameof(left));
		AssertionHelper.InvalidDoubleCheck(right, nameof(right));

		return new Vector3(left.X * right, left.Y * right, left.Z * right);
	}

	/// <summary>
	/// Operator that multiplies 2 Vector3 instances and returns the results as a Vector3.
	/// </summary>
	/// <param name="left">The first Vector3 operand.</param>
	/// <param name="right">The second Vector3 operand.</param>
	/// <exception cref="ArgumentNullException">Can be thrown if 'left' or 'right' is null.</exception>
	/// <returns>A Vector3 with the multiplication results from both operands.</returns>
	public static Vector3 operator *(Vector3 left, Vector3 right)
	{
		AssertionHelper.NullCheck(left, nameof(left));
		AssertionHelper.NullCheck(right, nameof(right));

		return new Vector3(left.X * right.X, left.Y * right.Y, left.Z * right.Z);
	}

	/// <summary>
	/// Operator that multiplies a Vector2 instance to a Vector3 instance and returns the results as a Vector2.
	/// </summary>
	/// <remarks>
	/// The 'Z'-component of 'right' is ignored.
	/// </remarks>
	/// <param name="left">The first Vector2 operand.</param>
	/// <param name="right">The second Vector2 operand.</param>
	/// <exception cref="ArgumentNullException">Can be thrown if 'left' or 'right' is null.</exception>
	/// <returns>A Vector2 with the multiplication results from both operands.</returns>
	public static Vector2 operator *(Vector2 left, Vector3 right)
	{
		AssertionHelper.NullCheck(left, nameof(left));
		AssertionHelper.NullCheck(right, nameof(right));

		return new Vector2(left.X * right.X, left.Y * right.Y);
	}

	/// <summary>
	/// Operator that multiplies a Vector3 instance to a Vector2 instance and returns the results as a Vector3.
	/// </summary>
	/// <param name="left">The first Vector2 operand.</param>
	/// <param name="right">The second Vector2 operand.</param>
	/// <exception cref="ArgumentNullException">Can be thrown if 'left' or 'right' is null.</exception>
	/// <returns>A Vector3 with the multiplication results from both operands.</returns>
	public static Vector3 operator *(Vector3 left, Vector2 right)
	{
		AssertionHelper.NullCheck(left, nameof(left));
		AssertionHelper.NullCheck(right, nameof(right));

		return new Vector3(left.X * right.X, left.Y * right.Y, left.Z);
	}

	#endregion Multiplication

	#region Division

	/// <summary>
	/// Operator that divides a double by both values of a Vector3 and returns the results as a Vector3.
	/// </summary>
	/// <param name="left">The first double operand.</param>
	/// <param name="right">The second Vector3 operand.</param>
	/// <exception cref="ArgumentException">If 'left' is either NaN, PositiveInfinity, or NegativeInfinity.</exception>
	/// <exception cref="ArgumentNullException">If 'right' is null.</exception>
	/// <returns>A Vector3 with the division results from both operands.</returns>
	public static Vector3 operator /(double left, Vector3 right)
	{
		AssertionHelper.InvalidDoubleCheck(left, nameof(left));
		AssertionHelper.NullCheck(right, nameof(right));

		return MathHelper.Equals(left, 0.0)
			? Zero
			: new Vector3(left / right.X, left / right.Y, left / right.Z);
	}

	/// <summary>
	/// Operator that divides both values of a Vector3 by a double and returns the results as a Vector3.
	/// </summary>
	/// <remarks>
	/// If the double value is '0.0', Vector3.Zero will be returned, since dividing by 0.0 is invalid.
	/// </remarks>
	/// <param name="left">The first Vector3 operand.</param>
	/// <param name="right">The second double operand.</param>
	/// <exception cref="ArgumentNullException">If 'left' is null</exception>
	/// <exception cref="ArgumentException">If 'right' is either NaN, PositiveInfinity, or NegativeInfinity.</exception>
	/// <returns>A Vector3 with the division results from both operands.</returns>
	public static Vector3 operator /(Vector3 left, double right)
	{
		AssertionHelper.NullCheck(left, nameof(left));
		AssertionHelper.InvalidDoubleCheck(right, nameof(right));
		AssertionHelper.ZeroCheck(right, nameof(right));

		return new Vector3(left.X / right, left.Y / right, left.Z / right);
	}

	/// <summary>
	/// Operator that divides one Vector3 instance from another and returns the results as a Vector3.
	/// </summary>
	/// <param name="left">The first Vector3 operand.</param>
	/// <param name="right">The second Vector3 operand.</param>
	/// <exception cref="ArgumentNullException">Can be thrown if 'left' or 'right' is null.</exception>
	/// <returns>A Vector3 with the division results from both operands.</returns>
	public static Vector3 operator /(Vector3 left, Vector3 right)
	{
		AssertionHelper.NullCheck(left, nameof(left));
		AssertionHelper.NullCheck(right, nameof(right));

		return new Vector3(left.X / right.X, left.Y / right.Y, left.Z / right.Z);
	}

	/// <summary>
	/// Operator that divides one Vector2 instance from a Vector3 instance and returns the results as a Vector2.
	/// </summary>
	/// <remarks>
	/// The 'Z'-component of 'right' is ignored.
	/// </remarks>
	/// <param name="left">The first Vector2 operand.</param>
	/// <param name="right">The second Vector3 operand.</param>
	/// <exception cref="ArgumentNullException">Can be thrown if 'left' or 'right' is null.</exception>
	/// <returns>A Vector2 with the division results from both operands.</returns>
	public static Vector2 operator /(Vector2 left, Vector3 right)
	{
		AssertionHelper.NullCheck(left, nameof(left));
		AssertionHelper.NullCheck(right, nameof(right));

		return new Vector2(left.X / right.X, left.Y / right.Y);
	}

	/// <summary>
	/// Operator that divides one Vector3 instance from a Vector2 instance and returns the results as a Vector3.
	/// </summary>
	/// <param name="left">The first Vector3 operand.</param>
	/// <param name="right">The second Vector2 operand.</param>
	/// <exception cref="ArgumentNullException">Can be thrown if 'left' or 'right' is null.</exception>
	/// <returns>A Vector3 with the division results from both operands.</returns>
	public static Vector3 operator /(Vector3 left, Vector2 right)
	{
		AssertionHelper.NullCheck(left, nameof(left));
		AssertionHelper.NullCheck(right, nameof(right));

		return new Vector3(left.X / right.X, left.Y / right.Y, left.Z);
	}

	#endregion Division
}