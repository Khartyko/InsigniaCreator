/** \addtogroup Library
 * @{
 */
using Khartyko.InsigniaCreator.Library.Utility.Helpers;

#pragma warning disable CS0660, CS0661, CS0659

namespace Khartyko.InsigniaCreator.Library.Data;

public class Matrix
{
    private readonly Vector3[] _data;

    public Vector3[] Data => _data;

    public Vector3 this[int idx]
    {
        get
        {
            AssertionHelper.RangeCheck(idx, -1, 3, nameof(idx));

            return idx switch
            {
                0 => _data[0],
                1 => _data[1],
                _ => _data[2]
            };
        }

        set
        {
            AssertionHelper.RangeCheck(idx, -1, 3, nameof(idx));
            AssertionHelper.NullCheck(value, nameof(value));

            _data[idx] = value;
        }
    }

    public double this[int y, int x]
    {
        get
        {
            AssertionHelper.RangeCheck(y, -1, 3, nameof(y));
            AssertionHelper.RangeCheck(x, -1, 3, nameof(x));
            
            return _data[y][x];
        }

        set
        {
            AssertionHelper.RangeCheck(y, -1, 3, nameof(y));
            AssertionHelper.RangeCheck(x, -1, 3, nameof(x));
            AssertionHelper.InvalidDoubleCheck(value, nameof(value));
            
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
        AssertionHelper.NullCheck(m0, nameof(m0));
        AssertionHelper.NullCheck(m1, nameof(m1));
        AssertionHelper.NullCheck(m2, nameof(m2));

        _data = new[] { m0, m1, m2 };
    }

    public Matrix(Matrix existingMatrix)
    {
        AssertionHelper.NullCheck(existingMatrix, nameof(existingMatrix));

        _data = new[]
        {
            new Vector3(existingMatrix[0]),
            new Vector3(existingMatrix[1]),
            new Vector3(existingMatrix[2])
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
        if (ReferenceEquals(null, obj)) 
        {
            return false;
        }

        if (ReferenceEquals(this, obj))
        {
            return true;
        }

        if (obj is not Matrix matrix)
        {
            return false;
        }

        for (var y = 0; y < 3; y++)
        {
            if (!_data[y].Equals(matrix._data[y]))
            {
                return false;
            }
        }

        return true;
    }

    public static Vector2 operator *(Vector2 vec2, Matrix matrix)
    {
        AssertionHelper.NullCheck(vec2, nameof(vec2));
        AssertionHelper.NullCheck(matrix, nameof(matrix));

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
        AssertionHelper.NullCheck(matrix, nameof(matrix));
        AssertionHelper.NullCheck(vec2, nameof(vec2));

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
        AssertionHelper.NullCheck(vec3, nameof(vec3));
        AssertionHelper.NullCheck(matrix, nameof(matrix));

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
        AssertionHelper.NullCheck(matrix, nameof(matrix));
        AssertionHelper.NullCheck(vec3, nameof(vec3));

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
        AssertionHelper.NullCheck(left, nameof(left));
        AssertionHelper.NullCheck(right, nameof(right));

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
}
/** @} */