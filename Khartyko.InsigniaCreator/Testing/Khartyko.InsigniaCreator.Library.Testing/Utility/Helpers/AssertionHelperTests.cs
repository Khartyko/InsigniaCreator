using Khartyko.InsigniaCreator.Library.Utility.Helpers;

#pragma warning disable CS8600, CS8604

namespace Khartyko.InsigniaCreator.Library.Testing.Utility.Helpers;

public class AssertionHelperTests
{
	#region NullCheck

	[Theory]
	[InlineData("A string")]
	[InlineData(42)]
	public void NullCheck_Succeeds(object target)
	{
		AssertionHelper.NullCheck(target, nameof(target));
	}

	[Fact]
	public void NullCheck_Fails()
	{
		object nullObj = null;
        
		Assert.Throws<ArgumentNullException>(() => AssertionHelper.NullCheck(nullObj, nameof(nullObj)));
	}

	#endregion NullCheck

	#region EmptyOrWhitespaceCheck

	[Theory]
	[InlineData("Not an empty string", "NotEmptyString")]
	[InlineData("123", "Numbers")]
	[InlineData("123 - has a value", "NumberAndLetters")]
	public void EmptyOrWhitespaceCheck_Succeeds(string target, string name)
	{
		AssertionHelper.EmptyOrWhitespaceCheck(target, name);
	}

	[Fact]
	public void EmptyOrWhitespaceCheck_NullTarget_Fails()
	{
		string nullString = null;
        
		Assert.Throws<ArgumentNullException>(() => AssertionHelper.EmptyOrWhitespaceCheck(nullString, "NullString"));
	}

	[Theory]
	[InlineData("", "Empty")]
	[InlineData("    ", "Spaces")]
	[InlineData("\t\r\n", "SpecialCharacters")]
	public void EmptyOrWhitespaceCheck_InvalidTarget_Fails(string target, string name)
	{
		Assert.Throws<ArgumentException>(() => AssertionHelper.EmptyOrWhitespaceCheck(target, name)); 
	}

	[Fact]
	public void EmptyOrWhitespaceCheck_NullName_Fails()
	{
		string nullString = null;

		Assert.Throws<ArgumentNullException>(() => AssertionHelper.EmptyOrWhitespaceCheck("ValidString", nullString));
	}

	#endregion EmptyOrWhitespaceCheck

	#region InvalidDoubleCheck

	[Theory]
	[InlineData(0.0, "Double holding a value of 0.0")]
	public void InvalidDoubleCheck_Succeeds(double input, string descriptor) =>
		AssertionHelper.InvalidDoubleCheck(input, descriptor);

	[Theory]
	[InlineData(double.NaN, "value")]
	[InlineData(double.PositiveInfinity, "value")]
	[InlineData(double.NegativeInfinity, "value")]
	public void InvalidDoubleCheck_BadInput_Fails(double input, string descriptor)
	{
		Assert.Throws<ArgumentException>(() => AssertionHelper.InvalidDoubleCheck(input, descriptor));
	}

	[Theory]
	[InlineData(1.0, "")]
	[InlineData(1.0, null)]
	public void InvalidDoubleCheck_BadDescriptor_Fails(double input, string descriptor)
	{
		Assert.Throws<ArgumentException>(() => AssertionHelper.InvalidDoubleCheck(input, descriptor));
	}

	#endregion InvalidDoubleCheck

	#region ZeroCheck

	[Theory]
	[InlineData(-3.5)]
	[InlineData(42)]
	[InlineData(double.MaxValue)]
	public void ZeroCheck_Succeeds(double value)
	{
		AssertionHelper.ZeroCheck(value, nameof(value));
	}

	[Fact]
	public void ZeroCheck_ZeroDouble_Fails()
	{
		Assert.Throws<ArgumentException>(() => AssertionHelper.ZeroCheck(0.0, "0.0"));
	}

	[Fact]
	public void ZeroCheck_InvalidDescriptor_Fails()
	{
		Assert.Throws<ArgumentException>(() => AssertionHelper.ZeroCheck(double.MaxValue, string.Empty));
		Assert.Throws<ArgumentException>(() => AssertionHelper.ZeroCheck(double.MaxValue, "\r\t\n"));
		Assert.Throws<ArgumentException>(() => AssertionHelper.ZeroCheck(double.MaxValue, "    "));
	}

	#endregion ZeroCheck
	
	#region PositiveCheck

	[Theory]
	[InlineData(1)]
	[InlineData(15)]
	[InlineData(3000)]
	[InlineData(2401)]
	[InlineData(343)]
	public void PositiveCheck_Succeeds(double value)
	{
		AssertionHelper.PositiveCheck(value, nameof(value));
	}

	[Theory]
	[InlineData(double.NaN)]
	[InlineData(double.PositiveInfinity)]
	[InlineData(double.NegativeInfinity)]
	public void PositiveCheck_InvalidDouble_Fails(double invalidValue)
	{
		Assert.Throws<ArgumentException>(() => AssertionHelper.PositiveCheck(invalidValue, nameof(invalidValue)));
	}

	[Fact]
	public void PositiveCheck_NullDescriptor_Fails()
	{
		const double validValue = 1.0;
		string nullDescriptor = null;

		Assert.Throws<ArgumentException>(() => AssertionHelper.PositiveCheck(validValue, nullDescriptor));
	}

	[Fact]
	public void PositiveCheck_NegativeDouble_Fails()
	{
		const double testValue = -1;
		
		Assert.Throws<ArgumentException>(() => AssertionHelper.PositiveCheck(testValue, nameof(testValue)));
	}
	
	#endregion PositiveCheck
	
	#region RangeCheck

	[Theory]
	[InlineData(0.0, -1.0, 1.0)]
	[InlineData(0.0, -0.001, 0.001)]
	public void RangeCheck_Succeeds(double value, double minimum, double maximum)
	{
		AssertionHelper.RangeCheck(value, minimum, maximum, nameof(value));
	}

	[Theory]
	[InlineData(double.NaN)]
	[InlineData(double.PositiveInfinity)]
	[InlineData(double.NegativeInfinity)]
	public void RangeCheck_InvalidDoubleValues_Fails(double value)
	{
		const double validValue = 0.0;

		Assert.Throws<ArgumentException>(() => AssertionHelper.RangeCheck(value, validValue, validValue, nameof(value)));
		Assert.Throws<ArgumentException>(() => AssertionHelper.RangeCheck(validValue, value, value, nameof(value)));
		Assert.Throws<ArgumentException>(() => AssertionHelper.RangeCheck(validValue, validValue, value, nameof(value)));
	}

	[Theory]
	[InlineData(1.0, 0.0)]
	[InlineData(-1.0, 0.0)]
	[InlineData(-3.5, 42.0)]
	public void RangeCheck_ValueEqualsMinimum_Fails(double invalidValue, double validValue)
	{
		Assert.Throws<ArgumentException>(() =>
			AssertionHelper.RangeCheck(invalidValue, invalidValue, validValue, nameof(invalidValue)));
	}

	[Theory]
	[InlineData(1.0, 0.0)]
	[InlineData(-1.0, 0.0)]
	[InlineData(-3.5, 42.0)]
	public void RangeCheck_ValueEqualsMaximum_Fails(double invalidValue, double validValue)
	{
		Assert.Throws<ArgumentException>(() =>
			AssertionHelper.RangeCheck(invalidValue, validValue, invalidValue, nameof(invalidValue)));
	}

	[Theory]
	[InlineData(0.0, 1.0, 2.0)]
	[InlineData(99.999, 100.0, -3.5)]
	public void RangeCheck_MinimumGreaterThanValue_Fails(double value, double minimum, double maximum)
	{
		Assert.Throws<ArgumentException>(() => AssertionHelper.RangeCheck(value, minimum, maximum, nameof(value)));
	}

	[Theory]
	[InlineData(1.0, -1.0, 0.0)]
	[InlineData(100.0, -3.5, 99.999)]
	public void RangeCheck_MaximumLessThanValue_Fails(double value, double minimum, double maximum)
	{
		Assert.Throws<ArgumentException>(() => AssertionHelper.RangeCheck(value, minimum, maximum, nameof(value)));
	}

	[Fact]
	public void RangeCheck_NullDescriptor_Fails()
	{
		const double minimum = -1.0;
		const double maximum = 1.0;
		const double value = 0.0;

		string nullString = null;

		Assert.Throws<ArgumentNullException>(() => AssertionHelper.RangeCheck(value, minimum, maximum, nullString));
	}
	
	[Theory]
	[InlineData("")]
	[InlineData("              ")]
	[InlineData("\t\n\r")]
	public void RangeCheck_InvalidDescriptor_Fails(string descriptor)
	{
		const double minimum = -1.0;
		const double maximum = 1.0;
		const double value = 0.0;

		Assert.Throws<ArgumentException>(() => AssertionHelper.RangeCheck(value, minimum, maximum, descriptor));
	}

	#endregion RangeCheck
}