using System.Reflection;
using Khartyko.InsigniaCreator.Library.Utility.Helpers;

#pragma warning disable CS8600, CS8604

namespace Khartyko.InsigniaCreator.Library.Testing.Utility.Helpers;

public class AssertionHelperTests
{
	private static object[] GenerateItems(int count)
	{
		var objects = new object[count];

		for (var i = 0; i < count; i++)
		{
			objects[i] = new object();
		}

		return objects;
	}
	
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
	[InlineData("")]
	[InlineData("\t")]
	[InlineData("\r")]
	[InlineData("\n")]
	[InlineData("    ")]
	public void InvalidDoubleCheck_BadDescriptor_Fails(string descriptor)
	{
		const double input = 1.0;
		
		Assert.Throws<ArgumentException>(() => AssertionHelper.InvalidDoubleCheck(input, descriptor));
	}

	[Fact]
	public void InvalidDoubleCheck_NullDescriptor_Fails()
	{
		const double input = 1.0;
		string nullDescriptor = null;
		
		Assert.Throws<ArgumentNullException>(() => AssertionHelper.InvalidDoubleCheck(input, nullDescriptor));
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

	#region IntValue

	[Theory]
	[InlineData(1)]
	[InlineData(15)]
	[InlineData(3000)]
	[InlineData(2401)]
	[InlineData(343)]
	public void PositiveCheck_IntValue_Succeeds(int value)
	{
		AssertionHelper.PositiveCheck(value, nameof(value));
	}

	[Fact]
	public void PositiveCheck_IntValue_NegativeInt_Fails()
	{
		const int testValue = -1;
		
		Assert.Throws<ArgumentException>(() => AssertionHelper.PositiveCheck(testValue, nameof(testValue)));
	}

	#endregion IntValue

	#region LongValue

	[Theory]
	[InlineData(1)]
	[InlineData(15)]
	[InlineData(3000)]
	[InlineData(2401)]
	[InlineData(343)]
	public void PositiveCheck_LongValue_Succeeds(long value)
	{
		AssertionHelper.PositiveCheck(value, nameof(value));
	}

	[Fact]
	public void PositiveCheck_LongValue_NegativeLong_Fails()
	{
		const long testValue = -1;
		
		Assert.Throws<ArgumentException>(() => AssertionHelper.PositiveCheck(testValue, nameof(testValue)));
	}

	#endregion LongValue

	#region DoubleValue

	[Theory]
	[InlineData(1)]
	[InlineData(15)]
	[InlineData(3000)]
	[InlineData(2401)]
	[InlineData(343)]
	public void PositiveCheck_DoubleValue_Succeeds(double value)
	{
		AssertionHelper.PositiveCheck(value, nameof(value));
	}

	[Theory]
	[InlineData(double.NaN)]
	[InlineData(double.PositiveInfinity)]
	[InlineData(double.NegativeInfinity)]
	public void PositiveCheck_DoubleValue_InvalidDouble_Fails(double invalidValue)
	{
		Assert.Throws<ArgumentException>(() => AssertionHelper.PositiveCheck(invalidValue, nameof(invalidValue)));
	}

	[Fact]
	public void PositiveCheck_DoubleValue_NullDescriptor_Fails()
	{
		const double validValue = 1.0;
		string nullDescriptor = null;

		Assert.Throws<ArgumentNullException>(() => AssertionHelper.PositiveCheck(validValue, nullDescriptor));
	}
	
	[Fact]
	public void PositiveCheck_DoubleValue_NegativeDouble_Fails()
	{
		const double testValue = -1;
		
		Assert.Throws<ArgumentOutOfRangeException>(() => AssertionHelper.PositiveCheck(testValue, nameof(testValue)));
	}

	#endregion DoubleValue
	
	#endregion PositiveCheck

	#region MinimumCheck

	[Theory]
	[InlineData(3, 0)]
	[InlineData(0, -3)]
	[InlineData(1, 0)]
	public void MinimumCheck_Succeeds(int value, int minimum)
	{
		AssertionHelper.MinimumCheck(value, minimum, nameof(value));
	}

	[Theory]
	[InlineData(3, 0)]
	[InlineData(0, -3)]
	[InlineData(1, 0)]
	public void MinimumCheck_NullDescriptor_Fails(int value, int minimum)
	{
		string nullString = null;
		
		Assert.Throws<ArgumentNullException>(() => AssertionHelper.MinimumCheck(value, minimum, nullString));
	}

	[Theory]
	[InlineData(3, 0, "")]
	[InlineData(0, -3, " ")]
	[InlineData(1, 0, "\n\r\t")]
	public void MinimumCheck_EmptyDescriptor_Fails(int value, int minimum, string descriptor)
	{
		Assert.Throws<ArgumentException>(() => AssertionHelper.MinimumCheck(value, minimum, descriptor));
	}

	[Theory]
	[InlineData(0, 3)]
	[InlineData(-3, 0)]
	[InlineData(0, 10)]
	public void MinimumCheck_ValueFailsConstraints_Fails(int value, int minimum)
	{
		Assert.Throws<ArgumentOutOfRangeException>(() => AssertionHelper.MinimumCheck(value, minimum, nameof(value)));
	}

	#endregion MinimumCheck
	
	#region RangeCheck

	#region IntValue

	[Theory]
	[InlineData(0.0, -1, 1.0)]
	public void RangeCheck_IntValue_Succeeds(int value, int minimum, int maximum)
	{
		AssertionHelper.RangeCheck(value, minimum, maximum, nameof(value));
	}

	[Theory]
	[InlineData(1.0, 0.0)]
	[InlineData(-1.0, 0.0)]
	[InlineData(-3.5, 42.0)]
	public void RangeCheck_ValueEqualsMinimum_IntValue_Fails(int invalidValue, int validValue)
	{
		Assert.Throws<ArgumentOutOfRangeException>(() =>
			AssertionHelper.RangeCheck(invalidValue, invalidValue, validValue, nameof(invalidValue)));
	}

	[Theory]
	[InlineData(1.0, 0.0)]
	[InlineData(-1.0, 0.0)]
	[InlineData(-3.5, 42.0)]
	public void RangeCheck_ValueEqualsMaximum_IntValue_Fails(int invalidValue, int validValue)
	{
		Assert.Throws<ArgumentOutOfRangeException>(() =>
			AssertionHelper.RangeCheck(invalidValue, validValue, invalidValue, nameof(invalidValue)));
	}

	[Theory]
	[InlineData(0.0, 1.0, 2.0)]
	[InlineData(99.999, 100.0, -3.5)]
	public void RangeCheck_MinimumGreaterThanValue_IntValue_Fails(int value, int minimum, int maximum)
	{
		Assert.Throws<ArgumentOutOfRangeException>(() => AssertionHelper.RangeCheck(value, minimum, maximum, nameof(value)));
	}

	[Theory]
	[InlineData(1.0, -1.0, 0.0)]
	[InlineData(100.0, -3.5, 99.999)]
	public void RangeCheck_MaximumLessThanValue_IntValue_Fails(int value, int minimum, int maximum)
	{
		Assert.Throws<ArgumentOutOfRangeException>(() => AssertionHelper.RangeCheck(value, minimum, maximum, nameof(value)));
	}

	[Fact]
	public void RangeCheck_NullDescriptor_IntValue_Fails()
	{
		const int minimum = -1;
		const int maximum = 1;
		const int value = 0;

		string nullString = null;

		Assert.Throws<ArgumentNullException>(() => AssertionHelper.RangeCheck(value, minimum, maximum, nullString));
	}
	
	[Theory]
	[InlineData("")]
	[InlineData("              ")]
	[InlineData("\t\n\r")]
	public void RangeCheck_InvalidDescriptor_IntValue_Fails(string descriptor)
	{
		const int minimum = -1;
		const int maximum = 1;
		const int value = 0;

		Assert.Throws<ArgumentException>(() => AssertionHelper.RangeCheck(value, minimum, maximum, descriptor));
	}

	#endregion IntValue
	
	#region DoubleValue

	[Theory]
	[InlineData(0.0, -1.0, 1.0)]
	[InlineData(0.0, -0.001, 0.001)]
	public void RangeCheck_DoubleValue_Succeeds(double value, double minimum, double maximum)
	{
		AssertionHelper.RangeCheck(value, minimum, maximum, nameof(value));
	}

	[Theory]
	[InlineData(double.NaN)]
	[InlineData(double.PositiveInfinity)]
	[InlineData(double.NegativeInfinity)]
	public void RangeCheck_InvalidDoubleValues_DoubleValue_Fails(double value)
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
	public void RangeCheck_ValueEqualsMinimum_DoubleValue_Fails(double invalidValue, double validValue)
	{
		Assert.Throws<ArgumentOutOfRangeException>(() =>
			AssertionHelper.RangeCheck(invalidValue, invalidValue, validValue, nameof(invalidValue)));
	}

	[Theory]
	[InlineData(1.0, 0.0)]
	[InlineData(-1.0, 0.0)]
	[InlineData(-3.5, 42.0)]
	public void RangeCheck_ValueEqualsMaximum_DoubleValue_Fails(double invalidValue, double validValue)
	{
		Assert.Throws<ArgumentOutOfRangeException>(() =>
			AssertionHelper.RangeCheck(invalidValue, validValue, invalidValue, nameof(invalidValue)));
	}

	[Theory]
	[InlineData(0.0, 1.0, 2.0)]
	[InlineData(99.999, 100.0, -3.5)]
	public void RangeCheck_MinimumGreaterThanValue_DoubleValue_Fails(double value, double minimum, double maximum)
	{
		Assert.Throws<ArgumentOutOfRangeException>(() => AssertionHelper.RangeCheck(value, minimum, maximum, nameof(value)));
	}

	[Theory]
	[InlineData(1.0, -1.0, 0.0)]
	[InlineData(100.0, -3.5, 99.999)]
	public void RangeCheck_MaximumLessThanValue_DoubleValue_Fails(double value, double minimum, double maximum)
	{
		Assert.Throws<ArgumentOutOfRangeException>(() => AssertionHelper.RangeCheck(value, minimum, maximum, nameof(value)));
	}

	[Fact]
	public void RangeCheck_NullDescriptor_DoubleValue_Fails()
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
	public void RangeCheck_InvalidDescriptor_DoubleValue_Fails(string descriptor)
	{
		const double minimum = -1.0;
		const double maximum = 1.0;
		const double value = 0.0;

		Assert.Throws<ArgumentException>(() => AssertionHelper.RangeCheck(value, minimum, maximum, descriptor));
	}

	#endregion DoubleValue
	
	#endregion RangeCheck

	#region EmptyCheck

	[Fact]
	public void EmptyCheck_Succeeds()
	{
		var objects = new[]
		{
			new object()
		};
		
		AssertionHelper.EmptyCheck(objects, nameof(objects));
	}

	[Fact]
	public void EmptyCheck_NullDescriptor_Fails()
	{
		object[] objects = Array.Empty<object>();

		Assert.Throws<ArgumentException>(() => AssertionHelper.EmptyCheck(objects, nameof(objects)));
	}

	[Fact]
	public void EmptyCheck_NullItems_Fails()
	{
		var objects = new[]
		{
			new object()
		};

		string nullDescriptor = null;

		Assert.Throws<ArgumentNullException>(() => AssertionHelper.EmptyCheck(objects, nullDescriptor));
	}

	[Fact]
	public void EmptyCheck_EmptyList_Fails()
	{
		object[] nullObjects = null;
		
		Assert.Throws<ArgumentNullException>(() => AssertionHelper.EmptyCheck(nullObjects, nameof(nullObjects)));
	}

	#endregion EmptyCheck
	
	#region MinimumCountCheck

	[Theory]
	[InlineData(1, 0)]
	[InlineData(15, 10)]
	public void MinimumCountCheck_Succeeds(int count, int minimumCount)
	{
		object[] objects = GenerateItems(count);
		
		AssertionHelper.MinimumCountCheck(objects, minimumCount, nameof(objects));
	}

	[Fact]
	public void MinimumCountCheck_NullDescriptor_Fails()
	{
		string nullDescriptor = null;
		object[] objects = GenerateItems(5);

		Assert.Throws<ArgumentNullException>(() => AssertionHelper.MinimumCountCheck(objects, 1, nullDescriptor));
	}

	[Fact]
	public void MinimumCountCheck_NullItems_Fails()
	{
		object[] nullObjects = null;

		Assert.Throws<ArgumentNullException>(() => AssertionHelper.MinimumCountCheck(nullObjects, 1, nameof(nullObjects)));
	}

	[Fact]
	public void MinimumCountCheck_EmptyList_Fails()
	{
		object[] objects = Array.Empty<object>();
		
		Assert.Throws<ArgumentException>(() => AssertionHelper.MinimumCountCheck(objects, 1, nameof(objects)));
	}

	[Theory]
	[InlineData(15, 20)]
	public void MinimumCountCheck_SubMinimum_Fails(int count, int minimumCount)
	{
		object[] objects = GenerateItems(count);

		Assert.Throws<ArgumentException>(() => AssertionHelper.MinimumCountCheck(objects, minimumCount, nameof(objects)));
	}
	
	#endregion MinimumCountCheck

	#region DuplicatesCheck

	[Fact]
	public void DuplicatesCheck_Succeeds()
	{
		var ints = new[]
		{
			1,
			2,
			3
		};
		
		AssertionHelper.DuplicatesCheck(ints, nameof(ints));
	}

	[Fact]
	public void DuplicatesCheck_NullDescriptor_Fails()
	{
		string nullDescriptor = null;
		var ints = new[]
		{
			1,
			2,
			3
		};

		Assert.Throws<ArgumentNullException>(() => AssertionHelper.DuplicatesCheck(ints, nullDescriptor));
	}

	[Fact]
	public void DuplicatesCheck_NullItems_Fails()
	{
		object[] nullObjects = null;

		Assert.Throws<ArgumentNullException>(() => AssertionHelper.DuplicatesCheck(nullObjects, nameof(nullObjects)));
	}

	[Fact]
	public void DuplicatesCheck_EmptyItems_Fails()
	{
		object[] emptyList = Array.Empty<object>();
		
		AssertionHelper.DuplicatesCheck(emptyList, nameof(emptyList));
	}

	[Fact]
	public void DuplicatesCheck_DuplicatesPresent_Fails()
	{
		var ints = new[]
		{
			1,
			1,
			2,
			2,
			3,
			3
		};
		
		Assert.Throws<ArgumentException>(() => AssertionHelper.DuplicatesCheck(ints, nameof(ints)));
	}

	#endregion DuplicatesCheck
}