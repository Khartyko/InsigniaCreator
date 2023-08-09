using Khartyko.InsigniaCreator.Library.Data;
using Khartyko.InsigniaCreator.Library.Testing.Utility;

#pragma warning disable CS8600, CS8604

namespace Khartyko.InsigniaCreator.Library.Testing.Data;

public class Vector3Tests
{
	#region Create

	[Theory]
	[InlineData(1.0)]
	[InlineData(-1.0)]
	[InlineData(-3.2)]
	public void Create_FromSingleValue_Succeeds(double value)
	{
		var vec = new Vector3(value);

		Assert.Equal(value, vec.X);
		Assert.Equal(value, vec.Y);
		Assert.Equal(1, vec.Z);
	}

	[Theory]
	[InlineData(double.NaN)]
	[InlineData(double.PositiveInfinity)]
	[InlineData(double.NegativeInfinity)]
	public void Create_FromSingleValue_Fails(double value)
	{
		Assert.Throws<ArgumentException>(() => new Vector3(value));
	}

	[Theory]
	[InlineData(1, 2)]
	[InlineData(-2, 2)]
	public void Create_FromXY_Succeeds(double x, double y)
	{
		var vec = new Vector3(x, y);

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
		Assert.Throws<ArgumentException>(() => new Vector3(x, y));
	}

	[Theory]
	[InlineData(1.0, 1.0, 1.0)]
	[InlineData(-2.0, 1.0, -3.0)]
	public void Create_FromXYZ_Succeeds(double x, double y, double z)
	{
		var vec = new Vector3(x, y, z);

		Assert.Equal(x, vec.X);
		Assert.Equal(y, vec.Y);
		Assert.Equal(z, vec.Z);
	}

	[Theory]
	[InlineData(double.NaN, 0, 0)]
	[InlineData(double.PositiveInfinity, 0, 0)]
	[InlineData(double.NegativeInfinity, 0, 0)]
	[InlineData(0, double.NaN, 0)]
	[InlineData(0, double.PositiveInfinity, 0)]
	[InlineData(0, double.NegativeInfinity, 0)]
	[InlineData(0, 0, double.NaN)]
	[InlineData(0, 0, double.PositiveInfinity)]
	[InlineData(0, 0, double.NegativeInfinity)]
	public void Create_FromXYZ_Fails(double x, double y, double z)
	{
		Assert.Throws<ArgumentException>(() => new Vector3(x, y, z));
	}

	[Theory]
	[InlineData(1, 2)]
	[InlineData(-2, 2)]
	public void Create_FromExistingVector2_Succeeds(double x, double y)
	{
		var initial = new Vector2(x, y);
		var duplicate = new Vector3(initial);

		Assert.Equal(x, duplicate.X);
		Assert.Equal(y, duplicate.Y);
	}

	[Fact]
	public void Create_FromExistingVector2_Fails()
	{
		Vector2 nullVector = null;

		Assert.Throws<ArgumentNullException>(() => new Vector3(nullVector));
	}

	[Theory]
	[InlineData(1.0, 1.0, 1.0)]
	[InlineData(-2.0, 1.0, -3.0)]
	public void Create_FromExistingSucceeds(double x, double y, double z)
	{
		var initial = new Vector3(x, y, z);
		var duplicate = new Vector3(initial);

		Assert.Equal(x, duplicate.X);
		Assert.Equal(y, duplicate.Y);
		Assert.Equal(z, duplicate.Z);
	}

	[Fact]
	public void Create_FromExistingFails()
	{
		Vector3 nullVector = null;

		Assert.Throws<ArgumentNullException>(() => new Vector3(nullVector));
	}

	#endregion Create

	#region X

	[Theory]
	[InlineData(1.0, 1.0, 1.0, 0.0)]
	[InlineData(-2.0, 1.0, -3.0, 2.5)]
	public void X_Succeeds(double x, double y, double z, double xUpdate)
	{
		var vec = new Vector3(x, y, z);

		Assert.Equal(x, vec.X);
		vec.X = xUpdate;
		Assert.Equal(xUpdate, vec.X);
		Assert.NotEqual(x, vec.X);
	}

	[Theory]
	[InlineData(1.0, 1.0, 1.0, double.NaN)]
	[InlineData(1.0, 1.0, 1.0, double.PositiveInfinity)]
	[InlineData(1.0, 1.0, 1.0, double.NegativeInfinity)]
	public void X_Fails(double x, double y, double z, double xUpdate)
	{
		var vec = new Vector3(x, y, z);

		Assert.Throws<ArgumentException>(() => vec.X = xUpdate);
		Assert.Equal(x, vec.X);
	}

	#endregion X

	#region Y

	[Theory]
	[InlineData(1.0, 1.0, 1.0, 0.0)]
	[InlineData(-2.0, 1.0, -3.0, 2.5)]
	public void Y_Succeeds(double x, double y, double z, double yUpdate)
	{
		var vec = new Vector3(x, y, z);
		Assert.Equal(y, vec.Y);
		vec.Y = yUpdate;
		Assert.Equal(yUpdate, vec.Y);
		Assert.NotEqual(y, vec.Y);
	}

	[Theory]
	[InlineData(1.0, 1.0, 1.0, double.NaN)]
	[InlineData(1.0, 1.0, 1.0, double.PositiveInfinity)]
	[InlineData(1.0, 1.0, 1.0, double.NegativeInfinity)]
	public void Y_Fails(double x, double y, double z, double yUpdate)
	{
		var vec = new Vector3(x, y, z);

		Assert.Throws<ArgumentException>(() => vec.Y = yUpdate);
		Assert.Equal(x, vec.X);
	}

	#endregion Y

	#region Z

	[Theory]
	[InlineData(1.0, 1.0, 1.0, 0.0)]
	[InlineData(-2.0, 1.0, -3.0, 2.5)]
	public void Z_Succeeds(double x, double y, double z, double zUpdate)
	{
		var vec = new Vector3(x, y, z);
		Assert.Equal(z, vec.Z);
		vec.Z = zUpdate;
		Assert.Equal(zUpdate, vec.Z);
		Assert.NotEqual(z, vec.Z);
	}

	[Theory]
	[InlineData(1.0, 1.0, 1.0, double.NaN)]
	[InlineData(1.0, 1.0, 1.0, double.PositiveInfinity)]
	[InlineData(1.0, 1.0, 1.0, double.NegativeInfinity)]
	public void Z_Fails(double x, double y, double z, double zUpdate)
	{
		var vec = new Vector3(x, y, z);

		Assert.Throws<ArgumentException>(() => vec.Z = zUpdate);
		Assert.Equal(x, vec.X);
	}

	#endregion Z

	#region Swizzling

	#region Vector2 Swizzling

	[Fact]
	public void Vector2_Swizzling_XY_Succeeds()
	{
		Vector3 subject = DataGenerator.GenerateRandomVector3();

		Vector2 actualValue = subject.XY;

		Assert.Equal(subject.X, actualValue.X);
		Assert.Equal(subject.Y, actualValue.Y);
	}

	[Fact]
	public void Vector2_Swizzling_XZ_Succeeds()
	{
		Vector3 subject = DataGenerator.GenerateRandomVector3();

		Vector2 actualValue = subject.XZ;

		Assert.Equal(subject.X, actualValue.X);
		Assert.Equal(subject.Z, actualValue.Y);
	}

	[Fact]
	public void Vector2_Swizzling_YX_Succeeds()
	{
		Vector3 subject = DataGenerator.GenerateRandomVector3();

		Vector2 actualValue = subject.YX;

		Assert.Equal(subject.Y, actualValue.X);
		Assert.Equal(subject.X, actualValue.Y);
	}

	[Fact]
	public void Vector2_Swizzling_YZ_Succeeds()
	{
		Vector3 subject = DataGenerator.GenerateRandomVector3();

		Vector2 actualValue = subject.YZ;

		Assert.Equal(subject.Y, actualValue.X);
		Assert.Equal(subject.Z, actualValue.Y);
	}

	[Fact]
	public void Vector2_Swizzling_ZX_Succeeds()
	{
		Vector3 subject = DataGenerator.GenerateRandomVector3();

		Vector2 actualValue = subject.ZX;

		Assert.Equal(subject.Z, actualValue.X);
		Assert.Equal(subject.X, actualValue.Y);
	}

	[Fact]
	public void Vector2_Swizzling_ZY_Succeeds()
	{
		Vector3 subject = DataGenerator.GenerateRandomVector3();

		Vector2 actualValue = subject.ZY;

		Assert.Equal(subject.Z, actualValue.X);
		Assert.Equal(subject.Y, actualValue.Y);
	}

	#endregion Vector2 Swizzling

	#region Vector3 Swizzling

	[Fact]
	public void Vector3_Swizzling_XYZ_Succeeds()
	{
		Vector3 subject = DataGenerator.GenerateRandomVector3();

		Vector3 actualValue = subject.XYZ;

		Assert.Equal(subject.X, actualValue.X);
		Assert.Equal(subject.Y, actualValue.Y);
		Assert.Equal(subject.Z, actualValue.Z);
	}

	[Fact]
	public void Vector3_Swizzling_ZYX_Succeeds()
	{
		Vector3 subject = DataGenerator.GenerateRandomVector3();

		Vector3 actualValue = subject.ZYX;

		Assert.Equal(subject.Z, actualValue.X);
		Assert.Equal(subject.Y, actualValue.Y);
		Assert.Equal(subject.X, actualValue.Z);
	}

	[Fact]
	public void Vector3_Swizzling_YXZ_Succeeds()
	{
		Vector3 subject = DataGenerator.GenerateRandomVector3();

		Vector3 actualValue = subject.YXZ;

		Assert.Equal(subject.Y, actualValue.X);
		Assert.Equal(subject.X, actualValue.Y);
		Assert.Equal(subject.Z, actualValue.Z);
	}

	[Fact]
	public void Vector3_Swizzling_ZXY_Succeeds()
	{
		Vector3 subject = DataGenerator.GenerateRandomVector3();

		Vector3 actualValue = subject.ZXY;

		Assert.Equal(subject.Z, actualValue.X);
		Assert.Equal(subject.X, actualValue.Y);
		Assert.Equal(subject.Y, actualValue.Z);
	}

	[Fact]
	public void Vector3_Swizzling_XZY_Succeeds()
	{
		Vector3 subject = DataGenerator.GenerateRandomVector3();

		Vector3 actualValue = subject.XZY;

		Assert.Equal(subject.X, actualValue.X);
		Assert.Equal(subject.Z, actualValue.Y);
		Assert.Equal(subject.Y, actualValue.Z);
	}

	[Fact]
	public void Vector3_Swizzling_YZX_Succeeds()
	{
		Vector3 subject = DataGenerator.GenerateRandomVector3();

		Vector3 actualValue = subject.YZX;

		Assert.Equal(subject.Y, actualValue.X);
		Assert.Equal(subject.Z, actualValue.Y);
		Assert.Equal(subject.X, actualValue.Z);
	}

	#endregion Vector3 Swizzling

	#endregion Swizzling

	#region Length

	[Theory]
	[InlineData(1, 1, 1, 1.732)]
	[InlineData(5, 5, 5, 8.660)]
	public void Length_Succeeds(double x, double y, double z, double expectedLength)
	{
		var vec = new Vector3(x, y, z);

		Assert.Equal(expectedLength, vec.Length);
	}

	#endregion Length

	#region Index

	[Theory]
	[InlineData(1, 1, 1, 0, 2)]
	[InlineData(2, -1, 3, 1, 5)]
	public void Index_Succeeds(double x, double y, double z, int index, double valueUpdate)
	{
		var vec = new Vector3(x, y, z);

		vec[index] = valueUpdate;
		Assert.Equal(valueUpdate, vec[index]);
	}

	[Theory]
	[InlineData(1, 1, 1, -1)]
	[InlineData(2, -1, 3, 3)]
	public void Index_Fails_BadIndex(double x, double y, double z, int index)
	{
		var vec = new Vector3(x, y, z);

		Assert.Throws<ArgumentOutOfRangeException>(() => vec[index]);
	}

	[Theory]
	[InlineData(1, 1, 1, 0, double.NaN)]
	[InlineData(2, -1, 3, 1, double.PositiveInfinity)]
	[InlineData(2, -1, 3, 1, double.NegativeInfinity)]
	public void Index_Fails_BadValueUpdate(double x, double y, double z, int index, double valueUpdate)
	{
		var vec = new Vector3(x, y, z);

		Assert.Throws<ArgumentException>(() => vec[index] = valueUpdate);
	}

	#endregion Index

	#region Operator +

	#region Vector3 + Vector3

	[Fact]
	public void AdditionOperator_BothVector3_Succeeds()
	{
		Vector3 vec0 = DataGenerator.GenerateRandomVector3();
		Vector3 vec1 = DataGenerator.GenerateRandomVector3();

		var expectedValues = new Vector3(
			vec0.X + vec1.X,
			vec0.Y + vec1.Y,
			vec0.Z + vec1.Z
		);

		Assert.Equal(expectedValues, vec0 + vec1);
	}

	[Fact]
	public void AdditionOperator_BothVector3_Fails()
	{
		Vector3 nullVector = null;
		var vec = new Vector3(1);

		Assert.Throws<ArgumentNullException>(() => vec + nullVector);
		Assert.Throws<ArgumentNullException>(() => nullVector + vec);
	}

	#endregion Vector3 + Vector3

	#region Vector2 + Vector3

	[Fact]
	public void AdditionOperator_Vector2AndVector3_Succeeds()
	{
		Vector2 left = DataGenerator.GenerateRandomVector2();
		Vector3 right = DataGenerator.GenerateRandomVector3();

		var expectedValues = new Vector2(left.X + right.X, left.Y + right.Y);

		Assert.Equal(expectedValues, left + right);
	}

	[Fact]
	public void AdditionOperator_Vector2AndVector3_Fails()
	{
		Vector2 nullVec2 = null;
		Vector3 nullVec3 = null;
		Vector2 vec2 = DataGenerator.GenerateRandomVector2();
		Vector3 vec3 = DataGenerator.GenerateRandomVector3();

		Assert.Throws<ArgumentNullException>(() => vec2 + nullVec3);
		Assert.Throws<ArgumentNullException>(() => nullVec2 + vec3);
	}

	#endregion Vector2 + Vector3

	#region Vector3 + Vector2

	[Fact]
	public void AdditionOperator_Vector3AndVector2_Succeeds()
	{
		Vector3 left = DataGenerator.GenerateRandomVector3();
		Vector2 right = DataGenerator.GenerateRandomVector2();

		var expectedValues = new Vector3(left.X + right.X, left.Y + right.Y, left.Z);

		Assert.Equal(expectedValues, left + right);
	}

	[Fact]
	public void AdditionOperator_Vector3AndVector2_Fails()
	{
		Vector2 nullVec2 = null;
		Vector3 nullVec3 = null;
		Vector2 vec2 = DataGenerator.GenerateRandomVector2();
		Vector3 vec3 = DataGenerator.GenerateRandomVector3();

		Assert.Throws<ArgumentNullException>(() => vec3 + nullVec2);
		Assert.Throws<ArgumentNullException>(() => nullVec3 + vec2);
	}

	#endregion Vector3 + Vector2

	#region Vector3 + Double

	[Fact]
	public void AdditionOperator_Vector3AndDouble_Succeeds()
	{
		Vector3 vector = DataGenerator.GenerateRandomVector3();
		double value = DataGenerator.GenerateRandomDouble();

		var expectedValues = new Vector3(vector.X + value, vector.Y + value, vector.Z + value);

		Assert.Equal(expectedValues, vector + value);
	}

	[Theory]
	[InlineData(double.NaN)]
	[InlineData(double.PositiveInfinity)]
	[InlineData(double.NegativeInfinity)]
	public void AdditionOperator_Vector3AndDouble_Fails(double invalidDouble)
	{
		Vector3 vector = DataGenerator.GenerateRandomVector3();

		Assert.Throws<ArgumentException>(() => vector + invalidDouble);
	}

	#endregion Vector3 + Double

	#region Double + Vector3

	[Fact]
	public void AdditionOperator_DoubleAndVector3_Succeeds()
	{
		double value = DataGenerator.GenerateRandomDouble();
		Vector3 vector = DataGenerator.GenerateRandomVector3();

		var expectedValues = new Vector3(value + vector.X, value + vector.Y, value + vector.Z);

		Assert.Equal(expectedValues, value + vector);
	}

	[Theory]
	[InlineData(double.NaN)]
	[InlineData(double.PositiveInfinity)]
	[InlineData(double.NegativeInfinity)]
	public void AdditionOperator_DoubleAndVector3_Fails(double invalidDouble)
	{
		Vector3 vector = DataGenerator.GenerateRandomVector3();

		Assert.Throws<ArgumentException>(() => invalidDouble + vector);
	}

	#endregion Double + Vector3

	#endregion Operator +

	#region Operator -

	#region Vector3 - Vector3

	[Fact]
	public void SubtractionOperator_BothVector3_Succeeds()
	{
		Vector3 left = DataGenerator.GenerateRandomVector3();
		Vector3 right = DataGenerator.GenerateRandomVector3();

		var expectedValues = new Vector3(
			left.X - right.X,
			left.Y - right.Y,
			left.Z - right.Z
		);

		Assert.Equal(expectedValues, left - right);
	}

	[Fact]
	public void SubtractionOperator_BothVector3_Fails()
	{
		Vector3 nullVector = null;
		var vec = new Vector3(1);

		Assert.Throws<ArgumentNullException>(() => vec - nullVector);
		Assert.Throws<ArgumentNullException>(() => nullVector - vec);
	}

	#endregion Vector3 - Vector3

	#region Vector2 - Vector3

	[Fact]
	public void SubtractionOperator_Vector2AndVector3_Succeeds()
	{
		Vector2 left = DataGenerator.GenerateRandomVector2();
		Vector3 right = DataGenerator.GenerateRandomVector3();

		var expectedValues = new Vector2(
			left.X - right.X,
			left.Y - right.Y
		);

		Assert.Equal(expectedValues, left - right);
	}

	[Fact]
	public void SubtractionOperator_Vector2AndVector3_Fails()
	{
		Vector2 nullVec2 = null;
		Vector3 nullVec3 = null;
		var vec2 = new Vector2(1);
		var vec3 = new Vector3(1);

		Assert.Throws<ArgumentNullException>(() => vec2 - nullVec3);
		Assert.Throws<ArgumentNullException>(() => nullVec2 - vec3);
	}

	#endregion Vector2 - Vector3

	#region Vector3 - Vector2

	[Fact]
	public void SubtractionOperator_Vector3AndVector2_Succeeds()
	{
		Vector3 left = DataGenerator.GenerateRandomVector3();
		Vector2 right = DataGenerator.GenerateRandomVector2();

		var expectedValues = new Vector3(
			left.X - right.X,
			left.Y - right.Y,
			left.Z
		);

		Assert.Equal(expectedValues, left - right);
	}

	[Fact]
	public void SubtractionOperator_Vector3AndVector2_Fails()
	{
		Vector2 nullVec2 = null;
		Vector3 nullVec3 = null;
		var vec2 = new Vector2(1);
		var vec3 = new Vector3(1);

		Assert.Throws<ArgumentNullException>(() => vec3 - nullVec2);
		Assert.Throws<ArgumentNullException>(() => nullVec3 - vec2);
	}

	#endregion Vector3 - Vector2

	#region Vector3 - Double

	[Fact]
	public void SubtractionOperator_Vector3AndDouble_Succeeds()
	{
		Vector3 vector = DataGenerator.GenerateRandomVector3();
		double value = DataGenerator.GenerateRandomDouble();

		var expectedVector = new Vector3(vector.X - value, vector.Y - value, vector.Z - value);

		Assert.Equal(expectedVector, vector - value);
	}

	[Theory]
	[InlineData(double.NaN)]
	[InlineData(double.PositiveInfinity)]
	[InlineData(double.NegativeInfinity)]
	public void SubtractionOperator_Vector3AndDouble_Fails(double invalidValue)
	{
		Vector3 vector = DataGenerator.GenerateRandomVector3();

		Assert.Throws<ArgumentException>(() => vector - invalidValue);
	}

	#endregion Vector3 - Double

	#region Double - Vector3

	[Fact]
	public void SubtractionOperator_DoubleAndVector3_Succeeds()
	{
		Vector3 vector = DataGenerator.GenerateRandomVector3();
		double value = DataGenerator.GenerateRandomDouble();

		var expectedVector = new Vector3(value - vector.X, value - vector.Y, value - vector.Z);

		Assert.Equal(expectedVector, value - vector);
	}

	[Theory]
	[InlineData(double.NaN)]
	[InlineData(double.PositiveInfinity)]
	[InlineData(double.NegativeInfinity)]
	public void SubtractionOperator_DoubleAndVector3_Fails(double invalidValue)
	{
		Vector3 vector = DataGenerator.GenerateRandomVector3();

		Assert.Throws<ArgumentException>(() => invalidValue - vector);
	}

	#endregion Double - Vector3

	#endregion Operator -

	#region Operator *

	#region Vector3 * Vector3

	[Fact]
	public void MultiplicationOperator_BothVector3_Succeeds()
	{
		Vector3 vec0 = DataGenerator.GenerateRandomVector3();
		Vector3 vec1 = DataGenerator.GenerateRandomVector3();

		var expectedValues = new Vector3(
			vec0.X * vec1.X,
			vec0.Y * vec1.Y,
			vec0.Z * vec1.Z
		);

		Assert.Equal(expectedValues, vec0 * vec1);
	}

	[Fact]
	public void MultiplicationOperator_BothVector3_Fails()
	{
		Vector3 nullVector = null;
		var vec = new Vector3(1);

		Assert.Throws<ArgumentNullException>(() => vec * nullVector);
		Assert.Throws<ArgumentNullException>(() => nullVector * vec);
	}

	#endregion Vector3 * Vector3

	#region Vector2 * Vector3

	[Fact]
	public void MultiplicationOperator_Vector2AndVector3_Succeeds()
	{
		Vector2 left = DataGenerator.GenerateRandomVector2();
		Vector3 right = DataGenerator.GenerateRandomVector3();

		var expectedValues = new Vector2(left.X * right.X, left.Y * right.Y);

		Assert.Equal(expectedValues, left * right);
	}

	[Fact]
	public void MultiplicationOperator_Vector2AndVector3_Fails()
	{
		Vector2 nullVec2 = null;
		Vector3 nullVec3 = null;
		Vector2 vec2 = DataGenerator.GenerateRandomVector2();
		Vector3 vec3 = DataGenerator.GenerateRandomVector3();

		Assert.Throws<ArgumentNullException>(() => vec2 * nullVec3);
		Assert.Throws<ArgumentNullException>(() => nullVec2 * vec3);
	}

	#endregion Vector2 * Vector3

	#region Vector3 * Vector2

	[Fact]
	public void MultiplicationOperator_Vector3AndVector2_Succeeds()
	{
		Vector3 left = DataGenerator.GenerateRandomVector3();
		Vector2 right = DataGenerator.GenerateRandomVector2();

		var expectedValues = new Vector3(left.X * right.X, left.Y * right.Y, left.Z);

		Assert.Equal(expectedValues, left * right);
	}

	[Fact]
	public void MultiplicationOperator_Vector3AndVector2_Fails()
	{
		Vector2 nullVec2 = null;
		Vector3 nullVec3 = null;
		Vector2 vec2 = DataGenerator.GenerateRandomVector2();
		Vector3 vec3 = DataGenerator.GenerateRandomVector3();

		Assert.Throws<ArgumentNullException>(() => vec3 * nullVec2);
		Assert.Throws<ArgumentNullException>(() => nullVec3 * vec2);
	}

	#endregion Vector3 * Vector2

	#region Vector3 * Double

	[Fact]
	public void MultiplicationOperator_Vector3AndDouble_Succeeds()
	{
		Vector3 vector = DataGenerator.GenerateRandomVector3();
		double value = DataGenerator.GenerateRandomDouble();

		var expectedValues = new Vector3(vector.X * value, vector.Y * value, vector.Z * value);

		Assert.Equal(expectedValues, vector * value);
	}

	[Theory]
	[InlineData(double.NaN)]
	[InlineData(double.PositiveInfinity)]
	[InlineData(double.NegativeInfinity)]
	public void MultiplicationOperator_Vector3AndDouble_Fails(double invalidDouble)
	{
		Vector3 vector = DataGenerator.GenerateRandomVector3();

		Assert.Throws<ArgumentException>(() => vector * invalidDouble);
	}

	#endregion Vector3 * Double

	#region Double * Vector3

	[Fact]
	public void MultiplicationOperator_DoubleAndVector3_Succeeds()
	{
		double value = DataGenerator.GenerateRandomDouble();
		Vector3 vector = DataGenerator.GenerateRandomVector3();

		var expectedValues = new Vector3(value * vector.X, value * vector.Y, value * vector.Z);

		Assert.Equal(expectedValues, value * vector);
	}

	[Theory]
	[InlineData(double.NaN)]
	[InlineData(double.PositiveInfinity)]
	[InlineData(double.NegativeInfinity)]
	public void MultiplicationOperator_DoubleAndVector3_Fails(double invalidDouble)
	{
		Vector3 vector = DataGenerator.GenerateRandomVector3();

		Assert.Throws<ArgumentException>(() => invalidDouble * vector);
	}

	#endregion Double * Vector3

	#endregion Operator *

	#region Operator /

	#region Vector3 / Vector3

	[Fact]
	public void DivisionOperator_BothVector3_Succeeds()
	{
		Vector3 left = DataGenerator.GenerateRandomVector3();
		Vector3 right = DataGenerator.GenerateRandomVector3();

		var expectedValues = new Vector3(
			left.X / right.X,
			left.Y / right.Y,
			left.Z / right.Z
		);

		Assert.Equal(expectedValues, left / right);
	}

	[Fact]
	public void DivisionOperator_BothVector3_Fails()
	{
		Vector3 nullVector = null;
		var vec = new Vector3(1);

		Assert.Throws<ArgumentNullException>(() => vec / nullVector);
		Assert.Throws<ArgumentNullException>(() => nullVector / vec);
	}

	#endregion Vector3 / Vector3

	#region Vector2 / Vector3

	[Fact]
	public void DivisionOperator_Vector2AndVector3_Succeeds()
	{
		Vector2 left = DataGenerator.GenerateRandomVector2();
		Vector3 right = DataGenerator.GenerateRandomVector3();

		var expectedValues = new Vector2(
			left.X / right.X,
			left.Y / right.Y
		);

		Assert.Equal(expectedValues, left / right);
	}

	[Fact]
	public void DivisionOperator_Vector2AndVector3_Fails()
	{
		Vector2 nullVec2 = null;
		Vector3 nullVec3 = null;
		var vec2 = new Vector2(1);
		var vec3 = new Vector3(1);

		Assert.Throws<ArgumentNullException>(() => vec2 / nullVec3);
		Assert.Throws<ArgumentNullException>(() => nullVec2 / vec3);
	}

	#endregion Vector2 / Vector3

	#region Vector3 / Vector2

	[Fact]
	public void DivisionOperator_Vector3AndVector2_Succeeds()
	{
		Vector3 left = DataGenerator.GenerateRandomVector3();
		Vector2 right = DataGenerator.GenerateRandomVector2();

		var expectedValues = new Vector3(
			left.X / right.X,
			left.Y / right.Y,
			left.Z
		);

		Assert.Equal(expectedValues, left / right);
	}

	[Fact]
	public void DivisionOperator_Vector3AndVector2_Fails()
	{
		Vector2 nullVec2 = null;
		Vector3 nullVec3 = null;
		var vec2 = new Vector2(1);
		var vec3 = new Vector3(1);

		Assert.Throws<ArgumentNullException>(() => vec3 / nullVec2);
		Assert.Throws<ArgumentNullException>(() => nullVec3 / vec2);
	}

	#endregion Vector3 / Vector2

	#region Vector3 / Double

	[Fact]
	public void DivisionOperator_Vector3AndDouble_Succeeds()
	{
		Vector3 vector = DataGenerator.GenerateRandomVector3();
		double value = DataGenerator.GenerateRandomDouble();

		var expectedVector = new Vector3(vector.X / value, vector.Y / value, vector.Z / value);

		Assert.Equal(expectedVector, vector / value);
	}

	[Theory]
	[InlineData(0.0)]
	[InlineData(double.NaN)]
	[InlineData(double.PositiveInfinity)]
	[InlineData(double.NegativeInfinity)]
	public void DivisionOperator_Vector3AndDouble_Fails(double invalidValue)
	{
		Vector3 vector = DataGenerator.GenerateRandomVector3();

		Assert.Throws<ArgumentException>(() => vector / invalidValue);
	}

	#endregion Vector3 / Double

	#region Double / Vector3

	[Fact]
	public void DivisionOperator_DoubleAndVector3_Succeeds()
	{
		Vector3 vector = DataGenerator.GenerateRandomVector3();
		double value = DataGenerator.GenerateRandomDouble();

		var expectedVector = new Vector3(value / vector.X, value / vector.Y, value / vector.Z);

		Assert.Equal(expectedVector, value / vector);
	}

	[Theory]
	[InlineData(double.NaN)]
	[InlineData(double.PositiveInfinity)]
	[InlineData(double.NegativeInfinity)]
	public void DivisionOperator_DoubleAndVector3_Fails(double invalidValue)
	{
		Vector3 vector = DataGenerator.GenerateRandomVector3();

		Assert.Throws<ArgumentException>(() => invalidValue / vector);
	}

	#endregion Double / Vector3

	#endregion Operator /
}