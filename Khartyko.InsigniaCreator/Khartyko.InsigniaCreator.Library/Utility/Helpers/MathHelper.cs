namespace Khartyko.InsigniaCreator.Library.Utility.Helpers;

public static class MathHelper
{
    private const double _tolerance = 0.0001;

    public static bool IsOdd(int value) => Math.Abs(value % 2) == 1;
    public static bool IsEven(int value) => Math.Abs(value % 2) == 0;

    public static bool Equals(double d0, double d1)
    {
        AssertionHelper.InvalidDoubleCheck(d0, nameof(d0));
        AssertionHelper.InvalidDoubleCheck(d1, nameof(d1));

        return Math.Abs(d0 - d1) < _tolerance;
    }

    public static bool LessThan(double d0, double d1)
    {
        AssertionHelper.InvalidDoubleCheck(d0, nameof(d0));
        AssertionHelper.InvalidDoubleCheck(d1, nameof(d1));

        return d0 < d1;
    }

    public static bool GreaterThan(double d0, double d1)
    {
        AssertionHelper.InvalidDoubleCheck(d0, nameof(d0));
        AssertionHelper.InvalidDoubleCheck(d1, nameof(d1));

        return d0 > d1;
    }

    public static double Round(double value)
    {
        AssertionHelper.InvalidDoubleCheck(value, nameof(value));

        return Math.Round(value * 1000) / 1000;
    }

    public static double Sqrt(double value)
    {
        AssertionHelper.PositiveCheck(value, nameof(value));

        return Round(Math.Sqrt(value));
    }

    public static double Pi() => Round(Pi(1, 1));

    public static double Pi(double numerator, double denominator)
    {
        AssertionHelper.ZeroCheck(denominator, nameof(denominator));
        AssertionHelper.InvalidDoubleCheck(numerator, nameof(numerator));

        return Math.PI * numerator / denominator;
    }

    public static double Cos(double theta)
    {
        AssertionHelper.InvalidDoubleCheck(theta, nameof(theta));

        return Round(Math.Cos(theta));
    }

    public static double Sin(double theta)
    {
        AssertionHelper.InvalidDoubleCheck(theta, nameof(theta));

        return Round(Math.Sin(theta));
    }

    public static double ToDegrees(double radians)
    {
        AssertionHelper.InvalidDoubleCheck(radians, nameof(radians));

        return Round(radians * 180 / Math.PI);
    }

    public static double ToRadians(double degrees)
    {
        AssertionHelper.InvalidDoubleCheck(degrees, nameof(degrees));

        return Round(degrees * Math.PI / 180);
    }

    public static double GetInteriorAngleFromSideCount(int sideCount)
    {
        AssertionHelper.MinimumCheck(sideCount, 3, nameof(sideCount));

        double totalDegrees = (sideCount - 2) * 180.0;
        double innerFacingAngle = totalDegrees / sideCount;

        return Round(innerFacingAngle);
    }
}