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
            MathHelper.InvalidDoubleCheck(value, "Vector2::X");
            _x = value;
        }
    }

    public double Y
    {
        get => _y;
        set
        {
            MathHelper.InvalidDoubleCheck(value, "Vector2::Y");
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
                    MathHelper.InvalidDoubleCheck(value, "Vector2::[idx] = >value<");
                    _x = value;
                    break;

                case 1:
                    MathHelper.InvalidDoubleCheck(value, "Vector2::[idx] = >value<");
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
        MathHelper.InvalidDoubleCheck(x, "Vector2::Vector2(>x<, y)");
        MathHelper.InvalidDoubleCheck(y, "Vector2::Vector2(x, >y<)");

        X = x;
        Y = y;
    }

    public Vector2(Vector2 vec2)
    {
        ObjectHelper.NullCheck(vec2, "Vector2::Vector2(>vec2<)");

        _x = vec2.X;
        _y = vec2.Y;
    }

    public Vector2(Vector3 vec3)
    {
        ObjectHelper.NullCheck(vec3, "Vector2::Vector2(>vec3<)");

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
        ObjectHelper.NullCheck(vector, nameof(vector));

        return new Vector2(-vector.X, -vector.Y);
    }
    
    public static Vector2 operator +(Vector2 left, double right)
    {
        ObjectHelper.NullCheck(left, "Vector2::operator +(>left<, right); 'left' is null");
        MathHelper.InvalidDoubleCheck(right, "Vector2::operator+(left, >right<)");

        return new Vector2(left.X + right, left.Y + right);
    }

    public static Vector2 operator -(Vector2 left, double right)
    {
        ObjectHelper.NullCheck(left, "Vector2::operator -(>left<, right); 'left' is null");
        MathHelper.InvalidDoubleCheck(right, "Vector2::operator -(left, >right<)");

        return new Vector2(left.X - right, left.Y - right);
    }

    public static Vector2 operator +(double left, Vector2 right)
    {
        MathHelper.InvalidDoubleCheck(left, "Vector2::operator+(left, >right<)");
        ObjectHelper.NullCheck(right, "Vector2::operator +(>left<, right); 'left' is null");

        return new Vector2(left + right.X, left + right.Y);
    }

    public static Vector2 operator -(double left, Vector2 right)
    {
        MathHelper.InvalidDoubleCheck(left, "Vector2::operator -(left, >right<)");
        ObjectHelper.NullCheck(right, "Vector2::operator -(>left<, right); 'left' is null");

        return new Vector2(left - right.X, left - right.Y);
    }

    public static Vector2 operator +(Vector2 left, Vector2 right)
    {
        ObjectHelper.NullCheck(left, "Vector2::operator +(>left<, right); 'left' is null");
        ObjectHelper.NullCheck(right, "Vector2::operator +(left, >right<); 'right' is null");

        return new Vector2(left.X + right.X, left.Y + right.Y);
    }

    public static Vector2 operator -(Vector2 left, Vector2 right)
    {
        ObjectHelper.NullCheck(left, "Vector2::operator -(>left<, right); 'left' is null");
        ObjectHelper.NullCheck(right, "Vector2::operator -(left, >right<); 'right' is null");

        return new Vector2(left.X - right.X, left.Y - right.Y);
    }

    public static Vector2 operator *(Vector2 left, Vector2 right)
    {
        ObjectHelper.NullCheck(left, "Vector2::operator *(>left<, right); 'left' is null");
        ObjectHelper.NullCheck(right, "Vector2::operator *(left, >right<); 'right' is null");

        return new Vector2(left.X * right.X, left.Y * right.Y);
    }

    public static Vector2 operator /(Vector2 left, Vector2 right)
    {
        ObjectHelper.NullCheck(left, "Vector2::operator /(>left<, right); 'left' is null");
        ObjectHelper.NullCheck(right, "Vector2::operator /(left, >right<); 'right' is null");

        return new Vector2(left.X / right.X, left.Y / right.Y);
    }

    public static Vector2 operator *(Vector2 vector, double value)
    {
        ObjectHelper.NullCheck(vector, "Vector2::operator *(>vector<, value); 'vector' is null");
        MathHelper.InvalidDoubleCheck(value, "Vector2::operator *(vector, >value<)");

        return new Vector2(vector.X * value, vector.Y * value);
    }

    public static Vector2 operator /(Vector2 vector, double value)
    {
        ObjectHelper.NullCheck(vector, "Vector2::operator /(>vector<, value); 'vector' is null");
        MathHelper.InvalidDoubleCheck(value, "Vector2::operator /(vector, >value<)");

        return new Vector2(vector.X / value, vector.Y / value);
    }

    public static Vector2 operator *(double value, Vector2 vector)
    {
        MathHelper.InvalidDoubleCheck(value, "Vector2::operator *(>value<, vector)");
        ObjectHelper.NullCheck(vector, "Vector2::operator *(value, >vector<); 'vector' is null");

        return new Vector2(value * vector.X, value * vector.Y);
    }

    public static Vector2 operator /(double value, Vector2 vector)
    {
        MathHelper.InvalidDoubleCheck(value, "Vector2::operator /(>value<, vector)");
        ObjectHelper.NullCheck(vector, "Vector2::operator /(value, >vector<); 'vector' is null");

        return new Vector2(value / vector.X, value / vector.Y);
    }
}