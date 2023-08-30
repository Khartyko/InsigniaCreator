/** \addtogroup Library
 * @{
 */
using Khartyko.InsigniaCreator.Library.Utility.Helpers;

#pragma warning disable CS0660, CS0661, CS0659

namespace Khartyko.InsigniaCreator.Library.Data;

public partial class Matrix
{
    /// <summary>
    /// This is a property that exposes the matrix's internal data in the form of 3 vectors.
    /// </summary>
    public Vector3[] Data { get; }

    /// <summary>
    /// This is an indexing operator on the 'Matrix' class, which allows for accessing the internal vectors.
    /// </summary>
    /// <param name="idx">The index of the Vector3 that is trying to be retrieved</param>
    /// <exception cref="ArgumentOutOfRangeException">Can be thrown if 'idx' is not between 0 and 2, inclusively.</exception>
    /// <exception cref="ArgumentException">Can be thrown if an indexed vector is set to null.</exception>
    public Vector3 this[int idx]
    {
        get
        {
            AssertionHelper.RangeCheck(idx, -1, 3, nameof(idx));

            return idx switch
            {
                0 => Data[0],
                1 => Data[1],
                _ => Data[2]
            };
        }

        set
        {
            AssertionHelper.RangeCheck(idx, -1, 3, nameof(idx));
            AssertionHelper.NullCheck(value, nameof(value));

            Data[idx] = value;
        }
    }

    /// <summary>
    /// This is an indexing operator that accepts 2 value to get a particular double value that's stored within its data
    /// </summary>
    /// <param name="y">The index of the row that's trying to be accessed.</param>
    /// <param name="x">The index of the column that's being accessed.</param>
    /// <exception cref="ArgumentOutOfRangeException">Can be thrown if either 'y' or 'x' are not between 0 and 2, inclusively.</exception>
    public double this[int y, int x]
    {
        get
        {
            AssertionHelper.RangeCheck(y, -1, 3, nameof(y));
            AssertionHelper.RangeCheck(x, -1, 3, nameof(x));
            
            return Data[y][x];
        }

        set
        {
            AssertionHelper.RangeCheck(y, -1, 3, nameof(y));
            AssertionHelper.RangeCheck(x, -1, 3, nameof(x));
            AssertionHelper.InvalidDoubleCheck(value, nameof(value));
            
            Data[y][x] = value;
        }
    }

    /// <summary>
    /// The default constructor that sets the values to that of an identity matrix.
    /// </summary>
    public Matrix()
    {
        Data = new[]
        {
            new Vector3(1, 0, 0),
            new Vector3(0, 1, 0),
            new Vector3(0, 0)
        };
    }

    /// <summary>
    /// A constructor that accepts 3 Vector3 to set each row of values.
    /// </summary>
    /// <param name="m0">The first row of values at index 0.</param>
    /// <param name="m1">The second row of values at index 1.</param>
    /// <param name="m2">The third row of values at index 2.</param>
    /// <exception cref="ArgumentNullException">Can be thrown if any of the Vector3's are null.</exception>
    public Matrix(Vector3 m0, Vector3 m1, Vector3 m2)
    {
        AssertionHelper.NullCheck(m0, nameof(m0));
        AssertionHelper.NullCheck(m1, nameof(m1));
        AssertionHelper.NullCheck(m2, nameof(m2));

        Data = new[] { m0, m1, m2 };
    }

    /// <summary>
    /// Create a new matrix from an existing Matrix.
    /// </summary>
    /// <param name="existingMatrix">A Matrix that's to be duplicated.</param>
    /// <exception cref="ArgumentNullException">Can be thrown if 'existingMatrix' is null.</exception>
    public Matrix(Matrix existingMatrix)
    {
        AssertionHelper.NullCheck(existingMatrix, nameof(existingMatrix));

        Data = new[]
        {
            new Vector3(existingMatrix[0]),
            new Vector3(existingMatrix[1]),
            new Vector3(existingMatrix[2])
        };
    }

    /// <summary>
    /// This will reset the values to that of an identity matrix.
    /// </summary>
    public void Reset()
    {
        Data[0][0] = 1.0;
        Data[0][1] = 0.0;
        Data[0][2] = 0.0;

        Data[1][0] = 0.0;
        Data[1][1] = 1.0;
        Data[1][2] = 0.0;

        Data[2][0] = 0.0;
        Data[2][1] = 0.0;
        Data[2][2] = 1.0;
    }

    /// <summary>
    /// This compares a nullable object instance to this instance of a Matrix, and later the internal values of both Matrices.
    /// </summary>
    /// <remarks>
    /// If the object is null, it'll return false.
    /// If the object is 'this', it'll return true.
    /// Otherwise, the values are compared outright.
    /// </remarks>
    /// <param name="obj">The object in question to compare to this Matrix instance.</param>
    /// <returns>A boolean value if the object is equal to this Matrix instance.</returns>
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
            if (!Data[y].Equals(matrix.Data[y]))
            {
                return false;
            }
        }

        return true;
    }
}
/** @} */