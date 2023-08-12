using Khartyko.InsigniaCreator.Library.Data;
using Khartyko.InsigniaCreator.Library.Testing.Utility;

#pragma warning disable CS8600
#pragma warning disable CS8625
#pragma warning disable CS8604

namespace Khartyko.InsigniaCreator.Library.Testing.Data;

public class MatrixTests
{
    public static IEnumerable<object[]> MatrixCopyConstructorData => new List<object[]>
    {
        new object[]
        {
            new Matrix()
        },
        new object[]
        {
            new Matrix(
                new Vector3(1, 2, 3),
                new Vector3(4, 5, 6),
                new Vector3(8, 9, 10)
            )
        }
    };

    public static IEnumerable<object[]> Vector2MatrixData => new List<object[]>
    {
        new object[]
        {
            new Vector2(1),
            new Matrix(),
            new Vector2(1)
        },

        new object[]
        {
            new Vector2(3, 5),
            new Matrix(
                new Vector3(1, -5, 3),
                new Vector3(4, -5, 7),
                new Vector3(0.5, 0.3, -6)
            ),
            new Vector2(-19, -6)
        }
    };

    public static IEnumerable<object[]> MatrixVector2Data => new List<object[]>
    {
        new object[]
        {
            new Matrix(),
            new Vector2(1),
            new Vector2(1)
        },

        new object[]
        {
            new Matrix(
                new Vector3(1, -5, 3),
                new Vector3(4, -5, 7),
                new Vector3(0.5, 0.3, -6)
            ),
            new Vector2(3, 5),
            new Vector2(-19, -6)
        }
    };

    public static IEnumerable<object[]> Vector3MatrixData => new List<object[]>
    {
        new object[]
        {
            new Vector3(1),
            new Matrix(),
            new Vector3(1)
        },
        new object[]
        {
            new Vector3(3, 4, 5),
            new Matrix(
                new Vector3(1, 2, 3),
                new Vector3(4, 5, 6),
                new Vector3(7, 8, 9)
            ),
            new Vector3(26, 62, 98)
        }
    };

    public static IEnumerable<object[]> MatrixVector3Data => new List<object[]>
    {
        new object[]
        {
            new Matrix(),
            new Vector3(1),
            new Vector3(1)
        },
        new object[]
        {
            new Matrix(
                new Vector3(1, 2, 3),
                new Vector3(4, 5, 6),
                new Vector3(7, 8, 9)
            ),
            new Vector3(3, 4, 5),
            new Vector3(26, 62, 98)
        }
    };

    public static IEnumerable<object[]> MatrixMatrixData => new List<object[]>
    {
        new object[]
        {
            new Matrix(),
            new Matrix(),
            new Matrix()
        },

        new object[]
        {
            new Matrix(
                new Vector3(1, 2, 3),
                new Vector3(4, 5, 6),
                new Vector3(7, 8, 9)
            ),
            new Matrix(
                new Vector3(-1, 2, -3),
                new Vector3(4, -5, 6),
                new Vector3(-7, 8, -9)
            ),
            new Matrix(
                new Vector3(-14, 16, -18),
                new Vector3(-26, 31, -36),
                new Vector3(-38, 46, -54)
            )
        }
    };

    [Fact]
    public void Index_Succeeds()
    {
        var matrix = new Matrix(
            Vector3.Zero,
            Vector3.One,
            Vector3.One * 2
        );

        Assert.Equal(Vector3.Zero, matrix[0]);
        Assert.Equal(Vector3.One, matrix[1]);
        Assert.Equal(Vector3.One * 2, matrix[2]);
    }

    [Fact]
    public void Index_BadIndex_Fails()
    {
        var matrix = new Matrix();

        Assert.Throws<ArgumentOutOfRangeException>(() => matrix[-1]);
        Assert.Throws<ArgumentOutOfRangeException>(() => matrix[4]);
    }

    [Fact]
    public void Matrix_Create_Default_Succeeds()
    {
        var matrix = new Matrix();
        var expectedData = new[]
        {
            new Vector3(1, 0, 0),
            new Vector3(0, 1, 0),
            new Vector3(0, 0)
        };

        var actualData = matrix.Data;

        for (var i = 0; i < 3; i++)
        {
            Assert.True(expectedData[i].Equals(actualData[i]));
        }
    }

    [Theory]
    [InlineData(1, 0, 0, 0, 1, 0, 0, 0, 1)]
    public void Matrix_Create_Parameterized_Succeeds(
        double m00, double m01, double m02,
        double m10, double m11, double m12,
        double m20, double m21, double m22
    )
    {
        var matrix = new Matrix(
            new Vector3(m00, m01, m02),
            new Vector3(m10, m11, m12),
            new Vector3(m20, m21, m22)
        );
        var expectedData = new[]
        {
            new Vector3(m00, m01, m02),
            new Vector3(m10, m11, m12),
            new Vector3(m20, m21, m22)
        };

        var actualData = matrix.Data;

        for (var i = 0; i < 3; i++)
        {
            Assert.True(expectedData[i].Equals(actualData[i]));
        }
    }

    [Theory]
    [InlineData(true, false, false)]
    [InlineData(false, true, false)]
    [InlineData(true, true, false)]
    [InlineData(true, false, true)]
    [InlineData(true, true, true)]
    [InlineData(false, true, true)]
    public void Matrix_Create_Parameterized_Fails(bool vec0Null, bool vec1Null, bool vec2Null)
    {
        Vector3? vec0 = vec0Null ? null : DataGenerator.GenerateRandomVector3();
        Vector3? vec1 = vec1Null ? null : DataGenerator.GenerateRandomVector3();
        Vector3? vec2 = vec2Null ? null : DataGenerator.GenerateRandomVector3();

        Assert.Throws<ArgumentNullException>(() => new Matrix(vec0, vec1, vec2));
    }

    [Theory, MemberData(nameof(MatrixCopyConstructorData))]
    public void Matrix_Create_CopyConstructor_Succeeds(Matrix matrix)
    {
        var duplicate = new Matrix(matrix);

        Assert.True(matrix.Equals(duplicate));
    }

    [Fact]
    public void Matrix_Create_CopyConstructor_Fails()
    {
        Assert.Throws<ArgumentNullException>(() => new Matrix(null));
    }

    [Fact]
    public void Matrix_Reset_Succeeds()
    {
        var expectedMatrix = new Matrix();
        Matrix actualMatrix = DataGenerator.GenerateRandomMatrix();

        actualMatrix.Reset();

        Assert.True(expectedMatrix.Equals(actualMatrix));
    }

    [Theory]
    [InlineData(0)]
    [InlineData(1)]
    [InlineData(2)]
    public void Matrix_Index_Y_Succeeds(int index)
    {
        var expectedData = new[]
        {
            new Vector3(1, 0, 0),
            new Vector3(0, 1, 0),
            new Vector3(0, 0),
        };
        var matrix = new Matrix();
        var actualData = matrix.Data;

        Assert.True(expectedData[index].Equals(actualData[index]));
    }

    [Theory]
    [InlineData(-1)]
    [InlineData(3)]
    public void Matrix_Index_Y_Fails(int index)
    {
        var matrix = new Matrix();
        Vector3 actual = null;

        Assert.Throws<ArgumentOutOfRangeException>(() => actual = matrix[index]);
        Assert.Null(actual);
    }

    [Theory]
    [InlineData(0, 0, 1.0)]
    [InlineData(0, 1, 0.0)]
    [InlineData(0, 2, 0.0)]
    [InlineData(1, 0, 0.0)]
    [InlineData(1, 1, 1.0)]
    [InlineData(1, 2, 0.0)]
    [InlineData(2, 0, 0.0)]
    [InlineData(2, 1, 0.0)]
    [InlineData(2, 2, 1.0)]
    public void Matrix_Index_YX_Succeeds(int y, int x, double expectedValue)
    {
        var matrix = new Matrix();

        Assert.Equal(expectedValue, matrix[y, x]);
    }

    [Theory]
    [InlineData(-1, 0)]
    [InlineData(0, -1)]
    [InlineData(-1, -1)]
    [InlineData(3, 0)]
    [InlineData(3, 3)]
    [InlineData(0, 3)]
    public void Matrix_Index_YX_Fails(int y, int x)
    {
        var matrix = new Matrix();
        double? actual = null;

        Assert.Throws<ArgumentOutOfRangeException>(() => actual = matrix[y, x]);
        Assert.False(actual.HasValue);
    }

    [Theory, MemberData(nameof(Vector2MatrixData))]
    public void Matrix_Multiplication_Vector2_Matrix_Succeeds(Vector2 left, Matrix right, Vector2 expected)
    {
        Vector2 actual = left * right;

        Assert.True(expected.Equals(actual));
    }

    [Theory]
    [InlineData(true, false)]
    [InlineData(false, true)]
    [InlineData(true, true)]
    public void Matrix_Multiplication_Vector2_Matrix_Fails(bool leftNull, bool rightNull)
    {
        Vector2? left = leftNull ? null : DataGenerator.GenerateRandomVector2();
        Matrix? right = rightNull ? null : DataGenerator.GenerateRandomMatrix();
        Vector2 result = null;

        Assert.Throws<ArgumentNullException>(() => result = left * right);
        Assert.Null(result);
    }

    [Theory, MemberData(nameof(MatrixVector2Data))]
    public void Matrix_Multiplication_Matrix_Vector2_Succeeds(Matrix left, Vector2 right, Vector2 expected)
    {
        Vector2 actual = left * right;

        Assert.True(expected.Equals(actual));
    }

    [Theory]
    [InlineData(true, false)]
    [InlineData(false, true)]
    [InlineData(true, true)]
    public void Matrix_Multiplication_Matrix_Vector2_Fails(bool leftNull, bool rightNull)
    {
        Matrix? left = leftNull ? null : DataGenerator.GenerateRandomMatrix();
        Vector2? right = rightNull ? null : DataGenerator.GenerateRandomVector2();
        Vector2? result = null;

        Assert.Throws<ArgumentNullException>(() => result = left * right);
        Assert.Null(result);
    }

    [Theory, MemberData(nameof(Vector3MatrixData))]
    public void Matrix_Multiplication_Vector3_Matrix_Succeeds(Vector3 left, Matrix right, Vector3 expected)
    {
        Vector3 actual = left * right;

        Assert.True(expected.Equals(actual));
    }

    [Theory]
    [InlineData(true, false)]
    [InlineData(false, true)]
    [InlineData(true, true)]
    public void Matrix_Multiplication_Vector3_Matrix_Fails(bool leftNull, bool rightNull)
    {
        Vector3? left = leftNull ? null : DataGenerator.GenerateRandomVector3();
        Matrix? right = rightNull ? null : DataGenerator.GenerateRandomMatrix();
        Vector3? result = null;

        Assert.Throws<ArgumentNullException>(() => result = left * right);
        Assert.Null(result);
    }

    [Theory, MemberData(nameof(MatrixVector3Data))]
    public void Matrix_Multiplication_Matrix_Vector3_Succeeds(Matrix left, Vector3 right, Vector3 expected)
    {
        Vector3 actual = left * right;

        Assert.True(expected.Equals(actual));
    }

    [Theory]
    [InlineData(true, false)]
    [InlineData(false, true)]
    [InlineData(true, true)]
    public void Matrix_Multiplication_Matrix_Vector3_Fails(bool leftNull, bool rightNull)
    {
        Matrix? left = leftNull ? null : DataGenerator.GenerateRandomMatrix();
        Vector3? right = rightNull ? null : DataGenerator.GenerateRandomVector3();
        Vector3? result = null;

        Assert.Throws<ArgumentNullException>(() => result = left * right);
        Assert.Null(result);
    }

    [Theory, MemberData(nameof(MatrixMatrixData))]
    public void Matrix_Multiplication_Matrix_Matrix_Succeeds(Matrix left, Matrix right, Matrix expected)
    {
        Matrix actual = left * right;

        Assert.True(expected.Equals(actual));
    }

    [Theory]
    [InlineData(true, false)]
    [InlineData(false, true)]
    [InlineData(true, true)]
    public void Matrix_Multiplication_Matrix_Matrix_Fails(bool leftNull, bool rightNull)
    {
        Matrix? left = leftNull ? null : DataGenerator.GenerateRandomMatrix();
        Matrix? right = rightNull ? null : DataGenerator.GenerateRandomMatrix();
        Matrix? result = null;

        Assert.Throws<ArgumentNullException>(() => result = left * right);
        Assert.Null(result);
    }
}