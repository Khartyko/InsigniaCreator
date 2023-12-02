/** \addtogroup Library
 * @{
 */

using Khartyko.InsigniaCreator.Library.Utility.Helpers;

#pragma warning disable CS0659, CS0660, CS0661

namespace Khartyko.InsigniaCreator.Library.Data;

/// <summary>
/// This class represents a color comprised of Hue, Saturation, and Value.
/// </summary>
public class HsvColor
{
	private static readonly Vector2 s_hueBounds = new(-0.001, 360.001);
	private static readonly Vector2 s_saturationBounds = new(-0.001, 1.001);
	private static readonly Vector2 s_valueBounds = new(-0.001, 1.001);

	private double _hue;
	private double _saturation;
	private double _value;

	/// <summary>
	/// This is which hue the color leans towards (i.e., red, blue, green, yellow).
	/// It is can only be values between 0 and 360 degrees.
	/// </summary>
	/// <exception cref="ArgumentException">Can be thrown if 'value' is NaN, PositiveInfinity, or 'NegativeInfinity'.</exception>
	/// <exception cref="ArgumentOutOfRangeException">Can be thrown if 'value' isn't between 0.0 and 360.0 (inclusively).</exception>
	public double Hue
	{
		get => _hue;
		set
		{
			AssertionHelper.InvalidDoubleCheck(value, nameof(value));
			AssertionHelper.RangeCheck(value, s_hueBounds.X, s_hueBounds.Y, nameof(value));

			_hue = value;
		}
	}

	/// <summary>
	/// This is how much of the hue is present.
	/// It only accepts values between 0.0 and 1.0.
	/// </summary>
	/// <exception cref="ArgumentException">Can be thrown if 'value' is NaN, PositiveInfinity, or 'NegativeInfinity'.</exception>
	/// <exception cref="ArgumentOutOfRangeException">Can be thrown if 'value' isn't between 0.0 and 1.0 (inclusively).</exception>
	public double Saturation
	{
		get => _saturation;
		set
		{
			AssertionHelper.InvalidDoubleCheck(value, nameof(value));
			AssertionHelper.RangeCheck(value, s_saturationBounds.X, s_saturationBounds.Y, nameof(value));

			_saturation = value;
		}
	}

	/// <summary>
	/// This is how bright the color is.
	/// It only accepts values between 0.0 and 1.0.
	/// </summary>
	/// <exception cref="ArgumentException">Can be thrown if 'value' is NaN, PositiveInfinity, or 'NegativeInfinity'.</exception>
	/// <exception cref="ArgumentOutOfRangeException">Can be thrown if 'value' isn't between 0.0 and 1.0 (inclusively).</exception>
	public double Value
	{
		get => _value;
		set
		{
			AssertionHelper.InvalidDoubleCheck(value, nameof(value));
			AssertionHelper.RangeCheck(value, s_valueBounds.X, s_valueBounds.Y, nameof(value));

			_value = value;
		}
	}

	/// <summary>
	/// This constructs a default HSV Color with the Hue, Saturation, and Value all being 0.0.
	/// </summary>
	public HsvColor()
	{
		Hue = 0;
		Saturation = 0;
		Value = 0;
	}

	/// <summary>
	/// This creates an HSV Color with the Hue, Saturation, and Value being specified.
	/// The values are strict
	/// </summary>
	/// <param name="hue">A double between 0.0 and 360.0 that represents the color's hue.</param>
	/// <param name="saturation">A number between 0.0 and 1.0 that represents the color's saturation.</param>
	/// <param name="value">A number 0.0 and 1.0 that represents the color's brightness/gamma.</param>
	/// <exception cref="ArgumentException">Can be thrown if any of the values passed in are NaN, PositiveInfinity, or NegativeInfinity.</exception>
	/// <exception cref="ArgumentOutOfRangeException">Can be thrown if any of the values passed in aren't within their acceptable ranges.</exception>
	public HsvColor(double hue, double saturation, double value)
	{
		AssertionHelper.InvalidDoubleCheck(hue, nameof(hue));
		AssertionHelper.InvalidDoubleCheck(saturation, nameof(saturation));
		AssertionHelper.InvalidDoubleCheck(value, nameof(value));

		AssertionHelper.RangeCheck(hue, s_hueBounds.X, s_hueBounds.Y, nameof(hue));
		AssertionHelper.RangeCheck(saturation, s_saturationBounds.X, s_saturationBounds.Y, nameof(hue));
		AssertionHelper.RangeCheck(value, s_valueBounds.X, s_valueBounds.Y, nameof(hue));

		Hue = hue;
		Saturation = saturation;
		Value = value;
	}

	/// <summary>
	/// This copies the values of one HSV Color to another HSV Color.
	/// </summary>
	/// <exception cref="ArgumentNullException">Can be thrown if 'hsvColor' is null.</exception>
	/// <param name="hsvColor">An existing HSV Color.</param>
	public HsvColor(HsvColor hsvColor)
	{
		AssertionHelper.NullCheck(hsvColor, nameof(hsvColor));

		Hue = hsvColor.Hue;
		Saturation = hsvColor.Saturation;
		Value = hsvColor.Value;
	}

	/// <summary>
	/// An override of the default Equals method that checks if it's Hue, Saturation, and Values are equal
	/// </summary>
	/// <remarks>
	/// If the object is null, it'll return false.
	/// If the object is 'this', it'll return true.
	/// Otherwise, the values are compared outright.
	/// </remarks>
	/// <param name="obj">The object that is compared against this instance of an HSV Color.</param>
	/// <returns>A boolean value as to whether 'obj' is equal to this instance of an HSV Color.</returns>
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

		return obj is HsvColor hsvColor
		       && MathHelper.Equals(Hue, hsvColor.Hue)
		       && MathHelper.Equals(Saturation, hsvColor.Saturation)
		       && MathHelper.Equals(Value, hsvColor.Value);
	}
}

/** @} */