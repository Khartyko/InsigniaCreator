using Khartyko.InsigniaCreator.Library.Utility.Helpers;

#pragma warning disable CS0659 // Type overrides Object.Equals(object o) but does not override Object.GetHashCode()

#pragma warning disable CS0660, CS0661

namespace Khartyko.InsigniaCreator.Library.Data;

public class HsvColor
{
	private static readonly Vector2 s_hueBounds = new(-0.001, 360.001);
	private static readonly Vector2 s_saturationBounds = new(-0.001, 1.001);
	private static readonly Vector2 s_valueBounds = new(-0.001, 1.001);

	private double _hue;
	private double _saturation;
	private double _value;

	public double Hue
	{
		get => _hue;
		set
		{
			MathHelper.InvalidDoubleCheck(value, "HsvColor::Hue");
			MathHelper.RangeCheck(value, s_hueBounds.X, s_hueBounds.Y, nameof(value));

			_hue = value;
		}
	}

	public double Saturation
	{
		get => _saturation;
		set
		{
			MathHelper.InvalidDoubleCheck(value, "HsvColor::Saturation");
			MathHelper.RangeCheck(value, s_saturationBounds.X, s_saturationBounds.Y, nameof(value));

			_saturation = value;
		}
	}

	public double Value
	{
		get => _value;
		set
		{
			MathHelper.InvalidDoubleCheck(value, "HsvColor::Value");
			MathHelper.RangeCheck(value, s_valueBounds.X, s_valueBounds.Y, nameof(value));

			_value = value;
		}
	}

	public HsvColor()
	{
		Hue = 0;
		Saturation = 0;
		Value = 0;
	}

	public HsvColor(double hue, double saturation, double value)
	{
		MathHelper.InvalidDoubleCheck(hue, nameof(hue));
		MathHelper.InvalidDoubleCheck(saturation, nameof(saturation));
		MathHelper.InvalidDoubleCheck(value, nameof(value));

		MathHelper.RangeCheck(hue, s_hueBounds.X, s_hueBounds.Y, nameof(hue));
		MathHelper.RangeCheck(saturation, s_saturationBounds.X, s_saturationBounds.Y, nameof(hue));
		MathHelper.RangeCheck(value, s_valueBounds.X, s_valueBounds.Y, nameof(hue));

		Hue = hue;
		Saturation = saturation;
		Value = value;
	}

	public HsvColor(HsvColor hsvColor)
	{
		ObjectHelper.NullCheck(hsvColor, nameof(hsvColor));

		Hue = hsvColor.Hue;
		Saturation = hsvColor.Saturation;
		Value = hsvColor.Value;
	}

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