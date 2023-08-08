using Khartyko.InsigniaCreator.Library.Utility.Helpers;

namespace Khartyko.InsigniaCreator.Library.Data;

public class Matrix
{
    private Vector3[] _data;

    public Vector3[] Data => _data;

    public Vector3 this[int idx]
    {
        get => idx switch
        {
            0 => _data[0],
            1 => _data[1],
            2 => _data[2],
            _ => throw new ArgumentOutOfRangeException(nameof(idx), "Matrix::[>idx<]")
        };

        set
        {
            if (0 > idx || idx >= 3)
            {
                throw new ArgumentOutOfRangeException(nameof(idx), "Matrix::[>idx<]");
            }

            ObjectHelper.NullCheck(value, "Matrix::[idx] = >value<");
            _data[idx] = value;
        }
    }

    public double this[int y, int x]
    {
        get
        {
            if (y is < 0 or >= 3)
            {
                throw new ArgumentOutOfRangeException(nameof(y), "Matrix::[>y<, x]");
            }

            return _data[y][x];
        }

        set
        {
            if (y is < 0 or >= 3)
            {
                throw new ArgumentOutOfRangeException(nameof(y), "Matrix::[>y<, x]");
            }

            MathHelper.InvalidDoubleCheck(value, "Matrix::[y, x] = >value<");
            _data[y][x] = value;
        }
    }

    public Matrix()
    {
        _data = new[]
        {
            new Vector3(1, 0, 0),
            new Vector3(0, 1, 0),
            new Vector3(0, 0)
        };
    }

    public Matrix(Vector3 m0, Vector3 m1, Vector3 m2)
    {
        ObjectHelper.NullCheck(m0, "Matrix::Matrix(>m0<, m1, m2)");
        ObjectHelper.NullCheck(m1, "Matrix::Matrix(m0, >m1<, m2)");
        ObjectHelper.NullCheck(m2, "Matrix::Matrix(m0, m1, >m2<)");

        _data = new[] { m0, m1, m2 };
    }

    public Matrix(Matrix other)
    {
        ObjectHelper.NullCheck(other, "Matrix::Matrix(>other<)");

        _data = new[]
        {
            new Vector3(other[0]),
            new Vector3(other[1]),
            new Vector3(other[2])
        };
    }

    public void Reset()
    {
        _data[0][0] = 1.0;
        _data[0][1] = 0.0;
        _data[0][2] = 0.0;

        _data[1][0] = 0.0;
        _data[1][1] = 1.0;
        _data[1][2] = 0.0;

        _data[2][0] = 0.0;
        _data[2][1] = 0.0;
        _data[2][2] = 1.0;
    }

    public override bool Equals(object? obj)
    {
        if (ReferenceEquals(null, obj)) return false;
        if (ReferenceEquals(this, obj)) return true;
        
        return obj.GetType() == GetType()
               && obj is Matrix matrix
               && this == matrix;
    }

    public override int GetHashCode()
    {
        // ReSharper disable NonReadonlyMemberInGetHashCode
        return _data.GetHashCode();
        // ReSharper restore NonReadonlyMemberInGetHashCode
    }

    public override string ToString() => $"{{\n  {_data[0]}\n  {_data[1]}\n {_data[2]}\n}}";

    public static Vector2 operator *(Vector2 vec2, Matrix matrix)
    {
        ObjectHelper.NullCheck(vec2, "Matrix::operator *(vec2, >matrix<)");
        ObjectHelper.NullCheck(matrix, "Matrix::operator *(>vec2<, matrix)");

        var tempVector = new Vector3(vec2);
        var resultVector = new Vector3(0, 0, 0);

        for (int y = 0; y < 3; y++)
        {
            for (int x = 0; x < 3; x++)
            {
                resultVector[y] += tempVector[x] * matrix[y, x];
            }
        }

        return new Vector2(resultVector);
    }

    public static Vector2 operator *(Matrix matrix, Vector2 vec2)
    {
        ObjectHelper.NullCheck(matrix, "Matrix::operator *(>matrix<, vec2)");
        ObjectHelper.NullCheck(vec2, "Matrix::operator *(matrix, >vec2<)");

        var tempVector = new Vector3(vec2);
        var resultVector = new Vector3(0, 0, 0);

        for (var y = 0; y < 3; y++)
        {
            for (var x = 0; x < 3; x++)
            {
                resultVector[y] += tempVector[x] * matrix[y, x];
            }
        }

        return new Vector2(resultVector);
    }

    public static Vector3 operator *(Vector3 vec3, Matrix matrix)
    {
        ObjectHelper.NullCheck(vec3, "Matrix::operator *(vec3, >matrix<)");
        ObjectHelper.NullCheck(matrix, "Matrix::operator *(>vec3<, matrix)");

        var resultVector = new Vector3(0, 0, 0);

        for (var y = 0; y < 3; y++)
        {
            for (var x = 0; x < 3; x++)
            {
                resultVector[y] += vec3[x] * matrix[y, x];
            }
        }

        return resultVector;
    }

    public static Vector3 operator *(Matrix matrix, Vector3 vec3)
    {
        ObjectHelper.NullCheck(matrix, "Matrix::operator *(>matrix<, vec3)");
        ObjectHelper.NullCheck(vec3, "Matrix::operator *(matrix, >vec3<)");

        var resultVector = new Vector3(0, 0, 0);

        for (var y = 0; y < 3; y++)
        {
            for (var x = 0; x < 3; x++)
            {
                resultVector[y] += vec3[x] * matrix[y, x];
            }
        }

        return resultVector;
    }

    public static Matrix operator *(Matrix left, Matrix right)
    {
        ObjectHelper.NullCheck(left, "Matrix::operator *(>left<, right)");
        ObjectHelper.NullCheck(right, "Matrix::operator *(left, >right<)");

        var result = new Matrix(
            new Vector3(0, 0, 0),
            new Vector3(0, 0, 0),
            new Vector3(0, 0, 0)
        );

        for (var y = 0; y < 3; y++)
        {
            for (var x = 0; x < 3; x++)
            {
                for (var w = 0; w < 3; w++)
                {
                    result[y, x] += left[y, w] * right[w, x];
                }
            }
        }

        return result;
    }

    public static bool operator ==(Matrix left, Matrix right)
    {
        ObjectHelper.NullCheck(left, "Matrix::operator ==(>left<, right)");
        ObjectHelper.NullCheck(right, "Matrix::operator ==(left, >right<)");

        return left[0] == right[0]
               && left[1] == right[1]
               && left[2] == right[2];
    }

    public static bool operator !=(Matrix left, Matrix right)
    {
        ObjectHelper.NullCheck(left, "Matrix::operator !=(>left<, right)");
        ObjectHelper.NullCheck(right, "Matrix::operator !=(left, >right<)");

        return left[0] != right[0]
               || left[1] != right[1]
               || left[2] != right[2];
    }
}