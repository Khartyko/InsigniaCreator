using Khartyko.InsigniaCreator.Library.Data;

namespace Khartyko.InsigniaCreator.TestingLibrary;

public struct RandomTransformData
{
    public Vector2 Scale { get; set; }
    public double Rotation { get; set; }
    public Vector2 Translation { get; set; }
    public Transform Transform { get; set; }
}