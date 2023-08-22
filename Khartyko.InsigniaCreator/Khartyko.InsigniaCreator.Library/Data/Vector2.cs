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
            AssertionHelper.InvalidDoubleCheck(value, "Vector2::X");
            
            _x = value;
        }
    }

    public double Y
    {
        get => _y;
        set
        {
            AssertionHelper.InvalidDoubleCheck(value, "Vector2::Y");
            
            _y = value;
        }
    }

    public double Length => MathHelper.Sqrt(X * X + Y * Y);

    public double this[int idx]
    {
        get => idx switch
        {
            0 => X,
            1 => Y,
            _ => throw new ArgumentOutOfRangeException(nameof(idx),
                $"Vector2::[>idx<]; 'idx' is out of range; got '{idx}'")
        };

        set
        {
            switch (idx)
            {
                case 0:
                    AssertionHelper.InvalidDoubleCheck(value, "Vector2::[idx] = >value<");
                    _x = value;
                    break;

                case 1:
                    AssertionHelper.InvalidDoubleCheck(value, "Vector2::[idx] = >value<");
                    _y = value;
                    break;

                default:
                    throw new ArgumentOutOfRangeException(nameof(idx), $"Vector2::[>idx<]; 'idx' is out of range, got '{idx}'");
            }
        }
    }

    public Vector2(double value)
        : this(value, value)
    {
    }

    public Vector2(double x, double y)
    {
        AssertionHelper.InvalidDoubleCheck(x, "Vector2::Vector2(>x<, y)");
        AssertionHelper.InvalidDoubleCheck(y, "Vector2::Vector2(x, >y<)");

        X = x;
        Y = y;
    }

    public Vector2(Vector2 vec2)
    {
        AssertionHelper.NullCheck(vec2, "Vector2::Vector2(>vec2<)");

        _x = vec2.X;
        _y = vec2.Y;
    }

    public Vector2(Vector3 vec3)
    {
        AssertionHelper.NullCheck(vec3, "Vector2::Vector2(>vec3<)");

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
        AssertionHelper.NullCheck(left, "Vector2::operator +(>left<, right); 'left' is null");
        AssertionHelper.InvalidDoubleCheck(right, "Vector2::operator+(left, >right<)");

        return new Vector2(left.X + right, left.Y + right);
    }

    public static Vector2 operator -(Vector2 left, double right)
    {
        AssertionHelper.NullCheck(left, "Vector2::operator -(>left<, right); 'left' is null");
        AssertionHelper.InvalidDoubleCheck(right, "Vector2::operator -(left, >right<)");

        return new Vector2(left.X - right, left.Y - right);
    }

    public static Vector2 operator +(double left, Vector2 right)
    {
        AssertionHelper.InvalidDoubleCheck(left, "Vector2::operator+(left, >right<)");
        AssertionHelper.NullCheck(right, "Vector2::operator +(>left<, right); 'left' is null");

        return new Vector2(left + right.X, left + right.Y);
    }

    public static Vector2 operator -(double left, Vector2 right)
    {
        AssertionHelper.InvalidDoubleCheck(left, "Vector2::operator -(left, >right<)");
        AssertionHelper.NullCheck(right, "Vector2::operator -(>left<, right); 'left' is null");

        return new Vector2(left - right.X, left - right.Y);
    }

    public static Vector2 operator +(Vector2 left, Vector2 right)
    {
        AssertionHelper.NullCheck(left, "Vector2::operator +(>left<, right); 'left' is null");
        AssertionHelper.NullCheck(right, "Vector2::operator +(left, >right<); 'right' is null");

        return new Vector2(left.X + right.X, left.Y + right.Y);
    }

    public static Vector2 operator -(Vector2 left, Vector2 right)
    {
        AssertionHelper.NullCheck(left, "Vector2::operator -(>left<, right); 'left' is null");
        AssertionHelper.NullCheck(right, "Vector2::operator -(left, >right<); 'right' is null");

        return new Vector2(left.X - right.X, left.Y - right.Y);
    }

    public static Vector2 operator *(Vector2 left, Vector2 right)
    {
        AssertionHelper.NullCheck(left, "Vector2::operator *(>left<, right); 'left' is null");
        AssertionHelper.NullCheck(right, "Vector2::operator *(left, >right<); 'right' is null");

        return new Vector2(left.X * right.X, left.Y * right.Y);
    }

    public static Vector2 operator /(Vector2 left, Vector2 right)
    {
        AssertionHelper.NullCheck(left, "Vector2::operator /(>left<, right); 'left' is null");
        AssertionHelper.NullCheck(right, "Vector2::operator /(left, >right<); 'right' is null");

        return new Vector2(left.X / right.X, left.Y / right.Y);
    }

    public static Vector2 operator *(Vector2 vector, double value)
    {
        AssertionHelper.NullCheck(vector, "Vector2::operator *(>vector<, value); 'vector' is null");
        AssertionHelper.InvalidDoubleCheck(value, "Vector2::operator *(vector, >value<)");

        return new Vector2(vector.X * value, vector.Y * value);
    }

    public static Vector2 operator /(Vector2 vector, double value)
    {
        AssertionHelper.NullCheck(vector, "Vector2::operator /(>vector<, value); 'vector' is null");
        AssertionHelper.InvalidDoubleCheck(value, "Vector2::operator /(vector, >value<)");

        return new Vector2(vector.X / value, vector.Y / value);
    }

    public static Vector2 operator *(double value, Vector2 vector)
    {
        AssertionHelper.InvalidDoubleCheck(value, "Vector2::operator *(>value<, vector)");
        AssertionHelper.NullCheck(vector, "Vector2::operator *(value, >vector<); 'vector' is null");

        return new Vector2(value * vector.X, value * vector.Y);
    }

    public static Vector2 operator /(double value, Vector2 vector)
    {
        AssertionHelper.InvalidDoubleCheck(value, "Vector2::operator /(>value<, vector)");
        AssertionHelper.NullCheck(vector, "Vector2::operator /(value, >vector<); 'vector' is null");

        return new Vector2(value / vector.X, value / vector.Y);
    }
}