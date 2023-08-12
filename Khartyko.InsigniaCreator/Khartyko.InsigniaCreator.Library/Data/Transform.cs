using Khartyko.InsigniaCreator.Library.Utility.Helpers;

namespace Khartyko.InsigniaCreator.Library.Data;

public class Transform
{
    private Vector2 _scale;
    private double _rotation;
    private Vector2 _translation;

    public Matrix Matrix { get; private set; }

    public Vector2 Scale
    {
        get => _scale;

        set
        {
            ObjectHelper.NullCheck(value, "Transform::Scale = >value<");

            _scale = value;

            ApplyChanges();
        }
    }

    public double Rotation
    {
        get => _rotation;

        set
        {
            MathHelper.InvalidDoubleCheck(value, "Transform::Rotation = >value<");

            _rotation = value;

            ApplyChanges();
        }
    }

    public Vector2 Translation
    {
        get => _translation;

        set
        {
            ObjectHelper.NullCheck(value, "Transform::Translation = >value<");

            _translation = value;

            ApplyChanges();
        }
    }

    public Transform()
    {
        _scale = new Vector2(1);
        _rotation = 0;
        _translation = new Vector2(0);

        Matrix = new Matrix();
    }

    public Transform(Vector2 scale, double rotation, Vector2 translation)
    {
        ObjectHelper.NullCheck(scale, "Transform::transform(>scale<, rotation, translation)");
        MathHelper.InvalidDoubleCheck(rotation, "Transform::transform(scale, >rotation<, translation)");
        ObjectHelper.NullCheck(translation, "Transform::transform(scale, rotation, >translation<)");

        _scale = scale;
        _rotation = rotation;
        _translation = translation;

        Matrix = new Matrix();

        ApplyChanges();
    }

    public Transform(Transform existing)
    {
        ObjectHelper.NullCheck(existing, nameof(existing));
        
        _scale = new Vector2(existing.Scale);
        _rotation = existing.Rotation;
        _translation = new Vector2(existing.Translation);

        Matrix = new Matrix();
        
        ApplyChanges();
    }

    private void ApplyChanges()
    {
        var translationMatrix = new Matrix(
            new Vector3(1, 0, _translation[0]),
            new Vector3(0, 1, _translation[1]),
            new Vector3(0, 0)
        );

        var cos = MathHelper.Cos(_rotation);
        var sin = MathHelper.Sin(_rotation);
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

    public void Reset()
    {
        _scale[0] = 1;
        _scale[1] = 1;

        _rotation = 0;

        _translation[0] = 0;
        _translation[1] = 0;

        ApplyChanges();
    }
}