using Khartyko.InsigniaCreator.Library.Utility.Helpers;

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
    public void MathHelper_IsOdd_IntVariant_Succeeds(int testValue, bool expectedResult)
    {
        var actualResult = MathHelper.IsOdd(testValue);
        
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
    public void MathHelper_IsEven_IntVariant_Succeeds(int testValue, bool expectedResult)
    {
        var actualResult = MathHelper.IsEven(testValue);
        
        Assert.Equal(expectedResult, actualResult);
    }

    #endregion IsEven (int)

    #region Equals
    
    [Theory]
    [InlineData(0.0, 0.0)]
    [InlineData(0.0001, 0.0001)]
    [InlineData(3.0001, 3.0001)]
    public void MathHelper_Equals_Succeeds(double d0, double d1) => Assert.True(MathHelper.Equals(d0, d1));

    [Theory]
    [InlineData(0.0001, 0.0002)]
    [InlineData(3, 10)]
    public void MathHelper_Equals_Fails_NoBadValues(double d0, double d1)
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
    public void MathHelper_Equals_Fails_BadValues(double d0, double d1)
    {
        Assert.Throws<ArgumentException>(() => MathHelper.Equals(d0, d1));
    }

    #endregion Equals

    #region Round
    
    [Theory]
    [InlineData(10.0001, 10.0)]
    [InlineData(10.12345, 10.123)]
    [InlineData(0.7665432, 0.767)]
    [InlineData(0.9999999, 1.0)]
    public void MathHelper_Round_Succeeds(double input, double expected) =>
        Assert.Equal(expected, MathHelper.Round(input));

    [Fact]
    public void MathHelper_Round_Fails()
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
    public void MathHelper_Sqrt_Succeeds(double input, double expected)
    {
        Assert.Equal(expected, MathHelper.Sqrt(input));
    }

    [Fact]
    public void MathHelper_Sqrt_Fails()
    {
        Assert.Throws<ArgumentOutOfRangeException>(() => MathHelper.Sqrt(-1));
        Assert.Throws<ArgumentException>(() => MathHelper.Sqrt(double.NaN));
        Assert.Throws<ArgumentException>(() => MathHelper.Sqrt(double.PositiveInfinity));
        Assert.Throws<ArgumentException>(() => MathHelper.Sqrt(double.NegativeInfinity));
    }

    #endregion Sqrt
    
    #region Pi
    
    [Fact]
    public void MathHelper_Pi_Succeeds()
    {
        var expected = Math.Round(Math.PI * 1000) / 1000;
        var actual = MathHelper.Pi();
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
    public void MathHelper_PiOverload_Succeeds(double d0, double d1, double expectedMultiplier)
    {
        var result = MathHelper.Pi(d0, d1);
        var expected = expectedMultiplier * Math.PI;
        Assert.Equal(expected, result);
    }

    [Fact]
    public void MathHelper_PiOverload_Fails()
    {
        Assert.Throws<ArgumentException>(() => MathHelper.Pi(double.NaN, 1.0));
        Assert.Throws<ArgumentOutOfRangeException>(() => MathHelper.Pi(1.0, 0.0));
        Assert.Throws<ArgumentException>(() => MathHelper.Pi(double.PositiveInfinity, 1.0));
        Assert.Throws<ArgumentException>(() => MathHelper.Pi(double.NegativeInfinity, 1.0));
    }

    #endregion PiOverload

    #region Cos

    [Theory]
    [InlineData(90, 0)]
    public void MathHelper_Cos_Succeeds(double theta, double expectedValue)
    {
        var radians = MathHelper.ToRadians(theta);
        var actualValue = MathHelper.Cos(radians);

        Assert.Equal(expectedValue, actualValue);
    }

    [Theory]
    [InlineData(double.NaN)]
    [InlineData(double.PositiveInfinity)]
    [InlineData(double.NegativeInfinity)]
    public void MathHelper_Cos_Fails(double value)
    {
        Assert.Throws<ArgumentException>(() => MathHelper.Cos(value));
    }

    #endregion Cos

    #region Sin

    [Theory]
    [InlineData(90, 1)]
    public void MathHelper_Sin_Succeeds(double theta, double expectedValue)
    {
        var radians = MathHelper.ToRadians(theta);
        var actualValue = MathHelper.Sin(radians);

        Assert.Equal(expectedValue, actualValue);
    }

    [Theory]
    [InlineData(double.NaN)]
    [InlineData(double.PositiveInfinity)]
    [InlineData(double.NegativeInfinity)]
    public void MathHelper_Sin_Fails(double value)
    {
        Assert.Throws<ArgumentException>(() => MathHelper.Sin(value));
    }

    #endregion Sin

    #region ToDegrees

    [Theory]
    [MemberData(nameof(ToDegreesTestData))]
    public void MathHelper_ToDegrees_Succeeds(double input, double expected)
    {
        var actual = MathHelper.ToDegrees(input);
        Assert.Equal(expected, actual);
    }

    [Fact]
    public void MathHelper_ToDegrees_Fails()
    {
        Assert.Throws<ArgumentException>(() => MathHelper.ToDegrees(double.NaN));
        Assert.Throws<ArgumentException>(() => MathHelper.ToDegrees(double.PositiveInfinity));
        Assert.Throws<ArgumentException>(() => MathHelper.ToDegrees(double.NegativeInfinity));
    }

    #endregion ToDegrees
    
    #region ToRadians
    
    [Theory]
    [MemberData(nameof(ToRadiansTestData))]
    public void MathHelper_ToRadians_Succeeds(double input, double expected)
    {
        var actual = MathHelper.ToRadians(input);
        Assert.Equal(expected, actual);
    }

    [Fact]
    public void MathHelper_ToRadians_Fails()
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
    public void MathHelper_GetInteriorAngleFromSideCount_Succeeds(int sideCount, double expected)
    {
        var actual = MathHelper.GetInteriorAngleFromSideCount(sideCount);

        Assert.Equal(expected, actual);
    }

    [Theory]
    [InlineData(-1)]
    [InlineData(0)]
    [InlineData(1)]
    [InlineData(2)]
    public void MathHelper_GetInteriorAngleFromSideCount_Fails(int sideCount)
    {
        Assert.Throws<ArgumentOutOfRangeException>(() => MathHelper.GetInteriorAngleFromSideCount(sideCount));
    }

    #endregion GetInteriorAngleFromSideCount

    #region Verify
    
    [Theory]
    [InlineData(0.0, "Double holding a value of 0.0")]
    public void MathHelper_Verify_Succeeds(double input, string descriptor) => MathHelper.Verify(input, descriptor);

    [Theory]
    [InlineData(double.NaN, "value")]
    [InlineData(double.PositiveInfinity, "value")]
    [InlineData(double.NegativeInfinity, "value")]
    public void MathHelper_Verify_Fails_BadInput(double input, string descriptor)
    {
        Assert.Throws<ArgumentException>(() => MathHelper.Verify(input, descriptor));
    }

    [Theory]
    [InlineData(1.0, "")]
    [InlineData(1.0, null)]
    public void MathHelper_Verify_Fails_BadDescriptor(double input, string descriptor)
    {
        Assert.Throws<ArgumentException>(() => MathHelper.Verify(input, descriptor));
    }

    #endregion Verify
}