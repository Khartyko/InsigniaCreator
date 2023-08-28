using Khartyko.InsigniaCreator.Library.Utility.Helpers;

namespace Khartyko.InsigniaCreator.Library.Data;

/// <summary>
/// Section of Vector2 that holds the static methods.
/// </summary>
public partial class Vector2
{
    /// <summary>
    /// Vector2 with 'X' and 'Y' values of 0.0.
    /// </summary>
    public static readonly Vector2 Zero = new(0, 0);
    /// <summary>
    /// Vector2 with 'X' and 'Y' values of 1.0.
    /// </summary>
    public static readonly Vector2 One = new(1, 1);

    /// <summary>
    /// Operator that negates a Vector2, making both values change their sign
    /// </summary>
    /// <remarks>
    /// This will throw an 'ArgumentNullException' if 'vector' is null
    /// </remarks>
    /// <param name="vector">The Vector2 to negate</param>
    /// <returns>A Vector2 that has negated values from the operand.</returns>
    public static Vector2 operator -(Vector2 vector)
    {
        AssertionHelper.NullCheck(vector, nameof(vector));

        return new Vector2(-vector.X, -vector.Y);
    }

    #region Addition

    /// <summary>
    /// Operator that adds both values of a Vector2 to a double and returns the results as a Vector2.
    /// </summary>
    /// <remarks>
    /// This will throw the following exceptions:
    /// - ArgumentException: If 'left' is either NaN, PositiveInfinity, or NegativeInfinity.
    /// - ArgumentNullException: If 'right' is null.
    ///
    /// This is functionally the same as Vector2 + double.
    /// </remarks>
    /// <param name="left">The first double operand.</param>
    /// <param name="right">The second Vector2 operand.</param>
    /// <returns>A Vector2 with the addition results from both operands.</returns>
    public static Vector2 operator +(double left, Vector2 right)
    {
        AssertionHelper.InvalidDoubleCheck(left, nameof(left));
        AssertionHelper.NullCheck(right, nameof(right));

        return new Vector2(left + right.X, left + right.Y);
    }

    /// <summary>
    /// Operator that adds a double to both values of a Vector2 and returns the results as a Vector2.
    /// </summary>
    /// <remarks>
    /// This will throw the following exceptions:
    /// - ArgumentNullException: If 'left' is null
    /// - ArgumentException: If 'right' is either NaN, PositiveInfinity, or NegativeInfinity
    ///
    /// This is functionally the same as double + Vector2.
    /// </remarks>
    /// <param name="left">The first Vector2 operand.</param>
    /// <param name="right">The second double operand.</param>
    /// <returns>A Vector2 with the addition results from both operands.</returns>
    public static Vector2 operator +(Vector2 left, double right)
    {
        AssertionHelper.NullCheck(left, nameof(left));
        AssertionHelper.InvalidDoubleCheck(right, nameof(right));

        return new Vector2(left.X + right, left.Y + right);
    }

    /// <summary>
    /// Operator that adds 2 Vector2 instances and returns the results as a Vector2.
    /// </summary>
    /// <remarks>
    /// This will throw an 'ArgumentNullException' if 'left' or 'right' is null.
    /// </remarks>
    /// <param name="left">The first Vector2 operand.</param>
    /// <param name="right">The second Vector2 operand.</param>
    /// <returns>A Vector2 with the addition results from both operands.</returns>
    public static Vector2 operator +(Vector2 left, Vector2 right)
    {
        AssertionHelper.NullCheck(left, nameof(left));
        AssertionHelper.NullCheck(right, nameof(right));

        return new Vector2(left.X + right.X, left.Y + right.Y);
    }

    #endregion Addition

    #region Subtraction

    /// <summary>
    /// Operator that subtracts both values of a Vector2 from a double and returns the results as a Vector2.
    /// </summary>
    /// <remarks>
    /// This will throw the following exceptions:
    /// - ArgumentException: If 'left' is either NaN, PositiveInfinity, or NegativeInfinity.
    /// - ArgumentNullException: If 'right' is null.
    ///
    /// This is functionally the same as 
    /// </remarks>
    /// <param name="left">The first double operand.</param>
    /// <param name="right">The second Vector2 operand.</param>
    /// <returns>A Vector2 with the subtraction results from both operands.</returns>
    public static Vector2 operator -(double left, Vector2 right)
    {
        AssertionHelper.InvalidDoubleCheck(left, nameof(left));
        AssertionHelper.NullCheck(right, nameof(right));

        return new Vector2(left - right.X, left - right.Y);
    }

    /// <summary>
    /// Operator that subtracts a double from both values of a Vector2 and returns the results as a Vector2.
    /// </summary>
    /// <remarks>
    /// This will throw the following exceptions:
    /// - ArgumentNullException: If 'left' is null.
    /// - ArgumentException: If 'right' is either NaN, PositiveInfinity, or NegativeInfinity.
    /// </remarks>
    /// <param name="left">The first Vector2 operand.</param>
    /// <param name="right">The second double operand.</param>
    /// <returns>A Vector2 with the subtraction results from both operands.</returns>
    public static Vector2 operator -(Vector2 left, double right)
    {
        AssertionHelper.NullCheck(left, nameof(left));
        AssertionHelper.InvalidDoubleCheck(right, nameof(right));

        return new Vector2(left.X - right, left.Y - right);
    }

    /// <summary>
    /// Operator that subtracts one Vector2 instance from another and returns the results as a Vector2.
    /// </summary>
    /// <remarks>
    /// This will throw an 'ArgumentNullException' if 'left' or 'right' is null.
    /// </remarks>
    /// <param name="left">The first Vector2 operand.</param>
    /// <param name="right">The second Vector2 operand.</param>
    /// <returns>A Vector2 with the subtraction results from both operands.</returns>
    public static Vector2 operator -(Vector2 left, Vector2 right)
    {
        AssertionHelper.NullCheck(left, nameof(left));
        AssertionHelper.NullCheck(right, nameof(right));

        return new Vector2(left.X - right.X, left.Y - right.Y);
    }

    #endregion Subtraction

    #region Multiplication

    /// <summary>
    /// Operator that multiplies both values of a Vector2 to a double and returns the results as a Vector2.
    /// </summary>
    /// <remarks>
    /// This will throw the following exceptions:
    /// - ArgumentException: If 'value' is either NaN, PositiveInfinity, or NegativeInfinity.
    /// - ArgumentNullException: If 'vector' is null.
    ///
    /// This is functionally the same as Vector2 * double.
    /// </remarks>
    /// <param name="left">The first double operand.</param>
    /// <param name="right">The second Vector2 operand.</param>
    /// <returns>A Vector2 with the multiplication results from both operands.</returns>
    public static Vector2 operator *(double left, Vector2 right)
    {
        AssertionHelper.InvalidDoubleCheck(left, nameof(left));
        AssertionHelper.NullCheck(right, nameof(right));

        return new Vector2(left * right.X, left * right.Y);
    }

    /// <summary>
    /// Operator that multiplies a double to both values of a Vector2 and returns the results as a Vector2.
    /// </summary>
    /// <remarks>
    /// This will throw the following exceptions:
    /// - ArgumentNullException: If 'left' is null.
    /// - ArgumentException: If 'right' is either NaN, PositiveInfinity, or NegativeInfinity.
    ///
    /// This is functionally the same as Vector2 * double.
    /// </remarks>
    /// <param name="left">The first Vector2 operand.</param>
    /// <param name="right">The second double operand.</param>
    /// <returns>A Vector2 with the multiplication results from both operands.</returns>
    public static Vector2 operator *(Vector2 left, double right)
    {
        AssertionHelper.NullCheck(left, nameof(left));
        AssertionHelper.InvalidDoubleCheck(right, nameof(right));

        return new Vector2(left.X * right, left.Y * right);
    }

    /// <summary>
    /// Operator that multiplies 2 Vector2 instances and returns the results as a Vector2.
    /// </summary>
    /// <remarks>
    /// This will throw an 'ArgumentNullException' if 'left' or 'right' is null.
    /// </remarks>
    /// <param name="left">The first Vector2 operand.</param>
    /// <param name="right">The second Vector2 operand.</param>
    /// <returns>A Vector2 with the multiplication results from both operands.</returns>
    public static Vector2 operator *(Vector2 left, Vector2 right)
    {
        AssertionHelper.NullCheck(left, nameof(left));
        AssertionHelper.NullCheck(right, nameof(right));

        return new Vector2(left.X * right.X, left.Y * right.Y);
    }

    #endregion Multiplication

    #region Division

    /// <summary>
    /// Operator that divides a double by both values of a Vector2 and returns the results as a Vector2.
    /// </summary>
    /// <remarks>
    /// This will throw the following exceptions:
    /// - ArgumentException: If 'left' is either NaN, PositiveInfinity, or NegativeInfinity.
    /// - ArgumentNullException: If 'right' is null.
    /// </remarks>
    /// <param name="left">The first double operand.</param>
    /// <param name="right">The second Vector2 operand.</param>
    /// <returns>A Vector2 with the division results from both operands.</returns>
    public static Vector2 operator /(double left, Vector2 right)
    {
        AssertionHelper.InvalidDoubleCheck(left, nameof(left));
        AssertionHelper.NullCheck(right, nameof(right));

        return new Vector2(left / right.X, left / right.Y);
    }

    /// <summary>
    /// Operator that divides both values of a Vector2 by a double and returns the results as a Vector2.
    /// </summary>
    /// <remarks>
    /// This will throw the following exceptions:
    /// - ArgumentNullException: If 'left' is null.
    /// - ArgumentException: If 'right' is either NaN, PositiveInfinity, or NegativeInfinity.
    /// </remarks>
    /// <param name="left">The first Vector2 operand.</param>
    /// <param name="right">The second double operand.</param>
    /// <returns>A Vector2 with the division results from both operands.</returns>
    public static Vector2 operator /(Vector2 left, double right)
    {
        AssertionHelper.NullCheck(left, nameof(left));
        AssertionHelper.InvalidDoubleCheck(right, nameof(right));

        return new Vector2(left.X / right, left.Y / right);
    }

    /// <summary>
    /// Operator that divides one Vector2 instance from another and returns the results as a Vector2.
    /// </summary>
    /// <remarks>
    /// This will throw an 'ArgumentNullException' if 'left' or 'right' is null.
    /// </remarks>
    /// <param name="left">The first Vector2 operand.</param>
    /// <param name="right">The second Vector2 operand.</param>
    /// <returns>A Vector2 with the division results from both operands.</returns>
    public static Vector2 operator /(Vector2 left, Vector2 right)
    {
        AssertionHelper.NullCheck(left, nameof(left));
        AssertionHelper.NullCheck(right, nameof(right));

        return new Vector2(left.X / right.X, left.Y / right.Y);
    }

    #endregion Division
}