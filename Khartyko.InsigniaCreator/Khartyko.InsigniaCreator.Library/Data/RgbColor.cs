/** \addtogroup Library
 * @{
 */
using Khartyko.InsigniaCreator.Library.Utility;
using Khartyko.InsigniaCreator.Library.Utility.Helpers;

#pragma warning disable CS0660, CS0661, CS0659

namespace Khartyko.InsigniaCreator.Library.Data;

/// <summary>
/// Represents a Color with Red, Green, Blue, and Alpha values.
/// </summary>
public partial class RgbColor
{
    /// <summary>
    /// The Red-component of a color.
    /// </summary>
    public byte R { get; set; }
    
    /// <summary>
    /// The Green-component of a color.
    /// </summary>
    public byte G { get; set; }
    
    /// <summary>
    /// The Blue-component of a color.
    /// </summary>
    public byte B { get; set; }
    
    /// <summary>
    /// The Alpha-component of a color.
    /// </summary>
    public byte A { get; set; }

    /// <summary>
    /// Constructs a color from a hexadecimal color string.
    /// Acceptable values are in the form of "RGB", "RGBA", "RRGGBB", and "RRGGBBAA".
    /// It is optional to prefix it with an octothorpe ('#').
    /// </summary>
    /// <param name="hexValue">A hexadecimal string value.</param>
    /// <exception cref="ArgumentException">Can be thrown during the "descriptor" check, or an invalid string passed in.</exception>
    /// <exception cref="ArgumentNullException">Can be thrown if "hexValue" is null.</exception>
    public RgbColor(string hexValue)
    {
        ParseHexString(hexValue);
    }

    /// <summary>
    /// Construct a color with uniform component values.
    /// </summary>
    /// <param name="value">The value to use for 'R', 'G', and 'B' components.</param>
    public RgbColor(byte value)
        : this(value, 255)
    {
    }

    /// <summary>
    /// Construct a color with uniform component values and specify an Alpha-component.
    /// </summary>
    /// <param name="value">The value to use for 'R', 'G', and 'B' components.</param>
    /// <param name="alpha">The value to use for the Alpha component.</param>
    public RgbColor(byte value = 255, byte alpha = 255)
        : this(value, value, value, alpha)
    {
    }

    /// <summary>
    /// Construct a color from all byte values. The Alpha-component is optional, and defaults to 255 (full opacity).
    /// </summary>
    /// <param name="r">The value to use for the Red-component</param>
    /// <param name="g">The value to use for the Green-component</param>
    /// <param name="b">The value to use for the Blue-component</param>
    /// <param name="a">The value to use for the Alpha-component</param>
    public RgbColor(byte r, byte g, byte b, byte a = 255)
    {
        R = r;
        G = g;
        B = b;
        A = a;
    }

    /// <summary>
    /// Construct a color from an existing color.
    /// </summary>
    /// <param name="other">An existing color to duplicate.</param>
    /// <exception cref="ArgumentNullException">If the "other" color is null.</exception>
    public RgbColor(RgbColor other)
    {
        AssertionHelper.NullCheck(other, nameof(other));

        R = other.R;
        G = other.G;
        B = other.B;
        A = other.A;
    }

    /// <summary>
    /// Get a color's hexadecimal string value. The octothorpe ('#') and alpha values are optional.
    /// </summary>
    /// <param name="includeOctothorpe">Decides whether the result is prefixed with an octothorpe.</param>
    /// <param name="includeAlpha">Decides whether the result is suffixed with the color's Alpha-component.</param>
    /// <returns>A hexadecimal string representing the values in this color instance.</returns>
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

    /// <summary>
    /// An override of ToString() that creates a string.
    /// </summary>
    /// <returns>A string containing the 'R', 'G', 'B', and 'A' values.</returns>
    public override string ToString()
    {
        string r = R.ToString().PadLeft(3, '0');
        string g = G.ToString().PadLeft(3, '0');
        string b = B.ToString().PadLeft(3, '0');
        string a = A.ToString().PadLeft(3, '0');
        
        return $"{{ r: {r}, g: {g}, b: {b}, a: {a} }}";
    }

    /// <summary>
    /// Override that compares the 'R', 'G', 'B', and 'A' values of this RgbColor instance and an object.
    /// </summary>
    /// <remarks>
    /// The following outcomes are possible:
    /// - If 'obj' is null, it'll return false.
    /// - If 'obj' is this, it'll return true.
    /// - Otherwise, the values are compared outright.
    /// </remarks>
    /// <param name="obj">The object to compare to this RgbColor instance.</param>
    /// <returns>A boolean value if the object is equal to this RgbColor instance.</returns>
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

    /// <summary>
    /// Parses a hexadecimal string into the components of an RgbColor.
    /// </summary>
    /// <param name="hexString">The hexadecimal string value.</param>
    /// <exception cref="ArgumentException">Can be thrown during the "descriptor" check, or an invalid string passed in.</exception>
    /// <exception cref="ArgumentNullException">Can be thrown if "hexValue" is null.</exception>
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
}
/** @} */