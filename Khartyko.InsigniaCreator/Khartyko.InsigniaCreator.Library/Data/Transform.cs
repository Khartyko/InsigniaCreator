/** \addtogroup Library
 * @{
 */
using Khartyko.InsigniaCreator.Library.Utility.Helpers;

namespace Khartyko.InsigniaCreator.Library.Data;

/// <summary>
/// Class that represents a Transformation Matrix, and stores the relevant data.
/// </summary>
public class Transform
{
    private Vector2 _scale;
    private double _rotation;
    private Vector2 _translation;

    /// <summary>
    /// Gets the transformed Matrix.
    /// </summary>
    public Matrix Matrix { get; private set; }

    /// <summary>
    /// Gets or Sets the Scale.
    /// </summary>
    /// <remarks>
    /// After setting the Scale, the Matrix will be updated to reflect the new changes.
    /// </remarks>
    /// <exception cref="ArgumentNullException">Can be thrown if it's set to null.</exception>
    public Vector2 Scale
    {
        get => _scale;

        set
        {
            AssertionHelper.NullCheck(value, nameof(value));

            _scale = value;

            ApplyChanges();
        }
    }

    /// <summary>
    /// Gets or Sets the Rotation.
    /// </summary>
    /// <exception cref="ArgumentException">Can be thrown if it's set to NaN, PositiveInfinity, or NegativeInfinity.</exception>
    public double Rotation
    {
        get => _rotation;

        set
        {
            AssertionHelper.InvalidDoubleCheck(value, nameof(value));

            _rotation = value;

            ApplyChanges();
        }
    }

    /// <summary>
    /// Gets or Sets the Translation.
    /// </summary>
    /// <remarks>
    /// After setting the Translation, the Matrix will be updated to reflect the new changes.
    /// </remarks>
    /// <exception cref="ArgumentNullException">Can be thrown if it's set to null.</exception>
    public Vector2 Translation
    {
        get => _translation;

        set
        {
            AssertionHelper.NullCheck(value, nameof(value));

            _translation = value;

            ApplyChanges();
        }
    }

    /// <summary>
    /// Constructor that defaults the values, and has an identity matrix for the Matrix data.
    /// </summary>
    /// <remarks>
    /// The Scale will have both 'X' and 'Y' values set to 1.0.
    /// The Rotation will be 0.0.
    /// The Translation will have both 'X' and 'Y' values set to 0.0.
    /// </remarks>
    public Transform()
    {
        _scale = new Vector2(1);
        _rotation = 0;
        _translation = new Vector2(0);

        Matrix = new Matrix();
    }

    /// <summary>
    /// Constructor that allows the specification of Scale, Rotation, and Translation.
    /// </summary>
    /// <param name="scale">The scale of the Transform.</param>
    /// <param name="rotation">The rotation of the Transform.</param>
    /// <param name="translation">The translation of the Transform.</param>
    /// <exception cref="ArgumentNullException">Can be thrown if either 'scale' or 'translation' are null.</exception>
    /// <exception cref="ArgumentException">Can be thrown if 'rotation' is NaN, PositiveInfinity, or NegativeInfinity.</exception>
    public Transform(Vector2 scale, double rotation, Vector2 translation)
    {
        AssertionHelper.NullCheck(scale, nameof(scale));
        AssertionHelper.InvalidDoubleCheck(rotation, nameof(rotation));
        AssertionHelper.NullCheck(translation, nameof(translation));

        _scale = scale;
        _rotation = rotation;
        _translation = translation;

        Matrix = new Matrix();

        ApplyChanges();
    }

    /// <summary>
    /// Constructor that copies the values of one Transform to another.
    /// </summary>
    /// <param name="existing">The existing Transform to duplicate.</param>
    /// <exception cref="ArgumentNullException">Can be thrown if 'existing' is null.</exception>
    public Transform(Transform existing)
    {
        AssertionHelper.NullCheck(existing, nameof(existing));
        
        _scale = new Vector2(existing.Scale);
        _rotation = existing.Rotation;
        _translation = new Vector2(existing.Translation);

        Matrix = new Matrix();
        
        ApplyChanges();
    }

    /// <summary>
    /// Resets the values of this Transform to their defaults.
    /// The Matrix will also be set to its identity state.
    /// </summary>
    /// <remarks>
    /// The Scale will have both 'X' and 'Y' values set to 1.0.
    /// The Rotation will be 0.0.
    /// The Translation will have both 'X' and 'Y' values set to 0.0.
    /// </remarks>
    public void Reset()
    {
        _scale[0] = 1;
        _scale[1] = 1;

        _rotation = 0;

        _translation[0] = 0;
        _translation[1] = 0;

        ApplyChanges();
    }

    /// <summary>
    /// Private method that applies the Scale, Rotation, and Translation to the Matrix.
    /// </summary>
    private void ApplyChanges()
    {
        var translationMatrix = new Matrix(
            new Vector3(1, 0, _translation[0]),
            new Vector3(0, 1, _translation[1]),
            new Vector3(0, 0)
        );

        double cos = MathHelper.Cos(_rotation);
        double sin = MathHelper.Sin(_rotation);
        var rotationMatrix = new Matrix(
            new Vector3(cos, -sin, 0),
            new Vector3(sin, cos, 0),
            new Vector3(0, 0)
        );

        var scaleMatrix = new Matrix(
            new Vector3(_scale[0], 0, 0),
            new Vector3(0, _scale[1], 0),
            new Vector3(0, 0)
        );

        Matrix = translationMatrix * rotationMatrix * scaleMatrix;
    }
}
/** @} */