namespace Khartyko.InsigniaCreator.Library.Utility.Helpers;

public static class MathHelper
{
    private const double _tolerance = 0.0001;

    public static bool IsOdd(int value) => Math.Abs(value % 2) == 1;
    public static bool IsEven(int value) => Math.Abs(value % 2) == 0;

    public static bool Equals(double d0, double d1)
    {
        Verify(d0, "MathHelper::Equals(>d0<, d1)");
        Verify(d1, "MathHelper::Equals(d0, >d1<)");

        return Math.Abs(d0 - d1) < _tolerance;
    }

    public static double Round(double value)
    {
        Verify(value, "MathHelper::Round(>value<)");

        return Math.Round(value * 1000) / 1000;
    }

    public static double Sqrt(double value)
    {
        Verify(value, "MathHelper::Sqrt(>value<)");

        if (value < 0)
        {
            throw new ArgumentOutOfRangeException(nameof(value), "MathHelper::Sqrt(value); 'value' passed in is 0");
        }

        return Round(Math.Sqrt(value));
    }

    public static double Pi() => Round(Pi(1, 1));

    public static double Pi(double numerator, double denominator)
    {
        if (Equals(denominator, 0.0))
        {
            throw new ArgumentOutOfRangeException(nameof(denominator),
                "MathHelper::Pi(numerator, >denominator<); denominator cannot be 0");
        }

        Verify(numerator, "MathHelper::Pi(>num<, den)");
        Verify(denominator, "MathHelper::Pi(num, >den<)");

        return Math.PI * numerator / denominator;
    }

    public static double Cos(double theta)
    {
        Verify(theta, "MathHelper::Cos(>theta<)");

        return Round(Math.Cos(theta));
    }

    public static double Sin(double theta)
    {
        Verify(theta, "MathHelper::Sin(>theta<)");

        return Round(Math.Sin(theta));
    }

    public static double ToDegrees(double radians)
    {
        Verify(radians, "MathHelper::ToDegrees(>radians<)");

        return Round(radians * 180 / Math.PI);
    }

    public static double ToRadians(double degrees)
    {
        Verify(degrees, "MathHelper::ToRadians(>degrees<)");

        return Round(degrees * Math.PI / 180);
    }

    public static double GetInteriorAngleFromSideCount(int sideCount)
    {
        if (sideCount < 3)
        {
            throw new ArgumentOutOfRangeException(nameof(sideCount), "sideCount cannot be less than 3");
        }

        var totalDegrees = (sideCount - 2) * 180.0;
        var innerFacingAngle = totalDegrees / sideCount;

        return Round(innerFacingAngle);
    }

    public static void Verify(double value, string descriptor)
    {
        if (string.IsNullOrWhiteSpace(descriptor))
        {
            throw ExceptionHelper.GenerateArgumentException(
                typeof(MathHelper),
                nameof(descriptor),
                "'descriptor' cannot be null, empty, or whitespace."
            );
        }

        if (double.IsNaN(value))
        {
            throw ExceptionHelper.GenerateArgumentException(
                typeof(MathHelper),
                nameof(descriptor),
                $"{descriptor} is NaN"
            );
        }

        if (double.IsPositiveInfinity(value))
        {
            throw ExceptionHelper.GenerateArgumentException(
                typeof(MathHelper),
                nameof(descriptor),
                $"{descriptor} is Positive Infinity"
            );
        }

        if (double.IsNegativeInfinity(value))
        {
            throw ExceptionHelper.GenerateArgumentException(
                typeof(MathHelper),
                nameof(descriptor),
                $"{descriptor} is Negative Infinity"
            );
        }
    }
}