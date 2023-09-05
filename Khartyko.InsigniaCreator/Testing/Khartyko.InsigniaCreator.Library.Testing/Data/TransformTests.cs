/** \addtogroup LibraryTests
 * @{
 */

using Khartyko.InsigniaCreator.Library.Data;
using Khartyko.InsigniaCreator.Library.Utility.Helpers;
using Khartyko.InsigniaCreator.TestingLibrary;

#pragma warning disable CS8625, CS8600, CS8604

namespace Khartyko.InsigniaCreator.Library.Testing.Data;

public class TransformTests
{
    #region Matrix

    [Fact]
    public void Matrix_Accessor_Succeeds()
    {
        var matrix = new Matrix();
        var transform = new Transform();

        Assert.True(matrix.Equals(transform.Matrix));
    }

    #endregion Matrix

    #region Scale

    [Fact]
    public void Scale_Accessor_Succeeds()
    {
        RandomTransformData data = DataGenerator.GenerateRandomTransformData(true, false, false);
        Vector2 scale = data.Scale;
        Transform transform = data.Transform;

        Assert.True(scale.Equals(transform.Scale));
    }

    [Fact]
    public void Scale_Mutator_Succeeds()
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
    public void Scale_Mutator_Fails()
    {
        var transform = new Transform();

        Assert.Throws<ArgumentNullException>(() => transform.Scale = null);
        Assert.True(new Vector2(1).Equals(transform.Scale));
    }

    #endregion Scale

    #region Rotation

    [Fact]
    public void Rotation_Accessor_Succeeds()
    {
        RandomTransformData data = DataGenerator.GenerateRandomTransformData(false, true, false);
        double expectedRotation = data.Rotation;
        Transform transform = data.Transform;

        Assert.Equal(expectedRotation, transform.Rotation);
    }

    [Fact]
    public void Rotation_Mutator_Succeeds()
    {
        RandomTransformData data = DataGenerator.GenerateRandomTransformData(false, true, false);
        double expectedRotation = DataGenerator.GenerateRandomDouble();
        double cos = MathHelper.Cos(expectedRotation);
        double sin = MathHelper.Sin(expectedRotation);
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

    [Theory, ClassData(typeof(InvalidDoubleData))]
    public void Rotation_Mutator_Fails(double value)
    {
        var transform = new Transform();

        Assert.Throws<ArgumentException>(() => transform.Rotation = value);
        Assert.Equal(0, transform.Rotation);
    }

    #endregion Rotation

    #region Translation

    [Fact]
    public void Translation_Accessor_Succeeds()
    {
        RandomTransformData data = DataGenerator.GenerateRandomTransformData(false, true, false);
        Vector2 expectedTranslation = data.Translation;
        Transform transform = data.Transform;

        Assert.True(expectedTranslation.Equals(transform.Translation));
    }

    [Fact]
    public void Translation_Mutator_Succeeds()
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
    public void Translation_Mutator_Fails()
    {
        var transform = new Transform();

        Assert.Throws<ArgumentNullException>(() => transform.Translation = null);
        Assert.True(new Vector2(0).Equals(transform.Translation));
    }

    #endregion Translation

    #region Constructor

    [Fact]
    public void Create_DefaultValues_Succeeds()
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
    public void Create_RandomValues_Succeeds(bool randomScale, bool randomRotation,
        bool randomTranslation)
    {
        RandomTransformData data = DataGenerator.GenerateRandomTransformData(randomScale, randomRotation, randomTranslation);
        Vector2 scale = data.Scale;
        double rotation = data.Rotation;
        Vector2 translation = data.Translation;
        Transform transform = data.Transform;

        Assert.True(scale.Equals(transform.Scale));
        Assert.Equal(rotation, transform.Rotation);
        Assert.True(translation.Equals(transform.Translation));
    }

    [Fact]
    public void Create_Fails_BadScale()
    {
        const double rotation = 0.0;
        var translation = new Vector2(0);

        Assert.Throws<ArgumentNullException>(() => new Transform(null, rotation, translation));
    }

    [Theory, ClassData(typeof(InvalidDoubleData))]
    public void Create_Fails_BadRotation(double rotation)
    {
        var scale = new Vector2(1);
        var translation = new Vector2(0);
        Assert.Throws<ArgumentException>(() => new Transform(scale, rotation, translation));
    }

    [Fact]
    public void Create_Fails_BadTranslation()
    {
        var scale = new Vector2(1);
        var rotation = 0.0;

        Assert.Throws<ArgumentNullException>(() => new Transform(scale, rotation, null));
    }

    [Fact]
    public void Create_FromExisting_Succeeds()
    {
        Vector2 scale = DataGenerator.GenerateRandomVector2();
        Vector2 translation = DataGenerator.GenerateRandomVector2();
        double rotation = DataGenerator.GenerateRandomDouble();

        var initialTransform = new Transform(scale, rotation, translation);

        var duplicateTransform = new Transform(initialTransform);

        Assert.Equal(initialTransform.Scale, duplicateTransform.Scale);
        Assert.Equal(initialTransform.Rotation, duplicateTransform.Rotation);
        Assert.Equal(initialTransform.Translation, duplicateTransform.Translation);
    }

    [Fact]
    public void Create_FromExisting_NullTransform_Fails()
    {
        Transform nullTransform = null;

        Assert.Throws<ArgumentNullException>(() => new Transform(nullTransform));
    }

    #endregion Constructor

    #region Reset

    [Theory]
    [InlineData(true, false, false)]
    [InlineData(false, true, false)]
    [InlineData(false, false, true)]
    [InlineData(true, true, false)]
    [InlineData(true, false, true)]
    [InlineData(false, true, true)]
    [InlineData(true, true, true)]
    public void Reset_Succeeds(bool randomScale, bool randomRotation, bool randomTranslation)
    {
        RandomTransformData data = DataGenerator.GenerateRandomTransformData(randomScale, randomRotation, randomTranslation);
        Vector2 scale = data.Scale;
        double rotation = data.Rotation;
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

    #endregion Reset
}

/** @} */