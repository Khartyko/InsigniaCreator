using Khartyko.InsigniaCreator.Library.Data;
using Khartyko.InsigniaCreator.Library.Testing.Utility;
using Khartyko.InsigniaCreator.Library.Testing.Utility.Model;
using Khartyko.InsigniaCreator.Library.Utility.Helpers;

#pragma warning disable CS8625

namespace Khartyko.InsigniaCreator.Library.Testing.Data;

public class TransformTests
{
    [Fact]
    public void Transform_Matrix_Accessor_Succeeds()
    {
        var matrix = new Matrix();
        var transform = new Transform();

        Assert.True(matrix.Equals(transform.Matrix));
    }

    [Fact]
    public void Transform_Scale_Accessor_Succeeds()
    {
        RandomTransformData data = DataGenerator.GenerateRandomTransformData(true, false, false);
        Vector2 scale = data.Scale;
        Transform transform = data.Transform;

        Assert.True(scale.Equals(transform.Scale));
    }

    [Fact]
    public void Transform_Scale_Mutator_Succeeds()
    {
        var transform = new Transform();
        Vector2 expectedScale = DataGenerator.GenerateRandomVector2();
        var expectedMatrix = new Matrix(
            new Vector3(expectedScale[0], 0, 0),
            new Vector3(0, expectedScale[1], 0),
            new Vector3(0, 0)
        );
        Matrix actualMatrix = transform.Matrix;

        transform.Scale = expectedScale;

        Assert.True(expectedScale.Equals(transform.Scale));
        Assert.False(expectedMatrix.Equals(actualMatrix));
    }

    [Fact]
    public void Transform_Scale_Mutator_Fails()
    {
        var transform = new Transform();

        Assert.Throws<ArgumentNullException>(() => transform.Scale = null);
        Assert.True(new Vector2(1).Equals(transform.Scale));
    }

    [Fact]
    public void Transform_Rotation_Accessor_Succeeds()
    {
        RandomTransformData data = DataGenerator.GenerateRandomTransformData(false, true, false);
        var expectedRotation = data.Rotation;
        Transform transform = data.Transform;

        Assert.Equal(expectedRotation, transform.Rotation);
    }

    [Fact]
    public void Transform_Rotation_Mutator_Succeeds()
    {
        RandomTransformData data = DataGenerator.GenerateRandomTransformData(false, true, false);
        var expectedRotation = DataGenerator.GenerateRandomDouble();
        var cos = MathHelper.Cos(expectedRotation);
        var sin = MathHelper.Sin(expectedRotation);
        var expectedMatrix = new Matrix(
            new Vector3(cos, -sin, 0),
            new Vector3(sin, cos, 0),
            new Vector3(0, 0)
        );
        Transform transform = data.Transform;

        transform.Rotation = expectedRotation;

        Assert.Equal(expectedRotation, transform.Rotation);
        Assert.True(expectedMatrix.Equals(transform.Matrix));
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
        RandomTransformData data = DataGenerator.GenerateRandomTransformData(false, true, false);
        Vector2 expectedRotation = data.Translation;
        Transform transform = data.Transform;

        Assert.True(expectedRotation.Equals(transform.Translation));
    }

    [Fact]
    public void Transform_Translation_Mutator_Succeeds()
    {
        var transform = new Transform();
        Vector2 expectedTranslation = DataGenerator.GenerateRandomVector2();
        var expectedMatrix = new Matrix(
            new Vector3(expectedTranslation[0], 0, 0),
            new Vector3(0, expectedTranslation[1], 0),
            new Vector3(0, 0)
        );
        Matrix actualMatrix = transform.Matrix;

        transform.Translation = expectedTranslation;

        Assert.True(expectedTranslation.Equals(transform.Translation));
        Assert.False(expectedMatrix.Equals(actualMatrix));
    }

    [Fact]
    public void Transform_Translation_Mutator_Fails()
    {
        var transform = new Transform();

        Assert.Throws<ArgumentNullException>(() => transform.Translation = null);
        Assert.True(new Vector2(0).Equals(transform.Translation));
    }

    [Fact]
    public void Transform_Create_DefaultValues_Succeeds()
    {
        var transform = new Transform();
        var matrix = new Matrix();
        var scale = new Vector2(1);
        var rotation = 0.0;
        var translation = new Vector2(0);

        Assert.True(matrix.Equals(transform.Matrix));
        Assert.True(scale.Equals(transform.Scale));
        Assert.Equal(rotation, transform.Rotation);
        Assert.True(translation.Equals(transform.Translation));
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
        RandomTransformData data = DataGenerator.GenerateRandomTransformData(randomScale, randomRotation, randomTranslation);
        Vector2 scale = data.Scale;
        var rotation = data.Rotation;
        Vector2 translation = data.Translation;
        Transform transform = data.Transform;

        Assert.True(scale.Equals(transform.Scale));
        Assert.Equal(rotation, transform.Rotation);
        Assert.True(translation.Equals(transform.Translation));
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
        RandomTransformData data = DataGenerator.GenerateRandomTransformData(randomScale, randomRotation, randomTranslation);
        Vector2 scale = data.Scale;
        var rotation = data.Rotation;
        Vector2 translation = data.Translation;
        Transform transform = data.Transform;
        var matrix = new Matrix();

        Assert.True(scale.Equals(transform.Scale));
        Assert.Equal(rotation, transform.Rotation);
        Assert.True(translation.Equals(transform.Translation));

        transform.Reset();

        Assert.True(new Vector2(1).Equals(transform.Scale));
        Assert.Equal(0, transform.Rotation);
        Assert.True(new Vector2(0).Equals(transform.Translation));
        Assert.True(matrix.Equals(transform.Matrix));
    }
}