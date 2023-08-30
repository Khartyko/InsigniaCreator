/** \addtogroup Library
 * @{
 */

using Khartyko.InsigniaCreator.Library.Utility.Helpers;

namespace Khartyko.InsigniaCreator.Library.Data;

/// <summary>
/// This class houses the static operator overloads
/// </summary>
public partial class Matrix
{
    /// <summary>
    /// This multiplies a Vector2 by a Matrix, and returns a modified Vector2.
    /// </summary>
    /// <remarks>
    /// This is functionally the same as matrix * vec2.
    /// </remarks>
    /// <param name="vec2">The Vector2 in question.</param>
    /// <param name="matrix">The Matrix that's used to create a modified Vector2.</param>
    /// <exception cref="ArgumentNullException">Can be thrown if either 'vec2' or 'matrix' are null.</exception>
    /// <returns>A Vector2 that has been transformed by a matrix.</returns>
    public static Vector2 operator *(Vector2 vec2, Matrix matrix)
    {
        AssertionHelper.NullCheck(vec2, nameof(vec2));
        AssertionHelper.NullCheck(matrix, nameof(matrix));

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

    /// <summary>
    /// This multiplies a Matrix by a Vector2, and returns a modified Vector2.
    /// </summary>
    /// <param name="matrix">The Matrix that's used to create a modified Vector2.</param>
    /// <param name="vec2">The Vector2 in question.</param>
    /// <exception cref="ArgumentNullException">Can be thrown if either 'matrix' or 'vec2' are null.</exception>
    /// <returns>A Vector2 that has been transformed by a matrix.</returns>
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

    /// <summary>
    /// This multiplies a Vector3 by a Matrix, and returns a modified Vector2.
    /// </summary>
    /// <remarks>
    /// This is functionally the same as matrix * vec3.
    /// </remarks>
    /// <param name="vec3">The Vector3 in question.</param>
    /// <param name="matrix">The Matrix that's used to create a modified Vector3.</param>
    /// <exception cref="ArgumentNullException">Can be thrown if either 'vec3' or 'matrix' are null.</exception>
    /// <returns>A Vector3 that has been transformed by a matrix.</returns>
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

    /// <summary>
    /// This multiplies a Matrix by a Vector3, and returns a modified Vector2.
    /// </summary>
    /// <param name="matrix">The Matrix that's used to create a modified Vector3.</param>
    /// <param name="vec3">The Vector3 in question.</param>
    /// <exception cref="ArgumentNullException">Can be thrown if either 'matrix' or 'vec3' are null.</exception>
    /// <returns>A Vector3 that has been transformed by a matrix.</returns>
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

    /// <summary>
    /// This multiplies 2 Matrices, and returns the resulting Matrix.
    /// </summary>
    /// <param name="left">This first Matrix operand.</param>
    /// <param name="right">The second Matrix operand.</param>
    /// <exception cref="ArgumentNullException">Can be thrown if either 'left' or 'right' are null.</exception>
    /// <returns>A Matrix that is the result of multiplying the data from both Matrices.</returns>
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