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
            MathHelper.InvalidDoubleCheck(value, "HsvColor::Hue");
            
            _hue = value;
        }
    }

    public double Saturation
    {
        get => _saturation;
        set
        {
            MathHelper.InvalidDoubleCheck(value, "HsvColor::Saturation");

            _saturation = value;
        }
    }

    public double Value
    {
        get => _value;
        set
        {
            MathHelper.InvalidDoubleCheck(value, "HsvColor::Value");

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
        MathHelper.InvalidDoubleCheck(hue, "HsvColor::HsvColor(>hue<, saturation, value)");
        MathHelper.InvalidDoubleCheck(saturation, "HsvColor::HsvColor(hue, >saturation<, value)");
        MathHelper.InvalidDoubleCheck(value, "HsvColor::HsvColor(hue, saturation, >value<)");
        
        Hue = hue;
        Saturation = saturation;
        Value = value;
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