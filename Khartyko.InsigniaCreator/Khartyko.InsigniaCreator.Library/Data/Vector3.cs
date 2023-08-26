using Khartyko.InsigniaCreator.Library.Utility.Helpers;

#pragma warning disable CS0660, CS0661, CS0659

namespace Khartyko.InsigniaCreator.Library.Data;

public class Vector3
{
	public static readonly Vector3 Zero = new(0);
	public static readonly Vector3 One = new(1);

	private double _x;
	private double _y;
	private double _z;

	public double X
	{
		get => _x;
		set
		{
			AssertionHelper.InvalidDoubleCheck(value, nameof(value));
			
			_x = value;
		}
	}

	public double Y
	{
		get => _y;
		set
		{
			AssertionHelper.InvalidDoubleCheck(value, nameof(value));

			_y = value;
		}
	}

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
	public Vector2 XY => new(X, Y);
	public Vector2 XZ => new(X, Z);
	public Vector2 YX => new(Y, X);
	public Vector2 YZ => new(Y, Z);
	public Vector2 ZX => new(Z, X);
	public Vector2 ZY => new(Z, Y);

	public Vector3 XYZ => new(X, Y, Z);
	public Vector3 ZYX => new(Z, Y, X);
	public Vector3 YXZ => new(Y, X, Z);
	public Vector3 ZXY => new(Z, X, Y);
	public Vector3 XZY => new(X, Z, Y);
	public Vector3 YZX => new(Y, Z, X);
	// ReSharper restore InconsistentNaming

	#endregion


	public double Length
	{
		get => MathHelper.Round(MathHelper.Sqrt(X * X + Y * Y + Z * Z));
	}

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

	public Vector3(double value)
		: this(value, value)
	{
	}

	public Vector3(double x, double y, double z = 1.0)
	{
		AssertionHelper.InvalidDoubleCheck(x, nameof(x));
		AssertionHelper.InvalidDoubleCheck(y, nameof(y));
		AssertionHelper.InvalidDoubleCheck(z, nameof(z));

		_x = x;
		_y = y;
		_z = z;
	}

	public Vector3(Vector2 vec2)
	{
		AssertionHelper.NullCheck(vec2, nameof(vec2));

		_x = vec2.X;
		_y = vec2.Y;
		_z = 1;
	}

	public Vector3(Vector2 xy, double z)
		: this(xy)
	{
		AssertionHelper.InvalidDoubleCheck(z, nameof(z));

		_z = z;
	}

	public Vector3(double x, Vector2 yz)
	{
		AssertionHelper.NullCheck(yz, nameof(yz));
		
		AssertionHelper.InvalidDoubleCheck(x, nameof(x));

		_x = x;
		_y = yz.X;
		_z = yz.Y;
	}

	public Vector3(Vector3 vec3)
	{
		AssertionHelper.NullCheck(vec3, nameof(vec3));

		_x = vec3.X;
		_y = vec3.Y;
		_z = vec3.Z;
	}

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

	public override string ToString() => $"{{ x: {X}, y: {Y}, z: {Z} }}";

	public static Vector3 operator -(Vector3 vector)
	{
		AssertionHelper.NullCheck(vector, nameof(vector));

		// Note: This might be changed in the future if the 'Z' value isn't used
		return new Vector3(-vector.X, -vector.Y, -vector.Z);
	}
	
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
}