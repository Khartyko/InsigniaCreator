/** \addtogroup Library
 * @{
 */

namespace Khartyko.InsigniaCreator.Library.Utility.Helpers;

/// <summary>
/// A collection of static maths-related methods.
/// </summary>
public static class MathHelper
{
    private const double c_tolerance = 0.0001;

    /// <summary>
    /// Checks if an integer value is odd.
    /// </summary>
    /// <param name="value">The value to check.</param>
    /// <returns>True if 'value' is odd, false otherwise.</returns>
    public static bool IsOdd(int value) => Math.Abs(value % 2) == 1;
    
    /// <summary>
    /// Checks if an integer value is even.
    /// </summary>
    /// <param name="value">The value to check.</param>
    /// <returns>True if 'value' is even, false otherwise.</returns>
    public static bool IsEven(int value) => Math.Abs(value % 2) == 0;

    /// <summary>
    /// Checks if two doubles are equal.
    /// </summary>
    /// <param name="d0">The first operand.</param>
    /// <param name="d1">The second operand.</param>
    /// <exception cref="ArgumentException">Can be thrown if 'd0' or 'd1' is NaN, PositiveInfinity, or NegativeInfinity.</exception>
    /// <returns>True if 'd0' and 'd1' are equal, false otherwise.</returns>
    public static bool Equals(double d0, double d1)
    {
        AssertionHelper.InvalidDoubleCheck(d0, nameof(d0));
        AssertionHelper.InvalidDoubleCheck(d1, nameof(d1));

        return Math.Abs(d0 - d1) < c_tolerance;
    }

    /// <summary>
    /// Checks if one double is less than another.
    /// </summary>
    /// <param name="d0">The first operand.</param>
    /// <param name="d1">The second operand.</param>
    /// <exception cref="ArgumentException">Can be thrown if 'd0' or 'd1' is NaN, PositiveInfinity, or NegativeInfinity.</exception>
    /// <returns>True of d0 is less than d1, false otherwise.</returns>
    public static bool LessThan(double d0, double d1)
    {
        AssertionHelper.InvalidDoubleCheck(d0, nameof(d0));
        AssertionHelper.InvalidDoubleCheck(d1, nameof(d1));

        return d0 < d1;
    }

    /// <summary>
    /// Checks if one double is greater than another.
    /// </summary>
    /// <param name="d0">The first operand.</param>
    /// <param name="d1">The second operand.</param>
    /// <exception cref="ArgumentException">Can be thrown if 'd0' or 'd1' is NaN, PositiveInfinity, or NegativeInfinity.</exception>
    /// <returns>True of d0 is greater than d1, false otherwise.</returns>
    public static bool GreaterThan(double d0, double d1)
    {
        AssertionHelper.InvalidDoubleCheck(d0, nameof(d0));
        AssertionHelper.InvalidDoubleCheck(d1, nameof(d1));

        return d0 > d1;
    }

    /// <summary>
    /// Round a double to 3 decimal places.
    /// </summary>
    /// <param name="value">The double to round.</param>
    /// <exception cref="ArgumentException">Can be thrown if 'value' is NaN, PositiveInfinity, or NegativeInfinity.</exception>
    /// <returns>A double that has been rounded to 3 decimal places.</returns>
    public static double Round(double value)
    {
        AssertionHelper.InvalidDoubleCheck(value, nameof(value));

        return Math.Round(value * 1000) / 1000;
    }

    /// <summary>
    /// Gets the square root of a double.
    /// </summary>
    /// <param name="value">The double to square root.</param>
    /// <exception cref="ArgumentOutOfRangeException">Can be thrown if 'value' is negative.</exception>
    /// <returns>A double that is the result of the square root operation.</returns>
    public static double Sqrt(double value)
    {
        AssertionHelper.PositiveCheck(value, nameof(value));

        return Round(Math.Sqrt(value));
    }

    /// <summary>
    /// Gets π with 3 decimal places.
    /// </summary>
    /// <returns>π with 3 decimal places.</returns>
    public static double Pi() => Round(Pi(1, 1));

    /// <summary>
    /// Get π multiplied be a specified numerator and denominator.
    /// </summary>
    /// <param name="numerator">The numerator to multiply π by.</param>
    /// <param name="denominator">The denominator to divide by.</param>
    /// <exception cref="ArgumentException">Can be thrown if either value is NaN, PositiveInfinity, or NegativeInfinity, or if 'denominator' is 0.0.</exception>
    /// <returns>A double value that is the result of the operation.</returns>
    public static double Pi(double numerator, double denominator)
    {
        AssertionHelper.ZeroCheck(denominator, nameof(denominator));
        AssertionHelper.InvalidDoubleCheck(numerator, nameof(numerator));

        return Math.PI * numerator / denominator;
    }

    /// <summary>
    /// Get the Cosine of an angle.
    /// </summary>
    /// <param name="theta">The angle, in radians.</param>
    /// <exception cref="ArgumentException">Can be thrown if 'theta' is NaN, PositiveInfinity, or NegativeInfinity.</exception>
    /// <returns>The Cosine of the specified angle.</returns>
    public static double Cos(double theta)
    {
        AssertionHelper.InvalidDoubleCheck(theta, nameof(theta));

        return Round(Math.Cos(theta));
    }

    /// <summary>
    /// Get the Sine of an angle.
    /// </summary>
    /// <param name="theta">The angle, in radians.</param>
    /// <exception cref="ArgumentException">Can be thrown if 'theta' is NaN, PositiveInfinity, or NegativeInfinity.</exception>
    /// <returns>The Sine of the specified angle.</returns>
    public static double Sin(double theta)
    {
        AssertionHelper.InvalidDoubleCheck(theta, nameof(theta));

        return Round(Math.Sin(theta));
    }

    /// <summary>
    /// Converts an angle from radians to degrees.
    /// </summary>
    /// <param name="radians">The angle to convert.</param>
    /// <exception cref="ArgumentException">Can be thrown if 'radians' is NaN, PositiveInfinity, or NegativeInfinity.</exception>
    /// <returns>An angle in degrees.</returns>
    public static double ToDegrees(double radians)
    {
        AssertionHelper.InvalidDoubleCheck(radians, nameof(radians));

        return Round(radians * 180 / Math.PI);
    }

    /// <summary>
    /// Converts an angle from degrees to radians.
    /// </summary>
    /// <param name="radians">The angle to convert.</param>
    /// <exception cref="ArgumentException">Can be thrown if 'degrees' is NaN, PositiveInfinity, or NegativeInfinity.</exception>
    /// <returns>An angle in radians.</returns>
    public static double ToRadians(double degrees)
    {
        AssertionHelper.InvalidDoubleCheck(degrees, nameof(degrees));

        return Round(degrees * Math.PI / 180);
    }

    /// <summary>
    /// Gets the interior angle of two sides in a regular polygon.
    /// </summary>
    /// <param name="sideCount">The number sides that the polygon has.</param>
    /// <returns>The interior angle based on the number of sides.</returns>
    public static double GetInteriorAngleFromSideCount(int sideCount)
    {
        AssertionHelper.MinimumCheck(sideCount, 3, nameof(sideCount));

        double totalDegrees = (sideCount - 2) * 180.0;
        double innerFacingAngle = totalDegrees / sideCount;

        return Round(innerFacingAngle);
    }
}

/** @} */