using Khartyko.InsigniaCreator.Library.Utility.Helpers;

namespace Khartyko.InsigniaCreator.Library.Data;

public class RgbColor
{
    public byte R { get; set; }
    public byte G { get; set; }
    public byte B { get; set; }
    public byte A { get; set; }

    public RgbColor(string hexValue)
    {
        ParseHexString(hexValue);
    }

    public RgbColor(byte value)
        : this(value, 255)
    {
    }

    public RgbColor(byte value = 255, byte alpha = 255)
        : this(value, value, value, alpha)
    {
    }

    public RgbColor(byte r = 255, byte g = 255, byte b = 255, byte a = 255)
    {
        R = r;
        G = g;
        B = b;
        A = a;
    }

    public RgbColor(RgbColor other)
    {
        ObjectHelper.NullCheck(other, "Color::Color(>other<)");

        R = other.R;
        G = other.G;
        B = other.B;
        A = other.A;
    }

    private byte ParseChar(char value)
    {
        int intValue = value;

        return intValue switch
        {
            // between 48 (0) - 57 (9)
            >= 48 and <= 57 => (byte)(intValue - 48),
            >= 65 and <= 70 => (byte)(intValue - 55),
            >= 97 and <= 102 => (byte)(intValue - 87),
            _ => throw new ArgumentOutOfRangeException(nameof(value),
                "Color::parseChar(>value<); 'value' is not an expected hexadecimal character")
        };
    }

    private byte ParseValue(string value)
    {
        var firstPlace = ParseChar(value[0]);
        var secondPlace = ParseChar(value[1]);

        return (byte)(firstPlace * 16 + secondPlace);
    }

    private (byte, byte, byte, byte) Parse3Case(string hexValue)
    {
        var r = hexValue[0];
        var g = hexValue[1];
        var b = hexValue[2];

        return Parse8Case($"{r}{r}{g}{g}{b}{b}FF");
    }

    private (byte, byte, byte, byte) Parse4Case(string hexValue)
    {
        var r = hexValue[0];
        var g = hexValue[1];
        var b = hexValue[2];
        var a = hexValue[3];

        return Parse8Case($"{r}{r}{g}{g}{b}{b}{a}{a}");
    }

    private (byte, byte, byte, byte) Parse6Case(string hexValue)
    {
        return Parse8Case($"{hexValue}FF");
    }

    private (byte, byte, byte, byte) Parse8Case(string hexValue)
    {
        var r = ParseValue(hexValue[..2]);
        var g = ParseValue(hexValue[2..4]);
        var b = ParseValue(hexValue[4..6]);
        var a = ParseValue(hexValue[6..8]);

        return (r, g, b, a);
    }

    private void ParseHexString(string hexString)
    {
        StringHelper.EmptyOrWhitespaceCheck(hexString, "Color::parseHexString(>hexString<)");
        
        var parsedHexValue = hexString.Length > 0 && hexString[0] == '#' ? hexString.Replace("#", "") : hexString;

        StringHelper.EmptyOrWhitespaceCheck(parsedHexValue, "Color::parseHexString(>parsedHexValue<)");

        var length = parsedHexValue.Length;

        var (r, g, b, a) = length switch
        {
            3 => Parse3Case(parsedHexValue),
            4 => Parse4Case(parsedHexValue),
            6 => Parse6Case(parsedHexValue),
            8 => Parse8Case(parsedHexValue),
            _ => throw new ArgumentException(
                "Color::parseHexString(>hexString<); 'hexValue' has an incorrect number of characters. Accepted values are in the format 'RGB', 'RGBA', '#RGB', '#RGBA', 'RRGGBB', '#RRGGBB', 'RRGGBBAA', and '#RRGGBBAA'",
                nameof(hexString))
        };

        R = r;
        G = g;
        B = b;
        A = a;
    }

    public string HexString(bool includeOctothorpe = true, bool includeAlpha = true)
    {
        var octothorpe = includeOctothorpe ? "#" : "";
        var bytes = new List<byte> { R, G, B };

        if (includeAlpha)
        {
            bytes.Add(A);
        }

        var hexString = Convert.ToHexString(bytes.ToArray());

        return $"{octothorpe}{hexString}";
    }

    public override string ToString() => $"R: {R.ToString().PadLeft(3, '0')}, G: {G.ToString().PadLeft(3, '0')}, B: {B.ToString().PadLeft(3, '0')}, A: {A.ToString().PadLeft(3, '0')}";

    public override bool Equals(object? obj) => obj is RgbColor color
                                                && color.R == R
                                                && color.G == G
                                                && color.B == B
                                                && color.A == A;

    public override int GetHashCode()
    {
        // ReSharper disable NonReadonlyMemberInGetHashCode
        return HashCode.Combine(R, G, B, A);
        // ReSharper restore NonReadonlyMemberInGetHashCode
    }

    public static bool operator ==(RgbColor left, RgbColor right) => left.R == right.R
                                                               && left.G == right.G
                                                               && left.B == right.B
                                                               && left.A == right.A;

    public static bool operator !=(RgbColor left, RgbColor right) => left.R != right.R
                                                               || left.G != right.G
                                                               || left.B != right.B
                                                               || left.A != right.A;
}