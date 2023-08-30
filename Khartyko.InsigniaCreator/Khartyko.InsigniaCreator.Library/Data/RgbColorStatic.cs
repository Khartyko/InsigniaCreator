/** \addtogroup Library
 * @{
 */

using Khartyko.InsigniaCreator.Library.Utility;
using Khartyko.InsigniaCreator.Library.Utility.Helpers;

namespace Khartyko.InsigniaCreator.Library.Data;

/// <summary>
/// Private static methods for RgbColor
/// </summary>
public partial class RgbColor
{
    /// <summary>
    /// Converts a char into a byte between 0-15.
    /// </summary>
    /// <remarks>
    /// Acceptable values are between 48 and 57, 65 and 70, 97 and 102.
    /// </remarks>
    /// <param name="value">A char that represents a single character of a hexadecimal color value.</param>
    /// <returns>A byte representation from the char value.</returns>
    /// <exception cref="ArgumentOutOfRangeException">'value' isn't a valid hexadecimal character.</exception>
    private static byte ParseChar(char value)
    {
        byte result;
        int intValue = value;
        
        switch (intValue)
        {
            case >= 48 and <= 57:
            {
                result = (byte)(intValue - 48);
                
                break;
            }
            
            case >= 65 and <= 70:
            {
                result = (byte)(intValue - 55);
                
                break;
            }
            
            case >= 97 and <= 102:
            {
                result = (byte)(intValue - 87);
                
                break;
            }

            default:
            {
                ReflectionMetadata metadata = ReflectionHelper.GetCallerMetadata();
                string signature = ReflectionHelper.ConstructMethodSignature(metadata, nameof(value));

                throw new ArgumentOutOfRangeException(nameof(value), $"{signature}\n\t'value' is not an expected hexadecimal character; got '{value}' -> '{intValue}'");
            }
        }

        return result;
    }

    /// <summary>
    /// Parses a hexadecimal color string into a byte.
    /// </summary>
    /// <param name="value">A 2-character string that represents a single color value.</param>
    /// <returns>4 bytes representing 'R', 'G', 'B', and 'A' values.</returns>
    private static byte ParseValue(string value)
    {
        byte firstPlace = ParseChar(value[0]);
        byte secondPlace = ParseChar(value[1]);

        return (byte)(firstPlace * 16 + secondPlace);
    }

    /// <summary>
    /// Parses a color in the form of "RGB" and returns all 4 color bytes.
    /// </summary>
    /// <param name="hexValue">A hexadecimal color string.</param>
    /// <returns>4 bytes representing 'R', 'G', 'B', and 'A' values.</returns>
    private static (byte, byte, byte, byte) Parse3Case(string hexValue)
    {
        char r = hexValue[0];
        char g = hexValue[1];
        char b = hexValue[2];

        return Parse8Case($"{r}{r}{g}{g}{b}{b}FF");
    }

    /// <summary>
    /// Parses a color in the form of "RGBA" and returns all 4 color bytes.
    /// </summary>
    /// <param name="hexValue">A hexadecimal color string.</param>
    /// <returns>4 bytes representing 'R', 'G', 'B', and 'A' values.</returns>
    private static (byte, byte, byte, byte) Parse4Case(string hexValue)
    {
        char r = hexValue[0];
        char g = hexValue[1];
        char b = hexValue[2];
        char a = hexValue[3];

        return Parse8Case($"{r}{r}{g}{g}{b}{b}{a}{a}");
    }

    /// <summary>
    /// Parses a color in the form of "RRGGBB" and returns all 4 color bytes.
    /// </summary>
    /// <param name="hexValue">A hexadecimal color string.</param>
    /// <returns>4 bytes representing 'R', 'G', 'B', and 'A' values.</returns>
    private static (byte, byte, byte, byte) Parse6Case(string hexValue)
    {
        return Parse8Case($"{hexValue}FF");
    }

    /// <summary>
    /// Parses a color in the form of "RRGGBBAA" and returns all 4 color bytes.
    /// </summary>
    /// <param name="hexValue">A hexadecimal color string.</param>
    /// <returns>4 bytes representing 'R', 'G', 'B', and 'A' values.</returns>
    private static (byte, byte, byte, byte) Parse8Case(string hexValue)
    {
        byte r = ParseValue(hexValue[..2]);
        byte g = ParseValue(hexValue[2..4]);
        byte b = ParseValue(hexValue[4..6]);
        byte a = ParseValue(hexValue[6..8]);

        return (r, g, b, a);
    }
}

/** @} */