using Khartyko.InsigniaCreator.Library.Data;

#pragma warning disable CS8604 // Possible null reference argument.

namespace Khartyko.InsigniaCreator.Library.Testing.Data;

public class HsvColorTests
{
	#region Hue Property

	[Fact]
	public void Hue_Get_Succeeds()
	{
		var hsvColor = new HsvColor();
		
		Assert.Equal(0.0, hsvColor.Hue);
	}

	[Theory]
	[InlineData(0.0, 1.0)]
	[InlineData(360.0, 0.0)]
	public void Hue_Set_Succeeds(float initialHue, float expectedHue)
	{
		var hsvColor = new HsvColor(initialHue, 0.0, 0.0);

		Assert.NotEqual(expectedHue, hsvColor.Hue);
		
		hsvColor.Hue = expectedHue;
		
		Assert.Equal(expectedHue, hsvColor.Hue);
	}

	[Fact]
	public void Hue_Set_InvalidDouble_Fails()
	{
		var hsvColor = new HsvColor();

		Assert.Throws<ArgumentException>(() => hsvColor.Hue = double.NaN);
		Assert.Throws<ArgumentException>(() => hsvColor.Hue = double.PositiveInfinity);
		Assert.Throws<ArgumentException>(() => hsvColor.Hue = double.NegativeInfinity);
	}

	[Theory]
	[InlineData(-1.0)]
	[InlineData(361.0)]
	public void Hue_Set_ValueOutOfRange_Fails(double hue)
	{
		var hsvColor = new HsvColor();

		Assert.Throws<ArgumentOutOfRangeException>(() => hsvColor.Hue = hue);
	}
	
	#endregion Hue Property

	#region Saturation Property

	[Fact]
	public void Saturation_Get_Succeeds()
	{
		var hsvColor = new HsvColor();
		
		Assert.Equal(0.0, hsvColor.Saturation);
	}

	[Theory]
	[InlineData(0.0, 1.0)]
	[InlineData(1.0, 0.0)]
	public void Saturation_Set_Succeeds(float initialSaturation, float expectedSaturation)
	{
		var hsvColor = new HsvColor(0.0, initialSaturation, 0.0);

		Assert.NotEqual(expectedSaturation, hsvColor.Saturation);
		
		hsvColor.Saturation = expectedSaturation;
		
		Assert.Equal(expectedSaturation, hsvColor.Saturation);
	}

	[Fact]
	public void Saturation_Set_Fails()
	{
		var hsvColor = new HsvColor();

		Assert.Throws<ArgumentException>(() => hsvColor.Saturation = double.NaN);
		Assert.Throws<ArgumentException>(() => hsvColor.Saturation = double.PositiveInfinity);
		Assert.Throws<ArgumentException>(() => hsvColor.Saturation = double.NegativeInfinity);
	}

	[Theory]
	[InlineData(-1.0)]
	[InlineData(2.0)]
	public void Saturation_Set_ValueOutOfRange_Fails(double saturation)
	{
		var hsvColor = new HsvColor();

		Assert.Throws<ArgumentOutOfRangeException>(() => hsvColor.Saturation = saturation);
	}

	#endregion Saturation Property

	#region Value Property

	[Fact]
	public void Value_Get_Succeeds()
	{
		var hsvColor = new HsvColor();
		
		Assert.Equal(0.0, hsvColor.Value);
	}

	[Theory]
	[InlineData(0.0, 1.0)]
	[InlineData(1.0, 0.0)]
	public void Value_Set_Succeeds(float initialValue, float expectedValue)
	{
		var hsvColor = new HsvColor(0.0, 0.0, initialValue);

		Assert.NotEqual(expectedValue, hsvColor.Value);
		
		hsvColor.Value = expectedValue;
		
		Assert.Equal(expectedValue, hsvColor.Value);
	}

	[Fact]
	public void Value_Set_Fails()
	{
		var hsvColor = new HsvColor();

		Assert.Throws<ArgumentException>(() => hsvColor.Value = double.NaN);
		Assert.Throws<ArgumentException>(() => hsvColor.Value = double.PositiveInfinity);
		Assert.Throws<ArgumentException>(() => hsvColor.Value = double.NegativeInfinity);
	}

	[Theory]
	[InlineData(-1.0)]
	[InlineData(2.0)]
	public void Value_Set_ValueOutOfRange_Fails(double value)
	{
		var hsvColor = new HsvColor();

		Assert.Throws<ArgumentOutOfRangeException>(() => hsvColor.Value = value);
	}

	#endregion Value Property

	#region Constructor

	[Fact]
	public void HsvColor_Parameterless_Succeeds()
	{
		var hsvColor = new HsvColor();
		
		Assert.Equal(0.0, hsvColor.Hue);
		Assert.Equal(0.0, hsvColor.Saturation);
		Assert.Equal(0.0, hsvColor.Value);
	}

	[Theory]
	[InlineData(0.0, 0.0, 0.0)]
	[InlineData(10.0, 1.0, 0.5)]
	[InlineData(360.0, 1.0, 1.0)]
	public void HsvColor_DoubleParameters_Succeeds(double hue, double saturation, double value)
	{
		var hsvColor = new HsvColor(hue, saturation, value);
		
		Assert.Equal(hue, hsvColor.Hue);
		Assert.Equal(saturation, hsvColor.Saturation);
		Assert.Equal(value, hsvColor.Value);
	}

	[Theory]
	[InlineData(-1.0)]
	[InlineData(361.0)]
	public void HsvColor_DoubleParameters_HueOutOfRange_Fails(double hue)
	{
		Assert.Throws<ArgumentOutOfRangeException>(() => new HsvColor(hue, 0.0, 0.0));
	}

	[Theory]
	[InlineData(-1.0)]
	[InlineData(1.1)]
	public void HsvColor_DoubleParameters_SaturationOutOfRange_Fails(double saturation)
	{
		Assert.Throws<ArgumentOutOfRangeException>(() => new HsvColor(0.0, saturation, 0.0));
	}

	[Theory]
	[InlineData(-1.0)]
	[InlineData(1.1)]
	public void HsvColor_DoubleParameters_ValueOutOfRange_Fails(double value)
	{
		Assert.Throws<ArgumentOutOfRangeException>(() => new HsvColor(0.0, 0.0, value));
	}

	[Theory]
	[InlineData(0.0, 0.0, 0.0)]
	[InlineData(360.0, 1.0, 1.0)]
	[InlineData(180.0, 0.5, 0.5)]
	[InlineData(180.0, 0.25, 0.75)]
	public void HsvColor_CopyConstructor_Succeeds(double hue, double saturation, double value)
	{
		var initialHsvColor = new HsvColor(hue, saturation, value);

		var duplicateHsvColor = new HsvColor(initialHsvColor);
		
		Assert.Equal(hue, duplicateHsvColor.Hue);
		Assert.Equal(saturation, duplicateHsvColor.Saturation);
		Assert.Equal(value, duplicateHsvColor.Value);
	}

	[Fact]
	public void HsvColor_CopyConstructor_NullValue_Fails()
	{
#pragma warning disable CS8625 // Cannot convert null literal to non-nullable reference type.
		Assert.Throws<ArgumentNullException>(() => new HsvColor(null));
#pragma warning restore CS8625 // Cannot convert null literal to non-nullable reference type.
	}
	
	#endregion Constructor

	#region Equals

	[Theory]
	[InlineData(0.0, 0.0, 0.0)]
	[InlineData(360.0, 1.0, 1.0)]
	[InlineData(180.0, 0.5, 0.5)]
	[InlineData(180.0, 0.25, 0.75)]
	public void HsvColor_Equals_Succeeds(double hue, double saturation, double value)
	{
		var initialHsvColor = new HsvColor(hue, saturation, value);

		var duplicateHsvColor = new HsvColor(hue, saturation, value);

		Assert.True(initialHsvColor.Equals(duplicateHsvColor));
		
		// ReSharper disable EqualExpressionComparison
		Assert.True(initialHsvColor.Equals(initialHsvColor));
		Assert.True(duplicateHsvColor.Equals(duplicateHsvColor));
		// ReSharper restore EqualExpressionComparison
	}

	[Theory]
	[InlineData(360.0, 1.0, 1.0)]
	[InlineData(180.0, 0.5, 0.5)]
	[InlineData(180.0, 0.25, 0.75)]
	public void HsvColor_Equals_ValidObject_Fails(double hue, double saturation, double value)
	{
		var subject = new HsvColor();
		var candidate = new HsvColor(hue, saturation, value);

		Assert.False(subject.Equals(candidate));
		Assert.False(candidate.Equals(subject));
	}

	[Theory]
	[InlineData(0.0, 0.0, 0.0)]
	[InlineData(360.0, 1.0, 1.0)]
	[InlineData(180.0, 0.5, 0.5)]
	[InlineData(180.0, 0.25, 0.75)]
	public void HsvColor_Equals_NullObject_Fails(double hue, double saturation, double value)
	{
		var initialHsvColor = new HsvColor(hue, saturation, value);

		Assert.False(initialHsvColor.Equals(null));
	}
	
	#endregion Equals
}