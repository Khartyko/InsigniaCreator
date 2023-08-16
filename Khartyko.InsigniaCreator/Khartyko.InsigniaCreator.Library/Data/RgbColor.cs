using Khartyko.InsigniaCreator.Library.Utility.Helpers;

#pragma warning disable CS0660, CS0661, CS0659

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

    public RgbColor(byte r, byte g, byte b, byte a = 255)
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

    private static byte ParseChar(char value)
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

    private static byte ParseValue(string value)
    {
        var firstPlace = ParseChar(value[0]);
        var secondPlace = ParseChar(value[1]);

        return (byte)(firstPlace * 16 + secondPlace);
    }

    private static (byte, byte, byte, byte) Parse3Case(string hexValue)
    {
        var r = hexValue[0];
        var g = hexValue[1];
        var b = hexValue[2];

        return Parse8Case($"{r}{r}{g}{g}{b}{b}FF");
    }

    private static (byte, byte, byte, byte) Parse4Case(string hexValue)
    {
        var r = hexValue[0];
        var g = hexValue[1];
        var b = hexValue[2];
        var a = hexValue[3];

        return Parse8Case($"{r}{r}{g}{g}{b}{b}{a}{a}");
    }

    private static (byte, byte, byte, byte) Parse6Case(string hexValue)
    {
        return Parse8Case($"{hexValue}FF");
    }

    private static (byte, byte, byte, byte) Parse8Case(string hexValue)
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
        string octothorpe = includeOctothorpe ? "#" : "";
        var bytes = new List<byte> { R, G, B };

        if (includeAlpha)
        {
            bytes.Add(A);
        }

        string hexString = Convert.ToHexString(bytes.ToArray());

        return $"{octothorpe}{hexString}";
    }

    public override string ToString()
    {
        string r = R.ToString().PadLeft(3, '0');
        string g = G.ToString().PadLeft(3, '0');
        string b = B.ToString().PadLeft(3, '0');
        string a = A.ToString().PadLeft(3, '0');
        
        return $"{{ r: {r}, g: {g}, b: {b}, a: {a} }}";
    }

    public override bool Equals(object? obj) => obj is RgbColor color
                                                && color.R == R
                                                && color.G == G
                                                && color.B == B
                                                && color.A == A;
}