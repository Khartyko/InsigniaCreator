using Khartyko.InsigniaCreator.Library.Data;

#pragma warning disable CS8625, CS8600

namespace Khartyko.InsigniaCreator.Library.Testing.Data;

public class RgbColorTests
{
    #region R
    
    [Theory]
    [InlineData(255, 255, 255, 0)]
    [InlineData(127, 0, 0, 255)]
    [InlineData(63, 255, 127, 128)]
    public void R_Succeeds(byte r, byte g, byte b, byte rUpdate)
    {
        var color = new RgbColor(r, g, b);
        Assert.Equal(r, color.R);
        color.R = rUpdate;
        Assert.Equal(rUpdate, color.R);
    }

    #endregion R

    #region G

    [Theory]
    [InlineData(255, 255, 255, 0)]
    [InlineData(127, 0, 0, 255)]
    [InlineData(63, 255, 127, 128)]
    public void G_Succeeds(byte r, byte g, byte b, byte gUpdate)
    {
        var color = new RgbColor(r, g, b);
        Assert.Equal(g, color.G);
        color.G = gUpdate;
        Assert.Equal(gUpdate, color.G);
    }

    #endregion G

    #region B

    [Theory]
    [InlineData(255, 255, 255, 0)]
    [InlineData(127, 0, 0, 255)]
    [InlineData(63, 255, 127, 128)]
    public void B_Succeeds(byte r, byte g, byte b, byte bUpdate)
    {
        var color = new RgbColor(r, g, b);
        Assert.Equal(b, color.B);
        color.B = bUpdate;
        Assert.Equal(bUpdate, color.B);
    }

    #endregion B
    
    #region A
    
    [Theory]
    [InlineData(255, 255, 255, 0)]
    [InlineData(127, 0, 0, 255)]
    [InlineData(63, 255, 127, 128)]
    public void A_Succeeds(byte r, byte g, byte b, byte aUpdate)
    {
        var color = new RgbColor(r, g, b);
        Assert.Equal(255, color.A);
        color.A = aUpdate;
        Assert.Equal(aUpdate, color.A);
    }

    #endregion A

    #region Constructor
    
    #region From HexValue
    
    [Theory]
    [InlineData("#FF0000FF", 255, 0, 0, 255)]
    [InlineData("0000ff7f", 0, 0, 255, 127)]
    [InlineData("#Ff0000", 255, 0, 0, 255)]
    [InlineData("FffFfF", 255, 255, 255, 255)]
    [InlineData("#FfFf", 255, 255, 255, 255)]
    [InlineData("00F8", 0, 0, 255, 136)]
    [InlineData("#F00", 255, 0, 0, 255)]
    [InlineData("0F0", 0, 255, 0, 255)]
    public void Create_FromString_Succeeds(string hexValue, byte r, byte g, byte b, byte a)
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
    public void Create_FromString_Fails(string hexValue)
    {
        Assert.ThrowsAny<ArgumentException>(() => new RgbColor(hexValue));
    }

    #endregion From HexValue

    #region Single Byte
    
    [Theory]
    [InlineData(255)]
    [InlineData(127)]
    [InlineData(0)]
    public void Create_FromSingleValue_Succeeds(byte value)
    {
        var color = new RgbColor(value);

        Assert.Equal(value, color.R);
        Assert.Equal(value, color.G);
        Assert.Equal(value, color.B);
    }

    #endregion Single Byte

    #region RGB Byte and Alpha Byte
    
    [Theory]
    [InlineData(0, 255)]
    [InlineData(127, 127)]
    [InlineData(255, 0)]
    public void Create_FromSingleValueAndAlpha_Succeeds(byte value, byte alpha)
    {
        var color = new RgbColor(value, alpha);

        Assert.Equal(value, color.R);
        Assert.Equal(value, color.G);
        Assert.Equal(value, color.B);
        Assert.Equal(alpha, color.A);
    }

    #endregion RGB Byte and Alpha Byte

    #region RGBA Bytes
    
    [Theory]
    [InlineData(255, 255, 255)]
    [InlineData(127, 0, 0)]
    [InlineData(63, 255, 127)]
    public void Create_FromRGB(byte r, byte g, byte b)
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
    public void Create_FromRGBA(byte r, byte g, byte b, byte a)
    {
        var color = new RgbColor(r, g, b, a);

        Assert.Equal(r, color.R);
        Assert.Equal(g, color.G);
        Assert.Equal(b, color.B);
        Assert.Equal(a, color.A);
    }

    #endregion RGBA Bytes
    
    #region From Existing
    
    [Theory]
    [InlineData(255, 255, 255)]
    [InlineData(127, 0, 0)]
    [InlineData(63, 255, 127)]
    public void Create_FromExisting_Succeeds(byte r, byte g, byte b)
    {
        var initial = new RgbColor(r, g, b);
        var duplicate = new RgbColor(initial);

        Assert.Equal(r, duplicate.R);
        Assert.Equal(g, duplicate.G);
        Assert.Equal(b, duplicate.B);
    }

    [Fact]
    public void Create_FromExisting_Fails()
    {
        Assert.Throws<ArgumentNullException>(() => new RgbColor((RgbColor)null));
    }

    #endregion From Existing

    #endregion Constructor
    
    #region HexString

    [Fact]
    public void HexString_IncludeOctothorpeOnly_Succeeds()
    {
        var color = new RgbColor(183, 65, 14, 127);
        const string trueResult = "#B7410E7F";
        const string falseResult = "B7410E7F";

        Assert.Equal(trueResult, color.HexString());
        Assert.Equal(trueResult, color.HexString(includeOctothorpe: true));
        Assert.Equal(falseResult, color.HexString(includeOctothorpe: false));
    }

    [Fact]
    public void HexString_IncludeAlphaOnly_Succeeds()
    {
        var color = new RgbColor(183, 65, 14, 127);
        const string trueResult = "#B7410E7F";
        const string falseResult = "#B7410E";

        Assert.Equal(trueResult, color.HexString());
        Assert.Equal(trueResult, color.HexString(includeAlpha: true));
        Assert.Equal(falseResult, color.HexString(includeAlpha: false));
    }

    [Fact]
    public void HexString_IncludeBoth_Succeeds()
    {
        var color = new RgbColor(183, 65, 14, 127);
        const string trueAndTrueResult = "#B7410E7F";
        const string trueAndFalseResult = "#B7410E";
        const string falseAndFalseResult = "B7410E";
        const string falseAndTrueResult = "B7410E7F";

        Assert.Equal(trueAndTrueResult, color.HexString());
        Assert.Equal(trueAndTrueResult, color.HexString(includeOctothorpe: true, includeAlpha: true));
        Assert.Equal(trueAndFalseResult, color.HexString(includeOctothorpe: true, includeAlpha: false));
        Assert.Equal(falseAndFalseResult, color.HexString(includeOctothorpe: false, includeAlpha: false));
        Assert.Equal(falseAndTrueResult, color.HexString(includeOctothorpe: false, includeAlpha: true));
    }
    
    #endregion HexString
    
    #region ToString

    [Fact]
    public void ToString_Succeeds()
    {
        var color = new RgbColor(183, 65, 14, 127);
        const string expectedResult = "{ r: 183, g: 065, b: 014, a: 127 }";
        
        Assert.Equal(expectedResult, color.ToString());
    }
    
    #endregion ToString
    
    #region Equals

    [Fact]
    public void Equals_Succeeds()
    {
        var rust0 = new RgbColor(183, 65, 14, 127);
        var rust1 = new RgbColor(183, 65, 14, 127);
        
        // ReSharper disable EqualExpressionComparison
        Assert.True(rust0.Equals(rust0));
        Assert.True(rust1.Equals(rust1));
        // ReSharper restore EqualExpressionComparison
        Assert.True(rust0.Equals(rust1));
        Assert.True(rust1.Equals(rust0));
    }

    [Fact]
    public void Equals_Null_Fails()
    {
        var rust = new RgbColor(183, 65, 14, 127);
        RgbColor nullColor = null;
        
        Assert.False(rust.Equals(nullColor));
    }

    [Fact]
    public void Equals_Dissimilar_Fails()
    {
        var rust = new RgbColor(183, 65, 14, 127);
        var cerulean = new RgbColor(0, 123, 167);
        
        Assert.False(rust.Equals(cerulean));
        Assert.False(cerulean.Equals(rust));
    }

    [Fact]
    public void Equals_RandomObject_Fails()
    {
        var rust = new RgbColor(183, 65, 14, 127);
        var testObject = new object();
        
        Assert.False(rust.Equals(testObject));
        Assert.False(testObject.Equals(rust));
    }
    
    #endregion Equals
}