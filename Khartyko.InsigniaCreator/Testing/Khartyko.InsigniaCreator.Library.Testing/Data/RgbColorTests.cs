using Khartyko.InsigniaCreator.Library.Data;

#pragma warning disable CS8625, CS8600

namespace Khartyko.InsigniaCreator.Library.Testing.Data;

public class RgbColorTests
{
    [Theory]
    [InlineData(255)]
    [InlineData(127)]
    [InlineData(0)]
    public void Color_Create_FromSingleValue_Succeeds(byte value)
    {
        var color = new RgbColor(value);

        Assert.Equal(value, color.R);
        Assert.Equal(value, color.G);
        Assert.Equal(value, color.B);
    }

    [Theory]
    [InlineData(0, 255)]
    [InlineData(127, 127)]
    [InlineData(255, 0)]
    public void Color_Create_FromSingleValueAndAlpha_Succeeds(byte value, byte alpha)
    {
        var color = new RgbColor(value, alpha);

        Assert.Equal(value, color.R);
        Assert.Equal(value, color.G);
        Assert.Equal(value, color.B);
        Assert.Equal(alpha, color.A);
    }

    [Theory]
    [InlineData(255, 255, 255)]
    [InlineData(127, 0, 0)]
    [InlineData(63, 255, 127)]
    public void Color_Create_FromRGB(byte r, byte g, byte b)
    {
        var color = new RgbColor(r, g, b);

        Assert.Equal(r, color.R);
        Assert.Equal(g, color.G);
        Assert.Equal(b, color.B);
    }

    [Theory]
    [InlineData(255, 255, 255, 255)]
    [InlineData(127, 0, 0, 127)]
    [InlineData(63, 255, 127, 0)]
    public void Color_Create_FromRGBA(byte r, byte g, byte b, byte a)
    {
        var color = new RgbColor(r, g, b, a);

        Assert.Equal(r, color.R);
        Assert.Equal(g, color.G);
        Assert.Equal(b, color.B);
        Assert.Equal(a, color.A);
    }

    [Theory]
    [InlineData(255, 255, 255)]
    [InlineData(127, 0, 0)]
    [InlineData(63, 255, 127)]
    public void Color_Create_FromExisting_Succeeds(byte r, byte g, byte b)
    {
        var initial = new RgbColor(r, g, b);
        var duplicate = new RgbColor(initial);

        Assert.Equal(r, duplicate.R);
        Assert.Equal(g, duplicate.G);
        Assert.Equal(b, duplicate.B);
    }

    [Fact]
    public void Color_Create_FromExisting_Fails()
    {
        Assert.Throws<ArgumentNullException>(() => new RgbColor((RgbColor)null));
    }

    [Theory]
    [InlineData("#FF0000FF", 255, 0, 0, 255)]
    [InlineData("0000ff7f", 0, 0, 255, 127)]
    [InlineData("#Ff0000", 255, 0, 0, 255)]
    [InlineData("FffFfF", 255, 255, 255, 255)]
    [InlineData("#FfFf", 255, 255, 255, 255)]
    [InlineData("00F8", 0, 0, 255, 136)]
    [InlineData("#F00", 255, 0, 0, 255)]
    [InlineData("0F0", 0, 255, 0, 255)]
    public void Color_Create_FromString_Succeeds(string hexValue, byte r, byte g, byte b, byte a)
    {
        var expectedColor = new RgbColor(r, g, b, a);
        var actualColor = new RgbColor(hexValue);

        Assert.Equal(expectedColor, actualColor);
        Assert.Equal(r, actualColor.R);
        Assert.Equal(g, actualColor.G);
        Assert.Equal(b, actualColor.B);
        Assert.Equal(a, actualColor.A);
    }

    [Theory]
    [InlineData("")]
    [InlineData("asdf")]
    [InlineData("fffff")]
    [InlineData("fffffff")]
    [InlineData("#")]
    [InlineData("#asdf")]
    [InlineData("#fffff")]
    [InlineData("#fffffff")]
    [InlineData(null)]
    public void Color_Create_FromString_Fails(string hexValue)
    {
        Assert.ThrowsAny<ArgumentException>(() => new RgbColor(hexValue));
    }

    [Theory]
    [InlineData(255, 255, 255, 0)]
    [InlineData(127, 0, 0, 255)]
    [InlineData(63, 255, 127, 128)]
    public void Color_R_Succeeds(byte r, byte g, byte b, byte rUpdate)
    {
        var color = new RgbColor(r, g, b);
        Assert.Equal(r, color.R);
        color.R = rUpdate;
        Assert.Equal(rUpdate, color.R);
    }

    [Theory]
    [InlineData(255, 255, 255, 0)]
    [InlineData(127, 0, 0, 255)]
    [InlineData(63, 255, 127, 128)]
    public void Color_G_Succeeds(byte r, byte g, byte b, byte gUpdate)
    {
        var color = new RgbColor(r, g, b);
        Assert.Equal(g, color.G);
        color.G = gUpdate;
        Assert.Equal(gUpdate, color.G);
    }

    [Theory]
    [InlineData(255, 255, 255, 0)]
    [InlineData(127, 0, 0, 255)]
    [InlineData(63, 255, 127, 128)]
    public void Color_B_Succeeds(byte r, byte g, byte b, byte bUpdate)
    {
        var color = new RgbColor(r, g, b);
        Assert.Equal(b, color.B);
        color.B = bUpdate;
        Assert.Equal(bUpdate, color.B);
    }

    [Theory]
    [InlineData(255, 255, 255, 0)]
    [InlineData(127, 0, 0, 255)]
    [InlineData(63, 255, 127, 128)]
    public void Color_A_Succeeds(byte r, byte g, byte b, byte aUpdate)
    {
        var color = new RgbColor(r, g, b);
        Assert.Equal(255, color.A);
        color.A = aUpdate;
        Assert.Equal(aUpdate, color.A);
    }
}