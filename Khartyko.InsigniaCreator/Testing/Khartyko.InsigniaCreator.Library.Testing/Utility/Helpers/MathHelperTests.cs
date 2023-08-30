/** \addtogroup LibraryTests
 * @{
 */
using Khartyko.InsigniaCreator.Library.Utility.Helpers;

#pragma warning disable CS8600, CS8604, CS8625 // Cannot convert null literal to non-nullable reference type.

namespace Khartyko.InsigniaCreator.Library.Testing.Utility.Helpers;

public class MathHelperTests
{
	public static IEnumerable<object[]> ToDegreesTestData
	{
		get
		{
			double Process(double degrees) => MathHelper.Round(degrees * 180 / Math.PI);

			return new[]
			{
				new object[] { 0, 0 },
				new object[] { 90, Process(90) },
				new object[] { 180, Process(180) },
				new object[] { 270, Process(270) }
			};
		}
	}

	public static IEnumerable<object[]> ToRadiansTestData
	{
		get
		{
			double Process(double radians) => MathHelper.Round(radians * Math.PI / 180);

			return new[]
			{
				new object[] { 0, 0 },
				new object[] { 90, Process(90) },
				new object[] { 180, Process(180) },
				new object[] { 270, Process(270) }
			};
		}
	}

	#region IsOdd (int)

	[Theory]
	[InlineData(-1, true)]
	[InlineData(0, false)]
	[InlineData(1, true)]
	[InlineData(10, false)]
	[InlineData(15, true)]
	public void IsOdd_IntVariant_Succeeds(int testValue, bool expectedResult)
	{
		bool actualResult = MathHelper.IsOdd(testValue);

		Assert.Equal(expectedResult, actualResult);
	}

	#endregion IsOdd (int)

	#region IsEven (int)

	[Theory]
	[InlineData(-1, false)]
	[InlineData(0, true)]
	[InlineData(1, false)]
	[InlineData(10, true)]
	[InlineData(15, false)]
	public void IsEven_IntVariant_Succeeds(int testValue, bool expectedResult)
	{
		bool actualResult = MathHelper.IsEven(testValue);

		Assert.Equal(expectedResult, actualResult);
	}

	#endregion IsEven (int)

	#region Equals

	[Theory]
	[InlineData(0.0, 0.0)]
	[InlineData(0.0001, 0.0001)]
	[InlineData(3.0001, 3.0001)]
	public void Equals_Succeeds(double d0, double d1) => Assert.True(MathHelper.Equals(d0, d1));

	[Theory]
	[InlineData(0.0001, 0.0002)]
	[InlineData(3, 10)]
	public void Equals_Fails_NoBadValues(double d0, double d1)
	{
		Assert.False(MathHelper.Equals(d0, d1));
	}

	[Theory]
	[InlineData(double.NaN, 1.0)]
	[InlineData(double.PositiveInfinity, 1.0)]
	[InlineData(double.NegativeInfinity, 1.0)]
	[InlineData(1.0, double.NaN)]
	[InlineData(1.0, double.PositiveInfinity)]
	[InlineData(1.0, double.NegativeInfinity)]
	public void Equals_Fails_BadValues(double d0, double d1)
	{
		Assert.Throws<ArgumentException>(() => MathHelper.Equals(d0, d1));
	}

	#endregion Equals

	#region LessThan

	[Theory]
	[InlineData(0, 1, true)]
	[InlineData(1, 0, false)]
	public static void LessThan_Succeeds(double d0, double d1, bool expectedResult)
	{
		bool actualResult = MathHelper.LessThan(d0, d1);
		
		Assert.Equal(expectedResult, actualResult);
	}

	[Theory]
	[InlineData(double.NaN)]
	[InlineData(double.PositiveInfinity)]
	[InlineData(double.NegativeInfinity)]
	public static void LessThan_InvalidDouble_Fails(double invalidValue)
	{
		double validValue = DataGenerator.GenerateRandomDouble();

		Assert.Throws<ArgumentException>(() => MathHelper.LessThan(invalidValue, validValue));
		Assert.Throws<ArgumentException>(() => MathHelper.LessThan(validValue, invalidValue));
	}

	#endregion LessThan

	#region GreaterThan

	[Theory]
	[InlineData(0, 1, false)]
	[InlineData(1, 0, true)]
	public static void GreaterThan_Succeeds(double d0, double d1, bool expectedResult)
	{
		bool actualResult = MathHelper.GreaterThan(d0, d1);
		
		Assert.Equal(expectedResult, actualResult);
	}

	[Theory]
	[InlineData(double.NaN)]
	[InlineData(double.PositiveInfinity)]
	[InlineData(double.NegativeInfinity)]
	public static void GreaterThan_InvalidDouble_Fails(double invalidValue)
	{
		double validValue = DataGenerator.GenerateRandomDouble();

		Assert.Throws<ArgumentException>(() => MathHelper.GreaterThan(invalidValue, validValue));
		Assert.Throws<ArgumentException>(() => MathHelper.GreaterThan(validValue, invalidValue));
	}

	#endregion GreaterThan
	
	#region Round

	[Theory]
	[InlineData(10.0001, 10.0)]
	[InlineData(10.12345, 10.123)]
	[InlineData(0.7665432, 0.767)]
	[InlineData(0.9999999, 1.0)]
	public void Round_Succeeds(double input, double expected) =>
		Assert.Equal(expected, MathHelper.Round(input));

	[Fact]
	public void Round_Fails()
	{
		Assert.Throws<ArgumentException>(() => MathHelper.Round(double.NaN));
		Assert.Throws<ArgumentException>(() => MathHelper.Round(double.PositiveInfinity));
		Assert.Throws<ArgumentException>(() => MathHelper.Round(double.NegativeInfinity));
	}

	#endregion Round

	#region Sqrt

	[Theory]
	[InlineData(4, 2)]
	[InlineData(9, 3)]
	[InlineData(13, 3.606)]
	[InlineData(42, 6.481)]
	public void Sqrt_Succeeds(double input, double expected)
	{
		Assert.Equal(expected, MathHelper.Sqrt(input));
	}

	[Fact]
	public void Sqrt_Fails()
	{
		Assert.Throws<ArgumentOutOfRangeException>(() => MathHelper.Sqrt(-1));
		Assert.Throws<ArgumentException>(() => MathHelper.Sqrt(double.NaN));
		Assert.Throws<ArgumentException>(() => MathHelper.Sqrt(double.PositiveInfinity));
		Assert.Throws<ArgumentException>(() => MathHelper.Sqrt(double.NegativeInfinity));
	}

	#endregion Sqrt

	#region Pi

	[Fact]
	public void Pi_Succeeds()
	{
		double expected = Math.Round(Math.PI * 1000) / 1000;
		double actual = MathHelper.Pi();
		Assert.Equal(expected, actual);
	}

	#endregion Pi

	#region PiOverload

	[Theory]
	[InlineData(1.0, 1.0, 1.0)]
	[InlineData(-1.0, -1.0, 1.0)]
	[InlineData(3.0, 4.0, 0.75)]
	[InlineData(-1.0, 2.0, -0.5)]
	[InlineData(2.0, 1.0, 2.0)]
	public void PiOverload_Succeeds(double d0, double d1, double expectedMultiplier)
	{
		double result = MathHelper.Pi(d0, d1);
		double expected = expectedMultiplier * Math.PI;
		Assert.Equal(expected, result);
	}

	[Fact]
	public void PiOverload_Fails()
	{
		Assert.Throws<ArgumentException>(() => MathHelper.Pi(double.NaN, 1.0));
		Assert.Throws<ArgumentException>(() => MathHelper.Pi(1.0, 0.0));
		Assert.Throws<ArgumentException>(() => MathHelper.Pi(double.PositiveInfinity, 1.0));
		Assert.Throws<ArgumentException>(() => MathHelper.Pi(double.NegativeInfinity, 1.0));
	}

	#endregion PiOverload

	#region Cos

	[Theory]
	[InlineData(90, 0)]
	public void Cos_Succeeds(double theta, double expectedValue)
	{
		double radians = MathHelper.ToRadians(theta);
		double actualValue = MathHelper.Cos(radians);

		Assert.Equal(expectedValue, actualValue);
	}

	[Theory]
	[InlineData(double.NaN)]
	[InlineData(double.PositiveInfinity)]
	[InlineData(double.NegativeInfinity)]
	public void Cos_Fails(double value)
	{
		Assert.Throws<ArgumentException>(() => MathHelper.Cos(value));
	}

	#endregion Cos

	#region Sin

	[Theory]
	[InlineData(90, 1)]
	public void Sin_Succeeds(double theta, double expectedValue)
	{
		double radians = MathHelper.ToRadians(theta);
		double actualValue = MathHelper.Sin(radians);

		Assert.Equal(expectedValue, actualValue);
	}

	[Theory]
	[InlineData(double.NaN)]
	[InlineData(double.PositiveInfinity)]
	[InlineData(double.NegativeInfinity)]
	public void Sin_Fails(double value)
	{
		Assert.Throws<ArgumentException>(() => MathHelper.Sin(value));
	}

	#endregion Sin

	#region ToDegrees

	[Theory]
	[MemberData(nameof(ToDegreesTestData))]
	public void ToDegrees_Succeeds(double input, double expected)
	{
		double actual = MathHelper.ToDegrees(input);
		Assert.Equal(expected, actual);
	}

	[Fact]
	public void ToDegrees_Fails()
	{
		Assert.Throws<ArgumentException>(() => MathHelper.ToDegrees(double.NaN));
		Assert.Throws<ArgumentException>(() => MathHelper.ToDegrees(double.PositiveInfinity));
		Assert.Throws<ArgumentException>(() => MathHelper.ToDegrees(double.NegativeInfinity));
	}

	#endregion ToDegrees

	#region ToRadians

	[Theory]
	[MemberData(nameof(ToRadiansTestData))]
	public void ToRadians_Succeeds(double input, double expected)
	{
		double actual = MathHelper.ToRadians(input);
		Assert.Equal(expected, actual);
	}

	[Fact]
	public void ToRadians_Fails()
	{
		Assert.Throws<ArgumentException>(() => MathHelper.ToRadians(double.NaN));
		Assert.Throws<ArgumentException>(() => MathHelper.ToRadians(double.PositiveInfinity));
		Assert.Throws<ArgumentException>(() => MathHelper.ToRadians(double.NegativeInfinity));
	}

	#endregion ToRadians

	#region GetInteriorAngleFromSideCount

	[Theory]
	[InlineData(3, 60)]
	[InlineData(7, 128.571)]
	public void GetInteriorAngleFromSideCount_Succeeds(int sideCount, double expected)
	{
		double actual = MathHelper.GetInteriorAngleFromSideCount(sideCount);

		Assert.Equal(expected, actual);
	}

	[Theory]
	[InlineData(-1)]
	[InlineData(0)]
	[InlineData(1)]
	[InlineData(2)]
	public void GetInteriorAngleFromSideCount_Fails(int sideCount)
	{
		Assert.Throws<ArgumentOutOfRangeException>(() => MathHelper.GetInteriorAngleFromSideCount(sideCount));
	}

	#endregion GetInteriorAngleFromSideCount
}
/** @} */