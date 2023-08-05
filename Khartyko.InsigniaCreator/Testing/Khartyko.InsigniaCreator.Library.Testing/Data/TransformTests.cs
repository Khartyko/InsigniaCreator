using Khartyko.InsigniaCreator.Library.Data;
using Khartyko.InsigniaCreator.Library.Testing.Utility;
using Khartyko.InsigniaCreator.Library.Utility.Helpers;

namespace Khartyko.InsigniaCreator.Library.Testing.Data;

public class TransformTests
{
    [Fact]
    public void Transform_Matrix_Accessor_Succeeds()
    {
        var matrix = new Matrix();
        var transform = new Transform();

        Assert.True(matrix == transform.Matrix);
    }

    [Fact]
    public void Transform_Scale_Accessor_Succeeds()
    {
        var data = DataGenerator.GenerateRandomTransformData(true, false, false);
        var scale = data.Scale;
        var transform = data.Transform;

        Assert.True(scale == transform.Scale);
    }

    [Fact]
    public void Transform_Scale_Mutator_Succeeds()
    {
        var transform = new Transform();
        var expectedScale = DataGenerator.GenerateRandomVector2();
        var expectedMatrix = new Matrix(
            new Vector3(expectedScale[0], 0, 0),
            new Vector3(0, expectedScale[1], 0),
            new Vector3(0, 0, 1)
        );
        var actualMatrix = transform.Matrix;

        transform.Scale = expectedScale;

        Assert.True(expectedScale == transform.Scale);
        Assert.False(expectedMatrix == actualMatrix);
    }

    [Fact]
    public void Transform_Scale_Mutator_Fails()
    {
        var transform = new Transform();

#pragma warning disable CS8625 // Cannot convert null literal to non-nullable reference type.
        Assert.Throws<ArgumentNullException>(() => transform.Scale = null);
#pragma warning restore CS8625 // Cannot convert null literal to non-nullable reference type.
        Assert.True(new Vector2(1) == transform.Scale);
    }

    [Fact]
    public void Transform_Rotation_Accessor_Succeeds()
    {
        var data = DataGenerator.GenerateRandomTransformData(false, true, false);
        var expectedRotation = data.Rotation;
        var transform = data.Transform;

        Assert.Equal(expectedRotation, transform.Rotation);
    }

    [Fact]
    public void Transform_Rotation_Mutator_Succeeds()
    {
        var data = DataGenerator.GenerateRandomTransformData(false, true, false);
        var expectedRotation = DataGenerator.GenerateRandomDouble();
        var cos = MathHelper.Cos(expectedRotation);
        var sin = MathHelper.Sin(expectedRotation);
        var expectedMatrix = new Matrix(
            new Vector3(cos, -sin, 0),
            new Vector3(sin, cos, 0),
            new Vector3(0, 0, 1)
        );
        var transform = data.Transform;

        transform.Rotation = expectedRotation;

        Assert.Equal(expectedRotation, transform.Rotation);
        Assert.True(expectedMatrix == transform.Matrix);
    }

    [Theory]
    [InlineData(double.NaN)]
    [InlineData(double.PositiveInfinity)]
    [InlineData(double.NegativeInfinity)]
    public void Transform_Rotation_Mutator_Fails(double value)
    {
        var transform = new Transform();

        Assert.Throws<ArgumentException>(() => transform.Rotation = value);
        Assert.Equal(0, transform.Rotation);
    }

    [Fact]
    public void Transform_Translation_Accessor_Succeeds()
    {
        var data = DataGenerator.GenerateRandomTransformData(false, true, false);
        var expectedRotation = data.Translation;
        var transform = data.Transform;

        Assert.True(expectedRotation == transform.Translation);
    }

    [Fact]
    public void Transform_Translation_Mutator_Succeeds()
    {
        var transform = new Transform();
        var expectedTranslation = DataGenerator.GenerateRandomVector2();
        var expectedMatrix = new Matrix(
            new Vector3(expectedTranslation[0], 0, 0),
            new Vector3(0, expectedTranslation[1], 0),
            new Vector3(0, 0, 1)
        );
        var actualMatrix = transform.Matrix;

        transform.Translation = expectedTranslation;

        Assert.True(expectedTranslation == transform.Translation);
        Assert.False(expectedMatrix == actualMatrix);
    }

    [Fact]
    public void Transform_Translation_Mutator_Fails()
    {
        var transform = new Transform();

#pragma warning disable CS8625 // Cannot convert null literal to non-nullable reference type.
        Assert.Throws<ArgumentNullException>(() => transform.Translation = null);
#pragma warning restore CS8625 // Cannot convert null literal to non-nullable reference type.
        Assert.True(new Vector2(0) == transform.Translation);
    }

    [Fact]
    public void Transform_Create_DefaultValues_Succeeds()
    {
        var transform = new Transform();
        var matrix = new Matrix();
        var scale = new Vector2(1);
        var rotation = 0.0;
        var translation = new Vector2(0);

        Assert.True(matrix == transform.Matrix);
        Assert.True(scale == transform.Scale);
        Assert.Equal(rotation, transform.Rotation);
        Assert.True(translation == transform.Translation);
    }

    [Theory]
    [InlineData(true, false, false)]
    [InlineData(false, true, false)]
    [InlineData(false, false, true)]
    [InlineData(true, true, false)]
    [InlineData(true, false, true)]
    [InlineData(false, true, true)]
    [InlineData(true, true, true)]
    public void Transform_Create_RandomValues_Succeeds(bool randomScale, bool randomRotation,
        bool randomTranslation)
    {
        var data = DataGenerator.GenerateRandomTransformData(randomScale, randomRotation, randomTranslation);
        var scale = data.Scale;
        var rotation = data.Rotation;
        var translation = data.Translation;
        var transform = data.Transform;

        Assert.True(scale == transform.Scale);
        Assert.Equal(rotation, transform.Rotation);
        Assert.True(translation == transform.Translation);
    }

    [Fact]
    public void Transform_Create_Fails_BadScale()
    {
        var rotation = 0.0;
        var translation = new Vector2(0);

        Assert.Throws<ArgumentNullException>(() => new Transform(null, rotation, translation));
    }

    [Theory]
    [InlineData(double.NaN)]
    [InlineData(double.PositiveInfinity)]
    [InlineData(double.NegativeInfinity)]
    public void Transform_Create_Fails_BadRotation(double rotation)
    {
        var scale = new Vector2(1);
        var translation = new Vector2(0);
        Assert.Throws<ArgumentException>(() => new Transform(scale, rotation, translation));
    }

    [Fact]
    public void Transform_Create_Fails_BadTranslation()
    {
        var scale = new Vector2(1);
        var rotation = 0;

        Assert.Throws<ArgumentNullException>(() => new Transform(scale, rotation, null));
    }

    [Theory]
    [InlineData(true, false, false)]
    [InlineData(false, true, false)]
    [InlineData(false, false, true)]
    [InlineData(true, true, false)]
    [InlineData(true, false, true)]
    [InlineData(false, true, true)]
    [InlineData(true, true, true)]
    public void Transform_Reset_Succeeds(bool randomScale, bool randomRotation, bool randomTranslation)
    {
        var data = DataGenerator.GenerateRandomTransformData(randomScale, randomRotation, randomTranslation);
        var scale = data.Scale;
        var rotation = data.Rotation;
        var translation = data.Translation;
        var transform = data.Transform;
        var matrix = new Matrix();

        Assert.True(scale == transform.Scale);
        Assert.Equal(rotation, transform.Rotation);
        Assert.True(translation == transform.Translation);

        transform.Reset();

        Assert.True(new Vector2(1) == transform.Scale);
        Assert.Equal(0, transform.Rotation);
        Assert.True(new Vector2(0) == transform.Translation);
        Assert.True(matrix == transform.Matrix);
    }
}