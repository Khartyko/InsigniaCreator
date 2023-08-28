/** \addtogroup Library
 * @{
 */
using Khartyko.InsigniaCreator.Library.Utility.Helpers;

#pragma warning disable CS0660, CS0661, CS0659

namespace Khartyko.InsigniaCreator.Library.Data;

/// <summary>
/// Class that represents 2 double values used in vector maths.
/// </summary>
public partial class Vector2
{
    private double _x;
    private double _y;

    /// <summary>
    /// Accesses the first value of a Vector2 instance.
    /// </summary>
    /// <exception cref="ArgumentException">Can be thrown if an attempt to assign it to NaN, PositiveInfinity, or NegativeInfinity.</exception>
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
    /// <exception cref="ArgumentException">Can be thrown if an attempt to assign it to NaN, PositiveInfinity, or NegativeInfinity.</exception>
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
    /// <param name="idx">The index of the value</param>
    /// <exception cref="ArgumentOutOfRangeException">'index' is neither 0 or 1.</exception>
    /// <exception cref="ArgumentException">Can be thrown if 'value' is NaN, PositiveInfinity, or NegativeInfinity.</exception>
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
    /// Constructs a Vector2 instance with both values being the double value passed in.
    /// </summary>
    /// <param name="value">The value of both 'X' and 'Y' components.</param>
    /// <exception cref="ArgumentException">Can be thrown if 'value' is NaN, PositiveInfinity, or NegativeInfinity.</exception>
    public Vector2(double value)
        : this(value, value)
    {
    }

    /// <summary>
    /// Constructs a Vector2 instance with 2 double values.
    /// </summary>
    /// <param name="x">The 'X' value.</param>
    /// <param name="y">The 'Y' value.</param>
    /// <exception cref="ArgumentException">Can be thrown if 'x' or 'y' is NaN, PositiveInfinity, or NegativeInfinity.</exception>
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
    /// <param name="vec2">The existing Vector2 to duplicate.</param>
    /// <exception cref="ArgumentNullException">Can be thrown if 'vec2' is null.</exception>
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
    /// </remarks>
    /// <param name="vec3">The existing Vector3 to duplicate.</param>
    /// <exception cref="ArgumentNullException">Can be thrown if 'vec3' is null.</exception>
    public Vector2(Vector3 vec3)
    {
        AssertionHelper.NullCheck(vec3, nameof(vec3));

        _x = vec3.X;
        _y = vec3.Y;
    }

    /// <summary>
    /// Override that compares the 'X' and 'Y' values of 2 Vector2's.
    /// </summary>
    /// <remarks>
    /// If the object is null, it'll return false.
    /// If the object is 'this', it'll return true.
    /// Otherwise, the values are compared outright.
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
    /// An override of ToString() that Constructs a string.
    /// </summary>
    /// <returns>A string containing the 'X' and 'Y' values.</returns>
    public override string ToString() => $"{{ x: {X}, y: {Y} }}";
}
/** @} */