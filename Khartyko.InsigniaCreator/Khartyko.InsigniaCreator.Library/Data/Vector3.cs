using Khartyko.InsigniaCreator.Library.Utility.Helpers;

#pragma warning disable CS0660, CS0661, CS0659

namespace Khartyko.InsigniaCreator.Library.Data;

public class Vector3
{
    private double _x;
    private double _y;
    private double _z;

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
    
    public double Z
    {
        get => _z;
        set
        {
            MathHelper.InvalidDoubleCheck(value, "Vector2::Z");
            _z = value;
        }
    }

    #region Swizzling

    // ReSharper disable InconsistentNaming
    public Vector2 XY => new Vector2(X, Y);
    public Vector2 XZ => new Vector2(X, Z);
    public Vector2 YX => new Vector2(Y, X);
    public Vector2 YZ => new Vector2(Y, Z);
    public Vector2 ZX => new Vector2(Z, X);
    public Vector2 ZY => new Vector2(Z, Y);

    public Vector3 XYZ => new(X, Y, Z);
    public Vector3 ZYX => new(Z, Y, X);
    public Vector3 YXZ => new(Y, X, Z);
    public Vector3 ZXY => new(Z, X, Y);
    public Vector3 XZY => new(X, Z, Y);
    public Vector3 YZX => new(Y, Z, X);
    // ReSharper restore InconsistentNaming

    #endregion


    public double Length => MathHelper.Round(MathHelper.Sqrt(X * X + Y * Y + Z * Z));

    public double this[int idx]
    {
        get => idx switch
        {
            0 => X,
            1 => Y,
            2 => Z,
            _ => throw new ArgumentOutOfRangeException(nameof(idx),
                $"Vector3::[>idx<]; 'idx' is out of range; got '{idx}'")
        };

        set
        {
            switch (idx)
            {
                case 0:
                    MathHelper.InvalidDoubleCheck(value, "Vector3::[idx] = >value<");
                    _x = value;
                    break;
                
                case 1:
                    MathHelper.InvalidDoubleCheck(value, "Vector3::[idx] = >value<");
                    _y = value;
                    break;
                
                case 2:
                    MathHelper.InvalidDoubleCheck(value, "Vector3::[idx] = >value<");
                    _z = value;
                    break;
                
                default:
                    throw new ArgumentOutOfRangeException(nameof(idx), $"Vector3::[>idx<]; 'idx' is out of range, got '{idx}'");
            }
        }
    }

    public Vector3(double value)
        : this(value, value)
    {
    }

    public Vector3(double x, double y, double z = 1.0)
    {
        MathHelper.InvalidDoubleCheck(x, "Vector3::Vector3(>x<, y, z)");
        MathHelper.InvalidDoubleCheck(y, "Vector3::Vector3(x, >y<, z)");
        MathHelper.InvalidDoubleCheck(z, "Vector3::Vector3(x, y, >z<)");

        _x = x;
        _y = y;
        _z = z;
    }

    public Vector3(Vector2 vec2)
    {
        ObjectHelper.NullCheck(vec2, "Vector3::Vector3(>vec2<)");

        _x = vec2.X;
        _y = vec2.Y;
        _z = 1;
    }

    public Vector3(Vector2 xy, double z)
        : this(xy)
    {
        MathHelper.InvalidDoubleCheck(z, "Vector3::Vector3(vec2, >z<");

        _z = z;
    }

    public Vector3(double x, Vector2 yz)
        : this(x, yz.X, yz.Y)
    {
    }

    public Vector3(Vector3 vec3)
    {
        ObjectHelper.NullCheck(vec3, "Vector3::Vector3(>vec3<)");

        _x = vec3.X;
        _y = vec3.Y;
        _z = vec3.Z;
    }

    public override bool Equals(object? obj)
    {
        if (ReferenceEquals(null, obj)) return false;
        if (ReferenceEquals(this, obj)) return true;

        return obj.GetType() == GetType()
               && obj is Vector3 vec3
               && this == vec3;
    }

    public override string ToString() => $"{{ x: {X}, y: {Y}, z: {Z} }}";

    public static Vector2 operator +(Vector2 vec2, Vector3 vec3)
    {
        if (vec2 is null)
        {
            throw new ArgumentNullException(nameof(vec2), "Vector3::operator+(>vec2<, vec3); 'vec2' is null");
        }

        if (vec3 is null)
        {
            throw new ArgumentNullException(nameof(vec3), "Vector3::operator+(vec2, >vec3<); 'vec3' is null");
        }

        return new Vector2(vec2.X + vec3.X, vec2.Y + vec3.Y);
    }
    
    public static Vector2 operator -(Vector2 vec2, Vector3 vec3) 
    {
        if (vec2 is null)
        {
            throw new ArgumentNullException(nameof(vec2), "Vector3::operator+(>vec2<, vec3); 'vec2' is null");
        }

        if (vec3 is null)
        {
            throw new ArgumentNullException(nameof(vec3), "Vector3::operator+(vec2, >vec3<); 'vec3' is null");
        }

        return new Vector2(vec2.X - vec3.X, vec2.Y - vec3.Y);
    }
    
    public static Vector3 operator +(Vector3 vec3, Vector2 vec2) 
    {
        if (vec3 is null)
        {
            throw new ArgumentNullException(nameof(vec3), "Vector3::operator+(>vec3<, vec2); 'vec3' is null");
        }
        
        if (vec2 is null)
        {
            throw new ArgumentNullException(nameof(vec2), "Vector3::operator+(vec3, >vec2<); 'vec2' is null");
        }

        return new Vector3(vec3.X + vec2.X, vec3.Y + vec2.Y, vec3.Z);
    }
    
    public static Vector3 operator -(Vector3 vec3, Vector2 vec2)   
    {
        if (vec3 is null)
        {
            throw new ArgumentNullException(nameof(vec3), "Vector3::operator-(>vec3<, vec2); 'vec3' is null");
        }

        if (vec2 is null)
        {
            throw new ArgumentNullException(nameof(vec2), "Vector3::operator-(vec3, >vec2<); 'vec2' is null");
        }

        return new Vector3(vec3.X - vec2.X, vec3.Y - vec2.Y, vec3.Z);
    }
    
    public static Vector3 operator +(Vector3 left, Vector3 right)
    {
        if (left is null)
        {
            throw new ArgumentNullException(nameof(left), "Vector3::operator-(>left<, right); 'left' is null");
        }

        if (right is null)
        {
            throw new ArgumentNullException(nameof(right), "Vector3::operator-(left, >right<); 'right' is null");
        }

        return new Vector3(left.X + right.X, left.Y + right.Y, left.Z + right.Z);
    }
    
    public static Vector3 operator -(Vector3 left, Vector3 right)
    {
        if (left is null)
        {
            throw new ArgumentNullException(nameof(left), "Vector3::operator-(>left<, right); 'left' is null");
        }

        if (right is null)
        {
            throw new ArgumentNullException(nameof(right), "Vector3::operator-(left, >right<); 'right' is null");
        }

        return new Vector3(left.X - right.X, left.Y - right.Y, left.Z - right.Z);
    }
}