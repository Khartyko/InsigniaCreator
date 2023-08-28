/** \addtogroup Library
 * @{
 */
using Khartyko.InsigniaCreator.Library.Utility.Helpers;
// ReSharper disable All

#pragma warning disable CS0660, CS0661, CS0659

namespace Khartyko.InsigniaCreator.Library.Data;

/// <summary>
/// Represents a vector with an 'X', 'Y', and 'Z' component.
/// </summary>
public partial class Vector3
{
	private double _x;
	private double _y;
	private double _z;

	/// <summary>
	/// Gets or Sets the X-component
	/// </summary>
	/// <exception cref="ArgumentException">Can be thrown if 'value' is NaN, PositiveInfinity, or NegativeInfinity.</exception>
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
	/// Gets or Sets the Y-component
	/// </summary>
	/// <exception cref="ArgumentException">Can be thrown if 'value' is NaN, PositiveInfinity, or NegativeInfinity.</exception>
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
	/// Gets or Sets the Z-component
	/// </summary>
	/// <exception cref="ArgumentException">Can be thrown if 'value' is NaN, PositiveInfinity, or NegativeInfinity.</exception>
	public double Z
	{
		get => _z;
		set
		{
			AssertionHelper.InvalidDoubleCheck(value, nameof(value));

			_z = value;
		}
	}

	#region Swizzling

	// ReSharper disable InconsistentNaming
	
	/// <summary>
	/// Creates a Vector2 from the 'X' and 'Y' values. 
	/// </summary>
	public Vector2 XY => new(X, Y);
	
	/// <summary>
	/// Creates a Vector2 from the 'X' and 'Z' values. 
	/// </summary>
	public Vector2 XZ => new(X, Z);
	
	/// <summary>
	/// Creates a Vector2 from the 'Y' and 'X' values. 
	/// </summary>
	public Vector2 YX => new(Y, X);
	
	/// <summary>
	/// Creates a Vector2 from the 'Y' and 'Z' values. 
	/// </summary>
	public Vector2 YZ => new(Y, Z);
	
	/// <summary>
	/// Creates a Vector2 from the 'Z' and 'X' values. 
	/// </summary>
	public Vector2 ZX => new(Z, X);
	
	/// <summary>
	/// Creates a Vector2 from the 'Z' and 'Y' values. 
	/// </summary>
	public Vector2 ZY => new(Z, Y);
	
	/// <summary>
	/// Creates a Vector3 from the 'X', 'Y', and 'Z' values.
	/// </summary>
	public Vector3 XYZ => new(X, Y, Z);
	
	/// <summary>
	/// Creates a Vector3 from the 'Z', 'Y', and 'X' values.
	/// </summary>
	public Vector3 ZYX => new(Z, Y, X);
	
	/// <summary>
	/// Creates a Vector3 from the 'Y', 'X', and 'Z' values.
	/// </summary>
	public Vector3 YXZ => new(Y, X, Z);
	
	/// <summary>
	/// Creates a Vector3 from the 'Z', 'X', and 'Y' values.
	/// </summary>
	public Vector3 ZXY => new(Z, X, Y);
	
	/// <summary>
	/// Creates a Vector3 from the 'X', 'Z', and 'Y' values.
	/// </summary>
	public Vector3 XZY => new(X, Z, Y);
	
	/// <summary>
	/// Creates a Vector3 from the 'Y', 'Z', and 'X' values.
	/// </summary>
	public Vector3 YZX => new(Y, Z, X);
	
	// ReSharper restore InconsistentNaming

	#endregion

	/// <summary>
	/// Calculates the length of a vector given the current X and Y values.
	/// </summary>
	public double Length
	{
		get => MathHelper.Round(MathHelper.Sqrt(X * X + Y * Y + Z * Z));
	}

	/// <summary>
	/// Gets a double using the index operator.
	/// </summary>
	/// <param name="idx">The index of the value</param>
	/// <exception cref="ArgumentOutOfRangeException">'index' is neither 0, 1, or 2.</exception>
	/// <exception cref="ArgumentException">Can be thrown if 'value' is NaN, PositiveInfinity, or NegativeInfinity.</exception>
	public double this[int idx]
	{
		get
		{
            AssertionHelper.RangeCheck(idx, -1, 3, nameof(idx));

            return idx switch
            {
	            0 => X,
	            1 => Y,
	            _ => Z
            };
		}

		set
		{
			AssertionHelper.RangeCheck(idx, -1, 3, nameof(idx));
			AssertionHelper.InvalidDoubleCheck(value, nameof(value));

			switch (idx)
			{
				case 0:
					X = value;
					break;
				
				case 1:
					Y = value;
					break;
				
				default:
					Z = value;
					break;
			}
		}
	}

	/// <summary>
	/// Constructs a Vector3 with both 'X' and 'Y' values being the double value passed in.
	/// </summary>
	/// <remarks>
	/// The 'Z' component will default to 1.0.
	/// </remarks>
	/// <param name="value">The value of both 'X' and 'Y' components.</param>
    /// <exception cref="ArgumentException">Can be thrown if 'value' is NaN, PositiveInfinity, or NegativeInfinity.</exception>
	public Vector3(double value)
		: this(value, value)
	{
	}

	/// <summary>
	/// Constructs a Vector3 using 2 or 3 doubles.
	/// </summary>
	/// <remarks>
	/// The 'z' argument is optional, and will default to 1.0 if not provided.
	/// </remarks>
	/// <param name="x">The 'X'-component of the Vector3.</param>
	/// <param name="y">The 'Y'-component of the Vector3.</param>
	/// <param name="z">The 'Z'-component of the Vector3.</param>
	/// <exception cref="ArgumentException">Can be thrown if any of the values are NaN, PositiveInfinity, or NegativeInfinity</exception>
	public Vector3(double x, double y, double z = 1.0)
	{
		AssertionHelper.InvalidDoubleCheck(x, nameof(x));
		AssertionHelper.InvalidDoubleCheck(y, nameof(y));
		AssertionHelper.InvalidDoubleCheck(z, nameof(z));

		_x = x;
		_y = y;
		_z = z;
	}

	/// <summary>
	/// Construct a Vector3 from an existing Vector2. The 'Z'-component will be 1.0.
	/// </summary>
	/// <param name="vec2">The existing Vector2 instance.</param>
	/// <exception cref="ArgumentNullException">Can be thrown if 'vec2' is null.</exception>
	public Vector3(Vector2 vec2)
	{
		AssertionHelper.NullCheck(vec2, nameof(vec2));

		_x = vec2.X;
		_y = vec2.Y;
		_z = 1;
	}

	/// <summary>
	/// Construct a Vector3 from an existing Vector2, with a specified 'Z'-component.
	/// </summary>
	/// <param name="vec2">The existing Vector2 instance.</param>
	/// <param name="z">The 'Z'-component to use.</param>
	/// <exception cref="ArgumentNullException">Can be thrown if 'vec2' is null.</exception>
	/// <exception cref="ArgumentException">Can be thrown if 'z' is NaN, PositiveInfinity, or NegativeInfinity.</exception>
	public Vector3(Vector2 xy, double z)
		: this(xy)
	{
		AssertionHelper.InvalidDoubleCheck(z, nameof(z));

		_z = z;
	}

	/// <summary>
	/// Constructs a Vector3 from an existing Vector2, but the 'X'-component is specified.
	/// The Vector2 is used for the 'Y'- and 'Z'-components.
	/// </summary>
	/// <param name="x">The 'X'-component.</param>
	/// <param name="yz">The 'Y'- and 'Z'-components.</param>
	/// <exception cref="ArgumentNullException">Can be thrown if 'yz' is null.</exception>
	/// <exception cref="ArgumentException">Can be thrown if 'x' is NaN, PositiveInfinity, or NegativeInfinity.</exception>
	public Vector3(double x, Vector2 yz)
	{
		AssertionHelper.InvalidDoubleCheck(x, nameof(x));
		AssertionHelper.NullCheck(yz, nameof(yz));

		_x = x;
		_y = yz.X;
		_z = yz.Y;
	}

	/// <summary>
	/// Constructs a copy of an existing Vector3 instance.
	/// </summary>
	/// <param name="vec3">The existing Vector3 instance to duplicate.</param>
	/// <exception cref="ArgumentNullException">Can be thrown if 'vec3' is null.</exception>
	public Vector3(Vector3 vec3)
	{
		AssertionHelper.NullCheck(vec3, nameof(vec3));

		_x = vec3.X;
		_y = vec3.Y;
		_z = vec3.Z;
	}

	/// <summary>
	/// Override that compares the 'X', 'Y', and 'Z' values of 2 Vector3's.
	/// </summary>
	/// <remarks>
	/// If the object is null, it'll return false.
	/// If the object is 'this', it'll return true.
	/// Otherwise, the values are compared outright.
	/// </remarks>
	/// <param name="obj">The object to compare to this Vector3 instance.</param>
	/// <returns>A boolean value if the object is equal to this Vector3 instance.</returns>
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

		return obj is Vector3 target
		       && MathHelper.Equals(X, target.X)
		       && MathHelper.Equals(Y, target.Y)
		       && MathHelper.Equals(Z, target.Z);
	}

	/// <summary>
	/// An override of ToString() that Constructs a string.
	/// </summary>
	/// <returns>A string containing the 'X', 'Y', and 'Z' values.</returns>
	public override string ToString() => $"{{ x: {X}, y: {Y}, z: {Z} }}";
}
/** @} */