/** \addtogroup Library
 * @{
 */
using Khartyko.InsigniaCreator.Library.Utility;
using Khartyko.InsigniaCreator.Library.Utility.Helpers;

#pragma warning disable CS0660, CS0661, CS0659

namespace Khartyko.InsigniaCreator.Library.Data;

public partial class RgbColor
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
        AssertionHelper.NullCheck(other, nameof(other));

        R = other.R;
        G = other.G;
        B = other.B;
        A = other.A;
    }

    private void ParseHexString(string hexString)
    {
        AssertionHelper.EmptyOrWhitespaceCheck(hexString, nameof(hexString));
        
        string parsedHexValue = hexString.Length > 0 && hexString[0] == '#' ? hexString.Replace("#", "") : hexString;

        AssertionHelper.EmptyOrWhitespaceCheck(parsedHexValue, nameof(parsedHexValue));

        switch (parsedHexValue.Length)
        {
            case 3:
                (R, G, B, A) = Parse3Case(parsedHexValue);
                
                break;
            
            case 4:
            {
                (R, G, B, A) = Parse4Case(parsedHexValue);
                
                break;
            }
            
            case 6:
            {
                (R, G, B, A) = Parse6Case(parsedHexValue);
                
                break;
            }
            
            case 8:
            {
                (R, G, B, A) = Parse8Case(parsedHexValue);
                
                break;
            }

            default:
            {
                ReflectionMetadata metadata = ReflectionHelper.GetCallerMetadata();
                string signature = ReflectionHelper.ConstructMethodSignature(metadata, nameof(hexString));

                throw new ArgumentException($"{signature}\n\t'hexValue' has an incorrect number of characters. Accepted values are in the format 'RGB', 'RGBA', '#RGB', '#RGBA', 'RRGGBB', '#RRGGBB', 'RRGGBBAA', and '#RRGGBBAA'");
            }
        }
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

    public override bool Equals(object? obj)
    {
        if (ReferenceEquals(obj, null))
        {
            return false;
        }

        if (ReferenceEquals(obj, this))
        {
            return true;
        }

        return obj is RgbColor color
            && color.R == R
            && color.G == G
            && color.B == B
            && color.A == A;
    }
}
/** @} */