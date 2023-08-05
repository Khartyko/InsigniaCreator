using Khartyko.InsigniaCreator.Library.Utility.Helpers;

namespace Khartyko.InsigniaCreator.Library.Data;

public class HsvColor
{
    private double _hue;
    private double _saturation;
    private double _value;

    public double Hue
    {
        get => _hue;
        set
        {
            MathHelper.Verify(value, "HsvColor::Hue");
            
            _hue = value;
        }
    }

    public double Saturation
    {
        get => _saturation;
        set
        {
            MathHelper.Verify(value, "HsvColor::Saturation");

            _saturation = value;
        }
    }

    public double Value
    {
        get => _value;
        set
        {
            MathHelper.Verify(value, "HsvColor::Value");

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
        MathHelper.Verify(hue, "HsvColor::HsvColor(>hue<, saturation, value)");
        MathHelper.Verify(saturation, "HsvColor::HsvColor(hue, >saturation<, value)");
        MathHelper.Verify(value, "HsvColor::HsvColor(hue, saturation, >value<)");
        
        Hue = double.IsInfinity(hue) ? 0 : hue;
        Saturation = double.IsInfinity(saturation) ? 0 : saturation;
        Value = double.IsInfinity(value) ? 0 : value;
    }

    public override bool Equals(object? obj)
    {
        return obj is HsvColor hsvColor 
               && this == hsvColor;
    }

    public override int GetHashCode()
    {
        // ReSharper disable NonReadonlyMemberInGetHashCode
        return HashCode.Combine(Hue, Saturation, Value);
        // ReSharper restore NonReadonlyMemberInGetHashCode
    }

    public static bool operator ==(HsvColor left, HsvColor right)
    {
        ObjectHelper.NullCheck(left, nameof(left));
        ObjectHelper.NullCheck(right, nameof(right));
        
        return MathHelper.Equals(left.Hue, right.Hue)
               && MathHelper.Equals(left.Saturation, right.Saturation)
               && MathHelper.Equals(left.Value, right.Value);
    }

    public static bool operator !=(HsvColor left, HsvColor right)
    {
        ObjectHelper.NullCheck(left, nameof(left));
        ObjectHelper.NullCheck(right, nameof(right));

        return !MathHelper.Equals(left.Hue, right.Hue)
               || !MathHelper.Equals(left.Saturation, right.Saturation)
               || !MathHelper.Equals(left.Value, right.Value);
    }
}