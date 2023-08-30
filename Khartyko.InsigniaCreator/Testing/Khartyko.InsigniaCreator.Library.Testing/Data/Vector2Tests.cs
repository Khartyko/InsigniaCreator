/** \addtogroup LibraryTests
 * @{
 */

using Khartyko.InsigniaCreator.Library.Data;
using Khartyko.InsigniaCreator.Library.Testing.Utility;

#pragma warning disable CS8600, CS8604

namespace Khartyko.InsigniaCreator.Library.Testing.Data;

public class Vector2Tests
{
    #region Create

    #region FromSingleValue

    [Theory]
    [InlineData(0)]
    [InlineData(1.0)]
    [InlineData(-42.0)]
    public void Create_FromSingleValue_Succeeds(double value)
    {
        var vec = new Vector2(value);

        Assert.NotNull(vec);
        Assert.Equal(value, vec.X);
        Assert.Equal(value, vec.Y);
    }

    [Fact]
    public void Create_FromSingleValue_Fails()
    {
        Assert.Throws<ArgumentException>(() => new Vector2(double.NaN));
        Assert.Throws<ArgumentException>(() => new Vector2(double.PositiveInfinity));
        Assert.Throws<ArgumentException>(() => new Vector2(double.NegativeInfinity));
    }

    #endregion FromSingleValue

    #region FromXY

    [Theory]
    [InlineData(1.0, 1.0)]
    [InlineData(7.0, -6.0)]
    [InlineData(-3.5, 1.0)]
    [InlineData(-1.0, -42.0)]
    public void Create_FromXY_Succeeds(double x, double y)
    {
        var vec = new Vector2(x, y);

        Assert.NotNull(vec);
        Assert.Equal(x, vec.X);
        Assert.Equal(y, vec.Y);
    }

    [Theory]
    [InlineData(double.NaN, 1.0)]
    [InlineData(double.PositiveInfinity, 1.0)]
    [InlineData(double.NegativeInfinity, 1.0)]
    [InlineData(1.0, double.NaN)]
    [InlineData(1.0, double.PositiveInfinity)]
    [InlineData(1.0, double.NegativeInfinity)]
    public void Create_FromXY_Fails(double x, double y)
    {
        Assert.Throws<ArgumentException>(() => new Vector2(x, y));
    }

    #endregion FromXY

    #region FromVector2

    [Theory]
    [InlineData(1.0, 1.0)]
    [InlineData(7.0, -6.0)]
    [InlineData(-3.5, 1.0)]
    [InlineData(-1.0, -42.0)]
    public void Create_FromVector2_Succeeds(double x, double y)
    {
        var initial = new Vector2(x, y);
        var duplicate = new Vector2(initial);

        Assert.Equal(initial.X, duplicate.X);
        Assert.Equal(initial.Y, duplicate.Y);
    }

    [Fact]
    public void Create_FromVector2_Fails()
    {
        Vector2 nullVector = null;

        Assert.Throws<ArgumentNullException>(() => new Vector2(nullVector));
    }

    #endregion FromVector2

    #region FromVector3

    [Theory]
    [InlineData(1.0, 1.0, 1.0)]
    [InlineData(0.0, 6.0, 0.0)]
    [InlineData(-7.0, -3.5, -4.2)]
    public void Create_FromVector3_Succeeds(double x, double y, double z)
    {
        var initial = new Vector3(x, y, z);
        var duplicate = new Vector2(initial);

        Assert.Equal(initial.X, duplicate.X);
        Assert.Equal(initial.Y, duplicate.Y);
    }

    [Fact]
    public void Create_FromVector3_Fails()
    {
        Vector3 nullVector = null;

        Assert.Throws<ArgumentNullException>(() => new Vector2(nullVector));
    }

    #endregion FromVector3

    #endregion Create

    #region X

    [Theory]
    [InlineData(1.0, 1.0, 0.0)]
    [InlineData(7.0, -6.0, 3.1)]
    [InlineData(-3.5, 1.0, -2.5)]
    [InlineData(-1.0, -42.0, -3.2)]
    public void X_Succeeds(double x, double y, double valueUpdate)
    {
        var vec = new Vector2(x, y);

        Assert.Equal(x, vec.X);

        vec.X = valueUpdate;

        Assert.Equal(valueUpdate, vec.X);
        Assert.NotEqual(x, vec.X);
    }

    [Theory]
    [InlineData(1.0, 1.0, double.NaN)]
    [InlineData(7.0, -6.0, double.PositiveInfinity)]
    [InlineData(-3.5, 1.0, double.NegativeInfinity)]
    public void X_Fails(double x, double y, double valueUpdate)
    {
        var vec = new Vector2(x, y);

        Assert.Throws<ArgumentException>(() => vec.X = valueUpdate);
        Assert.Equal(x, vec.X);
    }

    #endregion X

    #region Y

    [Theory]
    [InlineData(1.0, 1.0, 0.0)]
    [InlineData(7.0, -6.0, 3.1)]
    [InlineData(-3.5, 1.0, -2.5)]
    [InlineData(-1.0, -42.0, -3.2)]
    public void Y_Succeeds(double x, double y, double valueUpdate)
    {
        var vec = new Vector2(x, y);

        Assert.Equal(y, vec.Y);

        vec.Y = valueUpdate;

        Assert.Equal(valueUpdate, vec.Y);
        Assert.NotEqual(y, vec.Y);
    }

    [Theory]
    [InlineData(1.0, 1.0, double.NaN)]
    [InlineData(7.0, -6.0, double.PositiveInfinity)]
    [InlineData(-3.5, 1.0, double.NegativeInfinity)]
    public void Y_Fails(double x, double y, double valueUpdate)
    {
        var vec = new Vector2(x, y);

        Assert.Throws<ArgumentException>(() => vec.Y = valueUpdate);
        Assert.Equal(y, vec.Y);
    }

    #endregion Y

    #region Length

    [Theory]
    [InlineData(1.0, 1.0, 1.414)]
    [InlineData(3.0, -3.0, 4.243)]
    public void Length_Succeeds(double x, double y, double expectedLength)
    {
        var vec = new Vector2(x, y);

        Assert.Equal(expectedLength, vec.Length);
    }

    #endregion Length

    #region Index

    [Theory]
    [InlineData(1.0, 1.0, 0, 2.0)]
    [InlineData(7.0, -6.0, 1, -3.0)]
    [InlineData(-3.5, 1.0, 0, -1.0)]
    [InlineData(-1.0, -42.0, 1, 1.0)]
    public void Index_Succeeds(double x, double y, int index, double valueUpdate)
    {
        var vec = new Vector2(x, y);

        Assert.Equal(x, vec[0]);
        Assert.Equal(y, vec[1]);

        vec[index] = valueUpdate;

        Assert.Equal(valueUpdate, vec[index]);
    }

    [Theory]
    [InlineData(1.0, 1.0, 2)]
    [InlineData(7.0, -6.0, -1)]
    [InlineData(-3.5, 1.0, 2)]
    [InlineData(-1.0, -42.0, -1)]
    public void Index_BadIndex_Fails(double x, double y, int index)
    {
        var vec = new Vector2(x, y);

        Assert.Throws<ArgumentOutOfRangeException>(() => vec[index]);
    }

    [Theory]
    [InlineData(1.0, 1.0, 0, double.NaN)]
    [InlineData(7.0, -6.0, 1, double.PositiveInfinity)]
    [InlineData(-3.5, 1.0, 0, double.NegativeInfinity)]
    public void Index_BadValueUpdate_Fails(double x, double y, int index, double valueUpdate)
    {
        var vec = new Vector2(x, y);

        Assert.Throws<ArgumentException>(() => vec[index] = valueUpdate);
    }

    [Theory]
    [InlineData(-1)]
    [InlineData(3)]
    public void Index_Set_InvalidIndex_Fails(int index)
    {
        Vector2 vector = DataGenerator.GenerateRandomVector2();
        double value = DataGenerator.GenerateRandomDouble();

        Assert.Throws<ArgumentOutOfRangeException>(() => vector[index] = value);
    }

    #endregion Index

    #region Negation Operator

    [Fact]
    public void NegateVector_Succeeds()
    {
        Vector2 positiveVector = Vector2.One;
        Vector2 negativeVector = -positiveVector;
        
        Assert.NotEqual(positiveVector, negativeVector);
        Assert.Equal(positiveVector.X * -1, negativeVector.X);
        Assert.Equal(positiveVector.Y * -1, negativeVector.Y);
    }

    [Fact]
    public void NegateVector_NullVector_Fails()
    {
        Vector2 nullVector = null;

        Assert.Throws<ArgumentNullException>(() => -nullVector);
    }

    #endregion Negation Operator
    
    #region AdditionOperator

    [Theory]
    [InlineData(7.0, -6.0, -5.0, 3.2)]
    [InlineData(-3.5, 1.0, 7.6, 42)]
    [InlineData(-1.0, -42.0, 4.2, 103.9)]
    public void AdditionOperator_BothVector2_Succeeds(double x0, double y0, double x1, double y1)
    {
        var vec0 = new Vector2(x0, y0);
        var vec1 = new Vector2(x1, y1);

        Vector2? additionResult = vec0 + vec1;

        Assert.Equal(x0 + x1, additionResult.X);
        Assert.Equal(y0 + y1, additionResult.Y);
    }

    [Fact]
    public void AdditionOperator_BothVector2_Fails()
    {
        Vector2 nullVector = null;

        var vector = new Vector2(1);

        Assert.Throws<ArgumentNullException>(() => nullVector + vector);
        Assert.Throws<ArgumentNullException>(() => vector + nullVector);
    }

    [Fact]
    public void AdditionOperator_Vector2AndDouble_Succeeds()
    {
        Vector2 vector = Vector2.Zero;
        double value = DataGenerator.GenerateRandomDouble();

        Vector2 actualVector = vector + value;
        
        Assert.Equal(value, actualVector.X);
        Assert.Equal(value, actualVector.Y);
    }

    [Fact]
    public void AdditionOperator_Vector2AndDouble_NullVector_Fails()
    {
        Vector2 nullVector = null;
        double value = DataGenerator.GenerateRandomDouble();

        Assert.Throws<ArgumentNullException>(() => nullVector + value);
    }

    [Fact]
    public void AdditionOperator_Vector2AndDouble_InvalidDouble_Fails()
    {
        Vector2 vector = Vector2.Zero;

        Assert.Throws<ArgumentException>(() => vector + double.NaN);
        Assert.Throws<ArgumentException>(() => vector + double.PositiveInfinity);
        Assert.Throws<ArgumentException>(() => vector + double.NegativeInfinity);
    }

    [Fact]
    public void AdditionOperator_DoubleAndVector2_Succeeds()
    {
        Vector2 vector = Vector2.Zero;
        double value = DataGenerator.GenerateRandomDouble();

        Vector2 actualVector = value + vector;
        
        Assert.Equal(value, actualVector.X);
        Assert.Equal(value, actualVector.Y);
    }

    [Fact]
    public void AdditionOperator_DoubleAndVector2_NullVector_Fails()
    {
        Vector2 nullVector = null;
        double value = DataGenerator.GenerateRandomDouble();

        Assert.Throws<ArgumentNullException>(() => value + nullVector);
    }

    [Fact]
    public void AdditionOperator_DoubleAndVector2_InvalidDouble_Fails()
    {
        Vector2 vector = Vector2.Zero;

        Assert.Throws<ArgumentException>(() => double.NaN + vector);
        Assert.Throws<ArgumentException>(() => double.PositiveInfinity + vector);
        Assert.Throws<ArgumentException>(() => double.NegativeInfinity + vector);
    }

    #endregion AdditionOperator

    #region SubtractionOperator

    [Theory]
    [InlineData(7.0, -6.0, -5.0, 3.2)]
    [InlineData(-3.5, 1.0, 7.6, 42)]
    [InlineData(-1.0, -42.0, 4.2, 103.9)]
    public void SubtractionOperator_BothVector2_Succeeds(double x0, double y0, double x1, double y1)
    {
        var vec0 = new Vector2(x0, y0);
        var vec1 = new Vector2(x1, y1);

        Vector2 subtractionResult = vec0 - vec1;

        Assert.Equal(x0 - x1, subtractionResult.X);
        Assert.Equal(y0 - y1, subtractionResult.Y);
    }

    [Fact]
    public void SubtractionOperator_BothVector2_Fails()
    {
        Vector2 nullVector = null;

        var vector = new Vector2(1);

        Assert.Throws<ArgumentNullException>(() => nullVector - vector);
        Assert.Throws<ArgumentNullException>(() => vector - nullVector);
    }

    [Fact]
    public void SubtractionOperator_Vector2AndDouble_Succeeds()
    {
        Vector2 vector = Vector2.Zero;
        double value = DataGenerator.GenerateRandomDouble();

        Vector2 actualVector = vector - value;
        
        Assert.Equal(-value, actualVector.X);
        Assert.Equal(-value, actualVector.Y);
    }

    [Fact]
    public void SubtractionOperator_Vector2AndDouble_NullVector_Fails()
    {
        Vector2 nullVector = null;
        double value = DataGenerator.GenerateRandomDouble();

        Assert.Throws<ArgumentNullException>(() => nullVector - value);
    }

    [Fact]
    public void SubtractionOperator_Vector2AndDouble_InvalidDouble_Fails()
    {
        Vector2 vector = Vector2.Zero;

        Assert.Throws<ArgumentException>(() => vector - double.NaN);
        Assert.Throws<ArgumentException>(() => vector - double.PositiveInfinity);
        Assert.Throws<ArgumentException>(() => vector - double.NegativeInfinity);
    }

    [Fact]
    public void SubtractionOperator_DoubleAndVector2_Succeeds()
    {
        Vector2 vector = Vector2.Zero;
        double value = DataGenerator.GenerateRandomDouble();

        Vector2 actualVector = value - vector;
        
        Assert.Equal(value, actualVector.X);
        Assert.Equal(value, actualVector.Y);
    }

    [Fact]
    public void SubtractionOperator_DoubleAndVector2_NullVector_Fails()
    {
        Vector2 nullVector = null;
        double value = DataGenerator.GenerateRandomDouble();

        Assert.Throws<ArgumentNullException>(() => value - nullVector);
    }

    [Fact]
    public void SubtractionOperator_DoubleAndVector2_InvalidDouble_Fails()
    {
        Vector2 vector = Vector2.Zero;

        Assert.Throws<ArgumentException>(() => double.NaN - vector);
        Assert.Throws<ArgumentException>(() => double.PositiveInfinity - vector);
        Assert.Throws<ArgumentException>(() => double.NegativeInfinity - vector);
    }

    #endregion SubtractionOperator

    #region Multiplication

    #region BothVector2

    [Theory]
    [InlineData(1, 1, 1, 1, 1, 1)]
    [InlineData(10, 0.1, 0.1, 10, 1, 1)]
    [InlineData(2, -3.5, -42, 39, -84, -136.5)]
    public void MultiplyOperator_BothVector2_Succeeds(double x1, double y1, double x2, double y2,
        double expectedX, double expectedY)
    {
        var vec1 = new Vector2(x1, y1);
        var vec2 = new Vector2(x2, y2);

        var expectedResult = new Vector2(expectedX, expectedY);
        Vector2 actualResult = vec1 * vec2;

        Assert.Equal(expectedResult, actualResult);
    }

    [Fact]
    public void MultiplyOperator_BothVector2_Fails()
    {
        Vector2 nullVector = null;
        Vector2 goodVector = Vector2.One;

        Assert.Throws<ArgumentNullException>(() => nullVector * goodVector);
        Assert.Throws<ArgumentNullException>(() => goodVector * nullVector);
        Assert.Throws<ArgumentNullException>(() => nullVector * nullVector);
    }

    #endregion BothVector2

    #region Vector2AndDouble

    [Theory]
    [InlineData(1, 1, 1, 1, 1)]
    public void MultiplyOperator_Vector2AndDouble_Succeeds(double x, double y, double value, double expectedX,
        double expectedY)
    {
        var testVector = new Vector2(x, y);

        var expectedResult = new Vector2(expectedX, expectedY);
        Vector2 actualResult = testVector * value;

        Assert.Equal(expectedResult, actualResult);
    }

    [Theory]
    [InlineData(10, 1, double.NaN)]
    [InlineData(1, -10, double.PositiveInfinity)]
    [InlineData(-10, -1, double.NegativeInfinity)]
    public void MultiplyOperator_Vector2AndDouble_Fails(double x, double y, double value)
    {
        var vector = new Vector2(x, y);

        Assert.Throws<ArgumentException>(() => vector * value);
    }

    [Theory]
    [InlineData(-3.5)]
    [InlineData(42)]
    [InlineData(10000)]
    [InlineData(-15)]
    [InlineData(3.1415926)]
    public void MultiplyOperator_Vector2AndDouble_NullVector_Fails(double validValue)
    {
        Vector2 nullVector = null;

        Assert.Throws<ArgumentNullException>(() => nullVector * validValue);
    }

    #endregion Vector2AndDouble

    #region DoubleAndVector2

    [Theory]
    [InlineData(1, 1, 1, 1, 1)]
    public void MultiplyOperator_DoubleAndVector2_Succeeds(double value, double x, double y, double expectedX,
        double expectedY)
    {
        var testVector = new Vector2(x, y);
        var expectedVector = new Vector2(expectedX, expectedY);

        Vector2 actualVector = value * testVector;
        
        Assert.Equal(expectedVector, actualVector);
    }

    [Theory]
    [InlineData(double.NaN)]
    [InlineData(double.PositiveInfinity)]
    [InlineData(double.NegativeInfinity)]
    public void MultiplyOperator_DoubleAndVector2_Fails(double invalidDouble)
    {
        Vector2 validVector = Vector2.Zero;

        Assert.Throws<ArgumentException>(() => invalidDouble * validVector);
    }

    [Theory]
    [InlineData(-3.5)]
    [InlineData(42)]
    [InlineData(10000)]
    [InlineData(-15)]
    [InlineData(3.1415926)]
    public void MultiplyOperator_DoubleAndNullVector_Fails(double validValue)
    {
        Vector2 nullVector = null;

        Assert.Throws<ArgumentNullException>(() => validValue * nullVector);
    }
    
    #endregion DoubleAndVector2

    #endregion Multiplication

    #region Division

    #region BothVector2

    [Theory]
    [InlineData(1, 1, 1, 1, 1, 1)]
    public void DivisionOperator_BothVector2_Succeeds(double x1, double y1, double x2, double y2,
        double expectedX, double expectedY)
    {
        var vec1 = new Vector2(x1, y1);
        var vec2 = new Vector2(x2, y2);

        var expectedResult = new Vector2(expectedX, expectedY);
        Vector2 actualResult = vec1 / vec2;

        Assert.True(expectedResult.Equals(actualResult));
    }

    [Fact]
    public void DivisionOperator_BothVector2_Fails()
    {
        Vector2 nullVector = null;
        Vector2 validVector = Vector2.Zero;

        Assert.Throws<ArgumentNullException>(() => validVector / nullVector);
        Assert.Throws<ArgumentNullException>(() => nullVector / validVector);
    }

    #endregion BothVector2

    #region Vector2AndDouble

    [Theory]
    [InlineData(1, 1, 1, 1, 1)]
    public void DivisionOperator_Vector2AndDouble_Succeeds(double x, double y, double value, double expectedX,
        double expectedY)
    {
        var vec1 = new Vector2(x, y);

        var expectedResult = new Vector2(expectedX, expectedY);
        Vector2 actualResult = vec1 / value;

        Assert.Equal(expectedResult, actualResult);
    }

    [Theory]
    [InlineData(double.NaN)]
    [InlineData(double.PositiveInfinity)]
    [InlineData(double.NegativeInfinity)]
    public void DivisionOperator_Vector2AndDouble_Fails(double invalidDouble)
    {
        Vector2 validVector = Vector2.Zero;

        Assert.Throws<ArgumentException>(() => validVector / invalidDouble);
    }

    [Fact]
    public void DivisionOperator_Vector2AndDouble_ZeroValue_Fails()
    {
        Vector2 actualResult = Vector2.One / 0.0;
        
        Assert.Equal(Vector2.Zero, actualResult);
    }

    [Theory]
    [InlineData(-3.5)]
    [InlineData(42)]
    [InlineData(10000)]
    [InlineData(-15)]
    [InlineData(3.1415926)]
    public void DivisionOperator_Vector2AndDouble_NullVector_Fails(double validDouble)
    {
        Vector2 nullVector = null;

        Assert.Throws<ArgumentNullException>(() => nullVector / validDouble);
    }
    
    #endregion Vector2AndDouble

    #region DoubleAndVector2

    [Theory]
    [InlineData(1, 1, 1, 1, 1)]
    public void DivisionOperator_DoubleAndVector2_Succeeds(double value, double x, double y, double expectedX,
        double expectedY)
    {
        var testVector = new Vector2(x, y);
        var expectedVector = new Vector2(expectedX, expectedY);

        Vector2 actualVector = value / testVector;
        
        Assert.Equal(expectedVector, actualVector);
    }

    [Fact]
    public void DivisionOperator_DoubleAndVector2_DoubleIsZero_Succeeds()
    {
        Vector2 testVector = DataGenerator.GenerateRandomVector2();
        const double zero = 0.0;
        
        Assert.Equal(Vector2.Zero, zero / testVector);
    }

    [Theory]
    [InlineData(double.NaN)]
    [InlineData(double.PositiveInfinity)]
    [InlineData(double.NegativeInfinity)]
    public void DivisionOperator_DoubleAndVector2_Fails(double invalidDouble)
    {
        Vector2 validVector = Vector2.Zero;

        Assert.Throws<ArgumentException>(() => invalidDouble / validVector);
    }

    [Theory]
    [InlineData(-3.5)]
    [InlineData(42)]
    [InlineData(10000)]
    [InlineData(-15)]
    [InlineData(3.1415926)]
    public void DivisionOperator_DoubleAndVector2_NullVector_Fails(double validDouble)
    {
        Vector2 nullVector = null;

        Assert.Throws<ArgumentNullException>(() => validDouble / nullVector);
    }
    
    #endregion DoubleAndVector2

    #endregion Division

    #region Equals

    [Theory]
    [InlineData(1, 1)]
    [InlineData(2, -1)]
    public void Equals_Succeeds(double x, double y)
    {
        var vec0 = new Vector2(x, y);
        var vec1 = new Vector2(x, y);

        Assert.True(vec0.Equals(vec1));
        // ReSharper disable EqualExpressionComparison
        Assert.True(vec0.Equals(vec0));
        Assert.True(vec1.Equals(vec1));
        // ReSharper restore EqualExpressionComparison
    }

    [Theory]
    [InlineData(1, 2, -3, -3)]
    [InlineData(-1, -2, 3, 1)]
    public void Equals_Fails_NoBadData(double x0, double y0, double x1, double y1)
    {
        var vec0 = new Vector2(x0, y0);
        var vec1 = new Vector2(x1, y1);

        Assert.False(vec0.Equals(vec1));
    }

    [Fact]
    public void Equals_Fails_BadData()
    {
        Vector2 validVector = Vector2.One;
        Vector2 nullVector = null;

        Assert.False(validVector.Equals(nullVector));
    }

    #endregion Equals
}

/** @} */