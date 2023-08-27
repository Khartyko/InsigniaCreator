/** \addtogroup Library
 * @{
 */
using Khartyko.InsigniaCreator.Library.Utility.Helpers;

#pragma warning disable CS0660, CS0661, CS0659

namespace Khartyko.InsigniaCreator.Library.Data;

public class Vector2
{
    public static readonly Vector2 Zero = new(0, 0);
    public static readonly Vector2 One = new(1, 1);

    private double _x;
    private double _y;

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

    public double Length => MathHelper.Sqrt(X * X + Y * Y);

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

    public Vector2(double value)
        : this(value, value)
    {
    }

    public Vector2(double x, double y)
    {
        AssertionHelper.InvalidDoubleCheck(x, nameof(x));
        AssertionHelper.InvalidDoubleCheck(y, nameof(y));

        _x = x;
        _y = y;
    }

    public Vector2(Vector2 vec2)
    {
        AssertionHelper.NullCheck(vec2, nameof(vec2));

        _x = vec2.X;
        _y = vec2.Y;
    }

    public Vector2(Vector3 vec3)
    {
        AssertionHelper.NullCheck(vec3, nameof(vec3));

        _x = vec3.X;
        _y = vec3.Y;
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
        
        return obj is Vector2 target
               && MathHelper.Equals(X, target.X)
               && MathHelper.Equals(Y, target.Y);
    }

    public override string ToString() => $"{{ x: {X}, y: {Y} }}";

    // Operators
    public static Vector2 operator -(Vector2 vector)
    {
        AssertionHelper.NullCheck(vector, nameof(vector));

        return new Vector2(-vector.X, -vector.Y);
    }
    
    public static Vector2 operator +(Vector2 left, double right)
    {
        AssertionHelper.NullCheck(left, nameof(left));
        AssertionHelper.InvalidDoubleCheck(right, nameof(right));

        return new Vector2(left.X + right, left.Y + right);
    }

    public static Vector2 operator -(Vector2 left, double right)
    {
        AssertionHelper.NullCheck(left, nameof(left));
        AssertionHelper.InvalidDoubleCheck(right, nameof(right));

        return new Vector2(left.X - right, left.Y - right);
    }

    public static Vector2 operator +(double left, Vector2 right)
    {
        AssertionHelper.InvalidDoubleCheck(left, nameof(left));
        AssertionHelper.NullCheck(right, nameof(right));

        return new Vector2(left + right.X, left + right.Y);
    }

    public static Vector2 operator -(double left, Vector2 right)
    {
        AssertionHelper.InvalidDoubleCheck(left, nameof(left));
        AssertionHelper.NullCheck(right, nameof(right));

        return new Vector2(left - right.X, left - right.Y);
    }

    public static Vector2 operator +(Vector2 left, Vector2 right)
    {
        AssertionHelper.NullCheck(left, nameof(left));
        AssertionHelper.NullCheck(right, nameof(right));

        return new Vector2(left.X + right.X, left.Y + right.Y);
    }

    public static Vector2 operator -(Vector2 left, Vector2 right)
    {
        AssertionHelper.NullCheck(left, nameof(left));
        AssertionHelper.NullCheck(right, nameof(right));

        return new Vector2(left.X - right.X, left.Y - right.Y);
    }

    public static Vector2 operator *(Vector2 left, Vector2 right)
    {
        AssertionHelper.NullCheck(left, nameof(left));
        AssertionHelper.NullCheck(right, nameof(right));

        return new Vector2(left.X * right.X, left.Y * right.Y);
    }

    public static Vector2 operator /(Vector2 left, Vector2 right)
    {
        AssertionHelper.NullCheck(left, nameof(left));
        AssertionHelper.NullCheck(right, nameof(right));

        return new Vector2(left.X / right.X, left.Y / right.Y);
    }

    public static Vector2 operator *(Vector2 vector, double value)
    {
        AssertionHelper.NullCheck(vector, nameof(vector));
        AssertionHelper.InvalidDoubleCheck(value, nameof(value));

        return new Vector2(vector.X * value, vector.Y * value);
    }

    public static Vector2 operator /(Vector2 vector, double value)
    {
        AssertionHelper.NullCheck(vector, nameof(vector));
        AssertionHelper.InvalidDoubleCheck(value, nameof(value));

        return new Vector2(vector.X / value, vector.Y / value);
    }

    public static Vector2 operator *(double value, Vector2 vector)
    {
        AssertionHelper.InvalidDoubleCheck(value, nameof(value));
        AssertionHelper.NullCheck(vector, nameof(vector));

        return new Vector2(value * vector.X, value * vector.Y);
    }

    public static Vector2 operator /(double value, Vector2 vector)
    {
        AssertionHelper.InvalidDoubleCheck(value, nameof(value));
        AssertionHelper.NullCheck(vector, nameof(vector));

        return new Vector2(value / vector.X, value / vector.Y);
    }
}
/** @} */