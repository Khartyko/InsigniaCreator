/** \addtogroup Library
 * @{
 */
using Khartyko.InsigniaCreator.Library.Utility.Helpers;

#pragma warning disable CS0660, CS0661, CS0659

namespace Khartyko.InsigniaCreator.Library.Data;

public partial class Vector2
{
    private double _x;
    private double _y;

    /// <summary>
    /// Accesses the first value of a Vector2 instance.
    /// </summary>
    /// <remarks>
    /// This will throw an 'ArgumentException' if an attempt to assign it to NaN, PositiveInfinity, or NegativeInfinity.
    /// </remarks>
    public double X
    {
        get => _x;
        set
        {
            AssertionHelper.InvalidDoubleCheck(value, nameof(value));
            
            _x = value;
        }
    }

    /// <summary>
    /// Accesses the second value of a Vector2 instance.
    /// </summary>
    /// <remarks>
    /// This will throw an 'ArgumentException' if an attempt to assign it to NaN, PositiveInfinity, or NegativeInfinity.
    /// </remarks>
    public double Y
    {
        get => _y;
        set
        {
            AssertionHelper.InvalidDoubleCheck(value, nameof(value));
            
            _y = value;
        }
    }

    /// <summary>
    /// Calculates the length of a vector given the current X and Y values.
    /// </summary>
    public double Length => MathHelper.Sqrt(X * X + Y * Y);

    /// <summary>
    /// Gets a double using the index operator.
    /// </summary>
    /// <remarks>
    /// The following exceptions can be thrown:
    /// - ArgumentOutOfRangeException: If the index used is not 0 or 1.
    /// - ArgumentException: If the value is NaN, PositiveInfinity, or NegativeInfinity.
    /// </remarks>
    /// <param name="idx">The index of the value</param>
    public double this[int idx]
    {
        get
        {
            AssertionHelper.RangeCheck(idx, -1, 2, nameof(idx));

            return idx == 0 ? X : Y;
        }

        set
        {
            AssertionHelper.RangeCheck(idx, -1, 2, nameof(idx));
            AssertionHelper.InvalidDoubleCheck(value, nameof(value));

            if (idx == 0)
            {
                _x = value;
            }
            else
            {
                _y = value;
            }
        }
    }

    /// <summary>
    /// Creates a Vector2 instance with both values being the double value passed in.
    /// </summary>
    /// <remarks>
    /// This can throw an 'ArgumentException' if the value is NaN, PositiveInfinity, or NegativeInfinity.
    /// </remarks>
    /// <param name="value">The value of both 'X' and 'Y'.</param>
    public Vector2(double value)
        : this(value, value)
    {
    }

    /// <summary>
    /// Creates a Vector2 instance with 2 double values.
    /// </summary>
    /// <remarks>
    /// This can throw an 'ArgumentException' if either value is NaN, PositiveInfinity, or NegativeInfinity.
    /// </remarks>
    /// <param name="x">The 'X' value.</param>
    /// <param name="y">The 'Y' value.</param>
    public Vector2(double x, double y)
    {
        AssertionHelper.InvalidDoubleCheck(x, nameof(x));
        AssertionHelper.InvalidDoubleCheck(y, nameof(y));

        _x = x;
        _y = y;
    }

    /// <summary>
    /// Copies the values of one Vector2 to another.
    /// </summary>
    /// <remarks>
    /// This will throw an 'ArgumentNullException' if 'vec2' is null.
    /// </remarks>
    /// <param name="vec2">The existing Vector2 to duplicate.</param>
    public Vector2(Vector2 vec2)
    {
        AssertionHelper.NullCheck(vec2, nameof(vec2));

        _x = vec2.X;
        _y = vec2.Y;
    }

    /// <summary>
    /// Copies the values of one Vector3 to another.
    /// </summary>
    /// <remarks>
    /// The 'Z' value of the Vector3 is ignored.
    /// This will throw an 'ArgumentNullException' if 'vec3' is null.
    /// </remarks>
    /// <param name="vec2">The existing Vector3 to duplicate.</param>
    public Vector2(Vector3 vec3)
    {
        AssertionHelper.NullCheck(vec3, nameof(vec3));

        _x = vec3.X;
        _y = vec3.Y;
    }

    /// <summary>
    /// Override that compares the 'X' and 'Y' values of 2 Vector2s.
    /// </summary>
    /// <remarks>
    /// The following outcomes are possible:
    /// - If 'obj' is null, it'll return false.
    /// - If 'obj' is this, it'll return true.
    /// - Otherwise, the values are compared outright.
    /// </remarks>
    /// <param name="obj">The object to compare to this Vector2 instance.</param>
    /// <returns>A boolean value if the object is equal to this Vector2 instance.</returns>
    public override bool Equals(object? obj)
    {
        if (ReferenceEquals(null, obj)) 
        {
            return false;
        }
        
        if (ReferenceEquals(this, obj)) 
        {
            return true;
        }
        
        return obj is Vector2 target
               && MathHelper.Equals(X, target.X)
               && MathHelper.Equals(Y, target.Y);
    }

    /// <summary>
    /// An override of ToString() that creates a string.
    /// </summary>
    /// <returns>A string containing the 'X' and 'Y' values.</returns>
    public override string ToString() => $"{{ x: {X}, y: {Y} }}";
}
/** @} */